-- Scales - Access diagram summary
SELECT
	[UID]
   ,[CREATE_DT]
   ,[CHANGE_DT]
   ,[user]
   ,[LEVEL]
FROM [db_scales].[ACCESS]
ORDER BY [user] DESC
