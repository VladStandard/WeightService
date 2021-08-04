CREATE TABLE [db_scales].[OrderStatus] (
    [OrderId]       INT      NOT NULL,
    [CurrentDate]   DATETIME NOT NULL,
    [Id]            INT      IDENTITY (1, 1) NOT NULL,
    [CurrentStatus] TINYINT  NULL,
    CONSTRAINT [PK_OrderStatus] PRIMARY KEY CLUSTERED ([OrderId] ASC, [CurrentDate] ASC, [Id] ASC) ON [ScalesFileGroup]
) ON [ScalesFileGroup];


GO
GRANT UPDATE
    ON OBJECT::[db_scales].[OrderStatus] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT SELECT
    ON OBJECT::[db_scales].[OrderStatus] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT INSERT
    ON OBJECT::[db_scales].[OrderStatus] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT DELETE
    ON OBJECT::[db_scales].[OrderStatus] TO [db_scales_users]
    AS [scales_owner];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Состояние документа задание на фасовку. Текущее значение получать как срез последних.', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'OrderStatus';

