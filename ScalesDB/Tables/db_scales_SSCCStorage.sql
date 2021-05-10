CREATE TABLE [db_scales].[SSCCStorage]
(
	[GLN] INT NOT NULL PRIMARY KEY, 
    [COUNTER] INT NOT NULL DEFAULT 0
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Устарело, не использовать',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'SSCCStorage',
    @level2type = NULL,
    @level2name = NULL