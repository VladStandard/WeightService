
----------------------------------------------------------
----------------------------------------------------------
----------------------------------------------------------
:setvar ip  '10.0.20.67'
:setvar port 9100
----------------------------------------------------------
----------------------------------------------------------
----------------------------------------------------------

USE [ScalesDB]
GO

DECLARE @ip nvarchar(100) = $(ip)
DECLARE @port int = $(port)
DECLARE @ttfname nvarchar(100) = ''
DECLARE @ttfdata varbinary(max)

DECLARE cur CURSOR FOR 
SELECT [Name],CAST(REPLACE(REPLACE(CONVERT(varchar(max),ImageData),CHAR(13)+CHAR(10),''),' ','')  as XML ).value('.','varbinary(max)') 
FROM [db_scales].[TemplateResources]
WHERE id in (61,62,63)

EXECUTE [db_scales].[ZplLogoClear] @ip ,@port,'E:*.GRF';
EXECUTE [db_scales].[ZplPipe] @ip ,@port, '^XA^HWE:*.GRF^XZ';

OPEN cur 
FETCH NEXT FROM cur INTO @ttfname, @ttfdata

WHILE @@FETCH_STATUS = 0 BEGIN
	PRINT @ttfname
	EXECUTE [db_scales].[ZplLogoUpload] @ip ,@port, @ttfname, @ttfdata;
	WAITFOR DELAY '00:00:03';
	EXECUTE [db_scales].[ZplPipe] @ip ,@port, '^XA^HWE:*.GRF^XZ';
	FETCH NEXT FROM cur INTO @ttfname, @ttfdata
END

CLOSE cur
DEALLOCATE cur

EXECUTE [db_scales].[ZplPipe] @ip ,@port, '^XA^HWE:*.GRF^XZ';

GO






