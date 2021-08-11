CREATE TABLE [db_scales].[Scales]
(
	[Id]                    INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Description]	        NVARCHAR(150) ,
	[IdRRef]                UNIQUEIDENTIFIER NULL,
	[DeviceIP]		        VARCHAR(15),
	[DevicePort]	        SMALLINT,
	[DeviceMAC]		        VARCHAR(35),
    [OrganizationId]        INT NULL FOREIGN KEY REFERENCES [db_scales].[Organization] (Id),

	[DeviceSendTimeout]		SMALLINT,
	[DeviceReceiveTimeout]	SMALLINT,
	[DeviceComPort]			VARCHAR(5),
	[ZebraIP]				VARCHAR(15),
	[ZebraPort]				SMALLINT, 
    [UseOrder]              SMALLINT NULL DEFAULT 1,
	[VerScalesUI]		    VARCHAR(30), 
    [DeviceNumber]          INT NULL, 
	[CreateDate]            DATETIME NOT NULL DEFAULT GETDATE(),
	[ModifiedDate]          DATETIME NOT NULL DEFAULT GETDATE(),
	[TemplateIdDefault]     INT NULL FOREIGN KEY REFERENCES [db_scales].[Templates] (Id),
	[TemplateIdSeries]      INT NULL FOREIGN KEY REFERENCES [db_scales].[Templates] (Id), 
    [ScaleFactor]           INT NULL,
	[WorkShopId]            INT NOT NULL FOREIGN KEY REFERENCES [db_scales].[WorkShop] (Id) DEFAULT 0,
    [ZebraPrinterId]        INT NULL FOREIGN KEY REFERENCES [db_scales].[ZebraPrinter](Id),
    [Marked]                BIT NOT NULL DEFAULT 0,
    [HostId]                INT NULL FOREIGN KEY REFERENCES [db_scales].[Hosts]([ID]), 
    [LOG_TYPE] TINYINT NOT NULL DEFAULT 0 FOREIGN KEY REFERENCES [db_scales].[LOG_TYPES]([NUMBER]),

) ON [ScalesFileGroup]
GO

CREATE UNIQUE NONCLUSTERED INDEX IDX_Scales_HostId 
ON [db_scales].[Scales] ([HostId])
WHERE [HostId] IS NOT NULL

GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Справочник устройств взвешивания (весов). Часто редактируется Power Users.',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'Scales',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'коэффициент (обычно 1000)',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'Scales',
    @level2type = N'COLUMN',
    @level2name = N'ScaleFactor'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'ссылка на шаблон при печати групповой этикетки на серию',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'Scales',
    @level2type = N'COLUMN',
    @level2name = N'TemplateIdSeries'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'ссылка на шаблон этикетки',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'Scales',
    @level2type = N'COLUMN',
    @level2name = N'TemplateIdDefault'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'дата изменения',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'Scales',
    @level2type = N'COLUMN',
    @level2name = N'ModifiedDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'дата создания',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'Scales',
    @level2type = N'COLUMN',
    @level2name = N'CreateDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'номер устройства в цеху (часто совпадает с номером линии)',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'Scales',
    @level2type = N'COLUMN',
    @level2name = N'DeviceNumber'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Используется задание на фасовку на этой линии',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'Scales',
    @level2type = N'COLUMN',
    @level2name = N'UseOrder'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Настройка подключения к свистку весовой платформы',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'Scales',
    @level2type = N'COLUMN',
    @level2name = N'DeviceComPort'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'IP принтера ZEBRA',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'Scales',
    @level2type = N'COLUMN',
    @level2name = N'ZebraIP'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'PORT принтера ZEBRA',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'Scales',
    @level2type = N'COLUMN',
    @level2name = N'ZebraPort'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Наименование устройства',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'Scales',
    @level2type = N'COLUMN',
    @level2name = N'Description'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'ссылка на ключ устройства в 1С. для интеграции',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'Scales',
    @level2type = N'COLUMN',
    @level2name = N'IdRRef'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'IP моноблока',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'Scales',
    @level2type = N'COLUMN',
    @level2name = N'DeviceIP'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'PORT моноблока',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'Scales',
    @level2type = N'COLUMN',
    @level2name = N'DevicePort'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'MAC моноблока',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'Scales',
    @level2type = N'COLUMN',
    @level2name = N'DeviceMAC'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Настройки задержки опроса весовой платформы',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'Scales',
    @level2type = N'COLUMN',
    @level2name = N'DeviceSendTimeout'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'ссылка на цех',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'Scales',
    @level2type = N'COLUMN',
    @level2name = N'WorkShopId'