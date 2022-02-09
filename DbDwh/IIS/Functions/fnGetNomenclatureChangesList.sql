-- [IIS].[fnGetNomenclatureChangesList]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetNomenclatureChangesList]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnGetNomenclatureChangesList] (@StartDate DATETIME, @EndDate DATETIME = NULL, @Offset INT = 0, @RowCount INT = 10) RETURNS XML
AS BEGIN
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
				[N].[ID]
			FROM [DW].[DimNomenclatures] AS [N]
			LEFT JOIN [DW].[DimTypesOfNomenclature] AS [T] ON [N].[NomenclatureType] = [T].[CodeInIS] 
				--AND Nomenclature.[InformationSystemID] = [T].[InformationSystemID]
			WHERE [T].[GoodsForSale] = 1
			AND COALESCE([N].[Marked], 0) = 0
			AND (([N].[DLM] >= @StartDate) OR (@StartDate IS NULL))
			AND (([N].[DLM] < @EndDate) OR (@EndDate IS NULL))
			ORDER BY [N].[ID] OFFSET @Offset ROWS FETCH NEXT @RowCount ROWS ONLY
		RETURN (SELECT * FROM (SELECT
				 [N].[ID] "@ID"
				,[N].[Name] "@Name"
				,[N].[Code] "@Code"
				,[N].[MasterId] "@MasterId"
				,[N].[InformationSystemID] "@InformationSystemID"
				,[DW].[fnGetGuid1Cv2] ([N].[CodeInIS]) [@GUID_1C]
				,[N].[NameFull] "FullName"
				,[N].[CreateDate] "CreateDate"
				,[N].[DLM] "DLM"
				,[NG].[Name] "NomenclatureGroup"
				,JSON_VALUE([N].[Parents], '$.parents[0]') "Category"
				,[B].[Name] "Brand"
				,[N].[boxTypeName] "boxTypeName"
				,[N].[packTypeName] "packTypeName"
				,[N].[Unit] "Unit"
				,[COST].[Price] "PlannedCost"
				,CAST((SELECT
						[Price] AS "@Price"
						,[IsAction] AS "@IsAction"
						,[startdate] AS "@StartDate"
					FROM [DW].[FactPrices] AS [T]
					WHERE [PriceTypeID] = 0xBA6D90E6BA17BDD711E297052E5C534D
					AND [DocType] = 'DocumentRef.УстановкаЦенНоменклатуры'
					AND [IsAction] = 0
					AND [T].NomenclatureId = [N].CodeInIS
					AND [T].[Marked] = 0
					AND [T].[Posted] = 1
					FOR XML PATH ('Price'), BINARY BASE64)
				AS XML) AS Prices
			FROM [DW].[DimNomenclatures] AS [N]
			LEFT JOIN [DW].[DimTypesOfNomenclature] [T] ON [N].NomenclatureType = [T].[CodeInIS] --AND [n].[InformationSystemID] = [T].[InformationSystemID]
			LEFT JOIN [DW].[vwCurrentPlannedCost] AS [COST] ON [N].[ID] = [COST].[NomenclatureId]
			LEFT JOIN [DW].[DimNomenclatureGroups] AS [NG] ON [N].[NomenclatureGroup] = [NG].[CodeInIS]
			LEFT JOIN [DW].[DimBrands] AS [B] ON [N].[Brand] = [B].[CodeInIS]
			WHERE
			--JSON_VALUE([n].[Parents], '$.parents[0]') IN ('Колбасные изделия','Мясные продукты','Рыбная продукция')
			[T].[GoodsForSale] = 1
			AND COALESCE([N].[Marked], 0) = 0
			AND [N].[ID] IN (SELECT id FROM @RESULT)) AS D
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
GRANT EXECUTE ON [IIS].[fnGetNomenclatureChangesList] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @StartDate DATETIME = '2022-02-09T00:00:00'
DECLARE @EndDate DATETIME = '2022-02-10T00:00:00'
DECLARE @Offset INT = 0
DECLARE @RowCount INT = 10
SELECT [IIS].[fnGetNomenclatureChangesList] (@StartDate, @EndDate, @Offset, @RowCount) [fnGetNomenclatureChangesList]
