CREATE TABLE [db_scales].[ProductSeries]
(
	[Id]			INT NOT NULL PRIMARY KEY IDENTITY(-2147483648,1),
    [ScaleID]		INT NOT NULL FOREIGN KEY REFERENCES [db_scales].[Scales] (Id),
	[CreateDate]	DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    [UUID]			UNIQUEIDENTIFIER NULL DEFAULT NEWID(),
	[IsClose]       BIT DEFAULT 0 ,
	[SSCC]			VARCHAR(50) 

) ON [ScalesFileGroupJJ]
GO 

CREATE INDEX [ProductSeries_IDX2_SSCC]
	ON [db_scales].[ProductSeries]
	([SSCC],[Id])
GO
CREATE INDEX [ProductSeries_IDX1_ScaleID]
	ON [db_scales].[ProductSeries]
	([ScaleID],[Id])
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Серии взвешиваний продукциию. Недоступна пользователю на редактирование. Заполняется в процессе операции взвешивания.',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'ProductSeries',
    @level2type = NULL,
    @level2name = NULL