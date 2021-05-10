
----------------------------------------------------------
----------------------------------------------------------
----------------------------------------------------------
:setvar ip  '10.0.20.67'
:setvar port 9100
----------------------------------------------------------
----------------------------------------------------------
----------------------------------------------------------

-- Sets the darkness generally

USE [ScalesDB]
GO


DECLARE	@return_value INT

EXEC	@return_value = [db_scales].[ZplPipe]
		@ip = $(ip),
		@port = $(port),
		@zplCommand = N'~SD10
		';


--Saves the current settings to the EEPROM so that they persist after power off
EXEC	@return_value = [db_scales].[ZplPipe]
		@ip = $(ip),
		@port = $(port),
		@zplCommand = N'^XA^JUS^XZ';