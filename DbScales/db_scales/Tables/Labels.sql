CREATE TABLE [db_scales].[Labels] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [WeithingFactId] INT             NOT NULL,
    [Label]          VARBINARY (MAX) NULL,
    [CreateDate]     DATETIME        DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) ON [ScalesFileGroupLargeData],
    FOREIGN KEY ([WeithingFactId]) REFERENCES [db_scales].[WeithingFact] ([Id])
) ON [ScalesFileGroupLargeData] TEXTIMAGE_ON [ScalesFileGroupLargeData];


GO
GRANT SELECT
    ON OBJECT::[db_scales].[Labels] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT INSERT
    ON OBJECT::[db_scales].[Labels] TO [db_scales_users]
    AS [scales_owner];

