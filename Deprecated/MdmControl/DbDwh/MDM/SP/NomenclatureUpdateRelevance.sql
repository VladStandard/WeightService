CREATE PROCEDURE [MDM].[NomenclatureUpdateRelevance]
	@Id int,
	@Value tinyint
AS
BEGIN

	IF @Id is null BEGIN
        RAISERROR ('Id is null', 12, 1);
        RETURN 0;
    END;
	IF @Value < 0 or @Value > 2 BEGIN
        RAISERROR ('Value must be: null, 0, 1, 2', 16, 1);
        RETURN 0;
    END;

	UPDATE [DW].[DimNomenclatures]
	SET 
		 [RelevanceStatus] = @value
	WHERE 
		[Id]  = @Id;

	RETURN 1;
END
GO
