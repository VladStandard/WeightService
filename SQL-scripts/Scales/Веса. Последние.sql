-- Веса. Последние.
-- Версия 0.01.01.
select top 100 *
from [db_scales].[WeithingFact]
order by [db_scales].[WeithingFact].[WeithingDate] desc
