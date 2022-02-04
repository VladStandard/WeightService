------------------------------------------------------------------------------------------------------------------------
-- Table Select WeithingFact 3
------------------------------------------------------------------------------------------------------------------------
SELECT
	CAST([WF].[WEITHINGDATE] AS DATE) [WEITHINGDATE]
   ,COUNT(*) [COUNT]
   ,[S].[DESCRIPTION] [SCALE]
   ,[H].[NAME] [HOST]
   ,[P].[NAME] [PRINTER]
FROM [DB_SCALES].[WEITHINGFACT] [WF]
LEFT JOIN [DB_SCALES].[SCALES] [S] ON [WF].[SCALEID] = [S].[ID]
LEFT JOIN [DB_SCALES].[HOSTS] [H] ON [S].[HOSTID] = [H].[ID]
LEFT JOIN [DB_SCALES].[ZEBRAPRINTER] [P] ON [S].[ZEBRAPRINTERID] = [P].[ID]
GROUP BY CAST([WEITHINGDATE] AS DATE)
		,[S].[DESCRIPTION]
		,[H].[NAME]
		,[P].[NAME]
ORDER BY [WEITHINGDATE] DESC
------------------------------------------------------------------------------------------------------------------------
