CREATE TABLE [db_scales].[AttributeDefinationList] (
    [Id]                INT             IDENTITY (1, 1) NOT NULL,
    [AttDefDescription] NVARCHAR (250)  NULL,
    [Code]              NVARCHAR (50)   NULL,
    [DefaultValue]      NVARCHAR (1000) NULL,
    [Notes]             NVARCHAR (1000) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) ON [ScalesFileGroup]
) ON [ScalesFileGroup];

