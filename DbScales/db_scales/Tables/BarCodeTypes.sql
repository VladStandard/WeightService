CREATE TABLE [db_scales].[BarCodeTypes] (
    [Id]   INT            NOT NULL,
    [Name] NVARCHAR (150) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) ON [ScalesFileGroup]
) ON [ScalesFileGroup];


GO
GRANT SELECT
    ON OBJECT::[db_scales].[BarCodeTypes] TO [db_scales_users]
    AS [scales_owner];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Таблица для хранения перечня типов ШК. Не расширяется пользователями. Значения задаются при развертывании БД на сервере.', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'BarCodeTypes';

