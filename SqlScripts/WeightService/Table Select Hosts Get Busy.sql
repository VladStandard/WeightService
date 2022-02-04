------------------------------------------------------------------------------------------------------------------------
-- Table Select Hosts Get Busy
------------------------------------------------------------------------------------------------------------------------
SELECT
	[H].[Id]
   ,[H].[CreateDate]
   ,[H].[ModifiedDate]
   ,[H].[ACCESS_DT]
   ,[H].[Name]
   ,[S].[Id] [SCALE_ID]
   ,[S].[DESCRIPTION] [SCALE_DESCRIPTION]
   ,[H].[IP]
   ,[H].[MAC]
   ,[H].[IdRRef]
   ,[H].[Marked]
   ,[H].[SettingsFile]
FROM [db_scales].[Hosts] [H]
LEFT JOIN [db_scales].[Scales] [S] ON [H].[Id] = [S].[HOSTID]
WHERE [H].[Id] IN (SELECT [HOSTID]
	FROM [db_scales].[Scales]
	WHERE [Scales].[HOSTID] IS NOT NULL)
ORDER BY [H].[Name]
------------------------------------------------------------------------------------------------------------------------
