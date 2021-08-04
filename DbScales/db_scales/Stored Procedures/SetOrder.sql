CREATE PROCEDURE [db_scales].[SetOrder]

	@IDRRef uniqueidentifier,
	@PLU INT,
	@1СScaleID varchar(38),
	@TemplateID nvarchar(38),
	@PlaneBoxCount int,
	@PlanePalletCount int,
	@PlanePackingOperationBeginDate datetime,
	@PlanePackingOperationEndDate datetime,
	@ProductDate datetime,
	@Status tinyint,
	@ID int OUTPUT

AS
BEGIN

	BEGIN TRAN;
	
	-- проверить статус

	MERGE [db_scales].[Orders] AS target  
    USING (
	SELECT 
		@IDRRef,
		@PLU,
		UPPER(@1СScaleID),
		@PlaneBoxCount,
		@PlanePalletCount,
		@PlanePackingOperationBeginDate,
		@PlanePackingOperationEndDate,
		@ProductDate,
		UPPER(@TemplateID)

	) AS source (

		[1CRRefID],
		[PLU],
		[1СScaleID],
		[PlaneBoxCount],
		[PlanePalletCount],
		[PlanePackingOperationBeginDate],
		[PlanePackingOperationEndDate],
		[ProductDate],
		[TemplateID]

	)  
    ON (target.IDRRef = source.[1CRRefID]) 
	
	WHEN MATCHED THEN
        UPDATE SET 
			[PlaneBoxCount]			= source.[PlaneBoxCount] ,
			[PlanePalletCount]		= source.[PlanePalletCount] ,
			[PlanePackingOperationBeginDate]	= source.[PlanePackingOperationBeginDate] ,
			[PlanePackingOperationEndDate]		= source.[PlanePackingOperationEndDate] ,
			[ProductDate]	= source.[ProductDate] ,
			[ScaleID]		= source.[1СScaleID] ,
			[PLU]			= source.[PLU] ,
			[TemplateID]	= source.[TemplateID],
			[ModifiedDate]	= GETDATE()

    WHEN NOT MATCHED THEN 
	
        INSERT (
			IDRRef,
			[PLU],
			[ScaleID],
			[PlaneBoxCount],
			[PlanePalletCount],
			[PlanePackingOperationBeginDate],
			[PlanePackingOperationEndDate],
			[ProductDate],
			[TemplateID]
		)  
        VALUES 
		(
			source.[1CRRefID],
			source.[PLU],
			source.[1СScaleID],
			source.[PlaneBoxCount],
			source.[PlanePalletCount],
			source.[PlanePackingOperationBeginDate],
			source.[PlanePackingOperationEndDate],
			source.[ProductDate],
			source.[TemplateID]
		) ;

	SELECT @ID = @@IDENTITY;

	INSERT INTO [db_scales].[OrderStatus]
           ([OrderId]
           ,[CurrentDate]
           ,[CurrentStatus])
     VALUES (
           @ID
           ,GETDATE()
           ,@Status
		   )


	COMMIT TRAN;

	RETURN 0;

END
GO
GRANT EXECUTE
    ON OBJECT::[db_scales].[SetOrder] TO [db_scales_users]
    AS [scales_owner];

