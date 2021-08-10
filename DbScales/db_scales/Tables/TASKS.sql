CREATE TABLE [db_scales].[TASKS] (
    [UID]    UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [TASK_UID]   UNIQUEIDENTIFIER NOT NULL,
    [SCALE_ID]   INT NOT NULL,
    FOREIGN KEY ([TASK_UID]) REFERENCES [db_scales].[TASKS_TYPES] ([UID]),
    FOREIGN KEY ([SCALE_ID]) REFERENCES [db_scales].[SCALES] ([ID]),
    PRIMARY KEY CLUSTERED ([UID] ASC) ON [ScalesFileGroup],
) ON [ScalesFileGroup];


GO
GRANT SELECT ON OBJECT::[db_scales].[TASKS] TO [db_scales_users] AS [scales_owner];
