CREATE TABLE [db_scales].[TASKS_TYPES] (
    [UID]    UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [NAME]   NVARCHAR (32)    NOT NULL,
    PRIMARY KEY CLUSTERED ([UID] ASC) ON [ScalesFileGroup],
    UNIQUE NONCLUSTERED ([NAME] ASC) ON [ScalesFileGroup]
) ON [ScalesFileGroup];


GO
GRANT SELECT ON OBJECT::[db_scales].[TASKS_TYPES] TO [db_scales_users] AS [scales_owner];
