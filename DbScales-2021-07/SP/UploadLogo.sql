CREATE PROCEDURE [db_scales].[UploadLogo]
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

	--EXECUTE [db_scales].[ZplLogoClear] @ip ,@port,'E:*.GRF';
	--EXECUTE [db_scales].[ZplPipe] @ip ,@port, '^XA^HWE:*.GRF^XZ';

	OPEN cur 
	FETCH NEXT FROM cur INTO @ttfname, @ttfdata

	WHILE @@FETCH_STATUS = 0 BEGIN
		PRINT @ttfname
		EXECUTE [db_scales].[ZplLogoClear] @ip ,@port,@ttfname;
		EXECUTE [db_scales].[ZplLogoUpload] @ip ,@port, @ttfname, @ttfdata;
		WAITFOR DELAY '00:00:03';
		--EXECUTE [db_scales].[ZplPipe] @ip ,@port, '^XA^HWE:*.GRF^XZ';
		FETCH NEXT FROM cur INTO @ttfname, @ttfdata
	END

	CLOSE cur
	DEALLOCATE cur

	--EXECUTE [db_scales].[ZplPipe] @ip ,@port, '^XA^HWE:*.GRF^XZ';
	RETURN 0
END
GO 

GRANT EXECUTE ON [db_scales].[UploadLogo] TO  [db_scales_users]; 
GO