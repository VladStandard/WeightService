CREATE PROCEDURE [MDM].[ContragentMasterRowRemove]
	@MasterId int
AS
BEGIN

	BEGIN TRAN;

    UPDATE [DW].[DimContragents]
	SET 
		[NormalizationStatus] = NULL
		,[RelevanceStatus] =  NULL
		,[MasterId] =  NULL
	WHERE 
		[Id]  = @MasterId;

    DELETE FROM [DW].[DimContragents]
    WHERE ID = @MasterId;

	COMMIT TRAN;

	RETURN 1;
END
GO