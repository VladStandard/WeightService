-- Диаграмма. Взвешивания
-- Дата взвешиваний
select
	 cast([weithingdate] as date) [weithingdate]
	,count(*) [Count]
from [db_scales].[WeithingFact]
group by cast([WeithingDate] as date)
