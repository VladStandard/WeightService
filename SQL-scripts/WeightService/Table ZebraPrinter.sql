-- Таблица ZebraPrinter
use [ScalesDB]
------------------------------------------------------------------------------------------------------------------------
select
	 [Print].[Id]
	,[Print].[CreateDate]
	,[Print].[ModifiedDate]
	,[Print].[Name]
	,[Print].[IP]
	,[Print].[Port]
	,[Print].[Password]
	,[Print].[PrinterTypeId]
	,[Print].[Mac]
	,[Print].[PeelOffSet]
	,[Print].[DarknessLevel]
	,[Type].[Name] [Type_Name]
from [db_scales].[ZebraPrinter] [Print]
left join [db_scales].[ZebraPrinterType] [Type] on [Print].[PrinterTypeId]=[Type].[Id]
