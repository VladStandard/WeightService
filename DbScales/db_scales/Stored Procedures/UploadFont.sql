CREATE PROCEDURE [db_scales].[UploadFont]
	@ip  varchar(100),
	@port int,
	@id int 
AS
BEGIN

DECLARE @ttfname nvarchar(100) = ''
DECLARE @ttfdata varbinary(max)

DECLARE cur CURSOR FOR 
SELECT [Name],CAST(REPLACE(REPLACE(CONVERT(varchar(max),ImageData),CHAR(13)+CHAR(10),''),' ','')  as XML ).value('.','varbinary(max)') 
FROM [db_scales].[TemplateResources]
WHERE id = @id

--EXECUTE [db_scales].[ZplFontsClear] @ip ,@port,'E:*.TTF';
--EXECUTE [db_scales].[ZplPipe] @ip ,@port, '^XA^HWE:^XZ';
--WAITFOR DELAY '00:00:05';


OPEN cur 
FETCH NEXT FROM cur INTO @ttfname, @ttfdata
WHILE @@FETCH_STATUS = 0 BEGIN
	PRINT @ttfname
	EXECUTE [db_scales].[ZplFontsClear] @ip ,@port,@ttfname;
	EXECUTE [db_scales].[ZplFontUpload] @ip ,@port, @ttfname, @ttfdata;
	WAITFOR DELAY '00:00:05';
	FETCH NEXT FROM cur INTO @ttfname, @ttfdata
END

CLOSE cur
DEALLOCATE cur

--EXECUTE [db_scales].[ZplPipe] @ip ,@port, '^XA^HWE:^XZ';
--WAITFOR DELAY '00:00:05';

END
GO
GRANT EXECUTE
    ON OBJECT::[db_scales].[UploadFont] TO [db_scales_users]
    AS [scales_owner];

