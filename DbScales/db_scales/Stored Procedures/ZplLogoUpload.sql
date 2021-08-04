CREATE PROCEDURE [db_scales].[ZplLogoUpload]
@ip NVARCHAR (MAX) NULL, @port INT NULL, @logoname NVARCHAR (MAX) NULL, @logodata VARBINARY (MAX) NULL
AS EXTERNAL NAME [ScalesDB].[StoredProcedures].[ZplLogoUpload]

