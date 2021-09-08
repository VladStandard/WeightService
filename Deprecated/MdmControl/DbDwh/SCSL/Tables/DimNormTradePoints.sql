CREATE TABLE [SCSL].[DimNormTradePoints]
(
	[NormCodeTradePoint]			nvarchar(50) NULL,
	[NormAddressTradePoint]			nvarchar(500) NULL,
	[NormNameTradePoint]			nvarchar(500) NULL,
	[TradeAgent]					nvarchar(250) NULL,
	[ChainName]						nvarchar(250) NULL,
	[SalesChannel]					nvarchar(250) NULL,
	[RegionID]						int NULL,
	[CityID]						int NULL,
	[SalesChannelID]				int NULL,
	[ChainID]						int NULL,
	[FormatTT]						nvarchar(250) NULL,
	[FormatTTID]					int NULL,

	[Marked]						bit,
	[CHECKSUMM]						BIGINT,
	[NormTradePointID]				int	IDENTITY(-2147483648,1) NOT NULL,
	[CreateDate]					datetime NOT NULL DEFAULT GETDATE(),
	[DLM]							datetime  NOT NULL,
	[StatusID]						int  NOT NULL,
	[InformationSystemID]			int  NOT NULL,
	[NormTradePointIDinIS]			binary(16) NOT NULL,

	-- MDM
	RelevanceStatus tinyint default 0
	,NormalizationStatus tinyint default 0
	,MasterId int  NULL
	,CONSTRAINT chkDimDeliveryPlacesRelevanceStatus CHECK (NormalizationStatus in (0,1,2,3))
	,CONSTRAINT chkDimDeliveryPlacesNormalizationStatus CHECK (RelevanceStatus in (0,1,2))

	,CONSTRAINT [PK_DimNormTradePoints_1] PRIMARY KEY CLUSTERED ([InformationSystemID],[NormTradePointIDinIS],[NormTradePointID] ASC)

) ON [SCSLFileGroup]

GO