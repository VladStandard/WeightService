-- [IIS].[fnGetNomenclatureByCodePreview]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetNomenclatureByCode_Preview]
DROP FUNCTION IF EXISTS [IIS].[fnGetNomenclatureByCodePreview]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnGetNomenclatureByCodePreview] (@code NVARCHAR(255)) RETURNS XML
AS BEGIN
	-- DECLARE.
	DECLARE @xml XML = (
		SELECT * FROM (
			SELECT
			 [N].[ID]					"@ID"
			,[N].[Name]					"@Name"
			,[N].[Code]					"@Code"	
			,[N].[MasterId]				"@MasterId"
			,[N].[InformationSystemID]	"@InformationSystemID"
			,[DW].[fnGetGuid1Cv2] ([N].[CodeInIS]) [@GUID_1C]
			,[N].[NameFull]				"FullName"
			,[N].[CreateDate]		    "CreateDate"
			,[N].[DLM]				    "DLM"
			,[ng].[Name] "NomenclatureGroup"
			,json_value([N].[Parents], '$.parents[0]') "Category"
			,[b].[Name]					"Brand"
			,[N].[boxTypeName]			"boxTypeName"
			,[N].[packTypeName]			"packTypeName"
			,[N].[Unit]					"Unit"
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
			,cast((select [Price] as "@Price"
				,[IsAction] as "@IsAction"
				,[StartDate] as "@StartDate"
				from [DW].[FactPrices] as [fp]
				WHERE [PriceTypeID] = 0xBA6D90E6BA17BDD711E297052E5C534D -- Оптовые
					AND [DocType] = 'DocumentRef.УстановкаЦенНоменклатуры' AND [IsAction] = 0
					AND [fp].[NomenclatureID] = [N].[CodeInIS]
					AND [fp].[Marked] = 0 AND [fp].[Posted] = 1
				ORDER BY [StartDate]
				FOR XML PATH ('Price'), BINARY BASE64)
			AS XML) [Prices]
		from [DW].[DimNomenclatures] as [N]
		left join [DW].[DimTypesOfNomenclature] t
			on [N].NomenclatureType = t.[CodeInIS] --AND Nomenclature.[InformationSystemID] = t.[InformationSystemID]
		left join [DW].[vwCurrentPlannedCost] cost
			on [N].[ID] = cost.[NomenclatureID]
		left join [DW].[DimNomenclatureGroups] as ng
			on [N].[NomenclatureGroup] = ng.[CodeInIS]
		left join [DW].[DimBrands] as b
			on [N].[Brand] = b.[CodeInIS]
		where 
			--JSON_VALUE([n].[Parents], '$.parents[0]') IN ('Колбасные изделия','Мясные продукты','Рыбная продукция')
			t.[GoodsForSale] = 1
			and coalesce([N].[Marked],0) = 0 
			and [N].[Code] = @code
		) [DATA] FOR XML PATH ('Nomenclature'), BINARY BASE64)
	-- ATTRIBUTES.
	DECLARE @info XML = (SELECT '' FOR XML PATH(''), ROOT('Information'), BINARY BASE64)
	IF (@xml IS NULL) BEGIN
		SET @xml = (SELECT '' FOR XML PATH(''), ROOT('Response'), BINARY BASE64)
	END ELSE BEGIN
		DECLARE @Version NVARCHAR(1024) = 'v.0.6.140'
		DECLARE @Api NVARCHAR(1024) = '/api/nomenclature/?code=' + @code
		DECLARE @ApiPreview NVARCHAR(1024) = '/api/nomenclature_preview/?code=' + @code
		SET @info.modify ('insert attribute Version{sql:variable("@Version")} into (/Information)[1] ')
		SET @info.modify ('insert attribute Api{sql:variable("@Api")} into (/Information)[1] ')
		SET @info.modify ('insert attribute ApiPreview{sql:variable("@ApiPreview")} into (/Information)[1] ')
	END
	-- RESULT.
	RETURN (SELECT @info, @xml FOR XML PATH (''), ROOT('Goods'), BINARY BASE64)
END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[fnGetNomenclatureByCodePreview] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @Code NVARCHAR(255) = N'ЦБД00018851'
SELECT [IIS].[fnGetNomenclatureByCodePreview](@Code) [fnGetNomenclatureByCodePreview]
