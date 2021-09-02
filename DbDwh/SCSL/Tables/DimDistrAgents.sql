
CREATE TABLE [SCSL].[DimDistrAgents](

	[Marked] bit,
	[NormAgentIDInIS] [binary](16) NULL,
	[NormAgentID] [int] NULL,
	[DistrAgentCode] [nvarchar](50) NULL,
	[DistrAgentName] [nvarchar](150) NULL,
	[UseInReport] [int] NULL,

	[ID]				int IDENTITY(-2147483648,1) NOT NULL,
	[CHECKSUMM]			BIGINT NOT NULL,
	[CreateDate]		datetime NOT NULL DEFAULT GETDATE(),
	[DLM]				datetime  NOT NULL,
	[StatusID]			int  NOT NULL,
	[InformationSystemID] int  NOT NULL,
	[DistrAgentIDInIS]			varbinary(16)  NOT NULL,

	CONSTRAINT [PK_DistrAgents] PRIMARY KEY CLUSTERED  ([InformationSystemID] ASC, [DistrAgentIDInIS] ASC, [ID] ASC)

) ON [SCSLFileGroup]
GO