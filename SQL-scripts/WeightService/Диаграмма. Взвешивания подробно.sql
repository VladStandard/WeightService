-- Диаграмма. Взвешивания подробно
select cast([wf].[WeithingDate] as date) [WeithingDate]
	, [s].[Description] [Scale], [h].[Name] [Host], [p].[Name] [Printer]
	,[wf].*
from [db_scales].[WeithingFact] [wf]
left join [db_scales].[Scales] [s] on [wf].[ScaleId] = [s].[Id]
left join [db_scales].[Hosts] [h] on [s].[HostId] = [h].[Id]
left join [db_scales].[ZebraPrinter] [p] on [s].[ZebraPrinterId] = [p].[Id]
order by [wf].[WeithingDate] desc
