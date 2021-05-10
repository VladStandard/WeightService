CREATE TABLE [db_scales].[Templates]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[CategoryID]	NVARCHAR(150) NOT NULL,
    [IdRRef] UNIQUEIDENTIFIER NULL,
	[Title]			nvarchar(250),
	[ImageData]		varbinary(max),
	[CreateDate]	datetime NOT NULL DEFAULT(GETDATE()),
	[ModifiedDate]	datetime NOT NULL DEFAULT(GETDATE()),
    [Marked] BIT NOT NULL DEFAULT 0

) ON [ScalesFileGroup]

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Шаблоны этикетки в XSLT.',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'Templates',
    @level2type = NULL,
    @level2name = NULL