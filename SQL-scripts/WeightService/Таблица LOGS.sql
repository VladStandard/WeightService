-- Таблица LOGS
select [UID],[CREATE_DT],[FILE],[LINE],[MEMBER],[ICON],[MESSAGE]
from [db_scales].[LOGS]
order by [CREATE_DT] desc
