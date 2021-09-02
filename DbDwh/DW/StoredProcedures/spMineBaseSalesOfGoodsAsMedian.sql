
CREATE PROCEDURE [DW].[spMineBaseSalesOfGoodsAsMedian]
	@ContragentID varbinary(16)	,
	@NomenclatureID varbinary(16),
	@DeliveryPlaceID varbinary(16),
	@DateID int ,
	@Window int = 365

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @Method nvarchar(20) = N'MEDIAN(t='+CONVERT(nvarchar(12),@Window)+' day)';
	DECLARE @StartDateID int;

	DECLARE @Qty	decimal(15,3);
	DECLARE @Price  decimal(15,2);
	DECLARE @BasePrice  decimal(15,2);
	DECLARE @Cost	decimal(15,2);
	-----------------------------------------------------------------------
	DECLARE 
		@InformationSystemID int,
		@OrgID			     varbinary(16)


	SELECT 
			 @OrgID			     =  r.OrgID			
			,@DateID		     =  r.[DateID]
			,@StartDateID		 =  CONVERT(INT,FORMAT(DATEADD(day,-@Window,d.DocDate),'yyyyMMdd')) 
			,@Price              =  MAX(r.Price)
			,@BasePrice          =  MAX(r.BasePrice)
			,@Qty			     =  SUM(r.Qty) 
			,@Cost	             =  SUM(r.Cost) 
			,@InformationSystemID = r.InformationSystemID

		FROM [DW].[FactSalesOfGoods] r
		INNER JOIN [DW].[DocJournal] d
		ON r.InformationSystemID = d.InformationSystemID AND r.CodeInIS = d.CodeInIS
		WHERE 
			[ContragentID]			= @ContragentID
			and [NomenclatureID]	= @NomenclatureID
			and [DeliveryPlaceID]	= @DeliveryPlaceID
			and [DateID]			=
			(
				SELECT MAX(DateID) 
				FROM [DW].[FactSalesOfGoods]
					WHERE 
						[ContragentID]			= @ContragentID
						and [NomenclatureID]	= @NomenclatureID
						and [DeliveryPlaceID]	= @DeliveryPlaceID
						and [DateID]			<= @DateID
			)
		GROUP BY 
			r.OrgID			
			,r.[DateID]
			,CONVERT(INT,FORMAT(DATEADD(day,-@Window,d.DocDate),'yyyyMMdd'))
			,r.InformationSystemID



	IF @BasePrice <> @Price AND @BasePrice > 0 BEGIN

		DROP TABLE IF EXISTS #prc
		CREATE TABLE #prc ([NomenclatureID] varbinary(16), [DateID] int, Qty decimal(15,3))

		INSERT INTO #prc (NomenclatureID, DateId, Qty)
			SELECT [NomenclatureID],[DateID],SUM([Qty])[Qty]
			FROM [DW].[FactSalesOfGoods] f
			WHERE f.Active = 1
				AND  f.[Price]			= IIF(f.[BasePrice]=0, f.[Price] , f.[BasePrice])
				AND f.[DateID] between @StartDateID and @DateID
				AND f.NomenclatureID	= @NomenclatureID
				AND f.ContragentID		= @ContragentID
				AND f.DeliveryPlaceID	= @DeliveryPlaceID
			GROUP BY [NomenclatureID],[DateID]
		
		INSERT INTO #prc (NomenclatureID, DateId, Qty)
			SELECT [NomenclatureID],[DateID],SUM([Qty])[Qty]
			FROM [DW].[MineBaseSalesOfGoods] f
			WHERE 
				f.[DateID] between @StartDateID and @DateID
				AND f.NomenclatureID	= @NomenclatureID
				AND f.ContragentID		= @ContragentID
				AND f.DeliveryPlaceID	= @DeliveryPlaceID
				AND [DateID] NOT IN (SELECT [DateID] FROM #prc)
			GROUP BY [NomenclatureID],[DateID]

		----------------------------
		DECLARE @t1 TABLE (qry decimal(15,3), amount int)
		INSERT INTO @t1
			SELECT Qty ,count(*) 
			FROM #prc
			GROUP BY Qty
		----------------------------
		DECLARE @t2 TABLE (qry decimal(15,2), Less int, LessOrEqual int)
		INSERT INTO @t2
			SELECT p1.qry,	SUM(p2.amount) - p1.amount,	SUM(p2.amount)
			FROM @t1 p1
			INNER JOIN @t1 p2
			ON p1.qry >= p2.qry 
		GROUP BY p1.qry,p1.amount
		----------------------------
		DECLARE @Half real;
		SELECT @Half = MAX(LessOrEqual)/2.0	FROM @t2
		----------------------------
		SELECT @Qty = AVG(qry), @Cost = AVG(qry) * IIF(COALESCE(@BasePrice,0)=0,@Price,@BasePrice)
			FROM @t2 
			WHERE @Half between Less and LessOrEqual
		
	END

	-----------------------------------------------------------------------

	MERGE [DW].[MineBaseSalesOfGoods] as target
	USING 
	( 
		SELECT 
		@ContragentID 
		,@NomenclatureID
		,@DeliveryPlaceID
		,@DateID
        ,@Method
		,@Qty
        ,IIF(COALESCE(@BasePrice,0)=0,@Price,@BasePrice)
        ,@Cost
        ,@DLM
		,@InformationSystemID

	) 
	AS source 
	(
		 [ContragentID] 
		,[NomenclatureID]
		,[DeliveryPlaceID]
		,[DateID]
        ,[Method]
		,[Qty]
        ,[Price]
        ,[Cost]
        ,[DLM]
		,[InformationSystemID]
	)  
	ON 
	(
		target.[Method]				= source.[Method] 
		AND target.NomenclatureID	= source.NomenclatureID
		AND target.ContragentID		= source.ContragentID
		AND target.DeliveryPlaceID	= source.DeliveryPlaceID
		AND target.[DateID]			= source.[DateID]
	) 

	WHEN MATCHED 
	THEN UPDATE SET 
		[Qty]					= source.[Qty]
        ,[Price]				= source.[Price]
        ,[Cost]					= source.[Cost]
        ,[DLM]					= source.[DLM]

	WHEN NOT MATCHED BY TARGET THEN INSERT 
	(
		 [ContragentID] 
		,[NomenclatureID]
		,[DeliveryPlaceID]
		,[DateID]
		,[Method]
		,[Qty]
		,[Price]
		,[Cost]
		,[InformationSystemID]
	) 
	VALUES 
	(
     	 source.[ContragentID] 
     	,source.[NomenclatureID]
     	,source.[DeliveryPlaceID]
		,source.[DateID]
		,source.[Method]
		,source.[Qty]
		,source.[Price]
		,source.[Cost]
		,source.[InformationSystemID]
	);

END