------------------------------------------------------------------------------------------------------------------------
-- Table Select Labels
------------------------------------------------------------------------------------------------------------------------
SELECT
	[L].[ID]
   ,[L].[CREATEDATE]
   ,[S].[DESCRIPTION]
   ,[WF].[PLUID]
   ,[WF].[SCALEID]
   ,[WF].[WEITHINGDATE]
   ,[WF].[NETWEIGHT]
   ,[WF].[TAREWEIGHT]
   ,[WF].[PRODUCTDATE]
   ,[WF].[REGNUM]
   ,[WF].[KNEADING]
   ,[L].[ZPL]
   ,REPLACE(REPLACE([L].[ZPL], CHAR(13), ''), CHAR(10), '') [ZPL_STR]
FROM [DB_SCALES].[LABELS] [L]
LEFT JOIN [DB_SCALES].[WEITHINGFACT] [WF] ON [L].[WEITHINGFACTID] = [WF].[ID]
LEFT JOIN [DB_SCALES].[SCALES] [S] ON [WF].[SCALEID] = [S].[ID]
ORDER BY [CREATEDATE] DESC
------------------------------------------------------------------------------------------------------------------------
