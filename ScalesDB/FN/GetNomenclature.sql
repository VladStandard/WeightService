CREATE FUNCTION [db_scales].[GetNomenclature]
(
	@ID int = null
)
RETURNS TABLE
AS
RETURN
	SELECT  [Id]				 ,
			[IdRRef]			 ,
			[Code]				 ,
			[Marked]			 ,
			[Name]				 ,
			[NameFull]			 ,
			[Description]		 ,
			[Comment]			 ,
			[Brand]				 ,
			[GUID_Mercury]		 ,
			[NomenclatureType]	 ,
			[VATRate]			 

	FROM [db_scales].[Nomenclature]
	WHERE (([Id] = @ID) OR (@ID is null))
GO

GRANT SELECT ON [db_scales].[GetNomenclature] TO [db_scales_users]; 
GO




