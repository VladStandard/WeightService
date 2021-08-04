CREATE TABLE [db_scales].[Hosts] (
    [ID]           INT              IDENTITY (1, 1) NOT NULL,
    [NAME]         NVARCHAR (150)   NULL,
    [IP]           VARCHAR (15)     NULL,
    [MAC]          VARCHAR (35)     NULL,
    [IdRRef]       UNIQUEIDENTIFIER NOT NULL,
    [CreateDate]   DATETIME         DEFAULT (getdate()) NOT NULL,
    [ModifiedDate] DATETIME         DEFAULT (getdate()) NOT NULL,
    [MARKED]       BIT              DEFAULT 0 NOT NULL,
    [SettingsFile] XML              NULL,
    [IS_DEBUG] BIT NOT NULL DEFAULT 0, 
    PRIMARY KEY CLUSTERED ([Id] ASC) ON [ScalesFileGroup],
    UNIQUE NONCLUSTERED ([IdRRef] ASC) ON [ScalesFileGroup]
) ON [ScalesFileGroup] TEXTIMAGE_ON [ScalesFileGroup];


GO
GRANT UPDATE
    ON OBJECT::[db_scales].[Hosts] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT SELECT
    ON OBJECT::[db_scales].[Hosts] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT INSERT
    ON OBJECT::[db_scales].[Hosts] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT DELETE
    ON OBJECT::[db_scales].[Hosts] TO [db_scales_users]
    AS [scales_owner];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Справочник моноблоков.', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'Hosts';

