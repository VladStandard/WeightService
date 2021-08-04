CREATE PROCEDURE [db_scales].[ZplLogoClear]
@ip NVARCHAR (MAX) NULL, @port INT NULL, @mask NVARCHAR (MAX) NULL
AS EXTERNAL NAME [ScalesDB].[StoredProcedures].[ZplLogoClear]

