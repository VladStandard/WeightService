-- [IIS].[fnGetNomenclatureChangesList]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetNomenclatureChangesList]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnGetNomenclatureChangesList] (@StartDate DATETIME = NULL, @EndDate DATETIME = NULL, @Offset INT = 0, @RowCount INT = 10)
RETURNS XML
AS
BEGIN
	DECLARE @RESULT TABLE ([ID] INT)
	INSERT INTO @RESULT ([ID])
		SELECT
			Nomenclature.[ID]
		FROM [DW].[DimNomenclatures] AS Nomenclature
		LEFT JOIN [DW].[DimTypesOfNomenclature] t
			ON Nomenclature.NomenclatureType = t.[CodeInIS] --AND Nomenclature.[InformationSystemID] = t.[InformationSystemID]
		WHERE t.[GoodsForSale] = 1
		AND COALESCE(Nomenclature.[Marked], 0) = 0
		AND ((Nomenclature.[DLM] >= @StartDate)
		OR (@StartDate IS NULL))
		AND ((Nomenclature.[DLM] < @EndDate)
		OR (@EndDate IS NULL))
		ORDER BY Nomenclature.[ID] OFFSET @Offset ROWS FETCH NEXT @RowCount ROWS ONLY;
	RETURN (SELECT *
		FROM (SELECT
				[n].[ID] "@ID"
			   ,[n].[Name] "@Name"
			   ,[n].[Code] "@Code"
			   ,[n].[MasterId] "@MasterId"
			   ,[DW].[fnGetGuid1C]([n].[CodeInIS]) "@CodeInIS"
			   ,[n].[InformationSystemID] "@InformationSystemID"
			   ,[n].[NameFull] "FullName"
			   ,[n].[CreateDate] "CreateDate"
			   ,[n].[DLM] "DLM"
			   ,ng.[Name] "NomenclatureGroup"
			   ,JSON_VALUE([n].[Parents], '$.parents[0]') "Category"
			   ,b.[Name] "Brand"
			   ,[n].[boxTypeName] "boxTypeName"
			   ,[n].[packTypeName] "packTypeName"
			   ,[n].[Unit] "Unit"
			   ,cost.[Price] "PlannedCost"
			   ,CAST((SELECT
						[Price] AS "@Price"
					   ,[IsAction] AS "@IsAction"
					   ,[startdate] AS "@StartDate"
					FROM [DW].[FactPrices] AS [t]
					WHERE [PriceTypeID] = 0xBA6D90E6BA17BDD711E297052E5C534D
					AND [DocType] = 'DocumentRef.УстановкаЦенНоменклатуры'
					AND [IsAction] = 0
					AND [t].NomenclatureId = [n].CodeInIS
					AND [t].[Marked] = 0
					AND [t].[Posted] = 1
					FOR XML PATH ('Price'), BINARY BASE64)
				AS XML) AS Prices
			FROM [DW].[DimNomenclatures] AS [n]
			LEFT JOIN [DW].[DimTypesOfNomenclature] [t]
				ON [n].NomenclatureType = t.[CodeInIS] --AND [n].[InformationSystemID] = t.[InformationSystemID]
			LEFT JOIN [DW].[vwCurrentPlannedCost] cost
				ON [n].[ID] = cost.[NomenclatureId]
			LEFT JOIN [DW].[DimNomenclatureGroups] AS ng
				ON [n].[NomenclatureGroup] = ng.[CodeInIS]
			LEFT JOIN [DW].[DimBrands] AS b
				ON [n].[Brand] = b.[CodeInIS]
			WHERE
			--JSON_VALUE([n].[Parents], '$.parents[0]') IN ('Колбасные изделия','Мясные продукты','Рыбная продукция')
			t.[GoodsForSale] = 1
			AND COALESCE([n].[Marked], 0) = 0
			AND [n].[ID] IN (SELECT
					id
				FROM @RESULT)) AS D
		FOR XML PATH ('Nomenclature')
		, ROOT ('Goods')
		, BINARY BASE64)
END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[fnGetNomenclatureChangesList] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @StartDate DATETIME = '2021-01-01T00:00:00'
DECLARE @EndDate DATETIME = '2021-07-10T00:00:00'
DECLARE @Offset INT = 0
DECLARE @RowCount INT = 100
SELECT [IIS].[fnGetNomenclatureChangesList](@StartDate, @EndDate, @Offset, @RowCount) [fnGetNomenclatureChangesList]
