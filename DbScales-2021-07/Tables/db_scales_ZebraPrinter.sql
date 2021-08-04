CREATE TABLE [db_scales].[ZebraPrinter]
(
	[Id]			INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Name]			NVARCHAR(100),
	[IP]		    VARCHAR(15),
	[Port]		    SMALLINT, 
	[Password]		VARCHAR(15),
	[PrinterTypeId] INT NOT NULL FOREIGN KEY REFERENCES [db_scales].[ZebraPrinterType](Id),
	[Mac]			VARCHAR(20),
	[PeelOffSet]	BIT,
	[DarknessLevel] SMALLINT,
	[CreateDate]	DATETIME NOT NULL DEFAULT GETDATE(),
	[ModifiedDate]	DATETIME NOT NULL DEFAULT GETDATE(),
    [Marked] BIT NOT NULL DEFAULT 0

) ON [ScalesFileGroup]
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Принтеры Zebra',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'ZebraPrinter',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'^XA^MMP^XZ',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'ZebraPrinter',
    @level2type = N'COLUMN',
    @level2name = N'PeelOffSet'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'~SDxx',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'ZebraPrinter',
    @level2type = N'COLUMN',
    @level2name = N'DarknessLevel'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'9100',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'ZebraPrinter',
    @level2type = N'COLUMN',
    @level2name = N'Port'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'10.0.20.67',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'ZebraPrinter',
    @level2type = N'COLUMN',
    @level2name = N'IP'