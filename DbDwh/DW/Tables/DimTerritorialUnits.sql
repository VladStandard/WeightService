CREATE TABLE [DW].[TerritorialUnits]
(
	[TerritorialUnitID] [int] NOT NULL,
	[TerritorialUnit] [nvarchar](50) NOT NULL,
	CONSTRAINT [PK_TerritorialUnits] PRIMARY KEY CLUSTERED 	([TerritorialUnitID] ASC)
) ON [DIMFileGroup]

GO


