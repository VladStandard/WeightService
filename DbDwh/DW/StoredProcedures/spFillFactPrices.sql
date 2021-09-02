CREATE PROCEDURE [DW].[spFillFactPrices]

    @DeliveryPlaceID	varbinary(16),
    @NomenclatureID		varbinary(16),
	@PriceTypeID		varbinary(16),
    @Price				decimal(15,2),
	@IsAction			bit,
	@StartDate			datetime,
	@EndDate			datetime,
	@DocType			nvarchar(100),
	@Comment			nvarchar(1000),
	@Marked				bit,
	@Posted				bit,

	@StatusID				int,
	@InformationSystemID	int,
	@CodeInIS				varbinary(16),
	@ALineNo				int,
	@CHECKSUMM				bigint

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();
	DECLARE @DateID int = CONVERT(INT,FORMAT(@StartDate,'yyyyMMdd'));

	IF (@NomenclatureID IS NOT NULL) BEGIN

		MERGE [DW].[FactPrices] as target
		USING ( 
		SELECT 
			@DateID,
			@DeliveryPlaceID,
			@NomenclatureID,
			@PriceTypeID,
			@Price,
			@IsAction,
			@StartDate,
			@EndDate,
			@DocType,
			@CreateDate,
			@DLM,
			@StatusID,
			@InformationSystemID,
			@CodeInIS,
			@ALineNo,
			@CHECKSUMM,
			@Comment,
			@Marked,
			@Posted,
			CAST(@StartDate as date),
			(select top(1) ID from [DW].[DimNomenclatures] where [CodeInIS] = @NomenclatureID),
			(select top(1) ID from [DW].[DimPriceTypes] where [CodeInIS] = @PriceTypeID),
			(select top(1) c.ID from [DW].[DimContragents] c INNER JOIN [DW].[DimDeliveryPlaces] d ON d.ContragentID = c.[CodeInIS] where d.[CodeInIS] = @DeliveryPlaceID),
			(select top(1) ID from [DW].[DimDeliveryPlaces] where [CodeInIS] = @DeliveryPlaceID)

		) AS source (

			[DateID],
			[DeliveryPlaceID],
			[NomenclatureID],
			[PriceTypeID],
			[Price],
			[IsAction],
			[StartDate],
			[EndDate],
			[DocType],
			[CreateDate],
			[DLM],
			[StatusID],
			[InformationSystemID],
			[CodeInIS],
			[_LineNo],
			[CHECKSUMM],
			[Comment],
			[Marked],
			[Posted],

			[_DateID],
			[_NomenclatureID],
			[_PriceTypeID],
			[_ContragentID],
			[_DeliveryPlaceID]

		)  
		ON (target.[CodeInIs]				= source.[CodeInIs] 
		AND target.[_LineNo]				= source.[_LineNo] 
		AND target.[InformationSystemID]	= source.[InformationSystemID]) 

		WHEN MATCHED AND target.CHECKSUMM <> @CHECKSUMM
		THEN UPDATE SET 
			[DateID]			= @DateID,
			[DeliveryPlaceID]	= @DeliveryPlaceID,
			[NomenclatureID]	= @NomenclatureID,
			[PriceTypeID]		= @PriceTypeID,
			[Price]				= @Price,
			[IsAction]			= @IsAction,
			[StartDate]			= @StartDate,
			[EndDate]			= @EndDate,
			[DocType]			= @DocType,
			[DLM]				= @DLM,
			[StatusID]			= @StatusID,
			[CHECKSUMM]			= @CHECKSUMM,
			[Comment]			= @Comment,
			[Marked]			= @Marked,
			[Posted]			= @Posted,

			[_DateID]				= source.[_DateID]		   ,
			[_NomenclatureID]		= source.[_NomenclatureID] ,
			[_PriceTypeID]			= source.[_PriceTypeID]	   ,
			[_ContragentID]			= source.[_ContragentID]	,
			[_DeliveryPlaceID]		= source.[_DeliveryPlaceID]


	 --   WHEN NOT MATCHED BY SOURCE          
	 --       AND [InformationSystemID] = @InformationSystemID
	 --       AND [CodeInIS] = @CodeInIS
	 --       AND [LineNo] = @LineNo
		--THEN DELETE 

		WHEN NOT MATCHED BY TARGET THEN INSERT (
			[DateID],
			[DeliveryPlaceID],
			[NomenclatureID],
			[PriceTypeID],
			[Price],
			[IsAction],
			[StartDate],
			[EndDate],
			[DocType],
			[CreateDate],
			[DLM],
			[StatusID],
			[InformationSystemID],
			[CodeInIs],
			[_LineNo],
			[CHECKSUMM],
			[Comment],
			[Marked],
			[Posted],
			[_DateID]			,
			[_NomenclatureID]	,
			[_PriceTypeID]		,
			[_ContragentID]		,
			[_DeliveryPlaceID]	
		) VALUES (
			@DateID,
			@DeliveryPlaceID,
			@NomenclatureID,
			@PriceTypeID,
			@Price,
			@IsAction,
			@StartDate,
			@EndDate,
			@DocType,
			@CreateDate,
			@DLM,
			@StatusID,
			@InformationSystemID,
			@CodeInIS,
			@ALineNo,
			@CHECKSUMM,
			@Comment,
			@Marked,
			@Posted,
			source.[_DateID]   ,
			source.[_NomenclatureID] ,
			source.[_PriceTypeID]	   ,
			source.[_ContragentID]	,
			source.[_DeliveryPlaceID]
		);
	END 
	-- если номенклатуры нет
	-- значит нет строк документа
	-- если нет строк документа 
	-- значит он помечен на удаление
	-- стало быть и мне следует его пометить таковым со всеми строками
	ELSE BEGIN
		UPDATE [DW].[FactPrices]
		SET
			[Marked]			= @Marked,
			[Posted]			= @Posted
		WHERE 
			 [InformationSystemID] = @InformationSystemID
			 AND [CodeInIS] = @CodeInIS

	END;


END

GO

