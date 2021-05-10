CREATE TABLE [db_scales].[OrderTypes]
(
	
	[Id] INT NOT NULL PRIMARY KEY,
	[Description] nvarchar(250)

) ON [ScalesFileGroup]
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Таблица для хранения видов документа "заказ на фасовку". Не расширяется пользователями вносить изменения. Значения задаются при развертывании БД на сервере. ID не требует настройки IDENTITY',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'OrderTypes',
    @level2type = NULL,
    @level2name = NULL