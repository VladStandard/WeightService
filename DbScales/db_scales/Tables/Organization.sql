CREATE TABLE [db_scales].[Organization] (
    [Id]                             INT            NOT NULL,
    [Name]                           NVARCHAR (150) NOT NULL,
    [Marked]                         BIT            DEFAULT ((0)) NULL,
    [GLN]                            INT            NOT NULL,
    [SerializedRepresentationObject] XML            NULL,
    [CreateDate]                     DATETIME       DEFAULT (getdate()) NULL,
    [ModifiedDate]                   DATETIME       DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) ON [ScalesFileGroup]
) ON [ScalesFileGroup] TEXTIMAGE_ON [ScalesFileGroup];


GO
GRANT SELECT
    ON OBJECT::[db_scales].[Organization] TO [db_scales_users]
    AS [scales_owner];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Справочник организаций (ВС, Владимирский стандарт)', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'Organization';

