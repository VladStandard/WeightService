-- public ScaleEntity Load(string guid)
DECLARE @SCALEID NVARCHAR(255) = '8319C4A2-BD2E-11EA-BC3D-AC1F6B02AD52'
-- Загрузить данные таблицы Scales.
-- Версия 0.0.25.
SELECT
	 [ID]
	,[Description]
	,[1CRRefID]
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
	--,[DeviceNumber]
FROM [db_scales].[GetScaleByID] (@ScaleID)
