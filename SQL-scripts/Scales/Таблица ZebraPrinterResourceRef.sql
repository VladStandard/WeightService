-- Таблица ZebraPrinterResourceRef
use [ScalesDB]
------------------------------------------------------------------------------------------------------------------------
--insert into [db_scales].[ZebraPrinterResourceRef]([CreateDate],[ModifiedDate],[PrinterID],[ResourceID],[Description]) values(getdate(), getdate(), 127, 44, 'Description')
------------------------------------------------------------------------------------------------------------------------
select
	 [ZebraPrinterResourceRef].[Id]
	,[ZebraPrinterResourceRef].[CreateDate]
	,[ZebraPrinterResourceRef].[ModifiedDate]
	,[ZebraPrinterResourceRef].[Description]
	,[ZebraPrinterResourceRef].[PrinterID]
	,[ZebraPrinter].[Name] [Printer]
	,[ZebraPrinterResourceRef].[ResourceID]
	,[TemplateResources].[Name] [Resource]
	,[TemplateResources].[Type] [Resource_Type]
from [db_scales].[ZebraPrinterResourceRef]
left join [db_scales].[ZebraPrinter] on [ZebraPrinterResourceRef].[PrinterID] = [ZebraPrinter].[Id]
left join [db_scales].[TemplateResources] on [ZebraPrinterResourceRef].[ResourceID] = [TemplateResources].[Id]
order by [ZebraPrinter].[Name]
