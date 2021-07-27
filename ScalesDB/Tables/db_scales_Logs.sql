-- db_scales_LOGS
declare @drop bit = 1
declare @schema_id int = (select [schema_id] from [sys].[schemas] where [name]='db_scales')
declare @fk nvarchar(256)
declare @cmd nvarchar(max)
----------------------------------------------------------------------------------------------------
-- Drop contstraints 'LOGS'
if (@drop = 1) begin
	declare cur_drop cursor for 
		(select [fk].[name] [fk_constraint_name]
		from [sys].[foreign_keys] [fk]
		inner join [sys].[tables] [fk_tab] on [fk_tab].[object_id] = [fk].[parent_object_id]
		inner join [sys].[tables] [pk_tab] on [pk_tab].[object_id] = [fk].[referenced_object_id]
		where [fk_tab].[name]='LOGS' and [pk_tab].[name]='LOG_TYPES')
	open cur_drop
	fetch next from cur_drop into @fk
	while @@FETCH_STATUS=0 begin
		set @cmd = 'alter table [db_scales].[LOGS] drop constraint [' + @fk + ']'
		print '[ ] ' + @cmd
		exec sp_executesql @cmd
		fetch next from cur_drop into @fk
	end
	close cur_drop
	deallocate cur_drop
	print '[-] Drop contstraints LOGS - success'
end
----------------------------------------------------------------------------------------------------
-- Drop table
if (@drop = 1) begin
	if exists (select 1 from [sys].[tables] where [name]='LOGS' and [schema_id]=@schema_id) begin
		drop table [db_scales].[LOGS]
		print '[-] Drop table [db_scales].[LOGS] - success'
	end else begin 
		print '[ ] Drop table [db_scales].[LOGS] - is exists'
	end
end
----------------------------------------------------------------------------------------------------
-- Create table
begin
	if not exists (select 1 from [sys].[tables] where [name]='LOGS' and [schema_id]=@schema_id) begin
		create table [db_scales].[LOGS]
		(
			[UID] uniqueidentifier not null primary key default newid(),
			[CREATE_DT] datetime not null default getdate(),
			[HOST_ID] int null foreign key references [db_scales].[HOSTS](ID),
			[APP_UID] uniqueidentifier null foreign key references [db_scales].[APPS]([UID]),
			[VERSION] nvarchar(12) null,
			[FILE] nvarchar(32) not null,
			[LINE] int not null,
			[MEMBER] nvarchar(32) not null,
			[LOG_TYPE_UID] uniqueidentifier null,
			[MESSAGE] nvarchar(1024) not null,
		) on [ScalesFileGroup]
		print '[+] Create table [db_scales].[LOGS] - success'
	end else begin
		print '[ ] Create table [db_scales].[LOGS] - is exists'
	end
end
----------------------------------------------------------------------------------------------------
-- Alter table
begin
	alter table [db_scales].[LOGS] with check add foreign key([LOG_TYPE_UID]) references [db_scales].[LOG_TYPES] ([UID])
end
----------------------------------------------------------------------------------------------------
-- Create properties
begin
	if not exists (select 1 from [sys].[tables] where [name]='LOGS' and [schema_id]=@schema_id) begin
		exec sp_addextendedproperty @name = N'MS_Description', @value = N'Logs.', @level0type = N'SCHEMA', 
			@level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'Logs', @level2type = NULL, @level2name = NULL
		print '[+] Add properties for table [db_scales].[LOGS] - success'
	end else begin
		print '[ ] Add properties for table [db_scales].[LOGS] - is exists'
	end
end
----------------------------------------------------------------------------------------------------
go
-- Access rights
grant select on [db_scales].[LOGS] to [db_scales_users]
go
----------------------------------------------------------------------------------------------------
