-- Функция GetPrinterResources
use [ScalesDB]
declare @ZebraPrinterId int = 1
declare @Type varchar(3) = 'GRF'
------------------------------------------------------------------------------------------------------------------------
select
	 [Name]
	,[ImageData]
from [db_scales].[GetPrinterResources] (@ZebraPrinterId, @Type)
