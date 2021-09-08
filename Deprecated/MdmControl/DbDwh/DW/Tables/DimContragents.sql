
CREATE TABLE [DW].[DimContragents] (

	[Marked]			bit NULL,
	[Code]				nvarchar(15) NULL,
	[Name]				nvarchar(200) NULL,
	[FullName]			nvarchar(max) NULL,
	[IsBuyer]			bit,
	[IsSupplier]		bit,
	[GLN]				nvarchar(30) NULL,
	[GUID_Mercury]		nvarchar(36) NULL,
	[INN]				nvarchar(15) NULL,
	[KPP]				nvarchar(15) NULL,
	[Comment]			nvarchar(max) NULL,
	[Parents]			nvarchar(max) NULL,
	[OKPO]				nvarchar(10) NULL, 
	[ContragentType]	nvarchar(10) NULL, 
	[ContactInfo]		nvarchar(max) NULL,
	[ManagerID]			varbinary(16) NULL,
	[ConsolidatedClientID] int NULL,
	[NumberDebtDays]	int NULL,
	[AmountDue]			numeric(15,3) NULL,
	[DaysDeferment]		int NULL,
	[CommercialNetworkID] varbinary(16) NULL,
	[CommercialNetworkName] nvarchar(max) NULL,

	[ID] [int] IDENTITY(-2147483648,1) NOT NULL ,
	[CreateDate]		datetime NOT NULL ,
	[DLM]				datetime NOT NULL ,
	[StatusID]			int NOT NULL ,
	[InformationSystemID] int NOT NULL ,
	[CodeInIS]			varbinary(36) NOT NULL 

	-- MDM
	,RelevanceStatus tinyint default 0
	,NormalizationStatus tinyint default 0
	,MasterId int NULL
	--,CONSTRAINT FK_Contragents_MasterIdId FOREIGN KEY (MasterId) REFERENCES [DW].[DimContragents] (ID)
	,CONSTRAINT chkDimContragentsNormalizationStatus CHECK (NormalizationStatus in (0,1,2,3))
	,CONSTRAINT chkDimContragentsRelevanceStatus CHECK (RelevanceStatus in (0,1,2))

	,PRIMARY KEY CLUSTERED ([InformationSystemID] ASC,[CodeInIS] ASC, [ID] ASC)

) on [DIMFileGroup]
GO

