----------------------------------------------------------
----------------------------------------------------------
----------------------------------------------------------
:setvar ip  '10.0.20.69'
:setvar port 9100
----------------------------------------------------------
----------------------------------------------------------
----------------------------------------------------------

USE [ScalesDB]
GO

-- Sets the Print Mode to Peel-off, and

DECLARE	@return_value INT

EXEC	@return_value = [db_scales].[ZplPipe]
		@ip = $(ip),
		@port = $(port),
		@zplCommand = N'^XA^MMP^XZ';


--Saves the current settings to the EEPROM so that they persist after power off
EXEC	@return_value = [db_scales].[ZplPipe]
		@ip = $(ip),
		@port = $(port),
		@zplCommand = N'^XA^JUS^XZ';

		