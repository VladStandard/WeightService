CREATE PROCEDURE [db_scales].[ZplHostStatus]
@ip NVARCHAR (MAX) NULL, @port INT NULL
AS EXTERNAL NAME [ScalesDB].[StoredProcedures].[ZplHostStatus]

