-- Таблица ProductionFacility
use [ScalesDB]
declare @id int = 0
declare @delete bit = 0
set nocount on
------------------------------------------------------------------------------------------------------------------------
if (@delete = 1) begin
	print N'[-] Удаление включено'
	delete from [db_scales].[ProductionFacility] where [Id] = @id
end
------------------------------------------------------------------------------------------------------------------------
select
	 [Id]
	,[Name]
	,[CreateDate]
	,[ModifiedDate]
	,[IdRRef]
	,[Marked]
from [db_scales].[ProductionFacility]
order by [Id]
set nocount off
