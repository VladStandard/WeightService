CREATE PROCEDURE [db_scales].[ZplFontUpload]
@ip NVARCHAR (MAX) NULL, @port INT NULL, @ttfname NVARCHAR (MAX) NULL, @ttfdata VARBINARY (MAX) NULL
AS EXTERNAL NAME [ScalesDB].[StoredProcedures].[ZplFontUpload]

