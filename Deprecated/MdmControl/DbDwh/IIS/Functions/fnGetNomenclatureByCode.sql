-- [IIS].[fnGetNomenclatureByCode]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetNomenclatureByCode]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnGetNomenclatureByCode]
(
	@code nvarchar(255)
)
RETURNS xml
AS
BEGIN
	RETURN 	(
		select * from (
		select 
			 [n].[ID]					"@ID"
			,[n].[Name]					"@Name"
			,[n].[Code]					"@Code"	
			,[n].[MasterId]				"@MasterId"
			,[n].[InformationSystemID]	"@InformationSystemID"
			,[n].[NameFull]				"FullName"
			,[n].[CreateDate]		    "CreateDate"
			,[n].[DLM]				    "DLM"
			,[ng].[Name]				"NomenclatureGroup"
			,json_value([n].[Parents], '$.parents[0]') "Category"
			,[b].[Name]					"Brand"
			,[n].[boxTypeName]			"boxTypeName"
			,[n].[packTypeName]			"packTypeName"
			,[n].[Unit]					"Unit"
			,cost.[Price]				"PlannedCost"
			,cast((select [Price] as "@Price"
				,[IsAction] as "@IsAction"
				,[StartDate] as "@StartDate"
				from [DW].[FactPrices] as [fp]
				where 
				[PriceTypeID] = 0xBA6D90E6BA17BDD711E297052E5C534D 
				and [DocType] = 'DocumentRef.УстановкаЦенНоменклатуры'
				and [IsAction] = 0
				and [fp].NomenclatureID = [n].CodeInIS
				and [fp].[Marked] = 0 and [fp].[Posted] = 1
				for xml path ('Price'), binary base64 
			) as xml) as Prices
		from [DW].[DimNomenclatures] as [n]
		left join [DW].[DimTypesOfNomenclature] t
			on [n].NomenclatureType = t.[CodeInIS] --AND Nomenclature.[InformationSystemID] = t.[InformationSystemID]
		left join [DW].[vwCurrentPlannedCost] cost
			on [n].[ID] = cost.[NomenclatureID]
		left join [DW].[DimNomenclatureGroups] as ng
			on [n].[NomenclatureGroup] = ng.[CodeInIS]
		left join [DW].[DimBrands] as b
			on [n].[Brand] = b.[CodeInIS]
		where 
			--JSON_VALUE([n].[Parents], '$.parents[0]') IN ('Колбасные изделия','Мясные продукты','Рыбная продукция')
			t.[GoodsForSale] = 1
			and coalesce([n].[Marked],0) = 0 
			and [n].[Code] = @code
		) as D
		for xml path('Nomenclature')
			,root('Goods')
			,binary base64 
		)
END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[fnGetNomenclatureByCode] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @Code NVARCHAR(255) = N'ЦБД00004307'
SELECT [IIS].[fnGetNomenclatureByCode](@Code) [fnGetNomenclatureByCode]
