-- Blazor. 1. Устройства.
use [ScalesDB]
select
	 [sc].*
	,[t1].[Title] [TemplateDefaultTitle]          -- Типовой шаблон этикетки. Заголовок
	,[t2].[Title] [TemplateSeriesTitle]           -- Шаблон сум. этикетки. Id. Заголовок
from [db_scales].[Scales] [sc]
left join [db_scales].[Templates] [t1] on [sc].[TemplateIdDefault] = [t1].[1CTemplateID]
left join [db_scales].[Templates] [t2] on [sc].[TemplateIdSeries] = [t2].[1CTemplateID]

select * from [db_scales].[ProductionFacility]
select * from [db_scales].[WorkShop]
select * from [db_scales].[Scales]
