-- Функция GetScaleByID
use [ScalesDB]
declare @ScaleId int = 5
------------------------------------------------------------------------------------------------------------------------
select
	 [ID]
	,[Description]
	,[DeviceIP]
	,[DevicePort]
	,[DeviceMAC]
	,[DeviceSendTimeout]
	,[DeviceReceiveTimeout]
	,[DeviceComPort]
	,[ZebraIP]
	,[ZebraPort]
	,[VerScalesUI]
	,[UseOrder]
from [db_scales].[GetScaleByID] (@ScaleId)
