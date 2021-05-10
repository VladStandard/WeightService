CREATE FUNCTION [db_scales].[GetTemplates]
(
	@Category VARCHAR(150)
)
RETURNS TABLE AS RETURN
(
	SELECT
		[Id],
		[CategoryID],
		[Title],
		[ImageData],
		convert(nvarchar(max),[ImageData],0) as [TemplateAsString],
		[IdRRef]  
	FROM [db_scales].[Templates]
	WHERE [CategoryID] = @Category
	 AND [Marked] = 0
	
)
GO

GRANT SELECT ON [db_scales].[GetTemplates] TO  [db_scales_users]; 
GO
