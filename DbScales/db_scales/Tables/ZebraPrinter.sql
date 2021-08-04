CREATE TABLE [db_scales].[ZebraPrinter] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Name]          NVARCHAR (100) NULL,
    [IP]            VARCHAR (15)   NULL,
    [Port]          SMALLINT       NULL,
    [Password]      VARCHAR (15)   NULL,
    [PrinterTypeId] INT            NOT NULL,
    [Mac]           VARCHAR (20)   NULL,
    [PeelOffSet]    BIT            NULL,
    [DarknessLevel] SMALLINT       NULL,
    [CreateDate]    DATETIME       DEFAULT (getdate()) NOT NULL,
    [ModifiedDate]  DATETIME       DEFAULT (getdate()) NOT NULL,
    [Marked]        BIT            DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) ON [ScalesFileGroup],
    FOREIGN KEY ([PrinterTypeId]) REFERENCES [db_scales].[ZebraPrinterType] ([Id])
) ON [ScalesFileGroup];


GO
GRANT UPDATE
    ON OBJECT::[db_scales].[ZebraPrinter] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT SELECT
    ON OBJECT::[db_scales].[ZebraPrinter] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT INSERT
    ON OBJECT::[db_scales].[ZebraPrinter] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT DELETE
    ON OBJECT::[db_scales].[ZebraPrinter] TO [db_scales_users]
    AS [scales_owner];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'~SDxx', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'ZebraPrinter', @level2type = N'COLUMN', @level2name = N'DarknessLevel';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'^XA^MMP^XZ', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'ZebraPrinter', @level2type = N'COLUMN', @level2name = N'PeelOffSet';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'9100', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'ZebraPrinter', @level2type = N'COLUMN', @level2name = N'Port';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'10.0.20.67', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'ZebraPrinter', @level2type = N'COLUMN', @level2name = N'IP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Принтеры Zebra', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'ZebraPrinter';

