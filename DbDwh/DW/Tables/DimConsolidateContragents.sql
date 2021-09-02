CREATE TABLE [DW].[DimConsolidateContragents]
(
	[ConsolidatedClientID] [int] IDENTITY(-2147483648,1)  NOT NULL,
	[ConsolidatedClientName] [nvarchar](200) NULL,
	[INN] VARCHAR(15) NULL, 
    PRIMARY KEY CLUSTERED ([ConsolidatedClientID] ASC)

) ON [DIMFileGroup]
GO
