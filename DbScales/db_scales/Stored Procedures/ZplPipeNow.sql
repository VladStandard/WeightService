CREATE PROCEDURE [db_scales].[ZplPipeNow]
@ip NVARCHAR (MAX) NULL, @port INT NULL, @xslInput NVARCHAR (MAX) NULL, @xmlInput NVARCHAR (MAX) NULL
AS EXTERNAL NAME [ScalesDB].[StoredProcedures].[ZplPipeNow]

