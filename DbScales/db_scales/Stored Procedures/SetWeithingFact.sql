CREATE PROCEDURE [db_scales].[SetWeithingFact]
	@OrderID		varchar(38),
	@ScaleID		varchar(38),
	@PLU			int,
	@NetWeight		numeric(15,3),
	@TareWeight		numeric(15,3),
	@ProductDate	date,
	@Kneading		int  = null,
	@SSCC			varchar(50) OUTPUT,
	@WeithingDate	datetime2 OUTPUT,
	@xmldata		xml OUTPUT,
	@ID				int OUTPUT


AS
BEGIN

	DECLARE @GLN int
	DECLARE @UnitType tinyint
	DECLARE @Count int
	DECLARE @RegNum int

	SELECT  
		@GLN = 460710023,
		@UnitType = 1,
		@Count = 1 

	--SELECT TOP(1) 
	--	--@GLN = GLN,
	--	@UnitType = 1,
	--	@Count = 1 
	--FROM [db_scales].[Orders] 
	--WHERE [1CRRefID] = @OrderID;

	DECLARE @tbl TABLE (
		[SSCC]		varchar(36),
		[GLN]		int,
		[UnitID]	int,
		[UnitType]	tinyint,
		[SynonymSSCC] varchar(36),
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
      --FOR XML RAW('sscc'), TYPE
		FOR XML 
			PATH ('Item')
			,ROOT('SSCCList')
			--,ELEMENTS XSINIL
			,BINARY BASE64 	  
		)

	SELECT @SSCC = [SSCC], @WeithingDate = SYSDATETIME(), @RegNum = [UnitID] FROM @tbl 


	INSERT INTO [db_scales].[WeithingFact] 
		(
			[OrderID],
			[PluId],
			[SSCC],
			[WeithingDate],
			[NetWeight],
			[TareWeight],
			[ScaleID],
			[ProductDate],
			[SeriesID],
			[RegNum],
			[Kneading]
		) 
		VALUES 
		(
			@OrderID,
			@PLU,
			@SSCC,
			@WeithingDate,
			@NetWeight,
			@TareWeight,
			@ScaleID,
			@ProductDate,
			[db_scales].[GetCurrentProductSeriesId](@ScaleID),
			@RegNum,
			@Kneading
		);

	SELECT @ID = @@IDENTITY;

	RETURN 0;

END
GO
GRANT EXECUTE
    ON OBJECT::[db_scales].[SetWeithingFact] TO [db_scales_users]
    AS [scales_owner];

