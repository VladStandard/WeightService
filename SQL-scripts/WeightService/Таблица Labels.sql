-- Таблица Labels
select top 1000 *, convert(nvarchar(max),[Label],0) [Label_Str]
from [db_scales].[Labels]
order by [CreateDate] desc
