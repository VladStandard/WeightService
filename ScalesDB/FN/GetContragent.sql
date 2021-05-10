CREATE FUNCTION [db_scales].[GetContragent]
(	
	@ID int = null
)
RETURNS TABLE
AS
RETURN
	SELECT  
		[Id]				,
		[1CRRefID]			,
		[Name]				,
		[Marked]			

	FROM [db_scales].[Contragents]
	WHERE 
		(([Id] = @ID) OR (@ID is null))
GO

GRANT SELECT ON [db_scales].[GetContragent] TO [db_scales_users]; 
GO
