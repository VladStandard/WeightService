CREATE PROCEDURE [db_scales].[NewProductSeries]
    @ScaleID	VARCHAR(38),
	@SSCC		varchar(50) OUTPUT,
	@xmldata	XML OUTPUT
AS
BEGIN


	DECLARE @GLN		int= 460710023
	DECLARE @UnitType	tinyint = 0
	DECLARE @Count		int= 1 

	DECLARE @tbl TABLE (
		[SSCC]		varchar(50),
		[GLN]		int,
		[UnitID]	int,
		[UnitType]	tinyint,
		[SynonymSSCC] varchar(50),
		[Check]		int
	);

	INSERT INTO @tbl
	EXECUTE [db_sscc].[GetSSCCList] @GLN, @UnitType, @Count

	SET @xmldata = (
      SELECT 
		[SSCC]	as '@SSCC',
		[GLN]	as '@GLN',
		[UnitID] as '@UnitID',
		[UnitType] as '@UnitType',
		[SynonymSSCC] as '@SynonymSSCC',
		[Check]  as '@Check'
      FROM  @tbl
		FOR XML PATH ('Item'), ROOT('SSCCList'), BINARY BASE64 	  
	)

	SELECT @SSCC = [SSCC] FROM @tbl 

	BEGIN TRAN;

	UPDATE [db_scales].[ProductSeries]
	SET [IsClose] = 1
	WHERE 
		[IsClose] = 0
		AND [ScaleID] = @ScaleID;

	INSERT INTO [db_scales].[ProductSeries] ([ScaleID],[SSCC]) VALUES (@ScaleID,@SSCC);

	COMMIT TRAN;

	RETURN 0;

END
GO
GRANT EXECUTE
    ON OBJECT::[db_scales].[NewProductSeries] TO [db_scales_users]
    AS [scales_owner];

