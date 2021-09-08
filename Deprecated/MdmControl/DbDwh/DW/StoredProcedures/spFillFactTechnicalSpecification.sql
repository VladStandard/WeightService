CREATE PROCEDURE [DW].[spFillFactTechnicalSpecification]
	@Marked 						bit,
	@Posted 						bit,
	@DocDate						datetime,
	@DocNum 						nvarchar(12),
	@Продукция						binary(16),	     	     
	@Замес							float,
	@Ответственный					binary(16),
	@Организация					binary(16),
	@НормаВыходаГотовойПродукции	float,
	@СкладПроизводство				binary(16),
	@Номенклатура					binary(16),
	@Количество						numeric(15,2),    
	@ЕдиницаИзмерения				binary(16),    
	@КЕИ							nvarchar(15),    
	@СтатьяЗатрат					binary(16),
	
	@StatusID						int,
    @InformationSystemID			int,
	@CodeInIS						binary(16),
	@ALineNo						int,


	@CHECKSUMM						int

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();
	DECLARE @DateID int = CONVERT(INT,FORMAT(@DocDate,'yyyyMMdd'));

	MERGE [DW].[FactTechnicalSpecification] as target
	USING ( 
	SELECT 
	     @DateID
		,@Marked 					
		,@Posted 					
		,@DocDate					
		,@DocNum 					
		,@Продукция					
		,@Замес						
		,@Ответственный				
		,@Организация				
		,@НормаВыходаГотовойПродукции
		,@СкладПроизводство			
		,@Номенклатура				
		,@Количество					
		,@ЕдиницаИзмерения			
		,@КЕИ						
		,@СтатьяЗатрат				

        ,@CreateDate
        ,@DLM
        ,@StatusID
        ,@InformationSystemID
        ,@CodeInIS
		,@ALineNo
		,@CHECKSUMM

		,CAST(@DocDate as date)
		,(select top(1) ID from [DW].[DimNomenclatures] where [CodeInIS] = @Продукция)
		,(select top(1) ID from [DW].[DimOrganizations] where [CodeInIS] = @Организация)
		,(select top(1) ID from [DW].[DimStorages] where [CodeInIS] = @СкладПроизводство)
		,(select top(1) ID from [DW].[DimNomenclatures] where [CodeInIS] = @Номенклатура)


	) AS source (
	     [DateID]
		,[Marked]				
		,[Posted] 					
		,[DocDate]					
		,[DocNum] 					
		,[Продукция]					
		,[Замес]						
		,[Ответственный]				
		,[Организация]				
		,[НормаВыходаГотовойПродукции]
		,[StorageID]			
		,[Номенклатура]				
		,[Количество]					
		,[ЕдиницаИзмерения]			
		,[КЕИ]						
		,[СтатьяЗатрат]				

        ,[CreateDate]
        ,[DLM]
        ,[StatusID]
        ,[InformationSystemID]
        ,[CodeInIS]
        ,[_LineNo]
		,[CHECKSUMM]
		,[_DateID]        
		,[_ProductID]	
		,[_OrgID]			
		,[_StorageID]	
		,[_NomenclatureID]	

	)  
	ON (target.[CodeInIs] = source.[CodeInIs] 
		AND target.[_LineNo] = source.[_LineNo] 
		AND target.[InformationSystemID] = source.[InformationSystemID]
	) 

	WHEN MATCHED AND target.[CHECKSUMM] <> source.[CHECKSUMM]
	THEN UPDATE SET 
	     [DateID]						 = source.[DateID]						
		,[Marked]						 = source.[Marked]						
		,[Posted] 						 = source.[Posted] 						
		,[DocDate]						 = source.[DocDate]						
		,[DocNum] 						 = source.[DocNum] 						
		,[Продукция]					 = source.[Продукция]					
		,[Замес]						 = source.[Замес]						
		,[Ответственный]				 = source.[Ответственный]				
		,[OrgId]						 = source.[Организация]					
		,[НормаВыходаГотовойПродукции]	 = source.[НормаВыходаГотовойПродукции]	
		,[StorageID]					 = source.[StorageID]					
		,[NomenclatureID]				 = source.[Номенклатура]					
		,[Количество]					 = source.[Количество]					
		,[ЕдиницаИзмерения]				 = source.[ЕдиницаИзмерения]				
		,[КЕИ]							 = source.[КЕИ]							
		,[СтатьяЗатрат]					 = source.[СтатьяЗатрат]					

        ,[DLM]							 = source.DLM
        ,[StatusID]						 = source.StatusID
		,[CHECKSUMM]					 = source.CHECKSUMM

		,[_DateID]						 = source.[_DateID]			
		,[_ProductID]					 = source.[_ProductID]		
		,[_OrgID]						 = source.[_OrgID]			
		,[_StorageID]					 = source.[_StorageID]		
		,[_NomenclatureID]				 = source.[_NomenclatureID]
		,Active							 = 1

	--WHEN NOT MATCHED BY SOURCE AND 
	--	(target.[CodeInIs] = source.[CodeInIs] AND target.[InformationSystemID] = source.[InformationSystemID]) 
	--	THEN DELETE
	
	WHEN NOT MATCHED BY TARGET THEN INSERT (
		 [DateID]						
		,[Marked]						
		,[Posted] 						
		,[DocDate]						
		,[DocNum] 						
		,[Продукция]					
		,[Замес]						
		,[Ответственный]				
		,[OrgID]					
		,[НормаВыходаГотовойПродукции]	
		,[StorageID]					
		,[NomenclatureID]					
		,[Количество]					
		,[ЕдиницаИзмерения]				
		,[КЕИ]							
		,[СтатьяЗатрат]					
	
		,[CreateDate]
		,[DLM]
		,[StatusID]
		,[InformationSystemID]
		,[CodeInIS]
		,[_LineNo]
		,[CHECKSUMM]

		,[_DateID]						
		,[_ProductID]					
		,[_OrgID]						
		,[_StorageID]					
		,[_NomenclatureID]				

	) VALUES (
		source.[DateID]						
		,source.[Marked]						
		,source.[Posted] 						
		,source.[DocDate]						
		,source.[DocNum] 						
		,source.[Продукция]					
		,source.[Замес]						
		,source.[Ответственный]				
		,source.[Организация]					
		,source.[НормаВыходаГотовойПродукции]
		,source.[StorageID]					
		,source.[Номенклатура]				
		,source.[Количество]					
		,source.[ЕдиницаИзмерения]			
		,source.[КЕИ]							
		,source.[СтатьяЗатрат]				
	
		,source.CreateDate
		,source.DLM
		,source.StatusID
		,source.InformationSystemID
		,source.CodeInIS 
		,source.[_LineNo]
		,source.CHECKSUMM
	
		,source.[_DateID]			
		,source.[_ProductID]		
		,source.[_OrgID]			
		,source.[_StorageID]		
		,source.[_NomenclatureID]

	);

END

GO

