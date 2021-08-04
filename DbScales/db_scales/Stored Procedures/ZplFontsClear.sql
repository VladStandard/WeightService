CREATE PROCEDURE [db_scales].[ZplFontsClear]
@ip NVARCHAR (MAX) NULL, @port INT NULL, @mask NVARCHAR (MAX) NULL
AS EXTERNAL NAME [ScalesDB].[StoredProcedures].[ZplFontsClear]

