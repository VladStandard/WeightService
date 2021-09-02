
CREATE TABLE [ETL].[InformationSystems](

	[InformationSystemID] [int] NOT NULL  PRIMARY KEY CLUSTERED,
	[Name] [nvarchar](255) NOT NULL,
	[ConnectString1] [nvarchar](2048) NULL,
	[ConnectString2] [nvarchar](2048) NULL,
	[ConnectString3] [nvarchar](2048) NULL,
	[StatusID] [int] NOT NULL

)  on [ETLFileGroup]
GO

