CREATE TABLE [db_scales].[ZebraPrinterResourceRef]
(
	
	[ID] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[PrinterID]    int NOT NULL FOREIGN KEY REFERENCES [db_scales].[ZebraPrinter] (Id),
	[ResourceID]    int NOT NULL FOREIGN KEY REFERENCES [db_scales].[TemplateResources] (Id),
	[Description]   nvarchar(150) NULL,
    [CreateDate]	DATETIME NOT NULL DEFAULT GETDATE(),
	[ModifiedDate]  DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT AK_ZebraPrinterResourceRef UNIQUE([PrinterID],[ResourceID])

) ON [ScalesFileGroup]

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Связывает (много ко многим) принтеры с ресурсами, выгружаемыми на принтер',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'ZebraPrinterResourceRef',
    @level2type = NULL,
    @level2name = NULL
GO