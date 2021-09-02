CREATE PROCEDURE [DW].[spFillFactReturns]

	@DocNum				nvarchar(15),
	@DocDate			datetime,
	@DocType			nvarchar(150),
	@Marked				bit,
	@Posted				bit,
	@SalesCodeInIS      varbinary(16),
    @OrgID				varbinary(16),
    @ContragentID		varbinary(16),
    @DeliveryPlaceID	varbinary(16),
    @NomenclatureID		varbinary(16),
    @VATRate			nvarchar (10),
    @Qty				decimal(15,3),
    @Price				decimal(15,2),
    @Cost				decimal(15,2),
    @CostVAT			decimal(15,2),
    @OrderID            varbinary(16),

    @QtyNotFixed				decimal(15,3),
    @PriceNotFixed				decimal(15,2),
    @CostNotFixed				decimal(15,2),
    @CostVATNotFixed			decimal(15,2),

    @QtyBeforeChange				decimal(15,3),
    @PriceBeforeChange				decimal(15,2),
    @CostBeforeChange				decimal(15,2),
    @CostVATBeforeChange			decimal(15,2),


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
	DECLARE @DateID int = CONVERT(INT,FORMAT(@DocDate,'yyyyMMdd'));

	MERGE [DW].[FactReturns] as target
	USING ( 
	SELECT 
        @DateID
        ,@DocNum	
		,@DocDate
		,@DocType

        ,@Marked			
        ,@Posted			
        ,@SalesCodeInIS
        ,@OrgID
        ,@ContragentID
        ,@DeliveryPlaceID
        ,@NomenclatureID
        ,@Qty
        ,@Price
        ,@Cost
		,@VATRate
	    ,@CostVAT
        ,@OrderID

        ,@QtyNotFixed	
        ,@PriceNotFixed	
        ,@CostNotFixed	
        ,@CostVATNotFixed

        ,@QtyBeforeChange		
        ,@PriceBeforeChange		
        ,@CostBeforeChange		
        ,@CostVATBeforeChange	

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
		,(select top(1) ID from [DW].[FactSalesOfGoods] where [CodeInIS] = @SalesCodeInIS)

	) AS source (
        [DateID]
        ,[DocNum]	
		,[DocDate]
		,[DocType]

        ,[Marked]			
        ,[Posted]			
        ,[SalesCodeInIS]
        ,[OrgID]
        ,[ContragentID]
        ,[DeliveryPlaceID]
        ,[NomenclatureID]
        ,[Qty]
        ,[Price]
        ,[Cost]
		,[VATRate]
	    ,[CostVAT]
        ,[OrderID]

		,[QtyNotFixed]
        ,[PriceNotFixed]
        ,[CostNotFixed]
	    ,[CostVATNotFixed]
		
        ,[QtyBeforeChange]
        ,[PriceBeforeChange]
        ,[CostBeforeChange]	
        ,[CostVATBeforeChange]

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
	    ,[_SalesCodeID]		
	)  
	ON (target.[CodeInIs] = source.[CodeInIs] 
	AND target.[_LineNo] = source.[_LineNo] 
	AND target.[InformationSystemID] = source.[InformationSystemID]) 

	WHEN MATCHED AND target.CHECKSUMM <> @CHECKSUMM
	THEN UPDATE SET 
         [DateID]		    = @DateID 
		,[DocNum]	= source.[DocNum]	
		,[DocDate]	= source.[DocDate]
		,[DocType]	= source.[DocType]
		,[Marked]	= source.[Marked]	
		,[Posted]	= source.[Posted]	

        ,[SalesCodeInIS]    = @SalesCodeInIS
        ,[OrgID]		    = @OrgID
        ,[ContragentID]     = @ContragentID
        ,[DeliveryPlaceID]	= @DeliveryPlaceID
        ,[NomenclatureID]	= @NomenclatureID
        ,[Qty]			    = @Qty
        ,[Price]		    = @Price
        ,[Cost]			    = @Cost
		,[VATRate]		    = @VATRate
	    ,[CostVAT]		    = @CostVAT
	    ,[OrderID]		    = @OrderID

        ,[QtyNotFixed]			    = source.QtyNotFixed
        ,[PriceNotFixed]		    = source.PriceNotFixed
        ,[CostNotFixed]			    = source.CostNotFixed
	    ,[CostVATNotFixed]		    = source.CostVATNotFixed

        ,[QtyBeforeChange]			= source.QtyBeforeChange
        ,[PriceBeforeChange]		= source.PriceBeforeChange
        ,[CostBeforeChange]			= source.CostBeforeChange
	    ,[CostVATBeforeChange]		= source.CostVATBeforeChange
                                      
        ,[CreateDate]	    = @CreateDate
        ,[DLM]			    = @DLM
        ,[StatusID]		    = @StatusID
		,[CHECKSUMM]        = @CHECKSUMM

        ,[_DateID]          = source.[_DateID]          
        ,[_OrgID]			= source.[_OrgID]			
        ,[_ContragentID]	= source.[_ContragentID]	
        ,[_DeliveryPlaceID]	= source.[_DeliveryPlaceID]	
        ,[_NomenclatureID]	= source.[_NomenclatureID]	
        ,[_SalesCodeID]		= source.[_SalesCodeID]		


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

        ,[OrgID]
        ,[Marked]			
        ,[Posted]			
        ,[SalesCodeInIS]
        ,[ContragentID]
        ,[DeliveryPlaceID]
        ,[NomenclatureID]
        ,[Qty]
        ,[Price]
        ,[Cost]
		,[VATRate]
	    ,[CostVAT]
        ,[OrderID]

        ,[QtyNotFixed]		
        ,[PriceNotFixed]	
        ,[CostNotFixed]		
        ,[CostVATNotFixed]	

        ,[QtyBeforeChange]		
        ,[PriceBeforeChange]	
        ,[CostBeforeChange]		
        ,[CostVATBeforeChange]	

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
        ,[_SalesCodeID]		

	) VALUES (

        @DateID
		,source.[DocNum]	
		,source.[DocDate]
		,source.[DocType]

        ,@OrgID
        ,source.Marked
        ,source.Posted
        ,@SalesCodeInIS

        ,@ContragentID
        ,@DeliveryPlaceID
        ,@NomenclatureID

        ,@Qty
        ,@Price
        ,@Cost
		,@VATRate
	    ,@CostVAT
        ,@OrderID

        ,source.QtyNotFixed
        ,source.PriceNotFixed
        ,source.CostNotFixed
        ,source.CostVATNotFixed

        ,source.[QtyBeforeChange]		
        ,source.[PriceBeforeChange]	
        ,source.[CostBeforeChange]		
        ,source.[CostVATBeforeChange]	

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
        ,source.[_SalesCodeID]		

	);

END

GO
