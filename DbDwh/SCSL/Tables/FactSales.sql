CREATE TABLE [SCSL].[FactSales]
(

	[DocumentNumber]			nvarchar(50) NULL,
	[DocumentDate]				date NULL,
	[DeliveryDate]				date NULL,
	[DistrTradePointIDinIS]		binary(16) NULL,
	[DistrNomenclatureIDinIS]	binary(16) NULL,
	[SubdivisionIDinIS]			binary(16) NULL,
	[AgentDistrIDinIS]			binary(16) NULL,
	[ContragentIDinIS]			binary(16) NULL,
	[TradePointID]				int NULL,
	[NomenclatureID]			int NULL,
	[SubdivisionID]				int NULL,
	[AgentID]					int NULL,
	[ContragentID]				int NULL,
	[Qty]						decimal(15, 3) NULL,
	[Price]						decimal(15, 3) NULL,
	[Cost]						decimal(15, 3) NULL,
	[Volume]					decimal(15, 3) NULL,
	[isSecondarySales]			int NULL,
	[UseNDS]					int NULL,
	[PriceWithNDS]				int NULL,

	[Marked]						bit,
	[Posted]						bit,
	[CHECKSUMM]						BIGINT,
	[ID]							bigint	IDENTITY(-9223372036854775808,1) NOT NULL,
	[CreateDate]					datetime NOT NULL DEFAULT GETDATE(),
	[DLM]							datetime  NOT NULL,
	[StatusID]						int  NOT NULL,
	[InformationSystemID]			int  NOT NULL,
	[IDinIS]						binary(16) NOT NULL,
	[DateID]						int NOT NULL,
	[LineNo]						int NOT NULL,
	[Active]						bit,


	CONSTRAINT [PK_FactSales_1] PRIMARY KEY CLUSTERED ([InformationSystemID],[IDinIS],[LineNo],[ID] ASC)

) ON [SCSLFileGroup]

GO