CREATE PROCEDURE [MDM].[ContragentSetNotRelevance]
	@Id int
AS
BEGIN

	IF EXISTS(SELECT 1 FROM [DW].[DimContragents] WHERE [MasterId] = [ID] AND [ID] = @Id) 
	BEGIN 
        RAISERROR ('Нельзя сделать мастер-запись "не подлежащей нормализации".',12,1);
        RETURN 0;
    END;

	UPDATE [DW].[DimContragents]
	SET 
		[NormalizationStatus] = 3
		,[RelevanceStatus] = 2
		,[MasterId] = NULL
	WHERE 
		[Id]  = @Id;

	RETURN 1;

END
GO