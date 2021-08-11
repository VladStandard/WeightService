CREATE TABLE [db_scales].[BarCodeTypes]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[Name]	nvarchar(150)

) ON [ScalesFileGroup]

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Таблица для хранения перечня типов ШК. Не расширяется пользователями. Значения задаются при развертывании БД на сервере.',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'BarCodeTypes',
    @level2type = NULL,
    @level2name = NULL