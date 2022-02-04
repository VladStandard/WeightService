-- Blazor. 2. Устройство
use [ScalesDB]
select
	 [sc].[Id]                 -- ID
	,[sc].[Description]        -- Наименование
	,[sc].[DeviceNumber]       -- Номер
	,'' [Storage]              -- Склад
	,0 [State]                 -- Статус
	,[sc].[TemplateIdDefault] [TemplateDefaultId] -- Типовой шаблон этикетки. Id
	,[t1].[Title] [TemplateDefaultTitle]          -- Типовой шаблон этикетки. Заголовок
	,[sc].[TemplateIdSeries] [TemplateSeriesId]   -- Шаблон сум. этикетки. Id
	,[t2].[Title] [TemplateSeriesTitle]           -- Шаблон сум. этикетки. Id. Заголовок
from [db_scales].[Scales] [sc]
left join [db_scales].[Templates] [t1] on [sc].[TemplateIdDefault] = [t1].[1CTemplateID]
left join [db_scales].[Templates] [t2] on [sc].[TemplateIdSeries] = [t2].[1CTemplateID]
