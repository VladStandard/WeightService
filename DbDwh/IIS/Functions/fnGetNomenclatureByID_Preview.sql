-- [IIS].[fnGetNomenclatureByID_Preview]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetNomenclatureByID_Preview]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnGetNomenclatureByID_Preview] (@ID BIGINT) RETURNS XML
AS BEGIN
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
		,[ng].[Name] "NomenclatureGroup"
		,JSON_VALUE([N].[Parents], '$.parents[0]') "Category"
		,[b].[Name] "Brand"
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
			FROM [DW].[FactPrices] [fp]
			WHERE [PriceTypeID] = 0xBA6D90E6BA17BDD711E297052E5C534D -- Оптовые
				AND [DocType] = 'DocumentRef.УстановкаЦенНоменклатуры' AND [IsAction] = 0
				AND [fp].[NomenclatureID] = [N].[CodeInIS]
				AND [fp].[Marked] = 0 AND [fp].[Posted] = 1
			ORDER BY [StartDate]
			FOR XML PATH ('Price'), BINARY BASE64)
		AS XML) [Prices]
	FROM [DW].[DimNomenclatures] AS [N]
	LEFT JOIN [DW].[DimTypesOfNomenclature] t
		ON [N].NomenclatureType = t.[CodeInIS] --AND Nomenclature.[InformationSystemID] = t.[InformationSystemID]
	LEFT JOIN [DW].[vwCurrentPlannedCost] [cost] ON [N].[ID] = cost.[NomenclatureID]
	LEFT JOIN [DW].[DimNomenclatureGroups] AS ng
		ON [N].[NomenclatureGroup] = ng.[CodeInIS]
	LEFT JOIN [DW].[DimBrands] AS b
		ON [N].[Brand] = b.[CodeInIS]
	WHERE
	--JSON_VALUE([n].[Parents], '$.parents[0]') IN ('Колбасные изделия','Мясные продукты','Рыбная продукция')
	t.[GoodsForSale] = 1
	AND COALESCE([N].[Marked], 0) = 0
	AND [N].[ID] = @ID) AS D
	FOR XML PATH ('Nomenclature'), ROOT ('Goods'), BINARY BASE64)
END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[fnGetNomenclatureByID_Preview] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @ID BIGINT=-2147460739
SELECT [IIS].[fnGetNomenclatureByID_Preview](@ID) [fnGetNomenclatureByID_Preview]
