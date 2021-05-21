-- Таблица Templates
use [ScalesDB]
declare @id int = 0
declare @delete bit = 0
set nocount on
------------------------------------------------------------------------------------------------------------------------
if (@delete = 1) begin
	print N'[-] Удаление включено'
	delete from [db_scales].[Templates] where [Id] = @id
end
------------------------------------------------------------------------------------------------------------------------
select
	 [Id]
	,[CategoryID]
	,[IdRRef]
	,[Title]
	,[ImageData]
	,convert (nvarchar(max), [ImageData]) [ImageStr]
	,[CreateDate]
	,[ModifiedDate]
	,[Marked]
from [db_scales].[Templates]
order by [db_scales].[Templates].[Id]
------------------------------------------------------------------------------------------------------------------------
set nocount off
