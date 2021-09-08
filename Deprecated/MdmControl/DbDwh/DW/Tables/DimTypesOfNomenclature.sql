CREATE TABLE [DW].[DimTypesOfNomenclature]
(

	[Name] [nvarchar](150) NULL,
	[GoodsForSale] bit DEFAULT 0,
	[ID] [int] NOT NULL  IDENTITY(-2147483648,1),
	[CreateDate] datetime NOT NULL,
	[DLM] datetime  NOT NULL,
	[StatusID] int  NOT NULL,
	[InformationSystemID] int  NOT NULL,
	[CodeInIS] varbinary(16)  NOT NULL,
	
	PRIMARY KEY CLUSTERED ([InformationSystemID] ASC,[CodeInIS] ASC, [ID] ASC)

) on [DIMFileGroup]

GO

