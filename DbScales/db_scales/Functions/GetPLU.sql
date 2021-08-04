CREATE FUNCTION [db_scales].[GetPLU]
(
	@ScaleID int
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
		,[NomenclatureID] as RRefGoods
		,[PLU]
		,[UpperWeightThreshold]
		,[NominalWeight]			
		,[LowerWeightThreshold]
		,[CheckWeight]

	FROM [db_scales].[PLU]
	WHERE [ScaleID] = @ScaleID AND [Active] = 1 AND [Marked] = 0
GO
GRANT SELECT
    ON OBJECT::[db_scales].[GetPLU] TO [db_scales_users]
    AS [scales_owner];

