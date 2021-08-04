CREATE PROCEDURE [db_scales].[ZplPipe]
@ip NVARCHAR (MAX) NULL, @port INT NULL, @zplCommand NVARCHAR (MAX) NULL
AS EXTERNAL NAME [ScalesDB].[StoredProcedures].[ZplPipe]

