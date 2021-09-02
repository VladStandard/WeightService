CREATE TABLE [SCSL].[DimDistrTradePoints] (

	[NormTradePointIDinIS]		binary(16) NULL,
	[NormTradePointID]			int NULL,
	[DistrCodeTradePoint]		nvarchar(50) NULL,
	[DistrNameTradePoint]		nvarchar(500) NULL,
	[DistrAddressTradePoint]	nvarchar(500) NULL,
	[UseInRerort]				int NULL,

	[Marked]					bit,
	[CHECKSUMM]					BIGINT,
	[TradePointID]				int	IDENTITY(-2147483648,1) NOT NULL,
	[CreateDate]				datetime NOT NULL DEFAULT GETDATE(),
	[DLM]						datetime  NOT NULL,
	[StatusID]					int  NOT NULL,
	[InformationSystemID]		int  NOT NULL,
	[DistrTradePointIDinIS]		binary(16) NOT NULL,

	CONSTRAINT [PK_DistrTradePoints_1] PRIMARY KEY CLUSTERED ([InformationSystemID],[DistrTradePointIDinIS],[TradePointID] ASC)

) ON [SCSLFileGroup]
