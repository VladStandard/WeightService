-- Table Access diagram summary
select
	 [UID]
	,[CREATE_DT]
	,[CHANGE_DT]
	,[USER]
	,[LEVEL]
from [db_scales].[ACCESS]
order by [USER] desc
