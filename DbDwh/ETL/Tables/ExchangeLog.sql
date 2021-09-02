
CREATE TABLE [ETL].[ExchangeLogs](
	[ExchangeLogID] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY CLUSTERED,
	[ErrorLevelID] [int] NULL,
	[Error] [nvarchar](255) NULL,
	[Info] [nvarchar](max) NULL,
	[Date] [datetime] NULL,
	[FromInformationSystemID] [int] NULL,
	[ToInformationSystemID] [int] NULL,
	[ObjectError] [nvarchar](255) NULL,
	[ErrorMessage] [nvarchar](255) NULL,
	[IsEmailSended] [bit] NULL
)  on [ETLFileGroup] TEXTIMAGE_ON [ETLFileGroup]
GO

ALTER TABLE [ETL].[ExchangeLogs] ADD  CONSTRAINT [DF_ExchangeLogs_Level]  DEFAULT ((1)) FOR [ErrorLevelID]
GO

ALTER TABLE [ETL].[ExchangeLogs] ADD  CONSTRAINT [DF_ExchangeLogs_Date]  DEFAULT (getdate()) FOR [Date]
GO

ALTER TABLE [ETL].[ExchangeLogs]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeLogs_FromInformationSystems] FOREIGN KEY ([FromInformationSystemID])
REFERENCES [ETL].[InformationSystems] ([InformationSystemID])
GO

ALTER TABLE [ETL].[ExchangeLogs] CHECK CONSTRAINT [FK_ExchangeLogs_FromInformationSystems]
GO

ALTER TABLE [ETL].[ExchangeLogs]  WITH CHECK ADD  CONSTRAINT [FK_ExchangeLogs_ToInformationSystems] FOREIGN KEY([ToInformationSystemID])
REFERENCES [ETL].[InformationSystems] ([InformationSystemID])
GO

ALTER TABLE [ETL].[ExchangeLogs] CHECK CONSTRAINT [FK_ExchangeLogs_ToInformationSystems]
GO


