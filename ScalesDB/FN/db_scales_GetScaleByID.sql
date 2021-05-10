CREATE FUNCTION [db_scales].[GetScaleByID]
(
	@ScaleID int
)
RETURNS TABLE AS RETURN
(
	SELECT 
		[DeviceNumber] as ID,
		[Description],
		[IdRRef],
		[DeviceIP],
		[DevicePort],
		[DeviceMAC] ,
		[DeviceSendTimeout],
		[DeviceReceiveTimeout],
		[DeviceComPort],
		[ZebraIP],
		[ZebraPort],
		[VerScalesUI],
		[TemplateIdDefault], 
		[TemplateIdSeries],
		[DeviceNumber] as  DeviceID,
		[ScaleFactor],
		CAST(COALESCE([UseOrder],0) as tinyint) [UseOrder],
		[ZebraPrinterId]
	FROM  [db_scales].[Scales]
	WHERE [Id] = @ScaleID
)
GO

GRANT SELECT ON [db_scales].[GetScaleByID]
    TO  [db_scales_users]; 
GO
