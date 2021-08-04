CREATE TABLE [db_scales].[Orders] (
    [Id]                             INT              IDENTITY (1, 1) NOT NULL,
    [OrderType]                      INT              DEFAULT ((0)) NULL,
    [ProductDate]                    DATETIME         NULL,
    [PlaneBoxCount]                  INT              NULL,
    [PlanePalletCount]               INT              NULL,
    [PlanePackingOperationBeginDate] DATETIME         NULL,
    [PlanePackingOperationEndDate]   DATETIME         NULL,
    [ScaleId]                        INT              NOT NULL,
    [PLU]                            INT              NOT NULL,
    [IdRRef]                         UNIQUEIDENTIFIER NULL,
    [TemplateId]                     VARCHAR (38)     NOT NULL,
    [CreateDate]                     DATETIME         DEFAULT (getdate()) NOT NULL,
    [ModifiedDate]                   DATETIME         DEFAULT (getdate()) NOT NULL,
    [Marked]                         BIT              DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) ON [ScalesFileGroup]
) ON [ScalesFileGroup];


GO
GRANT UPDATE
    ON OBJECT::[db_scales].[Orders] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT SELECT
    ON OBJECT::[db_scales].[Orders] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT INSERT
    ON OBJECT::[db_scales].[Orders] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT DELETE
    ON OBJECT::[db_scales].[Orders] TO [db_scales_users]
    AS [scales_owner];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Задания на фасовку (документ). Очень часто редактируется пользователем.', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'Orders';

