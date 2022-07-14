-- Configure Printer Connectivity IP
-- SET IP Static
USE [ScalesDB]
GO

DECLARE	@return_value INT

EXEC	@return_value = [db_scales].[ZplPipe]
		--@ip = N'192.168.7.126',
		@ip = N'10.0.20.68',
		--@port = 6101,
		@port = 9100,
		@zplCommand = N'^XA
^ND2,P,10.0.20.68,255.255.255.0,192.168.0.1
^NBC
^NC1
^NPP
^NNOOP 001
^XZ
^XA
^JUS
^XZ';