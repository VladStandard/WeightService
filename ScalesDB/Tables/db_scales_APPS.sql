declare @schema nvarchar(255) = N'db_scales'
declare @table nvarchar(255) = N'APPS'
declare @drop bit = 1
----------------------------------------------------------------------------------------------------
declare @schema_id int = (select [schema_id] from [sys].[schemas] where [name]=@schema)
if (@drop = 1) begin
	if exists (select 1 from [sys].[tables] where [name]=@table and [schema_id]=@schema_id) begin
		drop table [db_scales].[APPS]
		print '[-] Drop the table ['+@schema+'].['+@table+']'
	end else begin 
		print '[!] Can not drop the table ['+@schema+'].['+@table+']'
	end
end
begin
	if not exists (select 1 from [sys].[tables] where [name]=@table and [schema_id]=@schema_id) begin
		create table [db_scales].[APPS]
		(
			[UID] uniqueidentifier not null primary key default newid(),
			[NAME] nvarchar(32) not null,
		) on [ScalesFileGroup]
		print '[+] Created the table ['+@schema+'].['+@table+']'
	end else begin
		print '[!] Can not create the table ['+@schema+'].['+@table+']'
	end
end
begin
	if exists (select 1 from [sys].[tables] where [name]=@table and [schema_id]=@schema_id) begin
		exec sp_addextendedproperty @name = N'MS_Description', @value = N'Apps.', @level0type = N'SCHEMA', 
			@level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'Apps', @level2type = NULL, @level2name = NULL
		print '[+] Added properties for the table ['+@schema+'].['+@table+']'
	end else begin
		print '[!] Can not add properties for the table ['+@schema+'].['+@table+']'
	end
end
