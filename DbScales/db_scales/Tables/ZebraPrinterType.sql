CREATE TABLE [db_scales].[ZebraPrinterType] (
    [Id]   INT            NOT NULL,
    [Name] NVARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) ON [ScalesFileGroup]
) ON [ScalesFileGroup];


GO
GRANT SELECT
    ON OBJECT::[db_scales].[ZebraPrinterType] TO [db_scales_users]
    AS [scales_owner];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Модели принтеров Zebra, их редактируем через скрипт один раз в 100 лет', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'ZebraPrinterType';

