CREATE TABLE [db_scales].[Scales] (
    [Id]                   INT              IDENTITY (1, 1) NOT NULL,
    [Description]          NVARCHAR (150)   NULL,
    [IdRRef]               UNIQUEIDENTIFIER NULL,
    [DeviceIP]             VARCHAR (15)     NULL,
    [DevicePort]           SMALLINT         NULL,
    [DeviceMAC]            VARCHAR (35)     NULL,
    [OrganizationId]       INT              NULL,
    [DeviceSendTimeout]    SMALLINT         NULL,
    [DeviceReceiveTimeout] SMALLINT         NULL,
    [DeviceComPort]        VARCHAR (5)      NULL,
    [ZebraIP]              VARCHAR (15)     NULL,
    [ZebraPort]            SMALLINT         NULL,
    [UseOrder]             SMALLINT         DEFAULT ((1)) NULL,
    [VerScalesUI]          VARCHAR (30)     NULL,
    [DeviceNumber]         INT              NULL,
    [CreateDate]           DATETIME         DEFAULT (getdate()) NOT NULL,
    [ModifiedDate]         DATETIME         DEFAULT (getdate()) NOT NULL,
    [TemplateIdDefault]    INT              NULL,
    [TemplateIdSeries]     INT              NULL,
    [ScaleFactor]          INT              NULL,
    [WorkShopId]           INT              DEFAULT ((0)) NOT NULL,
    [ZebraPrinterId]       INT              NULL,
    [Marked]               BIT              DEFAULT ((0)) NOT NULL,
    [HostId]               INT              NULL,
    [LOG_TYPE_UID]         UNIQUEIDENTIFIER NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) ON [ScalesFileGroup],
    FOREIGN KEY ([HostId]) REFERENCES [db_scales].[Hosts] ([Id]),
    FOREIGN KEY ([OrganizationId]) REFERENCES [db_scales].[Organization] ([Id]),
    FOREIGN KEY ([TemplateIdDefault]) REFERENCES [db_scales].[Templates] ([Id]),
    FOREIGN KEY ([TemplateIdSeries]) REFERENCES [db_scales].[Templates] ([Id]),
    FOREIGN KEY ([WorkShopId]) REFERENCES [db_scales].[WorkShop] ([Id]),
    FOREIGN KEY ([ZebraPrinterId]) REFERENCES [db_scales].[ZebraPrinter] ([Id])
) ON [ScalesFileGroup];


GO
GRANT UPDATE
    ON OBJECT::[db_scales].[Scales] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT SELECT
    ON OBJECT::[db_scales].[Scales] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT INSERT
    ON OBJECT::[db_scales].[Scales] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT DELETE
    ON OBJECT::[db_scales].[Scales] TO [db_scales_users]
    AS [scales_owner];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ссылка на цех', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'Scales', @level2type = N'COLUMN', @level2name = N'WorkShopId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'коэффициент (обычно 1000)', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'Scales', @level2type = N'COLUMN', @level2name = N'ScaleFactor';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ссылка на шаблон при печати групповой этикетки на серию', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'Scales', @level2type = N'COLUMN', @level2name = N'TemplateIdSeries';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ссылка на шаблон этикетки', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'Scales', @level2type = N'COLUMN', @level2name = N'TemplateIdDefault';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'дата изменения', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'Scales', @level2type = N'COLUMN', @level2name = N'ModifiedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'дата создания', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'Scales', @level2type = N'COLUMN', @level2name = N'CreateDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'номер устройства в цеху (часто совпадает с номером линии)', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'Scales', @level2type = N'COLUMN', @level2name = N'DeviceNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Используется задание на фасовку на этой линии', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'Scales', @level2type = N'COLUMN', @level2name = N'UseOrder';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PORT принтера ZEBRA', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'Scales', @level2type = N'COLUMN', @level2name = N'ZebraPort';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IP принтера ZEBRA', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'Scales', @level2type = N'COLUMN', @level2name = N'ZebraIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Настройка подключения к свистку весовой платформы', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'Scales', @level2type = N'COLUMN', @level2name = N'DeviceComPort';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Настройки задержки опроса весовой платформы', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'Scales', @level2type = N'COLUMN', @level2name = N'DeviceSendTimeout';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'MAC моноблока', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'Scales', @level2type = N'COLUMN', @level2name = N'DeviceMAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'PORT моноблока', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'Scales', @level2type = N'COLUMN', @level2name = N'DevicePort';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'IP моноблока', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'Scales', @level2type = N'COLUMN', @level2name = N'DeviceIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ссылка на ключ устройства в 1С. для интеграции', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'Scales', @level2type = N'COLUMN', @level2name = N'IdRRef';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Наименование устройства', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'Scales', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Справочник устройств взвешивания (весов). Часто редактируется Power Users.', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'Scales';

