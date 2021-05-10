CREATE TABLE [db_scales].[Contragents]
(
	[Id]					INT NOT NULL PRIMARY KEY IDENTITY(0,1),
	[CreateDate]			DATETIME NULL DEFAULT(GETDATE()),
	[ModifiedDate]			DATETIME NULL DEFAULT(GETDATE()), 
	[1CRRefID]				VARCHAR(38) NULL,
	[Name]					NVARCHAR(150) NOT NULL,
	[Marked]				BIT DEFAULT 0,

) ON [ScalesFileGroup]
