CREATE TABLE [SCSL].[DimNormContragents]
(

	[NormCodeContragent]		nvarchar(50),
	[NormNameContragent]		nvarchar(150),
	[NormNameFullContragent]	nvarchar(300),
	[INN]						nvarchar(50),
	[KPP]						nvarchar(50),

	[Marked]					bit,
	[CHECKSUMM]					BIGINT,
	[NormContragentID]			int	IDENTITY(-2147483648,1) NOT NULL,
	[CreateDate]				datetime NOT NULL DEFAULT GETDATE(),
	[DLM]						datetime  NOT NULL,
	[StatusID]					int  NOT NULL,
	[InformationSystemID]		int  NOT NULL,
	[NormContragentIDinIS]		binary(16) NOT NULL

		-- MDM
	,RelevanceStatus tinyint default 0
	,NormalizationStatus tinyint default 0
	,MasterId int NULL
	,CONSTRAINT chkDimContragentsNormalizationStatus CHECK (NormalizationStatus in (0,1,2,3))
	,CONSTRAINT chkDimContragentsRelevanceStatus CHECK (RelevanceStatus in (0,1,2))

	,CONSTRAINT [PK_DimNormContragents_1] PRIMARY KEY CLUSTERED ([InformationSystemID],[NormContragentIDinIS],[NormContragentID] ASC)

) ON [SCSLFileGroup]

GO