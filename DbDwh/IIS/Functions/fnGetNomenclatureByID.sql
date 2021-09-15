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
			   ,cost.[Price] "PlannedCost"
			   ,CAST((SELECT
						[Price] AS "@Price"
					   ,[IsAction] AS "@IsAction"
					   ,[StartDate] AS "@StartDate"
					FROM [DW].[FactPrices] AS [fp]
					WHERE [PriceTypeID] = 0xBA6D90E6BA17BDD711E297052E5C534D
					AND [DocType] = 'DocumentRef.УстановкаЦенНоменклатуры'
					AND [IsAction] = 0
					AND [fp].NomenclatureID = [N].CodeInIS
					AND [fp].[Marked] = 0
					AND [fp].[Posted] = 1
					FOR XML PATH ('Price'), BINARY BASE64)
				AS XML) AS Prices
			FROM [DW].[DimNomenclatures] AS [N]
			LEFT JOIN [DW].[DimTypesOfNomenclature] t
				ON [N].NomenclatureType = t.[CodeInIS] --AND Nomenclature.[InformationSystemID] = t.[InformationSystemID]
			LEFT JOIN [DW].[vwCurrentPlannedCost] cost
				ON [N].[ID] = cost.[NomenclatureID]
			LEFT JOIN [DW].[DimNomenclatureGroups] AS ng
				ON [N].[NomenclatureGroup] = ng.[CodeInIS]
			LEFT JOIN [DW].[DimBrands] AS b
				ON [N].[Brand] = b.[CodeInIS]
			WHERE
			--JSON_VALUE([n].[Parents], '$.parents[0]') IN ('Колбасные изделия','Мясные продукты','Рыбная продукция')
			t.[GoodsForSale] = 1
			AND COALESCE([N].[Marked], 0) = 0
			AND [N].[ID] = @ID) AS D
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
