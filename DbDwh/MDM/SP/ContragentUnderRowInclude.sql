CREATE PROCEDURE [MDM].[ContragentUnderRowInclude]
	@Id int,
	@MasterId int
AS
BEGIN

	IF @MasterID = @Id BEGIN
        RAISERROR ('Такая операция недопустима',12,1);
        RETURN 0;
    END;

	UPDATE [DW].[DimContragents]
	SET 
		[NormalizationStatus] = IIF(@MasterId is null,0,1)
		,[RelevanceStatus] = 1
		,[MasterId] = @MasterId
	WHERE 
		[Id]  = @Id;

	RETURN 1;

END
GO
