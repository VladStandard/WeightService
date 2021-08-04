CREATE TABLE [db_scales].[LOGS] (
    [UID]          UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [CREATE_DT]    DATETIME         DEFAULT (getdate()) NOT NULL,
    [HOST_ID]      INT              NULL,
    [APP_UID]      UNIQUEIDENTIFIER NULL,
    [VERSION]      NVARCHAR (12)    NULL,
    [FILE]         NVARCHAR (32)    NOT NULL,
    [LINE]         INT              NOT NULL,
    [MEMBER]       NVARCHAR (32)    NOT NULL,
    [LOG_TYPE_UID] UNIQUEIDENTIFIER NULL,
    [MESSAGE]      NVARCHAR (1024)  NOT NULL,
    PRIMARY KEY CLUSTERED ([UID] ASC) ON [ScalesFileGroup],
    FOREIGN KEY ([APP_UID]) REFERENCES [db_scales].[APPS] ([UID]),
    FOREIGN KEY ([HOST_ID]) REFERENCES [db_scales].[Hosts] ([Id]),
    FOREIGN KEY ([LOG_TYPE_UID]) REFERENCES [db_scales].[LOG_TYPES] ([UID])
) ON [ScalesFileGroup];


GO
GRANT SELECT
    ON OBJECT::[db_scales].[LOGS] TO [db_scales_users]
    AS [scales_owner];

