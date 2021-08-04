CREATE TABLE [db_scales].[ACCESS] (
    [UID]       UNIQUEIDENTIFIER DEFAULT (newid()) NOT NULL,
    [CREATE_DT] DATETIME         DEFAULT (getdate()) NOT NULL,
    [CHANGE_DT] DATETIME         DEFAULT (getdate()) NOT NULL,
    [USER]      NVARCHAR (32)    NOT NULL,
    [LEVEL]     BIT              NULL,
    PRIMARY KEY CLUSTERED ([UID] ASC) ON [ScalesFileGroup]
) ON [ScalesFileGroup];


GO
GRANT SELECT
    ON OBJECT::[db_scales].[ACCESS] TO [db_scales_users]
    AS [scales_owner];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Access.', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'ACCESS';

