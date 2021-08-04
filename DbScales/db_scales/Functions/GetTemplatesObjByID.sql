CREATE FUNCTION [db_scales].[GetTemplatesObjByID]
(
	@TemplateID int
)
RETURNS TABLE AS RETURN
(

	SELECT
		[CategoryID],[Title],convert(nvarchar(max),[ImageData],0) as [XslContent]
	FROM [db_scales].[Templates]
	WHERE Id = @TemplateID
	
)
GO
GRANT SELECT
    ON OBJECT::[db_scales].[GetTemplatesObjByID] TO [db_scales_users]
    AS [scales_owner];

