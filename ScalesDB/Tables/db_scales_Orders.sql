


CREATE TABLE [db_scales].[Orders]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[OrderType] INT DEFAULT 0,

	[ProductDate] [datetime] NULL,
	[PlaneBoxCount] [int] NULL,
	[PlanePalletCount] [int] NULL,
	[PlanePackingOperationBeginDate] [datetime] NULL,
	[PlanePackingOperationEndDate] [datetime] NULL,

	[ScaleId] int NOT NULL, -- ссылка на линию и товар
	[PLU] INT NOT NULL,            --       
	[IdRRef] UNIQUEIDENTIFIER NULL, --ссылка на документ
	[TemplateId] VARCHAR(38) NOT NULL,
	[CreateDate]	datetime NOT NULL DEFAULT(GETDATE()),
	[ModifiedDate]	datetime NOT NULL DEFAULT(GETDATE()), 
    [Marked] BIT NOT NULL DEFAULT 0

) ON [ScalesFileGroup]
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Задания на фасовку (документ). Очень часто редактируется пользователем.',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'Orders',
    @level2type = NULL,
    @level2name = NULL