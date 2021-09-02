CREATE TABLE [SCSL].[DimSubdivisions]
(
	[SubdivisionCode]				nvarchar(50) NULL,
	[SubdivisionName]				nvarchar(150) NULL,
	[UseInReport]					int NULL,
	[ConsolidatedClientID]		int NULL,
	[EmployeeID]					int NULL,
	
	[Marked]						bit,
	[CHECKSUMM]						BIGINT,
	[SubdivisionID]					int	IDENTITY(-2147483648,1) NOT NULL,
	[CreateDate]					datetime NOT NULL DEFAULT GETDATE(),
	[DLM]							datetime  NOT NULL,
	[StatusID]						int  NOT NULL,
	[InformationSystemID]			int  NOT NULL,
	[SubdivisionIDinIS]				binary(16) NOT NULL,

	CONSTRAINT [PK_DimSubdivisions_1] PRIMARY KEY CLUSTERED ([InformationSystemID],[SubdivisionIDinIS],[SubdivisionID] ASC)

) ON [SCSLFileGroup]

GO