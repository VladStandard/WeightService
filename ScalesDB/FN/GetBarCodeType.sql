
CREATE FUNCTION [db_scales].[GetBarCodeType]
(
	@ID int = null
)
RETURNS TABLE
AS
RETURN
	SELECT  
		[Id],[Name]			

	FROM [db_scales].[BarCodeTypes]
	WHERE 
		(([Id] = @ID) OR (@ID is null))
GO

GRANT SELECT ON [db_scales].[GetBarCodeType] TO [db_scales_users]; 
GO