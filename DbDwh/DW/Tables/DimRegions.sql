
CREATE TABLE [DW].[DimRegions](

	[RegionName] [nvarchar](150) NULL,
	[ID] [int] NOT NULL IDENTITY(-2147483648,1),
    [Code] NVARCHAR(9) NULL, 

	[CreateDate] datetime NOT NULL,
	[DLM] datetime  NOT NULL,
	[StatusID] int  NOT NULL,
	[InformationSystemID] int  NOT NULL,
	[CodeInIS] varbinary(16)  NOT NULL,

	[TerritorialUnitID] INT NULL DEFAULT 9, 

    PRIMARY KEY CLUSTERED ([InformationSystemID] ASC,[CodeInIS] ASC, [ID] ASC)


) on [DIMFileGroup]

GO

