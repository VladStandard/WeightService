CREATE TABLE [db_scales].[Templates] (
    [Id]           INT              IDENTITY (1, 1) NOT NULL,
    [CategoryID]   NVARCHAR (150)   NOT NULL,
    [IdRRef]       UNIQUEIDENTIFIER NULL,
    [Title]        NVARCHAR (250)   NULL,
    [ImageData]    VARBINARY (MAX)  NULL,
    [CreateDate]   DATETIME         DEFAULT (getdate()) NOT NULL,
    [ModifiedDate] DATETIME         DEFAULT (getdate()) NOT NULL,
    [Marked]       BIT              DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) ON [ScalesFileGroup]
) ON [ScalesFileGroup] TEXTIMAGE_ON [ScalesFileGroup];


GO
GRANT UPDATE
    ON OBJECT::[db_scales].[Templates] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT SELECT
    ON OBJECT::[db_scales].[Templates] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT INSERT
    ON OBJECT::[db_scales].[Templates] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT DELETE
    ON OBJECT::[db_scales].[Templates] TO [db_scales_users]
    AS [scales_owner];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Шаблоны этикетки в XSLT.', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'Templates';

