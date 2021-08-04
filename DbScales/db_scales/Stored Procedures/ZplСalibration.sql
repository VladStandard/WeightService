CREATE PROCEDURE [db_scales].[ZplСalibration]
@ip NVARCHAR (MAX) NULL, @port INT NULL
AS EXTERNAL NAME [ScalesDB].[StoredProcedures].[ZplСalibration]

