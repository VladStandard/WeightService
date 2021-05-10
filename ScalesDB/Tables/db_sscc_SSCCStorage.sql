CREATE TABLE [db_sscc].[SSCCStorage]
(
	[GLN] INT NOT NULL PRIMARY KEY, 
    [COUNTER] INT NOT NULL DEFAULT 0
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'таблица обеспечивает хранение текушей упаковки продукции в рамках предприятия',
    @level0type = N'SCHEMA',
    @level0name = N'db_sscc',
    @level1type = N'TABLE',
    @level1name = N'SSCCStorage',
    @level2type = NULL,
    @level2name = NULL