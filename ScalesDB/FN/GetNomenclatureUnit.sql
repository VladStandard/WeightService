CREATE FUNCTION [db_scales].[GetNomenclatureUnit]
(	
	@Owner int = null,
	@ID int = null
)
RETURNS TABLE
AS
RETURN
	SELECT Id,
		[1CRRefID]			,
		[Name]				,
		[NomenclatureId]	,
		[Marked]			,
		[PackWeight]		,
		[PackQuantly]		,
		[PackTypeId]		

	FROM [db_scales].[NomenclatureUnits]
	WHERE 
		([NomenclatureId] = @Owner OR @Owner is null)
		AND ([Id] = @ID OR @ID is null)
GO

GRANT SELECT ON [db_scales].[GetNomenclatureUnit] TO [db_scales_users]; 
GO