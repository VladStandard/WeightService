----------------------------------------------------------
:setvar ip  '10.0.20.67'
:setvar port 9100
----------------------------------------------------------


USE [ScalesDB]
GO

DECLARE @ip nvarchar(100) = $(ip)
DECLARE @port int = $(port)


EXECUTE [db_scales].[Zpl—alibration] 
   @ip
  ,@port
GO


