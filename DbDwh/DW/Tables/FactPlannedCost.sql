CREATE TABLE [DW].[FactPlannedCost]
(
	[Marked]	bit,
	[Posted]	bit,
	[DocNum]	varchar(15),
	[DocDate]	datetime,
	[NomenclatureInIS] binary(16),
	[UnitInIS]	binary(16),
	[Price]		decimal(15,3),

	[NomenclatureId] int,
	[NomenclatureName] nvarchar(150),
	[UnitId] int,
	[UnitName]  nvarchar(150),

	[ID] BIGINT NOT NULL IDENTITY(-9223372036854775808,1),
	[CreateDate]	datetime NOT NULL,
	[DLM]			datetime  NOT NULL,
	[StatusID]		int  NOT NULL,
	[InformationSystemID] int NOT NULL,
	[CodeInIS]		binary(16) NOT NULL,
	[CHECKSUMM]		bigint,
	[LineNo100]		int,

	PRIMARY KEY CLUSTERED ([InformationSystemID] ASC,[CodeInIS] ASC,[LineNo100] ASC, [ID] ASC)

) ON [FACTFileGrooup]
