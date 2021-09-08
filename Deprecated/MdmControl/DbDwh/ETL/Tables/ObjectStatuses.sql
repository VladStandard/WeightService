
CREATE TABLE [ETL].[ObjectStatuses](
	[DocumentStatusID] [int] IDENTITY (1,1) NOT NULL ,
	[InformationSystemID] [int] NOT NULL,
	[PackageID] varchar(38)  NOT NULL,
	[Name] [nvarchar](255) NOT NULL,

	[StatusID] [int] NOT NULL,
	[LastID] [bigint] NULL,
	[LastDateID] [int] NULL, 
    [LastDate] DATETIME NULL,

	[DLM] DATETIME NULL DEFAULT GETDATE(), 
	[Description] nvarchar(150),

    PRIMARY KEY CLUSTERED ([InformationSystemID] ASC, [Name] ASC, [DocumentStatusID] ASC, [PackageID] ASC)


) on [ETLFileGroup]
GO


