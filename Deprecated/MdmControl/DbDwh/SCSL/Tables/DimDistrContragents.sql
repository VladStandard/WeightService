CREATE TABLE [SCSL].[DimDistrContragents]
(
	[Marked]					bit,
	[NormContragentIDinIS]		[binary](16) NULL,
	[NormContragentID]			[int]        NULL,
	[DistrCodeContragent]		[nvarchar](50)  NULL,
	[DistrNameContragent]		[nvarchar](500) NULL,
	[DistrNameFullContragent]	[nvarchar](500) NULL,
	[INN]						[nvarchar](50)		NULL,
	[KPP]						[nvarchar](50)		NULL,
	[UseInReport]				[int]				NULL,

	[ID]							int IDENTITY(-2147483648,1) NOT NULL,
	[CHECKSUMM]						BIGINT NOT NULL,
	[CreateDate]					datetime NOT NULL DEFAULT GETDATE(),
	[DLM]							datetime  NOT NULL,
	[StatusID]						int  NOT NULL,
	[InformationSystemID]			int  NOT NULL,
	[DistrContragentIDInIS]			varbinary(16)  NOT NULL,

	CONSTRAINT [PK_DistrContragents] PRIMARY KEY CLUSTERED  ([InformationSystemID] ASC,[DistrContragentIDInIS] ASC,[ID] ASC)

) ON [SCSLFileGroup]


