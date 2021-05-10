-- Диаграмма. Устройства
-- Создано
select
	 cast([CreateDate] as date) [CreateDate]
	,count(*) [Count]
from [db_scales].[Scales]
group by cast([CreateDate] as date)
-- Изменено
select
	 cast([ModifyDate] as date) [ModifyDate]
	,count(*) [Count]
from [db_scales].[Scales]
group by cast([ModifyDate] as date)
