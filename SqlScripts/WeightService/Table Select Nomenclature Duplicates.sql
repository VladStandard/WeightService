------------------------------------------------------------------------------------------------------------------------
-- Table Select Nomenclature Duplicates
------------------------------------------------------------------------------------------------------------------------
SELECT
	[T1].[ID]
   ,[T1].[NAME]
   ,[T1].[CODE]
   ,[T2].[COUNT]
FROM [DB_SCALES].[NOMENCLATURE] [T1]
INNER JOIN (SELECT
		[CODE]
	   ,COUNT(*) [COUNT]
	FROM [DB_SCALES].[NOMENCLATURE]
	GROUP BY [CODE]
	HAVING COUNT(*) > 1) [T2] ON [T1].[CODE] = [T2].[CODE]
ORDER BY [T1].[NAME]
------------------------------------------------------------------------------------------------------------------------
SELECT [Id]
	  ,[Code]
	  ,[Name]
	  ,[IdRRef]
	  ,[SerializedRepresentationObject]
	  ,[CreateDate]
	  ,[ModifiedDate]
	  ,[Weighted]
FROM [DB_SCALES].[NOMENCLATURE]
WHERE [CODE] IS NULL OR [CODE] = '' OR [NAME] LIKE 'всдн оевйю%'
------------------------------------------------------------------------------------------------------------------------
