CREATE FUNCTION [db_scales].[GetScale]
(
	@Owner int,
	@ID int = null
)
RETURNS TABLE
AS
RETURN
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
		[ScaleFactor],
		[WorkShopId],
		CAST(COALESCE([UseOrder],0) as tinyint) [UseOrder]
	FROM  [db_scales].[Scales]
	WHERE 
	[WorkShopId] = @Owner
	AND (([Id] = @ID) OR (@ID is null))
	 AND [Marked] = 0
GO
GRANT SELECT
    ON OBJECT::[db_scales].[GetScale] TO [db_scales_users]
    AS [scales_owner];

