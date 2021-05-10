CREATE PROCEDURE [db_scales].[SetNewScale] (
	@ID int = null,
	@Description nvarchar(150) = null,
	@IP varchar(15) = null,
	@Port smallint = null,
	@DeviceMAC varchar(35) = null,
	@DeviceSendTimeout smallint  = null,
	@DeviceReceiveTimeout smallint = null,
	@DeviceComPort varchar(5) = null,
	@UseOrder smallint = 0,
	@VerScalesUI varchar(30) = null,
	--@TemplateIdDefault int = null,
	--@TemplateIdSeries  int = null,
	@ScaleFactor int = null,
	@IDN int output
)

AS
BEGIN	

	MERGE [db_scales].[Scales] AS target  
    USING (
		SELECT @Id,
		@Description,@IP,@Port,@DeviceMAC,@DeviceSendTimeout,
		@DeviceReceiveTimeout,@DeviceComPort,@UseOrder,
		@VerScalesUI,
		--(@TemplateIdDefault),	
		--(@TemplateIdSeries),
		@ScaleFactor
		
		) 
		AS source 
		([Id],
		[Description],[DeviceIP],[DevicePort],[DeviceMAC],
		[DeviceSendTimeout],[DeviceReceiveTimeout],[DeviceComPort],[UseOrder],
		[VerScalesUI],
		--[TemplateIdDefault],
		--[TemplateIdSeries],
		[ScaleFactor]
		)  
    ON (target.[Id] = source.[Id])  
    WHEN MATCHED THEN 
		UPDATE SET 
			[Description] = source.[Description],
			[DeviceIP] = source.[DeviceIP],
			[DevicePort] = source.[DevicePort],
			[DeviceMAC] = source.[DeviceMAC],
			[DeviceSendTimeout] = source.[DeviceSendTimeout],
			[DeviceReceiveTimeout] = source.[DeviceReceiveTimeout],
			[DeviceComPort] = source.[DeviceComPort],
			[UseOrder] = source.[UseOrder],
			[VerScalesUI] = source.[VerScalesUI],
			[ModifiedDate] = Getdate(),
			--[TemplateIdDefault] = source.[TemplateIdDefault],
			--[TemplateIdSeries] = source.[TemplateIdSeries],
			[ScaleFactor] = source.[ScaleFactor]

    WHEN NOT MATCHED THEN  
        INSERT 
		(
		[Description],
		[DeviceIP],
		[DevicePort],
		[DeviceMAC],
		[DeviceSendTimeout],
		[DeviceReceiveTimeout],
		[DeviceComPort],
		[UseOrder],
		[VerScalesUI],
		--[TemplateIdDefault], 
		--[TemplateIdSeries],
		[ScaleFactor]
		)
        VALUES 
		(
		source.[Description],
		source.[DeviceIP],
		source.[DevicePort],
		source.[DeviceMAC],
		source.[DeviceSendTimeout],
		source.[DeviceReceiveTimeout],
		source.[DeviceComPort],
		source.[UseOrder],
		source.[VerScalesUI],
		--source.[TemplateIdDefault],
		--source.[TemplateIdSeries], 
		source.[ScaleFactor] 
		);

	SELECT @IDN = @@IDENTITY;

	RETURN 0;

END
GO

GRANT EXECUTE ON [db_scales].[SetNewScale] TO [db_scales_users]; 
GO
