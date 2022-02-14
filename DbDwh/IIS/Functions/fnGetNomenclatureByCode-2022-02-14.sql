-- [IIS].[fnGetNomenclatureByCode]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetNomenclatureByCode]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnGetNomenclatureByCode] (@code NVARCHAR(255)) RETURNS XML
AS BEGIN
	RETURN (SELECT * FROM (SELECT 
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
		,cost.[Price]				"PlannedCost"
		,cast((select [Price] as "@Price"
			,[IsAction] as "@IsAction"
			,[StartDate] as "@StartDate"
			from [DW].[FactPrices] as [fp]
			where 
			[PriceTypeID] = 0xBA6D90E6BA17BDD711E297052E5C534D 
			and [DocType] = 'DocumentRef.УстановкаЦенНоменклатуры'
			and [IsAction] = 0
			and [fp].NomenclatureID = [N].CodeInIS
			and [fp].[Marked] = 0 and [fp].[Posted] = 1
			for xml path ('Price'), binary base64 
		) as xml) as Prices
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
	) as D
	FOR XML PATH ('Nomenclature'), ROOT ('Goods'), BINARY BASE64)
END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[fnGetNomenclatureByCode] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @Code NVARCHAR(255) = N'ЦБД00004307'
SELECT [IIS].[fnGetNomenclatureByCode](@Code) [fnGetNomenclatureByCode]
