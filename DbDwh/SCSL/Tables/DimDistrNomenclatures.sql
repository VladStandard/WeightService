
CREATE TABLE [SCSL].[DimDistrNomenclatures] (

	[NormNomenclatureID]		int NULL,
	[DistrCodeNomenclature]		nvarchar(50) NULL,
	[DistrNameNomenclature]		nvarchar(500) NULL,
	[DistrArticleNomenclature]	nvarchar(50) NULL,
	[UseInReport]				int NULL,
	[NormNomenclatureIdInIS]	binary(16) NULL,

	[Marked]					bit,
	[CHECKSUMM]					BIGINT,
	[NomenclatureID]			int	IDENTITY(-2147483648,1) NOT NULL,
	[CreateDate]				datetime NOT NULL DEFAULT GETDATE(),
	[DLM]						datetime  NOT NULL,
	[StatusID]					int  NOT NULL,
	[InformationSystemID]		int  NOT NULL,
	[DistrNomenclatureIDinIS]	binary(16) NOT NULL,

	CONSTRAINT [PK_DistrNomenclatures] PRIMARY KEY CLUSTERED ([InformationSystemID],[DistrNomenclatureIDinIS],[NomenclatureID] ASC)

) ON [SCSLFileGroup]
