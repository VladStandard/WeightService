CREATE FUNCTION [db_scales].[GetWorkShop]
(
	@Owner int = null,
	@ID int = null
)
RETURNS TABLE
AS
RETURN
	SELECT  
		[Id]
		,[Name]			
		,[ProductionFacilityID]
		,[CreateDate]	
		,[ModifiedDate]	
		,[IdRRef]		

	FROM [db_scales].[WorkShop]
	WHERE 
		([ProductionFacilityID] = @Owner OR @Owner is null)
		AND ([Id] = @ID OR @ID is null)
		 AND [Marked] = 0
GO
GRANT SELECT
    ON OBJECT::[db_scales].[GetWorkShop] TO [db_scales_users]
    AS [scales_owner];

