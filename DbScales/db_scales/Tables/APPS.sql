CREATE TABLE [db_scales].[APPS] (
    [UID]  UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [NAME] NVARCHAR (32)    NOT NULL,
    PRIMARY KEY CLUSTERED ([UID] ASC) ON [ScalesFileGroup]
) ON [ScalesFileGroup];


GO
GRANT SELECT
    ON OBJECT::[db_scales].[APPS] TO [db_scales_users]
    AS [scales_owner];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'APPS', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'APPS';

