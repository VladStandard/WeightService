delete FROM [ScalesDB].[db_scales].[Scales];
INSERT INTO [ScalesDB].[db_scales].[Scales]
           ([Description]
           ,[IdRRef]
           ,[DeviceIP]
           ,[DevicePort]
           ,[DeviceMAC]
           ,[DeviceSendTimeout]
           ,[DeviceReceiveTimeout]
           ,[DeviceComPort]
           ,[ZebraIP]
           ,[ZebraPort]
           ,[UseOrder]
           ,[VerScalesUI]
           ,[DeviceNumber]
           ,[TemplateIdDefault]
           ,[TemplateIdSeries]
           ,[ScaleFactor]
           ,[WorkShopId])
     
SELECT 
	[Description]
      ,cast([1CRRefID] AS UNIQUEIDENTIFIER)
      ,[DeviceIP]
      ,[DevicePort]
      ,[DeviceMAC]
      ,[DeviceSendTimeout]
      ,[DeviceReceiveTimeout]
      ,[DeviceComPort]
      ,[ZebraIP]
      ,[ZebraPort]
      ,[UseOrder]
      ,[VerScalesUI]
      ,[DeviceNumber]
      ,1
      ,[TemplateIdSeries]
      ,[ScaleFactor]
	  ,1
  FROM [ScaleDB_old].[db_scales].[Scales]


