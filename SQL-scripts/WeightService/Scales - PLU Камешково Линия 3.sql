----------------------------------------------------------------------------------------------------
-- Scales - PLU Камешково Линия 3
-- Connect from PALYCH\LUTON
----------------------------------------------------------------------------------------------------
declare @selectFrom bit = 1
declare @selectTo bit = 1
declare @commit bit = 0
declare @scaleFrom nvarchar(255) = N'Разработчик TSC'
declare @scaleTo nvarchar(255) = N'Разработчик Чудо Печка'
declare @scaleFromId int = null
declare @scaleToId int = null
declare @goodsTemplate nvarchar(255) = N'Чудо печка%'
declare @pluFrom table([ID] int)
declare @updated int = null
set nocount on
----------------------------------------------------------------------------------------------------
set @scaleFromId = (select [s].[Id] from [db_scales].[Scales] [s] where [s].[Description] = @scaleFrom)
if (@scaleFromId is null)
	print N'[!] Error found ID "null" for @scaleFrom "' + @scaleFrom + N'"'
else
	print N'[+] Found ID "' + cast(@scaleFromId as nvarchar(255)) + N'" for @scaleFrom "' + @scaleFrom + N'"'
set @scaleToId = (select [s].[Id] from [db_scales].[Scales] [s] where [s].[Description] = @scaleTo)
if (@scaleToId is null)
	print N'[!] Error found ID "null" for @scaleToI "' + @scaleTo + N'"'
else
	print N'[+] Found ID "' + cast(@scaleToId as nvarchar(255)) + N'" for @scaleTo "' + @scaleTo + N'"'
----------------------------------------------------------------------------------------------------
if (@selectFrom = 1) begin
	print N'[+] Select PLU for @scaleFrom "' + @scaleFrom + N'" is enabled'
	select [S].[Description], [PLU].*
	from [db_scales].[PLU] [PLU]
	left join [db_scales].[Scales] [S] on [PLU].[ScaleId] = [S].[ID]
	where [PLU].[ScaleId] = @scaleFromId and [PLU].[GoodsName] like @goodsTemplate order by [PLU].[GoodsName]
end else begin
	print N'[-] Select PLU for @scaleFrom "' + @scaleFrom + N'" is disabled'
end
------------------------------------------------------------------------------------------------------
begin tran
insert into @pluFrom([ID]) select [ID] from [db_scales].[PLU] 
	where [ScaleId] = @scaleFromId and [GoodsName] like @goodsTemplate order by [GoodsName]
update [db_scales].[PLU] set [ScaleId] = @scaleToId where [Id] in (select [ID] from @pluFrom)
set @updated = (select count([ID]) from [db_scales].[PLU] where [Id] in (select [ID] from @pluFrom))
print N'[+] Updated "' + cast(@updated as nvarchar(255)) + N'" rows'
if (@commit = 1) begin
	print N'[+] Commit is enabled'
	commit tran
end else begin
	print N'[-] Commit is disabled'
	rollback tran
end
----------------------------------------------------------------------------------------------------
if (@selectTo = 1) begin
	print N'[+] Select PLU for @scaleTo "' + @scaleTo + N'" is enabled'
	select [S].[Description], [PLU].*
	from [db_scales].[PLU] [PLU]
	left join [db_scales].[Scales] [S] on [PLU].[ScaleId] = [S].[ID]
	where [PLU].[ScaleId] = @scaleToId and [PLU].[GoodsName] like @goodsTemplate order by [PLU].[GoodsName]
end else begin
	print N'[-] Select PLU for @scaleTo "' + @scaleTo + N'" is disabled'
end
------------------------------------------------------------------------------------------------------
set nocount off
