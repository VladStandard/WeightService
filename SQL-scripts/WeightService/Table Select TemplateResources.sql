------------------------------------------------------------------------------------------------------------------------
-- Table Select TemplateResources
------------------------------------------------------------------------------------------------------------------------
declare @id int = 0
declare @delete bit = 0
declare @insert bit = 0
set nocount on
------------------------------------------------------------------------------------------------------------------------
if (@delete = 1) begin
	print N'[-] Удаление включено'
	delete from [db_scales].[TemplateResources] where [Id] = @id
end
------------------------------------------------------------------------------------------------------------------------
if (@insert = 1) begin
	print N'[-] Вставка включена'
end
------------------------------------------------------------------------------------------------------------------------
select
	 [db_scales].[TemplateResources].[Id]
	,[db_scales].[TemplateResources].[Marked]
	,[db_scales].[TemplateResources].[CreateDate]
	,[db_scales].[TemplateResources].[ModifiedDate]
	,[db_scales].[TemplateResources].[Name]
	,[db_scales].[TemplateResources].[Description]
	,[db_scales].[TemplateResources].[Type]
	,[db_scales].[TemplateResources].[ImageData]
from [db_scales].[TemplateResources]
order by [db_scales].[TemplateResources].[id]
set nocount off
------------------------------------------------------------------------------------------------------------------------
