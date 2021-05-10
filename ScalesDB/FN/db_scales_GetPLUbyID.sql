CREATE FUNCTION [db_scales].[GetPLUbyID]
(
	@ScaleID int,
	@PLU int
)
RETURNS TABLE
AS
RETURN
	SELECT  
		[Id]
		,[GoodsName]
		,[GoodsFullName]
		,[GoodsDescription]
		,[TemplateID]
		,[GTIN]
		,[EAN13]
		,[ITF14]
		,[GoodsShelfLifeDays]
		,[GoodsTareWeight]
		,[GoodsBoxQuantly]
		,NomenclatureId as RRefGoods
		,[Plu]
		,[Active]

		,[UpperWeightThreshold]
		,[NominalWeight]			
		,[LowerWeightThreshold]
		,[CheckWeight]

	FROM [db_scales].[PLU]
	WHERE [ScaleID] = @ScaleID
	AND [PLU] = @PLU
	AND [Marked] = 0
GO

GRANT SELECT ON [db_scales].[GetPLUbyID] TO [db_scales_users]; 
GO