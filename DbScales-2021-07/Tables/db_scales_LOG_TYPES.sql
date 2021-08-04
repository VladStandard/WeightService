-- db_scales_LOG_TYPES
declare @drop bit = 1
declare @insert bit = 1
declare @schema_id int = (select [schema_id] from [sys].[schemas] where [name]='db_scales')
declare @fk nvarchar(256)
declare @cmd nvarchar(max)
set nocount on
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
-- Drop contstraints 'SCALES'
if (@drop = 1) begin
	declare cur_drop cursor for 
		(select [fk].[name] [fk_constraint_name]
		from [sys].[foreign_keys] [fk]
		inner join [sys].[tables] [fk_tab] on [fk_tab].[object_id] = [fk].[parent_object_id]
		inner join [sys].[tables] [pk_tab] on [pk_tab].[object_id] = [fk].[referenced_object_id]
		where [fk_tab].[name]='SCALES' and [pk_tab].[name]='LOG_TYPES')
	open cur_drop
	fetch next from cur_drop into @fk
	while @@FETCH_STATUS=0 begin
		set @cmd = 'alter table [db_scales].[SCALES] drop constraint [' + @fk + ']'
		print '[ ] ' + @cmd
		exec sp_executesql @cmd
		fetch next from cur_drop into @fk
	end
	close cur_drop
	deallocate cur_drop
	print '[-] Drop contstraints SCALES - success'
end
----------------------------------------------------------------------------------------------------
-- Drop table [LOG_TYPES]
if (@drop = 1) begin
	if exists (select 1 from [sys].[tables] where [name]='LOG_TYPES' and [schema_id]=@schema_id) begin
		drop table [db_scales].[LOG_TYPES]
		print '[-] Drop table [db_scales].[LOG_TYPES] - success'
	end else begin 
		print '[ ] Drop table [db_scales].[LOG_TYPES] - is not exists'
	end
end
----------------------------------------------------------------------------------------------------
-- Create table [LOG_TYPES]
begin
	if not exists (select 1 from [sys].[tables] where [name]='LOG_TYPES' and [schema_id]=@schema_id) begin
		create table [db_scales].[LOG_TYPES]
		(
			[UID] uniqueidentifier not null primary key default(newid()),
			[NUMBER] tinyint not null unique,
			[ICON] nvarchar(32) not null,
		) on [ScalesFileGroup]
		print '[+] Create table [db_scales].[LOG_TYPES] - success'
	end else begin
		print '[ ] Create table [db_scales].[LOG_TYPES] - is exists'
	end
end
----------------------------------------------------------------------------------------------------
-- Create contstraints
begin
	if not exists (select [fk].[name] [fk_constraint_name]
		from [sys].[foreign_keys] [fk]
		inner join [sys].[tables] [fk_tab] on [fk_tab].[object_id] = [fk].[parent_object_id]
		inner join [sys].[tables] [pk_tab] on [pk_tab].[object_id] = [fk].[referenced_object_id]
		where [fk_tab].[name]='LOGS' and [pk_tab].[name]='LOG_TYPES') begin
		alter table [db_scales].[LOGS] with check add foreign key([LOG_TYPE_UID]) references [db_scales].[LOG_TYPES] ([UID])
		print '[+] Create contstraints in table [db_scales].[LOGS] - success'
	end else begin
		print '[ ] Create contstraints in table [db_scales].[LOGS] - is exists'
	end
	if exists (select [fk].[name] [fk_constraint_name]
		from [sys].[foreign_keys] [fk]
		inner join [sys].[tables] [fk_tab] on [fk_tab].[object_id] = [fk].[parent_object_id]
		inner join [sys].[tables] [pk_tab] on [pk_tab].[object_id] = [fk].[referenced_object_id]
		where [fk_tab].[name]='SCALES' and [pk_tab].[name]='LOG_TYPES') begin
		alter table [db_scales].[SCALES] with check add foreign key([LOG_TYPE_UID]) references [db_scales].[LOG_TYPES] ([UID])
		print '[+] Create contstraints in table [db_scales].[SCALES] - success'
	end else begin
		print '[ ] Create contstraints in table [db_scales].[SCALES] - is exists'
	end
end
----------------------------------------------------------------------------------------------------
-- Create properties
begin
	if not exists (select 1 from [sys].[tables] where [name]='LOG_TYPES' and [schema_id]=@schema_id) begin
		exec sp_addextendedproperty @name = N'MS_Description', @value = N'LOG_TYPES.', @level0type = N'SCHEMA', 
			@level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'LOG_TYPES', @level2type = NULL, @level2name = NULL
		print '[+] Add properties for table [db_scales].[LOG_TYPES] - success'
	end else begin
		print '[ ] Add properties for table [db_scales].[LOG_TYPES] - is exists'
	end
end
----------------------------------------------------------------------------------------------------
-- Table [LOG_TYPES] data
if (@drop=1) begin
	if exists (select 1 from [sys].[tables] where [name]='LOG_TYPES' and [schema_id]=@schema_id) begin
		if exists (select 1 from [db_scales].[LOG_TYPES]) begin
			truncate table [db_scales].[LOG_TYPES]
			print '[-] Truncate data in table [db_scales].[LOG_TYPES] - success'
		end else begin
			print '[ ] Truncate data in table [db_scales].[LOG_TYPES] - is not exists'
		end
	end
end
if (@insert=1) begin
	if exists (select 1 from [sys].[tables] where [name]='LOG_TYPES' and [schema_id]=@schema_id) begin
		insert into [db_scales].[LOG_TYPES]([NUMBER],[ICON]) values(0,N'None')
		insert into [db_scales].[LOG_TYPES]([NUMBER],[ICON]) values(1,N'Error')
		insert into [db_scales].[LOG_TYPES]([NUMBER],[ICON]) values(2,N'Stop')
		insert into [db_scales].[LOG_TYPES]([NUMBER],[ICON]) values(3,N'Question')
		insert into [db_scales].[LOG_TYPES]([NUMBER],[ICON]) values(4,N'Warning')
		insert into [db_scales].[LOG_TYPES]([NUMBER],[ICON]) values(5,N'Information')
		print '[+] Insert data into table [db_scales].[LOG_TYPES] - succeess'
	end else begin
		print '[+] Insert data into table [db_scales].[LOG_TYPES] - is exists'
	end
end
----------------------------------------------------------------------------------------------------
go
-- Access rights
grant select on [db_scales].[LOG_TYPES] to [db_scales_users]
print '[+] Access rights - success'
go
----------------------------------------------------------------------------------------------------
set nocount off
