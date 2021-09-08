CREATE TABLE [DW].[FactBalance]
(
	[DateID]			int NOT NULL,
	[IsActualPeriod]	bit, 
	[TotalPeriod]		datetime, 
	[CurrentDate]		datetime NOT NULL, 
	[OrgID]				binary(16) NOT NULL, 
	[NomenclatureID]	binary(16) NOT NULL, 
	[StorageID]			binary(16) NOT NULL,

	[_DateID]				date,
    [_OrgID]				int,
    [_NomenclatureID]		int,
    [_StorageID]			int,

	[Balance]			decimal(15,3),
    [CHECKSUMM]			BIGINT NOT NULL,

	[ID] BIGINT NOT NULL IDENTITY(-9223372036854775808,1),
	[CreateDate]			datetime NOT NULL,
	[DLM]					datetime NOT NULL,
	[StatusID]				int NOT NULL,
	[InformationSystemID]	int NOT NULL,

	PRIMARY KEY CLUSTERED (
		[InformationSystemID] ASC, 
		[OrgID] ASC,
		[StorageID] ASC,
		[NomenclatureID] ASC,
		[DateID] ASC,
		[ID] ASC
	)
 ) on [FACTFileGrooup]
GO

CREATE NONCLUSTERED INDEX [DWFactBalance_Dim01] ON [DW].[FactBalance]
(
	[InformationSystemID] ASC,
	[DateID] ASC,
    [OrgID]	 ASC,
	[StorageID] ASC,
    [NomenclatureID] ASC

 ) on [FACTFileGrooup]
GO

