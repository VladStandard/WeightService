CREATE TABLE [DW].[DimExpenditures]
(

	[Name] [nvarchar](150) NULL,
	[ID] int NOT NULL IDENTITY(-2147483648,1),
	[CreateDate] datetime NOT NULL,
	[DLM] datetime  NOT NULL,
	[StatusID] int  NOT NULL,
	[InformationSystemID] int  NOT NULL,
	[CodeInIS] varbinary(16)  NOT NULL,

	PRIMARY KEY CLUSTERED ([InformationSystemID] ASC,[CodeInIS] ASC, [ID] ASC)

) on [DIMFileGroup]

GO

