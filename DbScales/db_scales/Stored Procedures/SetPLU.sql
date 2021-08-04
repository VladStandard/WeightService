CREATE PROCEDURE [db_scales].[SetPLU]

	@NomenclatureId int,
	@ScaleID int,
	@PLU int,
	@Template nvarchar(38),
	@GoodsName nvarchar(150),
	@GoodsFullName nvarchar(max),
	@GoodsDescription nvarchar(max),
	@GTIN varchar(150),
	@EAN13 [varchar](150) ,
	@ITF14 [varchar](150) ,
	@GoodsShelfLifeDays tinyint,
	@GoodsTareWeight decimal(10,3),
	@GoodsBoxQuantly int,
	@ConsumerName nvarchar(150),
	@GLN int,
	@Active bit = 1,
	@ID int OUTPUT

AS
BEGIN

	BEGIN TRAN;
	
	-- проверить статус

	MERGE [db_scales].[PLU] AS target  
    USING (
	SELECT 
		
		@NomenclatureId,
		@ScaleID,
		@PLU,
		@GoodsName,
		@GoodsFullName,
		@GoodsDescription,
		@GTIN,
		@EAN13,
		@ITF14,
		@GoodsShelfLifeDays,
		@GoodsTareWeight,
		@GoodsBoxQuantly,
		@Template,
		@Active

	) AS source (

		[1CRRefGoods],
		[1CScaleID],
		[PLU],
		[GoodsName],
		[GoodsFullName],
		[GoodsDescription],
		[GTIN],
		[EAN13],
		[ITF14],
		[GoodsShelfLifeDays],
		[GoodsTareWeight],
		[GoodsBoxQuantly],
		[Template],
		[Active]

	)  
    ON 
	(target.ScaleID = source.[1CScaleID])
	AND (target.[PLU] = source.[PLU])
	
	WHEN MATCHED THEN

	UPDATE SET 

		[PLU] = source.[PLU],
		[GoodsName] = source.[GoodsName],
		[GoodsFullName] = source.[GoodsFullName],
		[GoodsDescription] = source.[GoodsDescription],
		[GTIN] = source.[GTIN],
		[EAN13] = source.[EAN13],
		[ITF14] = source.[ITF14],
		[GoodsShelfLifeDays] = source.[GoodsShelfLifeDays],
		[GoodsTareWeight] = source.[GoodsTareWeight],
		[GoodsBoxQuantly] = source.[GoodsBoxQuantly],
		[TemplateID] = source.[Template],
		[ModifiedDate]	= GETDATE(),
		[Active] = @Active

    WHEN NOT MATCHED THEN 
	
	INSERT (
		NomenclatureId,
		ScaleID,
		[Plu],
		[GoodsName],
		[GoodsFullName],
		[GoodsDescription],
		[GTIN],
		[EAN13],
		[ITF14],
		[GoodsShelfLifeDays],
		[GoodsTareWeight],
		[GoodsBoxQuantly],
		[TemplateID],
		[Active]

	) VALUES (

		source.[1CRRefGoods],
		source.[1CScaleID],
		source.[PLU],
		source.[GoodsName],
		source.[GoodsFullName],
		source.[GoodsDescription],
		source.[GTIN],
		source.[EAN13],
		source.[ITF14],
		source.[GoodsShelfLifeDays],
		source.[GoodsTareWeight],
		source.[GoodsBoxQuantly],
		source.[Template],
		source.[Active]

	);

	SELECT @ID = @@IDENTITY;

	COMMIT TRAN;

	RETURN 0;

END
GO
GRANT EXECUTE
    ON OBJECT::[db_scales].[SetPLU] TO [db_scales_users]
    AS [scales_owner];

