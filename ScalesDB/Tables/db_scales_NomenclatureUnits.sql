CREATE TABLE [db_scales].[NomenclatureUnits]
(
	[Id]					INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[CreateDate]			DATETIME NULL DEFAULT(GETDATE()),
	[ModifiedDate]			DATETIME NULL DEFAULT(GETDATE()), 
	[IdRRef] UNIQUEIDENTIFIER NULL,
	[Name]					NVARCHAR(150) NOT NULL,
	[NomenclatureId]		INT FOREIGN KEY REFERENCES [db_scales].[Nomenclature] (Id),
	[Marked]				BIT DEFAULT 0,
	[PackWeight]			DECIMAL(10,3),
	[PackQuantly]			INT,
	[PackTypeId]			INT FOREIGN KEY REFERENCES [db_scales].[Nomenclature] (Id)

)ON [ScalesFileGroup]
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Единицы хранения номенклатуры. Попросту варианты упаковок. Часто редактируется пользователем.',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'NomenclatureUnits',
    @level2type = NULL,
    @level2name = NULL