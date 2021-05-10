-- Диаграмма. Контрагенты
-- Создано
select
		cast([CreateDate] as date) [CreatedDate]
	,null [ModifiedDate]
	,count(*) [Count]
from [db_scales].[Contragents]
group by cast([CreateDate] as date)
order by [CreatedDate] asc
-- Изменено
select
	 null [CreatedDate]
	,cast([ModifiedDate] as date) [ModifiedDate]
	,count(*) [Count]
from [db_scales].[Contragents]
group by cast([ModifiedDate] as date)
