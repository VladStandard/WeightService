CREATE TABLE [SCSL].[DimNormNomenclatures]
(

	[NormCodeNomenclature]			nvarchar(50),
	[NormNameNomenclature]			nvarchar(500),
	[NormArticleNomenclature]		nvarchar(50),
	[NormWeightNomenclature]		decimal(15, 3),
	[CodeInUPP]						nvarchar(50),
	[NomenclatureIDinUPP]			int,

	[Marked]						bit,
	[CHECKSUMM]						BIGINT,
	[NormNomenclatureID]			int	IDENTITY(-2147483648,1) NOT NULL,
	[CreateDate]					datetime NOT NULL DEFAULT GETDATE(),
	[DLM]							datetime  NOT NULL,
	[StatusID]						int  NOT NULL,
	[InformationSystemID]			int  NOT NULL,
	[NormNomenclatureIDinIS]		binary(16) NOT NULL

		-- MDM
	,RelevanceStatus tinyint default 0
	,NormalizationStatus tinyint default 0
	,MasterId int  NULL
	,CONSTRAINT chkDimNomenclaturesNormalizationStatus CHECK (NormalizationStatus in (0,1,2,3))
	,CONSTRAINT chkDimNomenclaturesRelevanceStatus CHECK (RelevanceStatus in (0,1,2))

	,CONSTRAINT [PK_DimNormNomenclatures_1] PRIMARY KEY CLUSTERED ([InformationSystemID],[NormNomenclatureIDinIS],[NormNomenclatureID] ASC)

) ON [SCSLFileGroup]

GO