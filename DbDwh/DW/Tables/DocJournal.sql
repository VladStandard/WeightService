CREATE TABLE [DW].[DocJournal]
(
	[DateKey] int,
	[Marked] bit,
	[Posted] bit,
	[DocNum] nvarchar(15),
	[DocDate] datetime,
	[DocType] nvarchar(50),
	[OrgID] binary(16),

	[ID] [bigint] IDENTITY(-9223372036854775808,1) NOT NULL,
	[CreateDate] datetime NOT NULL,
	[DLM] datetime  NOT NULL,
	[StatusID] int  NOT NULL,
	[InformationSystemID] int  NOT NULL,
	[CodeInIS] varbinary(16)  NOT NULL,
	[CHECKSUMM] BIGINT ,

	PRIMARY KEY CLUSTERED ([InformationSystemID] ASC,[CodeInIS] ASC, [ID] ASC)

 ) on [FACTFileGrooup]
GO


CREATE NONCLUSTERED INDEX [DWDocJournal_Dim01] ON [DW].[DocJournal]
(
	[InformationSystemID] ASC,
    [OrgID]	 ASC,
	[DocType] ASC,
	[DateKey] ASC
 ) on [FACTFileGrooup]
GO

