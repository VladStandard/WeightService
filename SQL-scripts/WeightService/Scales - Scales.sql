------------------------------------------------------------------------------------------------------------------------
-- Table [Scales]
------------------------------------------------------------------------------------------------------------------------
SELECT
	[S].[Id]
   ,[S].[Description]
   ,[S].[IdRRef]
   ,[S].[DeviceIP]
   ,[S].[DevicePort]
   ,[S].[DeviceMAC]
   ,[S].[OrganizationId]
   ,[S].[DeviceSendTimeout]
   ,[S].[DeviceReceiveTimeout]
   ,[S].[DeviceComPort]
   ,[S].[ZebraIP]
   ,[S].[ZebraPort]
   ,[S].[UseOrder]
   ,[S].[VerScalesUI]
   ,[S].[DeviceNumber]
   ,[S].[CreateDate]
   ,[S].[ModifiedDate]
   ,[S].[TemplateIdDefault]
   ,[S].[TemplateIdSeries]
   ,[S].[ScaleFactor]
   ,[S].[WorkShopId]
   ,[WS].[Name] [WORKSHOP]
   ,[S].[ZebraPrinterId]
   ,[ZP].[Name] [PRINTER]
   ,[S].[Marked]
   ,[S].[HostId]
   ,[S].[LOG_TYPE_UID]
   ,[LT].[ICON]
FROM [db_scales].[Scales] [S]
LEFT JOIN [db_scales].[ZebraPrinter] [ZP]
	ON [S].[ZebraPrinterId] = [ZP].[Id]
LEFT JOIN [db_scales].[WORKSHOP] [WS]
	ON [S].[WorkShopId] = [WS].[Id]
LEFT JOIN [db_scales].[LOG_TYPES] [LT]
	ON [S].[LOG_TYPE_UID] = [LT].[UID]
ORDER BY [S].[Id]
------------------------------------------------------------------------------------------------------------------------
