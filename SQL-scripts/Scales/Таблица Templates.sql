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
	 [db_scales].[Templates].[Id]
	,[db_scales].[Templates].[Marked]
	,[db_scales].[Templates].[CreateDate]
	,[db_scales].[Templates].[ModifiedDate]
	,[db_scales].[Templates].[CategoryID]
	,[db_scales].[Templates].[Title]
	,[db_scales].[Templates].[ImageData]
	--,cast([db_scales].[Templates].[ImageData] as varchar(max)) [ImageStr]
	,convert (nvarchar(max), [ImageData]) [ImageStr]
from [db_scales].[Templates]
order by [db_scales].[Templates].[Id]
set nocount off
