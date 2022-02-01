------------------------------------------------------------------------------------------------------------------------
-- Table Select TemplateResourceRef
------------------------------------------------------------------------------------------------------------------------
declare @id int = 0
declare @delete bit = 0
set nocount on
------------------------------------------------------------------------------------------------------------------------
if (@delete = 1) begin
	print N'[-] Удаление включено'
	delete from [db_scales].[TemplateResourceRef] where [Id] = @id
end
------------------------------------------------------------------------------------------------------------------------
select
	 [Ref].[Id]
	,[Ref].[CreateDate]
	,[Ref].[ModifiedDate]
	,[Ref].[TemplateID]
	,[Tem].[Title] [Tem_Title]
	,[Ref].[ResourceID]
	,[Res].[Name] [Res_Name]
	,[Ref].[Description]
from [db_scales].[TemplateResourceRef] [Ref]
left join [db_scales].[TemplateResources] [Res] on [Ref].[ResourceID]=[Res].[Id]
left join [db_scales].[Templates] [Tem] on [Ref].[TemplateID]=[Tem].[Id]
order by [Ref].[id]
set nocount off
------------------------------------------------------------------------------------------------------------------------
