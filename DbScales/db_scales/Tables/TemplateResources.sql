CREATE TABLE [db_scales].[TemplateResources] (
    [Id]           INT              IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50)    NULL,
    [Description]  NVARCHAR (500)   NULL,
    [Type]         VARCHAR (3)      NULL,
    [ImageData]    VARBINARY (MAX)  NULL,
    [CreateDate]   DATETIME         DEFAULT (getdate()) NOT NULL,
    [ModifiedDate] DATETIME         DEFAULT (getdate()) NOT NULL,
    [IdRRef]       UNIQUEIDENTIFIER NULL,
    [Marked]       BIT              DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) ON [ScalesFileGroup],
    CONSTRAINT [TemplateResources_check_type] CHECK ([Type]='TTF' OR [Type]='GRF')
) ON [ScalesFileGroup] TEXTIMAGE_ON [ScalesFileGroup];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Тип ресурса: ШРИФТ or ЛОГОТИП', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'TemplateResources', @level2type = N'COLUMN', @level2name = N'Type';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Ресурсы для загрузки в принтер ZEBRA вместе с шаблоном этикетки', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'TemplateResources';

