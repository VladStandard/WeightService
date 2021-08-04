CREATE TABLE [db_scales].[AttributeValues] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [AttrDefinionID] INT            NOT NULL,
    [OrderID]        INT            NOT NULL,
    [Value]          NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) ON [ScalesFileGroup]
) ON [ScalesFileGroup] TEXTIMAGE_ON [ScalesFileGroup];

