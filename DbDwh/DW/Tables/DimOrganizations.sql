CREATE TABLE [DW].[DimOrganizations]
(
	[Id] int NOT NULL IDENTITY(-2147483648,1),
	[Description] nvarchar(150),

	[CreateDate] datetime NOT NULL,
	[DLM] datetime  NOT NULL,
	[StatusID] int  NOT NULL,
	[InformationSystemID] int  NOT NULL,
	[CodeInIS] varbinary(16)  NOT NULL,

	PRIMARY KEY CLUSTERED ([InformationSystemID] ASC,[CodeInIS] ASC, [Id] ASC)


)on [DIMFileGroup]


GO


