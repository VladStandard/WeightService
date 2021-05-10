
CREATE FUNCTION [db_scales].[GetProductionFacility]
(
	@ID int = null
)
RETURNS TABLE
AS
RETURN
	SELECT  
		[Id]
		,[Name]			
		,[CreateDate]	
		,[ModifiedDate]	
		,[IdRRef]		

	FROM [db_scales].[ProductionFacility]
	WHERE (([Id] = @ID) OR (@ID is null))  AND [Marked] = 0
GO

GRANT SELECT ON [db_scales].[GetProductionFacility] TO [db_scales_users]; 
GO