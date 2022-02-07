-- [IIS].[fnGetNomenclatureChangesListPreview]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetNomenclatureChangesListPreview]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnGetNomenclatureChangesListPreview] (@StartDate DATETIME, @EndDate DATETIME = NULL, @Offset INT = 0, @RowCount INT = 10) RETURNS XML
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
				-- Раздел <PlannedCost>
				,[cost].[Price] [PlannedCost]
				,(SELECT [FPC].[DLM] FROM [DW].[FactPlannedCost] [FPC] WHERE [FPC].[ID]=[cost].[ID]) [PlannedCostDlm]
				-- Стоимость
				,CAST(
					(SELECT * FROM (
						(SELECT TOP 1 'Week' [@PriceType], [Price] [@Price], [FP].[StartDate] [@StartDate], [FP].[EndDate] [@EndDate], [FP].[DLM] [@DLM]
							FROM [DW].[FactPrices] [FP]
							WHERE [FP].[_NomenclatureID]=[N].[ID]
							AND [FP].[PriceTypeID]=0x9FD1001A4D872B0E11E085DB174FF51D -- Плановая себестоимость => недельная с/с
							ORDER BY [FP].[DateID] DESC
						UNION
						 SELECT TOP 1 'Month' [@PriceType], [Price] [@Price], [FP].[StartDate] [@StartDate], [FP].[EndDate] [@EndDate], [FP].[DLM] [@DLM]
							FROM [DW].[FactPrices] [FP]
							WHERE [FP].[_NomenclatureID]=[N].[ID]
							AND [FP].[PriceTypeID]=0x80DFA4BF01016D5011E7E6E13ECE5C99 -- яПлановая с/с для Ф.О. => месячная с/с
							ORDER BY [FP].[DateID] DESC
					)) [Cost]
					FOR XML PATH ('Cost'), BINARY BASE64)
				AS XML) [Costs]
				-- Раздел <Prices>
				,CAST((SELECT
						 [Price] [@Price]
						,[IsAction] [@IsAction]
						,[StartDate] [@StartDate]
					FROM [DW].[FactPrices] AS [fp]
					WHERE [PriceTypeID] = 0xBA6D90E6BA17BDD711E297052E5C534D -- Оптовые
						AND [DocType] = 'DocumentRef.УстановкаЦенНоменклатуры' AND [IsAction] = 0
						AND [fp].[NomenclatureID] = [N].[CodeInIS]
						AND [fp].[Marked] = 0 AND [fp].[Posted] = 1
					ORDER BY [StartDate]
					FOR XML PATH ('Price'), BINARY BASE64)
				AS XML) [Prices]
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
GRANT EXECUTE ON [IIS].[fnGetNomenclatureChangesListPreview] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @StartDate DATETIME = '2022-02-07T12:00:00'
DECLARE @EndDate DATETIME = '2022-02-07T16:00:00'
DECLARE @Offset INT = 0
DECLARE @RowCount INT = 1000
SELECT [IIS].[fnGetNomenclatureChangesListPreview] (@StartDate, @EndDate, @Offset, @RowCount) [fnGetNomenclatureChangesListPreview]
