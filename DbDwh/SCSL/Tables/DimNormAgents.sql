CREATE TABLE [SCSL].[DimNormAgents] (

	[NormCodeAgent]				nvarchar(50),
	[NormNameAgent]				nvarchar(150),
	[WorkStartDate]				date,
	[AgentInUPP]				nvarchar(150),
	[WorkStartDateInUPP]		date,
	[ParentAgentID]				int,

	[Marked]					bit,
	[CHECKSUMM]					BIGINT,
	[NormAgentID]				int	IDENTITY(-2147483648,1) NOT NULL,
	[CreateDate]				datetime NOT NULL DEFAULT GETDATE(),
	[DLM]						datetime  NOT NULL,
	[StatusID]					int  NOT NULL,
	[InformationSystemID]		int  NOT NULL,
	[NormAgentIDInIS]			binary(16) NOT NULL,

	CONSTRAINT [PK_DimNormAgents_1] PRIMARY KEY CLUSTERED ([InformationSystemID],[NormAgentIDInIS],[NormAgentID] ASC)

) ON [SCSLFileGroup]


