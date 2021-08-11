CREATE TABLE [db_scales].[PLU]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[GoodsName]					[nvarchar](150) NULL,
	[GoodsFullName]				[nvarchar](max) NULL,
	[GoodsDescription]			[nvarchar](max) NULL,
	[TemplateID] INT			NULL FOREIGN KEY REFERENCES [db_scales].[Templates] (Id),
	[GTIN]						[varchar](150) NULL,
	[EAN13]						[varchar](150) NULL,
	[ITF14]						[varchar](150) NULL,
	[GoodsShelfLifeDays]		[tinyint] NULL,
	[GoodsTareWeight]			[decimal](10, 3) NULL,
	[GoodsBoxQuantly]			[int] NULL,
	[ScaleId] int				NOT NULL FOREIGN KEY REFERENCES [db_scales].[Scales] (Id),
	[NomenclatureId] int		NOT NULL FOREIGN KEY REFERENCES [db_scales].[Nomenclature] (Id),
	[Plu] int NOT NULL,
	[CreateDate] [datetime] NULL DEFAULT(GETDATE()),
	[ModifiedDate] [datetime] NULL DEFAULT(GETDATE()), 
    [Active] BIT DEFAULT 1,
	[UpperWeightThreshold] decimal(10, 3) NULL,
	[NominalWeight]			decimal(10, 3) NULL,
	[LowerWeightThreshold] decimal(10, 3) NULL,
	[CheckWeight] BIT DEFAULT 1,
	[Marked] BIT NOT NULL DEFAULT 0

) ON [ScalesFileGroup]

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Справочник единиц взвешивания на устройстве (весах). Часто изменяются пользователями.',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'PLU',
    @level2type = NULL,
    @level2name = NULL