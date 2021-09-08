

CREATE TABLE [DW].[DimEmployees] (

	[FullName] [nvarchar](150) NULL,
	[PositionName] [nvarchar](150) NULL,
	[PositionID] varbinary(16) NULL,
	[DepartmentID] varbinary(16)   NULL,
	[OrgID] varbinary(16)  NOT NULL,

	[ID] int IDENTITY(-2147483648,1) NOT NULL,
	[CreateDate] datetime NOT NULL,
	[DLM] datetime  NOT NULL,
	[StatusID] int  NOT NULL,
	[InformationSystemID] int  NOT NULL,
	[CodeInIS] varbinary(16)  NOT NULL,

	[Birthday] DATE NULL, 
	[ДатаПриема] date,
	[ДатаУвольнения] date,

    PRIMARY KEY CLUSTERED ([InformationSystemID] ASC,[CodeInIS] ASC, [ID] ASC)

	
) on [DIMFileGroup]
GO



