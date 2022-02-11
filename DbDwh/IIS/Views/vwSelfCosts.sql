-- [DW].[vwSelfCosts]

-- DROP VIEW
DROP VIEW IF EXISTS [DW].[vwSelfCosts]
GO

-- CREATE 
CREATE VIEW [DW].[vwSelfCosts] AS
(
	SELECT TOP 1 WITH TIES
		 [FP].[PriceTypeID]
		,CASE WHEN [FP].[PriceTypeID]=0x9FD1001A4D872B0E11E085DB174FF51D THEN 'Month' ELSE '' END [PriceType]
		,[FP].[DateID]
		,[DN].[ID] [NomenclatureID]
		,[DN].[Name] [Nomenclature]
		,[Price]
		,[FP].[StartDate]
		--,[FP].[EndDate] -- ANYTHERE IS NULL
		,[FP].[DLM]
	FROM [DW].[FactPrices] [FP]
	LEFT JOIN [DW].[DimNomenclatures] [DN] ON [DN].[ID]=[FP].[_NomenclatureID]
		AND [FP].[PriceTypeID]=0x9FD1001A4D872B0E11E085DB174FF51D -- Плановая себестоимость => месячная с/с
	WHERE [FP].[PriceTypeID]=0x9FD1001A4D872B0E11E085DB174FF51D 
		AND ([DN].[ID] IS NOT NULL AND [DN].[Name] IS NOT NULL)
	ORDER BY ROW_NUMBER() OVER(PARTITION BY [DN].[ID] ORDER BY [FP].[DateID] DESC)
	UNION
	SELECT TOP 1 WITH TIES
		 [FP].[PriceTypeID]
		,CASE WHEN [FP].[PriceTypeID]=0x80DFA4BF01016D5011E7E6E13ECE5C99 THEN 'Week' ELSE '' END [PriceType]
		,[FP].[DateID]
		,[DN].[ID] [NomenclatureID]
		,[DN].[Name] [Nomenclature]
		,[Price]
		,[FP].[StartDate]
		--,[FP].[EndDate] -- ANYTHERE IS NULL
		,[FP].[DLM]
	FROM [DW].[FactPrices] [FP]
	LEFT JOIN [DW].[DimNomenclatures] [DN] ON [DN].[ID]=[FP].[_NomenclatureID]
		AND [FP].[PriceTypeID]=0x80DFA4BF01016D5011E7E6E13ECE5C99 -- яПлановая с/с для Ф.О. => недельная с/с
	WHERE [FP].[PriceTypeID]=0x80DFA4BF01016D5011E7E6E13ECE5C99
		AND ([DN].[ID] IS NOT NULL AND [DN].[Name] IS NOT NULL)
	ORDER BY ROW_NUMBER() OVER(PARTITION BY [DN].[ID] ORDER BY [FP].[DateID] DESC)
)
GO

-- COUNT UNIQUES
SELECT COUNT(DISTINCT [NomenclatureID]) [COUNT_UNIQUES] FROM [DW].[vwSelfCosts]
-- GET ALL
DECLARE @NomenclatureId INT = -2147460739
SELECT *
FROM [DW].[vwSelfCosts]
WHERE [NomenclatureID]=@NomenclatureId
ORDER BY [PriceType] ASC, [DateID] DESC, [NomenclatureID] ASC
-- FIND DUPLICATES
SELECT [PriceType], [NomenclatureID], [Nomenclature], COUNT(*) [COUNT]
FROM [DW].[vwSelfCosts]
GROUP BY [PriceType], [NomenclatureID], [Nomenclature]
HAVING COUNT(*) > 1
