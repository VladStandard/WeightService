------------------------------------------------------------------------------------------------------------------------
-- Scales - PLU
------------------------------------------------------------------------------------------------------------------------
DECLARE @id INT = 200
SELECT
	[db_scales].[PLU].[id]
   ,[db_scales].[PLU].[Marked]
   ,[db_scales].[PLU].[PLU]
   ,[db_scales].[PLU].[GoodsName]
   ,[db_scales].[PLU].[GoodsFullName]
   ,[db_scales].[PLU].[GoodsDescription]
   ,[db_scales].[PLU].[TemplateID]
   ,[db_scales].[PLU].[GTIN]
   ,[db_scales].[PLU].[EAN13]
   ,[db_scales].[PLU].[ITF14]
   ,[db_scales].[PLU].[GoodsShelfLifeDays]
   ,[db_scales].[PLU].[GoodsTareWeight]
   ,[db_scales].[PLU].[GoodsBoxQuantly]
   ,[db_scales].[PLU].[CreateDate]
   ,[db_scales].[PLU].[ModifiedDate]
   ,[db_scales].[PLU].[LowerWeightThreshold]
   ,[db_scales].[PLU].[NominalWeight]
   ,[db_scales].[PLU].[UpperWeightThreshold]
   ,[db_scales].[Scales].[id] [SCALES_ID]
   ,[db_scales].[Scales].[ScaleFactor]
FROM [db_scales].[PLU]
LEFT JOIN [db_scales].[Scales] ON [PLU].[ScaleId] = [Scales].[id]
--WHERE [db_scales].[PLU].[id] = @id
ORDER BY [db_scales].[PLU].[GoodsName]
------------------------------------------------------------------------------------------------------------------------
