CREATE TABLE [db_scales].[Nomenclature] (
    [Id]                             INT              NOT NULL,
    [Code]                           NVARCHAR (30)    NULL,
    [Name]                           NVARCHAR (300)   NULL,
    [IdRRef]                         UNIQUEIDENTIFIER NULL,
    [SerializedRepresentationObject] XML              NULL,
    [CreateDate]                     DATETIME         DEFAULT (getdate()) NULL,
    [ModifiedDate]                   DATETIME         DEFAULT (getdate()) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) ON [ScalesFileGroup]
) ON [ScalesFileGroup] TEXTIMAGE_ON [ScalesFileGroup];


GO
GRANT SELECT
    ON OBJECT::[db_scales].[Nomenclature] TO [db_scales_users]
    AS [scales_owner];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Справочник номенклатуры. Часто редактируется пользователем.', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'Nomenclature';

