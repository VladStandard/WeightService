CREATE PROCEDURE [db_scales].[SetWeithingFactWithTemplate]
	@OrderId		int,
	@ScaleId			int,
	@TemplateId		int,
	@Variables		xml,
	@PLU			int,
	@NetWeight		numeric(15,3),
	@TareWeight		numeric(15,3),
	@ProductDate	date,
	@Kneading		int = null,
	@SSCC			varchar(50)		OUTPUT,
	@WeithingDate	datetime2		OUTPUT,
	@Label			nvarchar(max)	OUTPUT
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
	--	@GLN = GLN,
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
	EXECUTE [db_scales].[GetSSCCTable] @GLN, @UnitType, @Count

	DECLARE @Template nvarchar(max);
	SELECT @Template = [db_scales].[BuildZplLabel](@TemplateId, @Variables);

	SELECT 
		@SSCC = [SSCC], 
		@WeithingDate = SYSDATETIME(),
		@Label = @Template, 
		@RegNum = [UnitID] 
		FROM @tbl 

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
			@ScaleId,
			@ProductDate,
			[db_scales].[GetCurrentProductSeriesId](@ScaleId),
			@RegNum,
			@Kneading
		);

	RETURN 0;

END

Go

GRANT EXECUTE ON [db_scales].[SetWeithingFactWithTemplate] TO [db_scales_users]; 
GO
