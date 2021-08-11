CREATE TABLE [db_scales].[ZebraPrinterType]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name] nvarchar(100)
) ON [ScalesFileGroup]
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Модели принтеров Zebra, их редактируем через скрипт один раз в 100 лет',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'ZebraPrinterType',
    @level2type = NULL,
    @level2name = NULL
GO