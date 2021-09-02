CREATE TABLE [DW].[DimStorages]

(
	[Code] nvarchar(15),
	[Description] nvarchar(150),

	[Id] int NOT NULL IDENTITY(-2147483648,1),
	[CreateDate] datetime NOT NULL,
	[DLM] datetime  NOT NULL,
	[StatusID] int  NOT NULL,
	[InformationSystemID] int  NOT NULL,
	[CodeInIs] varbinary(16)  NOT NULL,

	PRIMARY KEY CLUSTERED ([InformationSystemID] ASC,[CodeInIs] ASC, [ID] ASC)

)on [DIMFileGroup]
GO


