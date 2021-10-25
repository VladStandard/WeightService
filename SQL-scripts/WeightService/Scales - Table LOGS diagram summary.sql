-- Scales - Table LOGS diagram summary
-- 1. Connect from PALYCH\LUTON
-- use [ScalesDB]
set nocount on
declare @count int
declare @rows int
declare @host_id int
-- 2. User settings
declare @select bit = 1
declare @delete bit = 0
declare @commit bit = 0
declare @create_dt datetime = '2021-10-25 00:00'
declare @host nvarchar(255) = N''
----------------------------------------------------------------------------------------------------
set @host_id = (select [Id] from [db_scales].[Hosts] where [Name] = @host)
set @rows = (select [p].[rows] from sys.tables [t]
  inner join sys.indexes [i] ON [t].[object_id] = [i].[object_id]
  inner join sys.partitions [p] ON [i].[object_id] = [p].[object_id] and [i].[index_id] = [p].[index_id]
where [t].[name] = 'LOGS')
print N'[v] All rows count before run script: ' + cast(@rows as nvarchar(255))
----------------------------------------------------------------------------------------------------
-- Delete & commit
if (@delete = 1) begin
	print N'[v] Enabled delete mode'
	print N'[v] Value for @create_dt: ' + cast(@create_dt as nvarchar(255))
	print N'[v] Value for @host: ' + @host
	begin tran
	set @count = (select count(1) from [db_scales].[LOGS] where [CREATE_DT] < @create_dt and [HOST_ID] = (case when @host_id > 0 then @host_id end))
	delete from [db_scales].[LOGS] where [CREATE_DT] < @create_dt and [HOST_ID] = (case when @host_id > 0 then @host_id end)
	print N'[v] Found ' + cast(@count as nvarchar(255)) + ' rows for deleted'
	if (@commit = 1) begin
		print N'[v] Enabled commit mode'
		commit tran
		print N'[v] Commit ' + cast(@count as nvarchar(255)) + ' rows for deleted'
	end else begin
		print N'[ ] Disabled commit mode'
		rollback tran
	end
end else begin
	print N'[ ] Disabled delete mode'
end
----------------------------------------------------------------------------------------------------
-- Select
if (@select = 1) begin
	print N'[v] Enabled select mode'
	select 
		 [l].[UID]
		,[l].[CREATE_DT]
		,[s].[Description] [SCALE]
		,[h].[Name] [HOST]
		,[a].[NAME] [APP]
		,[l].[VERSION]
		,[l].[FILE]
		,[l].[LINE]
		,[l].[MEMBER]
		,[lt].[ICON]
		,[l].[MESSAGE]
	from [db_scales].[LOGS] [l]
	left join [db_scales].[Hosts] [h] on [h].[ID]=[l].[HOST_ID]
	left join [db_scales].[Scales] [s] on [s].[HostId]=[h].[ID]
	left join [db_scales].[APPS] [a] on [a].[UID]=[l].[APP_UID]
	left join [db_scales].[LOG_TYPES] [lt] on [lt].[UID]=[l].[LOG_TYPE_UID]
	where [CREATE_DT] > @create_dt --and [HOST_ID] = (case when @host_id > 0 then @host_id end)
	order by [l].[CREATE_DT] desc
end else begin
	print N'[ ] Disabled select mode'
end
----------------------------------------------------------------------------------------------------
set @rows = (select [p].[rows] from sys.tables [t]
  inner join sys.indexes [i] ON [t].[object_id] = [i].[object_id]
  inner join sys.partitions [p] ON [i].[object_id] = [p].[object_id] and [i].[index_id] = [p].[index_id]
where [t].[name] = 'LOGS')
print N'[v] All rows count after run script: ' + cast(@rows as nvarchar(255))
----------------------------------------------------------------------------------------------------
set nocount off
