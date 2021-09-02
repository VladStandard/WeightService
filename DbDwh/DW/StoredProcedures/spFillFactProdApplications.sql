CREATE PROCEDURE [DW].[spFillFactProdApplications]

	@Marked						bit,
	@Posted						bit,
	@DocNum						nvarchar(12),
	@DocDate					datetime,
	@OrgID						binary(16),	     	     
	@СкладCырья					binary(16),
	@СкладГотовойПродукции		binary(16),
	@Комментарий				nvarchar(1000),
	@ВидОперации				nvarchar(25),
	@Номенклатура				binary(16),
	@ЕдиницаИзмеренияМест		nvarchar(100),
	@Характеристика				nvarchar(100),
	@КоличествоЗамесов			float,
	@Замес						float,
	@Рецептура					binary(16),
	@ФаршВес					float,
	@ФаршРасчет					float,
	@ФаршУтиль					float,
	@СреднийВесШтуки			float,
	@ВыходГотовойПродукции		float,
	@БракГотовойПродукции		float,
	@БракДегустация				float,
	@ОтклонениеКилограмм		float,
	@ОтклонениПроцент			float,
	@БракИзРецепта				float,
	@ВидОболочки				binary(16),
	@Примечание					nvarchar(1000),
	@ЗаказНаПроизводство		binary(16),
	@СерияНоменклатуры			binary(16),
	@КоличествоЗамесовОтделКачества float,
	@StatusID					int ,
	@InformationSystemID		int ,
	@CodeInIS					varbinary(16) ,
	@ALineNo						int ,
	@CHECKSUMM					bigint

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();
	DECLARE @DateID int = CONVERT(INT,FORMAT(@DocDate,'yyyyMMdd'));

	MERGE [DW].[FactProdApplications] as target
	USING ( 
	SELECT 

         @DateID
		,@Marked						
		,@Posted						
		,@DocNum						
		,@DocDate					
		,@OrgID						
		,@СкладCырья					
		,@СкладГотовойПродукции		
		,@Комментарий				
		,@ВидОперации				
		,@Номенклатура				
		,@ЕдиницаИзмеренияМест		
		,@Характеристика				
		,@КоличествоЗамесов			
		,@Замес						
		,@Рецептура					
		,@ФаршВес					
		,@ФаршРасчет					
		,@ФаршУтиль					
		,@СреднийВесШтуки			
		,@ВыходГотовойПродукции		
		,@БракГотовойПродукции		
		,@БракДегустация				
		,@ОтклонениеКилограмм		
		,@ОтклонениПроцент			
		,@БракИзРецепта				
		,@ВидОболочки				
		,@Примечание					
		,@ЗаказНаПроизводство		
		,@СерияНоменклатуры			
		,@КоличествоЗамесовОтделКачества

        ,@CreateDate
        ,@DLM
        ,@StatusID
        ,@InformationSystemID
        ,@CodeInIS
		,@ALineNo
		,@CHECKSUMM


		,CAST(@DocDate as date)
		,(select top(1) ID from [DW].[DimOrganizations] where [CodeInIS] = @OrgID)
		,(select top(1) ID from [DW].[DimStorages] where [CodeInIS] = @СкладCырья)
		,(select top(1) ID from [DW].[DimStorages] where [CodeInIS] = @СкладГотовойПродукции)
		,(select top(1) ID from [DW].[DimNomenclatures] where [CodeInIS] = @Номенклатура)

	) AS source (

	     [DateID]
		,[Marked]						
		,[Posted]						
		,[DocNum]						
		,[DocDate]					
		,[OrgID]					
		,[СкладCырья]					
		,[СкладГотовойПродукции]		
		,[Комментарий]				
		,[ВидОперации]				
		,[Номенклатура]				
		,[ЕдиницаИзмеренияМест]		
		,[Характеристика]				
		,[КоличествоЗамесов]			
		,[Замес]						
		,[Рецептура]					
		,[ФаршВес]					
		,[ФаршРасчет]					
		,[ФаршУтиль]					
		,[СреднийВесШтуки]			
		,[ВыходГотовойПродукции]		
		,[БракГотовойПродукции]		
		,[БракДегустация]				
		,[ОтклонениеКилограмм]		
		,[ОтклонениПроцент]			
		,[БракИзРецепта]				
		,[ВидОболочки]				
		,[Примечание]					
		,[ЗаказНаПроизводство]		
		,[СерияНоменклатуры]			
		,[КоличествоЗамесовОтделКачества]

        ,[CreateDate]
        ,[DLM]
        ,[StatusID]
        ,[InformationSystemID]
        ,[CodeInIS]
        ,[_LineNo]
		,[CHECKSUMM]

		,[_DateID]          
		,[_OrgID]			
		,[_Склад сырья]
		,[_Склад готовой продукции]
		,[_Номенклатура]			


	)  
	ON (target.[CodeInIs] = source.[CodeInIs] 
	AND target.[_LineNo] = source.[_LineNo] 
	AND target.[InformationSystemID] = source.[InformationSystemID]) 

	WHEN MATCHED AND target.CHECKSUMM <> @CHECKSUMM
	THEN UPDATE SET 
         [DateID]	= source.[DateID]
		,[DocNum]	= source.[DocNum]	
		,[DocDate]	= source.[DocDate]
		,[Marked]	= source.[Marked]	
		,[Posted]	= source.[Posted]	

		,[OrgID]								= source.[OrgID]					
		,[Склад сырья]							= source.[СкладCырья]					
		,[Склад готовой продукции]				= source.[СкладГотовойПродукции]		
		,[Комментарий]							= source.[Комментарий]				
		,[Вид операции]							= source.[ВидОперации]				
		,[Номенклатура]							= source.[Номенклатура]				
		,[Единица измерения мест]				= source.[ЕдиницаИзмеренияМест]		
		,[Характеристика]						= source.[Характеристика]				
		,[Количество замесов]					= source.[КоличествоЗамесов]			
		,[Замес]								= source.[Замес]						
		,[Рецептура]							= source.[Рецептура]					
		,[Фарш Вес]								= source.[ФаршВес]					
		,[Фарш Расчет]							= source.[ФаршРасчет]					
		,[Фарш Утиль]							= source.[ФаршУтиль]					
		,[Средний вес штуки]					= source.[СреднийВесШтуки]			
		,[Выход готовой продукции]				= source.[ВыходГотовойПродукции]		
		,[Брак готовой продукции]				= source.[БракГотовойПродукции]		
		,[Брак дегустация]						= source.[БракДегустация]				
		,[Отклонение килограмм]					= source.[ОтклонениеКилограмм]		
		,[Отклонение процент]					= source.[ОтклонениПроцент]			
		,[Брак из рецепта]						= source.[БракИзРецепта]				
		,[Вид оболочки]							= source.[ВидОболочки]				
		,[Примечание]							= source.[Примечание]					
		,[Заказ на производство]				= source.[ЗаказНаПроизводство]		
		,[Серия номенклатуры]					= source.[СерияНоменклатуры]			
		,[Количество замесов отдел качества]	= source.[КоличествоЗамесовОтделКачества]

        ,[CreateDate]	= @CreateDate
        ,[DLM]			= @DLM
        ,[StatusID]		= @StatusID
		,[CHECKSUMM]    = @CHECKSUMM

		,[_DateID]          = source.[_DateID]          
		,[_OrgID]			= source.[_OrgID]			
		,[_Склад сырья]					= source.[_Склад сырья]	
		,[_Склад готовой продукции]		= source.[_Склад готовой продукции]	
		,[_Номенклатура]				= source.[_Номенклатура]	
		,Active = 1


 --   WHEN NOT MATCHED BY SOURCE          
 --       AND [InformationSystemID] = @InformationSystemID
 --       AND [CodeInIS] = @CodeInIS
 --       AND [LineNo] = @LineNo
	--THEN DELETE 

	WHEN NOT MATCHED BY TARGET THEN INSERT (
        [DateID]
		,[DocNum]	
		,[DocDate]	
		,[Marked]	
		,[Posted]	

		,[OrgID]							
		,[Склад сырья]						
		,[Склад готовой продукции]			
		,[Комментарий]						
		,[Вид операции]						
		,[Номенклатура]						
		,[Единица измерения мест]			
		,[Характеристика]					
		,[Количество замесов]				
		,[Замес]							
		,[Рецептура]						
		,[Фарш Вес]							
		,[Фарш Расчет]						
		,[Фарш Утиль]						
		,[Средний вес штуки]				
		,[Выход готовой продукции]			
		,[Брак готовой продукции]			
		,[Брак дегустация]					
		,[Отклонение килограмм]				
		,[Отклонение процент]				
		,[Брак из рецепта]					
		,[Вид оболочки]						
		,[Примечание]						
		,[Заказ на производство]			
		,[Серия номенклатуры]				
		,[Количество замесов отдел качества]

        ,[CreateDate]
        ,[DLM]
        ,[StatusID]
        ,[InformationSystemID]
        ,[CodeInIs]
        ,[_LineNo]
		,[CHECKSUMM]
		,[_DateID]          
		,[_OrgID]			
		,[_Склад сырья]				
		,[_Склад готовой продукции]	
		,[_Номенклатура]			

	) VALUES (
        @DateID
		,source.[DocNum]	
		,source.[DocDate]
		,source.[Marked]	
		,source.[Posted]	
		,source.[OrgID]					
		,source.[СкладCырья]					
		,source.[СкладГотовойПродукции]		
		,source.[Комментарий]				
		,source.[ВидОперации]				
		,source.[Номенклатура]				
		,source.[ЕдиницаИзмеренияМест]		
		,source.[Характеристика]				
		,source.[КоличествоЗамесов]			
		,source.[Замес]						
		,source.[Рецептура]					
		,source.[ФаршВес]					
		,source.[ФаршРасчет]					
		,source.[ФаршУтиль]					
		,source.[СреднийВесШтуки]			
		,source.[ВыходГотовойПродукции]		
		,source.[БракГотовойПродукции]		
		,source.[БракДегустация]				
		,source.[ОтклонениеКилограмм]		
		,source.[ОтклонениПроцент]			
		,source.[БракИзРецепта]				
		,source.[ВидОболочки]				
		,source.[Примечание]					
		,source.[ЗаказНаПроизводство]		
		,source.[СерияНоменклатуры]			
		,source.[КоличествоЗамесовОтделКачества]

		,@CreateDate
		,@DLM
		,@StatusID
		,@InformationSystemID
		,@CodeInIS 
		,@ALineNo
		,@CHECKSUMM
		,source.[_DateID]          
		,source.[_OrgID]			
		,source.[_Склад сырья]	
		,source.[_Склад готовой продукции]
		,source.[_Номенклатура]	

	);

END

GO
