CREATE FUNCTION [db_scales].[GetTemplateByID]
(
	@ID int
)
RETURNS nvarchar(max)
AS
BEGIN
	RETURN 	(SELECT TOP (1) convert(nvarchar(max),[ImageData],0) [XslContent]
	FROM [db_scales].[Templates]
	WHERE Id = @ID)
END
GO

GRANT EXECUTE ON [db_scales].[GetTemplateByID]
    TO  [db_scales_users]; 
GO