-- Linked server CREATIO - SQLSRSP01
-- Connect from CREATIO\INS1
USE [master]
declare @server_name nvarchar(255) = N'SQLSRSP01\LEEDS'
if (exists (SELECT 1 FROM sys.servers where [Name] = @server_name)) begin
	EXEC master.dbo.sp_dropserver @server=@server_name, @droplogins='droplogins'
end
EXEC master.dbo.sp_addlinkedserver @server=@server_name, @srvproduct=N'SQL Server'
EXEC master.dbo.sp_addlinkedsrvlogin @rmtsrvname=@server_name,
	@useself=N'False', @locallogin=N'KOLBASA-VS\Morozov_DV', @rmtuser=N'LINK_DWH', @rmtpassword='Qwert!@#$%'
EXEC master.dbo.sp_serveroption @server=@server_name, @optname=N'collation compatible', @optvalue=N'false'
EXEC master.dbo.sp_serveroption @server=@server_name, @optname=N'data access', @optvalue=N'true'
EXEC master.dbo.sp_serveroption @server=@server_name, @optname=N'dist', @optvalue=N'false'
EXEC master.dbo.sp_serveroption @server=@server_name, @optname=N'pub', @optvalue=N'false'
EXEC master.dbo.sp_serveroption @server=@server_name, @optname=N'rpc', @optvalue=N'true'
EXEC master.dbo.sp_serveroption @server=@server_name, @optname=N'rpc out', @optvalue=N'true'
EXEC master.dbo.sp_serveroption @server=@server_name, @optname=N'sub', @optvalue=N'false'
EXEC master.dbo.sp_serveroption @server=@server_name, @optname=N'connect timeout', @optvalue=N'0'
EXEC master.dbo.sp_serveroption @server=@server_name, @optname=N'collation name', @optvalue=null
EXEC master.dbo.sp_serveroption @server=@server_name, @optname=N'lazy schema validation', @optvalue=N'false'
EXEC master.dbo.sp_serveroption @server=@server_name, @optname=N'query timeout', @optvalue=N'0'
EXEC master.dbo.sp_serveroption @server=@server_name, @optname=N'use remote collation', @optvalue=N'true'
EXEC master.dbo.sp_serveroption @server=@server_name, @optname=N'remote proc transaction promotion', @optvalue=N'true'
GO
SELECT * FROM OPENQUERY([SQLSRSP01\LEEDS], 'SELECT TOP (10) [Code],[Name] FROM [VSDWH].[DW].[DimNomenclatures] WHERE [Marked]=0')
