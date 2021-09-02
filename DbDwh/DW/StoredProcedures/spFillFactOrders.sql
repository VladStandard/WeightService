CREATE PROCEDURE [DW].[spFillFactOrders]

	@DocNum				nvarchar(15),
	@DocDate			datetime,
	@DocType			nvarchar(150),
	@Marked				bit,
	@Posted				bit,
    @OrgID				varbinary(16),
    @ContragentID		varbinary(16),
    @DeliveryPlaceID	varbinary(16),
    @NomenclatureID		varbinary(16),
    @VATRate			nvarchar (10),
    @Qty				decimal(15,3),
    @Price				decimal(15,2),
    @PriceVAT			decimal(15,2),
    @Cost				decimal(15,2),
    @CostVAT			decimal(15,2),

	@DateOfPayment		datetime,		--Документ.ЗаказПокупателя.Реквизит.ДатаОплаты
	@OrderDate			datetime,		--Документ.ЗаказПокупателя.Реквизит.ВС_ЗаказДата
	@ShippingDate		datetime,		--Документ.ЗаказПокупателя.Реквизит.ДатаОтгрузки
	@GlobeNumber		nvarchar(100),

	@StatusID				int,
	@InformationSystemID	int,
	@CodeInIS			varbinary(16),
	@ALineNo			int,
	@CHECKSUMM			bigint

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();
	DECLARE @DateID int = CONVERT(INT,FORMAT(@DocDate,'yyyyMMdd'));

	MERGE [DW].[FactOrdersOfGoods] as target
	USING ( 
	SELECT 

        @DateID

		,@DocNum	
		,@DocDate
		,@DocType
		,@Marked	
		,@Posted	

        ,@OrgID
        ,@ContragentID
        ,@DeliveryPlaceID
        ,@NomenclatureID
        ,@Qty
        ,@Price
        ,@Cost
		,@VATRate
	    ,@PriceVAT
	    ,@CostVAT

		,@DateOfPayment		--Документ.ЗаказПокупателя.Реквизит.ДатаОплаты
		,@OrderDate			--Документ.ЗаказПокупателя.Реквизит.ВС_ЗаказДата
		,@ShippingDate		--Документ.ЗаказПокупателя.Реквизит.ДатаОтгрузки
		,@GlobeNumber

        ,@CreateDate
        ,@DLM
        ,@StatusID
        ,@InformationSystemID
        ,@CodeInIS
		,@ALineNo
		,@CHECKSUMM

		,CAST(@DocDate as date)
		,(select top(1) ID from [DW].[DimOrganizations] where [CodeInIS] = @OrgID)
		,(select top(1) ID from [DW].[DimContragents] where [CodeInIS] = @ContragentID)
		,(select top(1) ID from [DW].[DimDeliveryPlaces] where [CodeInIS] = @DeliveryPlaceID)
		,(select top(1) ID from [DW].[DimNomenclatures] where [CodeInIS] = @NomenclatureID)

	) AS source (
        [DateID]
		,[DocNum]	
		,[DocDate]
		,[DocType]
		,[Marked]	
		,[Posted]	

        ,[OrgID]
        ,[ContragentID]
        ,[DeliveryPlaceID]
        ,[NomenclatureID]
        ,[Qty]
        ,[Price]
        ,[Cost]
		,[VATRate]
		,[PriceVAT]
	    ,[CostVAT]

		,[DateOfPayment] 		--Документ.ЗаказПокупателя.Реквизит.ДатаОплаты
		,[OrderDate]			--Документ.ЗаказПокупателя.Реквизит.ВС_ЗаказДата
		,[ShippingDate]			--Документ.ЗаказПокупателя.Реквизит.ДатаОтгрузки
		,[GlobeNumber]

        ,[CreateDate]
        ,[DLM]
        ,[StatusID]
        ,[InformationSystemID]
        ,[CodeInIS]
        ,[_LineNo]
		,[CHECKSUMM]
		,[_DateID]          
		,[_OrgID]			
		,[_ContragentID]	
		,[_DeliveryPlaceID]	
		,[_NomenclatureID]	


	)  
	ON (target.[CodeInIs] = source.[CodeInIs] 
	AND target.[_LineNo] = source.[_LineNo] 
	AND target.[InformationSystemID] = source.[InformationSystemID]) 

	WHEN MATCHED AND target.CHECKSUMM <> @CHECKSUMM
	THEN UPDATE SET 
         [DateID]		= @DateID 
		,[DocNum]	= source.[DocNum]	
		,[DocDate]	= source.[DocDate]
		,[DocType]	= source.[DocType]
		,[Marked]	= source.[Marked]	
		,[Posted]	= source.[Posted]	

        ,[OrgID]		= @OrgID
        ,[ContragentID] = @ContragentID
        ,[DeliveryPlaceID]	= @DeliveryPlaceID
        ,[NomenclatureID]	= @NomenclatureID
        ,[Qty]			= @Qty
        ,[Price]		= @Price
        ,[Cost]			= @Cost
		,[VATRate]		= @VATRate
		,[PriceVAT]		= @PriceVAT
	    ,[CostVAT]		= @CostVAT

		,[DateOfPayment] = @DateOfPayment	--Документ.ЗаказПокупателя.Реквизит.ДатаОплаты
		,[OrderDate]	=  @OrderDate		--Документ.ЗаказПокупателя.Реквизит.ВС_ЗаказДата
		,[ShippingDate]	=  @ShippingDate	--Документ.ЗаказПокупателя.Реквизит.ДатаОтгрузки
		,[GlobeNumber]	= source.[GlobeNumber]


        ,[CreateDate]	= @CreateDate
        ,[DLM]			= @DLM
        ,[StatusID]		= @StatusID
		,[CHECKSUMM]    = @CHECKSUMM

		,[_DateID]          = source.[_DateID]          
		,[_OrgID]			= source.[_OrgID]			
		,[_ContragentID]	= source.[_ContragentID]	
		,[_DeliveryPlaceID]	= source.[_DeliveryPlaceID]	
		,[_NomenclatureID]	= source.[_NomenclatureID]	



 --   WHEN NOT MATCHED BY SOURCE          
 --       AND [InformationSystemID] = @InformationSystemID
 --       AND [CodeInIS] = @CodeInIS
 --       AND [LineNo] = @LineNo
	--THEN DELETE 

	WHEN NOT MATCHED BY TARGET THEN INSERT (
        [DateID]
		,[DocNum]	
		,[DocDate]	
		,[DocType]	
		,[Marked]	
		,[Posted]	
        ,[OrgID]
        ,[ContragentID]
        ,[DeliveryPlaceID]
        ,[NomenclatureID]
        ,[Qty]
        ,[Price]
        ,[Cost]
		,[VATRate]
	    ,[PriceVAT]
	    ,[CostVAT]

		,[DateOfPayment]	--Документ.ЗаказПокупателя.Реквизит.ДатаОплаты
		,[OrderDate]		--Документ.ЗаказПокупателя.Реквизит.ВС_ЗаказДата
		,[ShippingDate]		--Документ.ЗаказПокупателя.Реквизит.ДатаОтгрузки
		,[GlobeNumber]

        ,[CreateDate]
        ,[DLM]
        ,[StatusID]
        ,[InformationSystemID]
        ,[CodeInIs]
        ,[_LineNo]
		,[CHECKSUMM]
		,[_DateID]          
		,[_OrgID]			
		,[_ContragentID]	
		,[_DeliveryPlaceID]	
		,[_NomenclatureID]	

	) VALUES (
        @DateID
		,source.[DocNum]	
		,source.[DocDate]
		,source.[DocType]
		,source.[Marked]	
		,source.[Posted]	
        ,@OrgID
        ,@ContragentID
        ,@DeliveryPlaceID
        ,@NomenclatureID
        ,@Qty
        ,@Price
        ,@Cost
		,@VATRate
	    ,@PriceVAT
	    ,@CostVAT

		,@DateOfPayment	--Документ.ЗаказПокупателя.Реквизит.ДатаОплаты
		,@OrderDate		--Документ.ЗаказПокупателя.Реквизит.ВС_ЗаказДата
		,@ShippingDate	--Документ.ЗаказПокупателя.Реквизит.ДатаОтгрузки
		,source.[GlobeNumber]
		,@CreateDate
		,@DLM
		,@StatusID
		,@InformationSystemID
		,@CodeInIS 
		,@ALineNo
		,@CHECKSUMM
		,source.[_DateID]          
		,source.[_OrgID]			
		,source.[_ContragentID]	
		,source.[_DeliveryPlaceID]	
		,source.[_NomenclatureID]	

	);

END

GO
