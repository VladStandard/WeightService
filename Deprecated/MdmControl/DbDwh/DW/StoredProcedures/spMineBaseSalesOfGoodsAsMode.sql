
CREATE PROCEDURE [DW].[spMineBaseSalesOfGoodsAsMode]
	@ContragentID varbinary(16)	,
	@NomenclatureID varbinary(16),
	@DeliveryPlaceID varbinary(16),
	@DateID int 

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @Window int = 365;
	DECLARE @Method nvarchar(20) = N'MODE(t='+CONVERT(nvarchar(12),@Window)+' day)';
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
		HAVING COUNT(*) >= ALL (
			SELECT COUNT(*) * (1 - 0.87)		-- погрешность - срезает экстремумы
												-- появляется несколько мод
												-- чем больше погрешность тем больше мод
			FROM [DW].[FactSalesOfGoods] ff
			WHERE ff.Active = 1
				AND  ff.[Price]			= IIF(ff.[BasePrice]=0, ff.[Price] , ff.[BasePrice])
				AND ff.[DateID] between @StartDateID and @DateID
				AND ff.NomenclatureID	= @NomenclatureID
				AND ff.ContragentID		= @ContragentID
				AND ff.DeliveryPlaceID	= @DeliveryPlaceID
			GROUP BY [NomenclatureID],[DateID]
		)

		SELECT @Qty = AVG(qry), @Cost = AVG(qry) * IIF(COALESCE(@BasePrice,0)=0,@Price,@BasePrice)
		FROM #prc 
		
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