CREATE TABLE [db_scales].[TemplateResources]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Name]			nvarchar(50),
    [Description]   nvarchar(500),
	[Type]			varchar(3),
	[ImageData]		varbinary(max),
	[CreateDate]	datetime NOT NULL DEFAULT GETDATE(),
	[ModifiedDate]  DATETIME NOT NULL DEFAULT GETDATE(), 
    [IdRRef] UNIQUEIDENTIFIER NULL,
    [Marked] BIT NOT NULL DEFAULT 0,
    CONSTRAINT [TemplateResources_check_type]  CHECK ([Type] IN ('GRF', 'TTF'))

) ON [ScalesFileGroup]

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Тип ресурса: ШРИФТ or ЛОГОТИП',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'TemplateResources',
    @level2type = N'COLUMN',
    @level2name = N'Type'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Ресурсы для загрузки в принтер ZEBRA вместе с шаблоном этикетки',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'TemplateResources',
    @level2type = NULL,
    @level2name = NULL