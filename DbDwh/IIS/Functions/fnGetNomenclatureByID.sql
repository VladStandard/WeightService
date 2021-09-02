-- [IIS].[fnGetNomenclatureByID]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetNomenclatureByID]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnGetNomenclatureByID] (@ID BIGINT)
RETURNS XML
AS
BEGIN
	RETURN (SELECT
			*
		FROM (SELECT
				[n].[ID] "@ID"
			   ,[n].[Name] "@Name"
			   ,[n].[Code] "@Code"
			   ,[n].[MasterId] "@MasterId"
			   ,[n].[InformationSystemID] "@InformationSystemID"
			   ,[n].[NameFull] "FullName"
			   ,[n].[CreateDate] "CreateDate"
			   ,[n].[DLM] "DLM"
			   ,[ng].[Name] "NomenclatureGroup"
			   ,JSON_VALUE([n].[Parents], '$.parents[0]') "Category"
			   ,[b].[Name] "Brand"
			   ,[n].[boxTypeName] "boxTypeName"
			   ,[n].[packTypeName] "packTypeName"
			   ,[n].[Unit] "Unit"
			   ,cost.[Price] "PlannedCost"
			   ,CAST((SELECT
						[Price] AS "@Price"
					   ,[IsAction] AS "@IsAction"
					   ,[StartDate] AS "@StartDate"
					FROM [DW].[FactPrices] AS [fp]
					WHERE [PriceTypeID] = 0xBA6D90E6BA17BDD711E297052E5C534D
					AND [DocType] = 'DocumentRef.УстановкаЦенНоменклатуры'
					AND [IsAction] = 0
					AND [fp].NomenclatureID = [n].CodeInIS
					AND [fp].[Marked] = 0
					AND [fp].[Posted] = 1
					FOR XML PATH ('Price'), BINARY BASE64)
				AS XML) AS Prices
			FROM [DW].[DimNomenclatures] AS [n]
			LEFT JOIN [DW].[DimTypesOfNomenclature] t
				ON [n].NomenclatureType = t.[CodeInIS] --AND Nomenclature.[InformationSystemID] = t.[InformationSystemID]
			LEFT JOIN [DW].[vwCurrentPlannedCost] cost
				ON [n].[ID] = cost.[NomenclatureID]
			LEFT JOIN [DW].[DimNomenclatureGroups] AS ng
				ON [n].[NomenclatureGroup] = ng.[CodeInIS]
			LEFT JOIN [DW].[DimBrands] AS b
				ON [n].[Brand] = b.[CodeInIS]
			WHERE
			--JSON_VALUE([n].[Parents], '$.parents[0]') IN ('Колбасные изделия','Мясные продукты','Рыбная продукция')
			t.[GoodsForSale] = 1
			AND COALESCE([n].[Marked], 0) = 0
			AND [n].[ID] = @ID) AS D
		FOR XML PATH ('Nomenclature')
		, ROOT ('Goods')
		, BINARY BASE64)
END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[fnGetNomenclatureByID] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @ID BIGINT = -2147480396
SELECT [IIS].[fnGetNomenclatureByID](@ID) [fnGetNomenclatureByID]
