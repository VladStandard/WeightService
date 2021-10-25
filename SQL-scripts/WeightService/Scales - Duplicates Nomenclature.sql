-- Scales - Duplicates Nomenclature
SELECT
	[T1].[Id]
   ,[T1].[Name]
   ,[T1].[Code]
   ,[T2].[COUNT]
FROM [db_scales].[Nomenclature] [T1]
INNER JOIN (SELECT
		[Code]
	   ,COUNT(*) [COUNT]
	FROM [db_scales].[Nomenclature]
	GROUP BY [Code]
	HAVING COUNT(*) > 1) [T2] ON [T1].[Code] = [T2].[Code]
ORDER BY [T1].[Name]

select * from [db_scales].[Nomenclature]
where [Code] is null or [Code] = '' or  [Name] like 'Чудо печка%'