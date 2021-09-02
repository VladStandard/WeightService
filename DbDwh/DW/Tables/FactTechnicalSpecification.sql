CREATE TABLE [DW].[FactTechnicalSpecification]
(

	[DocNum]			nvarchar(15),
	[DocDate]			datetime,
	[DocType]			nvarchar(50),
	[Marked]			bit,
	[Posted]			bit,
	[DateID]			int NOT NULL,

	[_DateID]			date,  
	[_ProductID]		int,
	[_OrgID]			int,
	[_StorageID]		int,
	[_NomenclatureID]	int,

	[Продукция]						binary(16)		,	
	[Замес]							float			,
	[OrgID]							binary(16)		,
	[Ответственный]					binary(16)		,
	[НормаВыходаГотовойПродукции]	float			,
	[StorageID]						binary(16)		,
	[NomenclatureID]				binary(16)		,
	[Количество]					decimal(15,3)	,	
	[ЕдиницаИзмерения]				binary(16)		,
	[КЕИ]							nvarchar(15)	,
	[СтатьяЗатрат]					binary(16)		,


	[ID] BIGINT NOT NULL  IDENTITY(-9223372036854775808,1),
	[CreateDate] datetime NOT NULL,
	[DLM] datetime  NOT NULL,
	[StatusID] int  NOT NULL,
	[InformationSystemID] int NOT NULL,
	[CodeInIS] varbinary(16) NOT NULL,
	[_LineNo] int NOT NULL,
	[CHECKSUMM] BIGINT NOT NULL,
	[Active] BIT NULL DEFAULT 1, 
    PRIMARY KEY CLUSTERED ([InformationSystemID] ASC,[CodeInIS] ASC,[_LineNo] ASC, [ID] ASC)

) on [FACTFileGrooup]
GO

CREATE NONCLUSTERED INDEX [FactTechnicalSpecification_Dim01] ON [DW].[FactTechnicalSpecification]
(
	[_DateID]			ASC,
    [_StorageID]		ASC,
    [_ProductID]		ASC,
    [_NomenclatureID]	ASC

) on [FACTFileGrooup]
GO
