USE [ScalesDB]
DECLARE @1CRRefID NVARCHAR(255) = 'F1A90176-894E-11EA-9E4C-4CCC6A93A440'
DECLARE @Description NVARCHAR(255) = 'Линия 10'
DECLARE @DeviceIP NVARCHAR(255) = null
DECLARE @DevicePort INT = 0
DECLARE @DeviceMAC NVARCHAR(255) = null
DECLARE @DeviceSendTimeout INT = 100
DECLARE @DeviceReceiveTimeout INT = 100
DECLARE @DeviceComPort NVARCHAR(255) = 'COM2'
DECLARE @ZebraIP NVARCHAR(255) = '192.168.7.127'
DECLARE @ZebraPort INT = 9100
DECLARE @UseOrder INT = 0
DECLARE @VerScalesUI NVARCHAR(255) = null
-- SetNewScale
DECLARE @ID int
EXECUTE [db_scales].[SetNewScale] 
    @1CRRefID, @Description, @DeviceIP, @DevicePort, @DeviceMAC, 
    @DeviceSendTimeout, @DeviceReceiveTimeout, @DeviceComPort, 
    @ZebraIP, @ZebraPort, @UseOrder, @VerScalesUI
    ,@ID OUTPUT