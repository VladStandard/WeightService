-- Configure Printer Connectivity DHCP
-- SET DHCP
USE [ScalesDB]
GO

DECLARE	@return_value INT

EXEC	@return_value = [db_scales].[ZplPipe]
		--@ip = N'192.168.7.126',
		@ip = N'10.0.20.68',
		--@port = 6101,
		@port = 9100,
		@zplCommand = N'^XA
^ND2,A
^NBC
^NC1
^NPP
^XZ
^XA
^JUS
^XZ';




