CREATE TABLE [db_scales].[OrderStatus]
(
	
	[OrderId] int NOT NULL,
	[CurrentDate] datetime NOT NULL,
	[Id] INT NOT NULL IDENTITY(1,1),
	[CurrentStatus] TINYINT,
	CONSTRAINT  PK_OrderStatus  PRIMARY KEY CLUSTERED  ([OrderId],[CurrentDate],[Id])

) ON [ScalesFileGroup]


GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Состояние документа задание на фасовку. Текущее значение получать как срез последних.',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'OrderStatus',
    @level2type = NULL,
    @level2name = NULL