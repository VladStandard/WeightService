-- [IIS].[fnGetNomenclatureList]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetNomenclatureList]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnGetNomenclatureList] (@offset INT = 0, @rowcount INT = 10)

RETURNS XML
AS
BEGIN
	DECLARE @RESULT TABLE ([ID] INT)
	INSERT INTO @RESULT ([ID])
		SELECT
			[n].[ID]
		FROM [DW].[DimNomenclatures] AS [n]
		LEFT JOIN [DW].[DimTypesOfNomenclature] [t]
			ON [n].NomenclatureType = t.[CodeInIS]
		WHERE t.[GoodsForSale] = 1
		AND COALESCE([n].[Marked], 0) = 0
		ORDER BY [n].[ID] OFFSET (@offset * @rowcount) ROWS FETCH NEXT @rowcount ROWS ONLY
	RETURN (SELECT
			*
		FROM (SELECT
				[N].[ID] "@ID"
			   ,[N].[Name] "@Name"
			   ,[N].[Code] "@Code"
			   ,[N].[MasterId] "@MasterId"
			   ,[N].[InformationSystemID] "@InformationSystemID"
			   ,[DW].[fnGetGuid1C]([N].[CodeInIS]) "@CodeInIS"
			   ,[N].[NameFull] "FullName"
			   ,ng.[Name] "NomenclatureGroup"
			   ,JSON_VALUE([N].[Parents], '$.parents[0]') "Category"
			   ,b.[Name] "Brand"
			   ,[N].[boxTypeName] "boxTypeName"
			   ,[N].[packTypeName] "packTypeName"
			   ,[N].[Unit] "Unit"
			   ,cost.[Price] "PlannedCost"
			   ,CAST((SELECT
						[Price] AS "@Price"
					   ,IsAction AS "@IsAction"
					   ,[StartDate] AS "@StartDate"
					FROM [DW].[FactPrices] AS t
					WHERE [PriceTypeID] = 0xBA6D90E6BA17BDD711E297052E5C534D
					AND [DocType] = 'DocumentRef.УстановкаЦенНоменклатуры'
					AND IsAction = 0
					AND t.NomenclatureID = [N].CodeInIS
					FOR XML PATH ('Price'), BINARY BASE64)
				AS XML) AS Prices
			FROM [DW].[DimNomenclatures] [N]
			LEFT JOIN [DW].[DimTypesOfNomenclature] t
				ON [N].NomenclatureType = t.[CodeInIS]
			LEFT JOIN [DW].[vwCurrentPlannedCost] cost
				ON [N].[ID] = cost.[NomenclatureID]
			LEFT JOIN [DW].[DimNomenclatureGroups] AS ng
				ON [N].[NomenclatureGroup] = ng.[CodeInIS]
			LEFT JOIN [DW].[DimBrands] AS b
				ON [N].[Brand] = b.[CodeInIS]
			WHERE
			--JSON_VALUE(Nomenclature.[Parents], '$.parents[0]') IN ('Колбасные изделия','Мясные продукты','Рыбная продукция')
			t.[GoodsForSale] = 1
			AND COALESCE([N].[Marked], 0) = 0
			AND [N].[ID] IN (SELECT
					id
				FROM @RESULT)) AS D
		FOR XML PATH ('Nomenclature')
		, ROOT ('Goods')
		, BINARY BASE64)
END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[fnGetNomenclatureList] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @Offset INT = 0
DECLARE @RowCount INT = 100
SELECT [IIS].[fnGetNomenclatureList](@Offset, @RowCount) [fnGetNomenclatureList]
