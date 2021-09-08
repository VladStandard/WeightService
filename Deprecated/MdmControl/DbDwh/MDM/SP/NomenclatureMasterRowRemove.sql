CREATE PROCEDURE [MDM].[NomenclatureMasterRowRemove]
	@MasterId int
AS
BEGIN

	BEGIN TRAN; 

    UPDATE [DW].[DimNomenclatures]
	SET 
		[NormalizationStatus] = NULL
		,[RelevanceStatus] =  NULL
		,[MasterId] =  NULL
	WHERE 
		[Id]  = @MasterId;

    DELETE FROM [DW].[DimNomenclatures]
    WHERE ID = @MasterId;

	COMMIT TRAN;

	RETURN 1;
END
GO