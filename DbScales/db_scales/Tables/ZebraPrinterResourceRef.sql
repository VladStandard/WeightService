CREATE TABLE [db_scales].[ZebraPrinterResourceRef] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [PrinterID]    INT            NOT NULL,
    [ResourceID]   INT            NOT NULL,
    [Description]  NVARCHAR (150) NULL,
    [CreateDate]   DATETIME       DEFAULT (getdate()) NOT NULL,
    [ModifiedDate] DATETIME       DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC) ON [ScalesFileGroup],
    FOREIGN KEY ([PrinterID]) REFERENCES [db_scales].[ZebraPrinter] ([Id]),
    FOREIGN KEY ([ResourceID]) REFERENCES [db_scales].[TemplateResources] ([Id]),
    CONSTRAINT [AK_ZebraPrinterResourceRef] UNIQUE NONCLUSTERED ([PrinterID] ASC, [ResourceID] ASC) ON [ScalesFileGroup]
) ON [ScalesFileGroup];


GO
GRANT UPDATE
    ON OBJECT::[db_scales].[ZebraPrinterResourceRef] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT SELECT
    ON OBJECT::[db_scales].[ZebraPrinterResourceRef] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT INSERT
    ON OBJECT::[db_scales].[ZebraPrinterResourceRef] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT DELETE
    ON OBJECT::[db_scales].[ZebraPrinterResourceRef] TO [db_scales_users]
    AS [scales_owner];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Связывает (много ко многим) принтеры с ресурсами, выгружаемыми на принтер', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'ZebraPrinterResourceRef';

