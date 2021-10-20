CREATE TABLE [db_scales].[Nomenclature]
(
	 [Id]					INT NOT NULL
	,[Code]					nvarchar (30)
	,[Name]					nvarchar (300)
  ,[IdRRef]        UNIQUEIDENTIFIER NULL
	,[SerializedRepresentationObject] XML NULL
	,[CreateDate]			datetime NULL DEFAULT(GETDATE())
	,[ModifiedDate]			datetime NULL DEFAULT(GETDATE())
  ,[Weighted] bit NULL
  ,PRIMARY KEY CLUSTERED (	[Id] ASC )

) ON [ScalesFileGroup]

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Справочник номенклатуры. Часто редактируется пользователем.',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'Nomenclature',
    @level2type = NULL,
    @level2name = NULL