CREATE FUNCTION [db_scales].[GetBarCode]
(
	@NomenclatureId int		= null
	,@BarCodeTypeId  int	 = null	
	,@NomenclatureUnitId int = null
	,@ContragentId int = null		
	,@Id int = null

)
RETURNS TABLE
AS
RETURN
	SELECT  
		[Id]				
		,[CreateDate]		
		,[ModifiedDate]		
		,[BarCodeTypeId]		
		,[NomenclatureId]	
		,[NomenclatureUnitId]
		,[ContragentId]		
		,[Value]				
	FROM [db_scales].[BarCodes]
	WHERE 
		(([NomenclatureId] = @NomenclatureId) OR (@NomenclatureId is null))
		AND (([BarCodeTypeId] = @BarCodeTypeId) OR (@BarCodeTypeId is null))
		AND (([NomenclatureUnitId] = @NomenclatureUnitId) OR (@NomenclatureUnitId is null))
		AND (([ContragentId] = @ContragentId) OR (@ContragentId is null))
		AND (([Id] = @Id) OR (@Id is null))
GO

GRANT SELECT ON [db_scales].[GetBarCode] TO [db_scales_users]; 
GO



