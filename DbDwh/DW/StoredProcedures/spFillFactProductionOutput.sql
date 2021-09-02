CREATE PROCEDURE [DW].[spFillFactProductionOutput]
	@DocNum				nvarchar(15),
	@DocDate			datetime,
	@DocType			nvarchar(150),
	@Marked				bit,
	@Posted				bit,
    @OrgID				varbinary(16),
	@StorageOut			varbinary(16),
	@StorageIn			varbinary(16),
    @NomenclatureID		varbinary(16),
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

	MERGE [DW].[FactProductionOutput] as target
	USING ( 
	SELECT 

         @DateID

        ,@DocNum	
		,@DocDate
		,@DocType
		,@Marked	
		,@Posted	

        ,@OrgID
        ,@StorageOut	
        ,@StorageIn	
        ,@NomenclatureID
        ,@Qty
        ,@Price
        ,@Cost
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
		,(select top(1) ID from [DW].[DimStorages] where [CodeInIS] = @StorageOut)
		,(select top(1) ID from [DW].[DimStorages] where [CodeInIS] = @StorageIn)
		,(select top(1) ID from [DW].[DimNomenclatures] where [CodeInIS] = @NomenclatureID)


	) AS source (
        [DateID]
        ,[DocNum]	
		,[DocDate]
		,[DocType]
		,[Marked]	
		,[Posted]	

        ,[OrgID]
        ,[StorageOut]
        ,[StorageIn]
        ,[NomenclatureID]

        ,[Qty]
        ,[Price]
        ,[Cost]
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
		,[_StorageOut]	
		,[_StorageIn]	
		,[_NomenclatureID]	

	)  
	ON (target.[CodeInIs] = source.[CodeInIs] 
		AND target.[_LineNo] = source.[_LineNo] 
		AND target.[InformationSystemID] = source.[InformationSystemID]
	) 

	WHEN MATCHED AND target.[CHECKSUMM] <> source.[CHECKSUMM]
	THEN UPDATE SET 
         [DateID]			 = source.DateID 
		,[DocNum]	= source.[DocNum]	
		,[DocDate]	= source.[DocDate]
		,[DocType]	= source.[DocType]
		,[Marked]	= source.[Marked]	
		,[Posted]	= source.[Posted]	

        ,[OrgID]			 = source.OrgID
        ,[StorageOut]		 = source.StorageOut
        ,[StorageIn]		 = source.StorageIn
        ,[NomenclatureID]	 = source.NomenclatureID
        ,[Qty]				 = source.Qty
        ,[Price]			 = source.Price
        ,[Cost]				 = source.Cost
	    ,[CostVAT]			 = source.CostVAT

        ,[CreateDate]		 = source.CreateDate
        ,[DLM]				 = source.DLM
        ,[StatusID]			 = source.StatusID
		,[CHECKSUMM]		 = source.CHECKSUMM

		,[_DateID]           = source.[_DateID]
		,[_OrgID]			 = source.[_OrgID]
		,[_StorageOut]		 = source.[_StorageOut]
		,[_StorageIn]		 = source.[_StorageIn]
		,[_NomenclatureID]	 = source.[_NomenclatureID]
		,Active = 1

	--WHEN NOT MATCHED BY SOURCE AND 
	--	(target.[CodeInIs] = source.[CodeInIs] AND target.[InformationSystemID] = source.[InformationSystemID]) 
	--	THEN DELETE
	
	WHEN NOT MATCHED BY TARGET THEN INSERT (
         [DateID]
        ,[DocNum]	
		,[DocDate]	
		,[DocType]	
		,[Marked]	
		,[Posted]	

        ,[OrgID]
        ,[StorageOut]
        ,[StorageIn]
        ,[NomenclatureID]
        ,[Qty]
        ,[Price]
        ,[Cost]
	    ,[CostVAT]

        ,[CreateDate]
        ,[DLM]
        ,[StatusID]
        ,[InformationSystemID]
        ,[CodeInIs]
        ,[_LineNo]
		,[CHECKSUMM]

		,[_DateID]          
		,[_OrgID]			
		,[_StorageOut]	
		,[_StorageIn]	
		,[_NomenclatureID]	

	) VALUES (

         source.DateID
		,source.[DocNum]	
		,source.[DocDate]
		,source.[DocType]
		,source.[Marked]	
		,source.[Posted]	

        ,source.OrgID
        ,source.StorageOut
        ,source.StorageIn
        ,source.NomenclatureID
        ,source.Qty
        ,source.Price
        ,source.Cost
	    ,source.CostVAT

		,source.CreateDate
		,source.DLM
		,source.StatusID
		,source.InformationSystemID
		,source.CodeInIS 
		,source.[_LineNo]
		,source.CHECKSUMM

		,source.[_DateID]          
		,source.[_OrgID]			
		,source.[_StorageOut]	
		,source.[_StorageIn]	
		,source.[_NomenclatureID]	

	);

END

GO

