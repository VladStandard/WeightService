-- [IIS].[fnGetNomenclatureList]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetNomenclatureList]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnGetNomenclatureList] (@StartDate DATETIME, @EndDate DATETIME = NULL, @Offset INT = 0, @RowCount INT = 10) RETURNS XML
AS
BEGIN
	-- DECLARE.
	DECLARE @xml XML = '<Response />'
	DECLARE @check NVARCHAR(1024) = NULL
	DECLARE @check_xml XML = NULL
	DECLARE @ResultCount INT = 0
	SET @EndDate = ISNULL(@EndDate, GETDATE())
	-- CHECKS.
	SET @check = (select [dbo].[fnCheckDates] (@StartDate, @EndDate))
	IF (@check IS NULL) BEGIN
		SET @check = (select [dbo].[fnCheckRowCount] (@RowCount))
	END 
	IF (@check IS NOT NULL) BEGIN
		SET @check_xml = (SELECT [dbo].[fnGetXmlMessage] (NULL, 'Error', 'Description', @check))
		SET @xml.modify('insert sql:variable("@check_xml") as first into (/Response)[1]')
	END
	ELSE BEGIN
		DECLARE @RESULT TABLE ([ID] INT)
		INSERT INTO @RESULT ([ID])
			SELECT
				[Nomenclature].[ID]
			FROM [DW].[DimNomenclatures] AS [Nomenclature]
			LEFT JOIN [DW].[DimTypesOfNomenclature] [t]
				ON [Nomenclature].NomenclatureType = t.[CodeInIS]
			WHERE t.[GoodsForSale] = 1
			AND COALESCE([Nomenclature].[Marked], 0) = 0
			AND (([Nomenclature].[DLM] >= @StartDate) OR (@StartDate IS NULL))
			AND (([Nomenclature].[DLM] < @EndDate) OR (@EndDate IS NULL))
			ORDER BY [Nomenclature].[ID] OFFSET @Offset ROWS FETCH NEXT @RowCount ROWS ONLY
		RETURN (SELECT * FROM (SELECT
				 [Nomenclature].[ID] "@ID"
				,[Nomenclature].[Name] "@Name"
				,[Nomenclature].[Code] "@Code"
				,[Nomenclature].[MasterId] "@MasterId"
				,[Nomenclature].[InformationSystemID] "@InformationSystemID"
				,[DW].[fnGetGuid1Cv2] ([Nomenclature].[CodeInIS]) [@GUID_1C]
				,[Nomenclature].[NameFull] "FullName"
				,[Nomenclature].[CreateDate] "CreateDate"
				,[Nomenclature].[DLM] "DLM"
				,ng.[Name] "NomenclatureGroup"
				,JSON_VALUE([Nomenclature].[Parents], '$.parents[0]') "Category"
				,b.[Name] "Brand"
				,[Nomenclature].[boxTypeName] "boxTypeName"
				,[Nomenclature].[packTypeName] "packTypeName"
				,[Nomenclature].[Unit] "Unit"
				,cost.[Price] "PlannedCost"
				,CAST((SELECT
						[Price] AS "@Price"
						,IsAction AS "@IsAction"
						,[StartDate] AS "@StartDate"
					FROM [DW].[FactPrices] AS t
					WHERE [PriceTypeID] = 0xBA6D90E6BA17BDD711E297052E5C534D
					AND [DocType] = 'DocumentRef.УстановкаЦенНоменклатуры'
					AND IsAction = 0
					AND t.NomenclatureID = [Nomenclature].CodeInIS
					AND [Marked] = 0 AND [Posted] = 1
					FOR XML PATH ('Price'), BINARY BASE64)
				AS XML) AS Prices
			FROM [DW].[DimNomenclatures] [Nomenclature]
			LEFT JOIN [DW].[DimTypesOfNomenclature] t
				ON [Nomenclature].NomenclatureType = t.[CodeInIS]
			LEFT JOIN [DW].[vwCurrentPlannedCost] cost
				ON [Nomenclature].[ID] = cost.[NomenclatureID]
			LEFT JOIN [DW].[DimNomenclatureGroups] AS ng
				ON [Nomenclature].[NomenclatureGroup] = ng.[CodeInIS]
			LEFT JOIN [DW].[DimBrands] AS b
				ON [Nomenclature].[Brand] = b.[CodeInIS]
			WHERE
			--JSON_VALUE(Nomenclature.[Parents], '$.parents[0]') IN ('Колбасные изделия','Мясные продукты','Рыбная продукция')
			t.[GoodsForSale] = 1
			AND COALESCE([Nomenclature].[Marked], 0) = 0
			AND [Nomenclature].[ID] IN (SELECT id FROM @RESULT)) AS D
		FOR XML PATH ('Nomenclature'), ROOT ('Goods'), BINARY BASE64)
	END
	-- ATTRIBUTES.
	IF (@xml IS NULL) BEGIN
		SET @xml = '<Response />'
	END
	SET @xml.modify ('insert attribute StartDate{sql:variable("@StartDate")} into (/Response)[1] ')
	SET @xml.modify ('insert attribute EndDate{sql:variable("@EndDate")} into (/Response)[1] ')
	SET @xml.modify ('insert attribute Offset{sql:variable("@Offset")} into (/Response)[1] ')
	SET @xml.modify ('insert attribute RowCount{sql:variable("@RowCount")} into (/Response)[1] ')
	SET @xml.modify ('insert attribute ResultCount{sql:variable("@ResultCount")} into (/Response)[1] ')
	-- RESULT.
	RETURN @xml
END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[fnGetNomenclatureList] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @StartDate DATETIME = '2021-01-01T00:00:00'
DECLARE @EndDate DATETIME = '2021-12-30T00:00:00'
DECLARE @Offset INT = 0
DECLARE @RowCount INT = 10
SELECT [IIS].[fnGetNomenclatureList] (@StartDate, @EndDate, @Offset, @RowCount) [fnGetNomenclatureList]
