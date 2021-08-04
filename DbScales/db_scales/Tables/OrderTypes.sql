CREATE TABLE [db_scales].[OrderTypes] (
    [Id]          INT            NOT NULL,
    [Description] NVARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) ON [ScalesFileGroup]
) ON [ScalesFileGroup];


GO
GRANT SELECT
    ON OBJECT::[db_scales].[OrderTypes] TO [db_scales_users]
    AS [scales_owner];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Таблица для хранения видов документа "заказ на фасовку". Не расширяется пользователями вносить изменения. Значения задаются при развертывании БД на сервере. ID не требует настройки IDENTITY', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'OrderTypes';

