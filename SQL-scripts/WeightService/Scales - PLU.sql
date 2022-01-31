------------------------------------------------------------------------------------------------------------------------
-- Scales - PLU
------------------------------------------------------------------------------------------------------------------------
declare @id int = 200
use [ScalesDB]
select
	 [db_scales].[PLU].[Id]
	,[db_scales].[PLU].[Marked]
	,[db_scales].[PLU].[Plu]
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
	,[db_scales].[Scales].[Id] [Scales_Id]
	,[db_scales].[Scales].[ScaleFactor]
from [db_scales].[PLU]
left join [db_scales].[Scales] on [PLU].[ScaleId] = [Scales].[Id]
where [db_scales].[PLU].[Id] = @id
order by [db_scales].[PLU].[GoodsName]
------------------------------------------------------------------------------------------------------------------------
