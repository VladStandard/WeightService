CREATE TABLE [STG].[FactTechnicalSpecification]
(
	[DocNum]			nvarchar(15),
	[DocDate]			datetime,
	[DocType]			nvarchar(50),
	[Marked]			bit,
	[Posted]			bit,
	[DateID]			int NOT NULL,

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
	[StatusID]						int  NOT NULL,
	[InformationSystemID]			int NOT NULL,
	[CodeInIS]						varbinary(16) NOT NULL,
	[LineNo]						int NOT NULL,
	[CHECKSUMM]						BIGINT NOT NULL

) on [ETLFileGroup]
GO
