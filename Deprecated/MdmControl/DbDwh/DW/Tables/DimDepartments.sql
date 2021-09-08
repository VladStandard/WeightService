CREATE TABLE [DW].[DimDepartments]
(
	[Code] nvarchar(15),
	[Name] nvarchar(150),
	[OrganizationID] varbinary(16),
	[Level] int,

	[Id] INT NOT NULL IDENTITY(-2147483648,1) ,
	[CreateDate] datetime NOT NULL,
	[DLM] datetime  NOT NULL,
	[StatusID] int  NOT NULL,
	[InformationSystemID] int  NOT NULL,
	[CodeInIs] varbinary(16)  NOT NULL,
	[ParentCodeInIS] varbinary(16)  NOT NULL

	PRIMARY KEY CLUSTERED ([InformationSystemID] ASC,[CodeInIs] ASC, [ID] ASC)

)on [DIMFileGroup]
GO

