CREATE TABLE [db_scales].[Errors] (
    [Id]             INT             IDENTITY (1, 1) NOT NULL,
    [CreatedDate]    DATETIME        DEFAULT (getdate()) NOT NULL,
    [ModifiedDate]   DATETIME        DEFAULT (getdate()) NOT NULL,
    [FilePath]       NVARCHAR (1024) NULL,
    [LineNumber]     SMALLINT        NULL,
    [MemberName]     NVARCHAR (128)  NULL,
    [Exception]      NVARCHAR (4000) NOT NULL,
    [InnerException] NVARCHAR (4000) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) ON [ScalesFileGroup]
) ON [ScalesFileGroup];

