CREATE TABLE [db_scales].[Contragents] (

	[Id] int NOT NULL,
	[Name] nvarchar(150) NOT NULL,
	[Marked] bit NULL DEFAULT ((0)),
    [IdRRef]        UNIQUEIDENTIFIER NULL,
    [SerializedRepresentationObject] XML NULL,
	[CreateDate] datetime NULL DEFAULT (getdate()),
	[ModifiedDate] datetime NULL DEFAULT (getdate()),
    PRIMARY KEY CLUSTERED (	[Id] ASC )

) ON [ScalesFileGroup]
GO


EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Справочник контрагентов',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'Contragents',
    @level2type = NULL,
    @level2name = NULL