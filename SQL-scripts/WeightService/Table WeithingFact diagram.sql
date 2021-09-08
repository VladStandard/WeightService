-- Table WeithingFact diagram summary
SELECT
	CAST([wf].[WeithingDate] AS DATE) [WeithingDate]
   ,[s].[Description] [Scale]
   ,[h].[Name] [Host]
   ,[p].[Name] [Printer]
   ,[wf].*
FROM [db_scales].[WeithingFact] [wf]
LEFT JOIN [db_scales].[Scales] [s]
	ON [wf].[ScaleId] = [s].[Id]
LEFT JOIN [db_scales].[Hosts] [h]
	ON [s].[HostId] = [h].[Id]
LEFT JOIN [db_scales].[ZebraPrinter] [p]
	ON [s].[ZebraPrinterId] = [p].[Id]
ORDER BY [wf].[WeithingDate] DESC
