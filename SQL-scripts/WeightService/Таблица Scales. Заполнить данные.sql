-- Таблица Scales. Заполнить данные
use [ScalesDB]
--delete from [db_scales].[Scales]
-- Record 1
declare @idrref uniqueidentifier = '7B832E43-BAA4-11EA-BC3C-AC1F6B02AD52'
declare @templateGuid uniqueidentifier = 'CEE481F7-C806-11EA-BC3F-AC1F6B02AD52'
declare @templateId int
set @templateId = (select [Id] from [db_scales].[Templates] where [IdRRef] = @templateGuid)
declare @description nvarchar(max) = N'Line 18 (ОП01)'
if not exists (select 1 from [db_scales].[Scales] where [Description] = @description) begin
	insert into [db_scales].[Scales] (
		[Description], [IdRRef], [DeviceIP], [DevicePort], [DeviceMAC], [DeviceSendTimeout], [DeviceReceiveTimeout], [DeviceComPort],
		[ZebraIP], [ZebraPort], [UseOrder], [VerScalesUI], [DeviceNumber],
		[CreateDate], [ModifyDate], [TemplateIdDefault], [TemplateIdSeries], [ScaleFactor], [WorkShopId])
		values (@description, @idrref, '192.168.4.169', 0, '00E0670F1636', 100, 100, 'COM6', 
		'192.168.7.126', 9100, 0, '', 3, 
		getdate(), getdate(), @templateId, null, 1, 1)
end
-- Record 2
set @idrref = '8319C4A2-BD2E-11EA-BC3D-AC1F6B02AD52'
set @templateGuid = 'CEE481F7-C806-11EA-BC3F-AC1F6B02AD52'
set @templateId = NULL
set @description = N'Line PC208'
if not exists (select 1 from [db_scales].[Scales] where [Description] = @description) begin
	insert into [db_scales].[Scales] (
		[Description], [IdRRef], [DeviceIP], [DevicePort], [DeviceMAC], [DeviceSendTimeout], [DeviceReceiveTimeout], [DeviceComPort],
		[ZebraIP], [ZebraPort], [UseOrder], [VerScalesUI], [DeviceNumber],
		[CreateDate], [ModifyDate], [TemplateIdDefault], [TemplateIdSeries], [ScaleFactor], [WorkShopId])
		values (@description, @idrref, '', 0, '', 120, 100, 'COM4', 
		'192.168.7.127', 9100, 0, '', 2265, 
		getdate(), getdate(), @templateId, null, null, 1)
end
-- Record 3
set @idrref = 'E3ED0806-DBCA-11EA-BC43-AC1F6B02AD52'
set @templateGuid = 'CEE481F7-C806-11EA-BC3F-AC1F6B02AD52'
set @templateId = (select [Id] from [db_scales].[Templates] where [IdRRef] = @templateGuid)
set @description = N'Line 13 (ОП01)'
if not exists (select 1 from [db_scales].[Scales] where [Description] = @description) begin
	insert into [db_scales].[Scales] (
		[Description], [IdRRef], [DeviceIP], [DevicePort], [DeviceMAC], [DeviceSendTimeout], [DeviceReceiveTimeout], [DeviceComPort],
		[ZebraIP], [ZebraPort], [UseOrder], [VerScalesUI], [DeviceNumber],
		[CreateDate], [ModifyDate], [TemplateIdDefault], [TemplateIdSeries], [ScaleFactor], [WorkShopId])
		values (@description, @idrref, '192.168.5.72', 5100, '80EE73EC593C', 100, 100, 'COM5', 
		'192.168.3.193', 9100, 0, '', 13, 
		getdate(), getdate(), @templateId, null, 0, 1)
end
-- Record 4
set @description = N'PC231'
set @idrref = '6F6D5AA9-0480-11EB-BC47-AC1F6B02AD52'
set @templateGuid = 'CEE481F7-C806-11EA-BC3F-AC1F6B02AD52'
set @templateId = (select [Id] from [db_scales].[Templates] where [IdRRef] = @templateGuid)
if not exists (select 1 from [db_scales].[Scales] where [Description] = @description) begin
	insert into [db_scales].[Scales] (
		[Description], [IdRRef], [DeviceIP], [DevicePort], [DeviceMAC], [DeviceSendTimeout], [DeviceReceiveTimeout], [DeviceComPort],
		[ZebraIP], [ZebraPort], [UseOrder], [VerScalesUI], [DeviceNumber],
		[CreateDate], [ModifyDate], [TemplateIdDefault], [TemplateIdSeries], [ScaleFactor], [WorkShopId])
		values (@description, @idrref, '192.168.5.82', 0, '80EE73DCB5D7', 15, 50, 'COM3', 
		'192.168.7.126', 9100, 0, '', 999, 
		getdate(), getdate(), @templateId, null, 1000, 1)
end
-- Выборка
select * from [db_scales].[Scales] order by [Id]
