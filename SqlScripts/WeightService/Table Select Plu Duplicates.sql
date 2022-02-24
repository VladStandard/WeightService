------------------------------------------------------------------------------------------------------------------------
-- Table Select Plu Duplicates
------------------------------------------------------------------------------------------------------------------------
SELECT
	 [H].[Name] [Host]
	,[S].[Description] [Scale]
	,[T1].[ID]
	,[T1].[ScaleId]
	,[T2].[count]
	,[N].[Id]
	,[N].[Name]
	,[N].[Code]
FROM [DB_SCALES].[PLU] [T1]
INNER JOIN (SELECT
		[ScaleId]
	   ,COUNT(*) [COUNT]
	FROM [DB_SCALES].[PLU]
	GROUP BY [ScaleId]
	HAVING COUNT(*) > 1) [T2] ON [T1].[ScaleId] = [T2].[ScaleId]
LEFT JOIN [db_scales].[Nomenclature] [N] ON [T1].[NomenclatureId] = [N].[Id]
LEFT JOIN [db_scales].[Scales] [S] ON [T1].[ScaleId] = [S].[Id]
LEFT JOIN [db_scales].[Hosts] [H] ON [S].[HostId] = [H].[Id]
ORDER BY [T1].[ScaleId], [N].[Id]
------------------------------------------------------------------------------------------------------------------------
