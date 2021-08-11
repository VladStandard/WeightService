CREATE TABLE [db_scales].[WeithingFact]
(

	[Id]			INT NOT NULL PRIMARY KEY IDENTITY(-2147483648,1),
	[PluId]			INT NOT NULL,
    [ScaleId]		INT     NOT NULL FOREIGN KEY REFERENCES [db_scales].[Scales] (Id),
    [SeriesId]		INT     NULL FOREIGN KEY REFERENCES [db_scales].[ProductSeries] (Id),
	[OrderId]		INT     NULL FOREIGN KEY REFERENCES [db_scales].[Orders] (Id),
	[SSCC]			VARCHAR(50) NOT NULL,
	[WeithingDate]	DATETIME2,
	[NetWeight]		NUMERIC(15, 3) NOT NULL, 
    [TareWeight]	NUMERIC(15, 3) NULL, 
    [UUID]			UNIQUEIDENTIFIER NULL DEFAULT NEWID(), 
    [ProductDate]	DATE NULL, 
	[RegNum]		INT, 
    [Kneading] INT NULL
   

) ON [ScalesFileGroupJJ]
GO

CREATE INDEX [WeithingFact_IDX1_PackageIDProductDate]
	ON [db_scales].[WeithingFact]
	([SeriesID],[ProductDate],[Id])
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Регистрация факта взвешивания',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'WeithingFact',
    @level2type = NULL,
    @level2name = NULL
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'ссылка на PLU',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'WeithingFact',
    @level2type = N'COLUMN',
    @level2name = N'PluId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'ссылка на весы',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'WeithingFact',
    @level2type = N'COLUMN',
    @level2name = N'ScaleId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'ссылка на задание (если есть)',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'WeithingFact',
    @level2type = N'COLUMN',
    @level2name = N'OrderID'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'SSCC код',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'WeithingFact',
    @level2type = N'COLUMN',
    @level2name = N'SSCC'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'дата взвешивания',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'WeithingFact',
    @level2type = N'COLUMN',
    @level2name = N'WeithingDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'вес нетто',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'WeithingFact',
    @level2type = N'COLUMN',
    @level2name = N'NetWeight'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'вес тары',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'WeithingFact',
    @level2type = N'COLUMN',
    @level2name = N'TareWeight'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'дата производства',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'WeithingFact',
    @level2type = N'COLUMN',
    @level2name = N'ProductDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'ссылка на производственную серию',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'WeithingFact',
    @level2type = N'COLUMN',
    @level2name = N'SeriesId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'замес',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'WeithingFact',
    @level2type = N'COLUMN',
    @level2name = N'Kneading'
GO
