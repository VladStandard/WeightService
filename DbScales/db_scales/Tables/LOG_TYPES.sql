CREATE TABLE [db_scales].[LOG_TYPES] (
    [UID]    UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [NUMBER] TINYINT          NOT NULL,
    [ICON]   NVARCHAR (32)    NOT NULL,
    PRIMARY KEY CLUSTERED ([UID] ASC) ON [ScalesFileGroup],
    UNIQUE NONCLUSTERED ([NUMBER] ASC) ON [ScalesFileGroup]
) ON [ScalesFileGroup];


GO
GRANT SELECT
    ON OBJECT::[db_scales].[LOG_TYPES] TO [db_scales_users]
    AS [scales_owner];

