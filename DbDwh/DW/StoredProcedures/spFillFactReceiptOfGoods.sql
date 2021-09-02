CREATE PROCEDURE [DW].[spFillFactReceiptOfGoods]

	@DocNum				nvarchar(15),
	@DocDate			datetime,
	@DocType			nvarchar(150),
	@Marked				bit,
	@Posted				bit,
    @OrgID				varbinary(16),
    @ContragentID		varbinary(16),
    @StorageID			varbinary(16),
    @NomenclatureID		varbinary(16),
    @VATRate			nvarchar (10),
    @Qty				decimal(15,3),
    @Price				decimal(15,2),
    @Cost				decimal(15,2),
    @CostVAT			decimal(15,2),
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

	MERGE [DW].[FactReceiptOfGoods] as target
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
        ,@StorageID
        ,@NomenclatureID
        ,@Qty
        ,@Price
        ,@Cost
		,@VATRate
	    ,@CostVAT
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
		,(select top(1) ID from [DW].[DimStorages] where [CodeInIS] = @StorageID)
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
	    ,[CostVAT]
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
        ,[_StorageID]	   
        ,[_NomenclatureID]	

	)  
	ON (
		target.[InformationSystemID] = source.[InformationSystemID]
		AND target.[CodeInIs] = source.[CodeInIs] 
		AND target.[_LineNo] = source.[_LineNo] 
		) 

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
        ,[StorageID]	= @StorageID
        ,[NomenclatureID]	= @NomenclatureID
        ,[Qty]			= @Qty
        ,[Price]		= @Price
        ,[Cost]			= @Cost
		,[VATRate]		= @VATRate
	    ,[CostVAT]		= @CostVAT
        ,[CreateDate]	= @CreateDate
        ,[DLM]			= @DLM
        ,[StatusID]		= @StatusID
		,[CHECKSUMM]    = @CHECKSUMM

        ,[_DateID]        = source.[_DateID]         
        ,[_OrgID]		  = source.[_OrgID]		  
        ,[_ContragentID]  = source.[_ContragentID]  
        ,[_StorageID]	  = source.[_StorageID]	   
        ,[_NomenclatureID]= source.[_NomenclatureID]
		
	--WHEN NOT MATCHED BY SOURCE AND 
	--	(target.[InformationSystemID] = source.[InformationSystemID] AND target.[CodeInIs] = source.[CodeInIs] ) 
	--	THEN DELETE

	WHEN NOT MATCHED BY TARGET THEN INSERT (
        [DateID]
        ,[DocNum]	
		,[DocDate]	
		,[DocType]	
		,[Marked]	
		,[Posted]	

        ,[OrgID]
        ,[ContragentID]
        ,[StorageID]
        ,[NomenclatureID]
        ,[Qty]
        ,[Price]
        ,[Cost]
		,[VATRate]
	    ,[CostVAT]
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
        ,[_StorageID]	  
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
        ,@StorageID
        ,@NomenclatureID
        ,@Qty
        ,@Price
        ,@Cost
		,@VATRate
	    ,@CostVAT
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
        ,source.[_StorageID]	  
        ,source.[_NomenclatureID]

	);

END

GO

