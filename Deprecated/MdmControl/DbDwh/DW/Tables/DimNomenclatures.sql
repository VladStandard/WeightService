CREATE TABLE [DW].[DimNomenclatures]
(
    [Code] nvarchar(15)
	, [Marked] bit
	, [Name] nvarchar (150)
	, [Parents] nvarchar (1024)
    , [Article] nvarchar (25)
    , [Weighted] bit
    , [GUID_Mercury] nvarchar (36)
    , [KeepTrackOfCharacteristics] bit
    , [NameFull] nvarchar (512)
    , [Comment] nvarchar (512)
    , [IsService] bit
    , [IsProduct] bit default 0
    , [AdditionalDescriptionOfNomenclature] nvarchar (MAX)
    , [NomenclatureGroupCost] varbinary (16)
    , [NomenclatureGroup] varbinary (16)
    , [ArticleCost] varbinary (16)
    , [Brand] varbinary (16)
    , [NomenclatureType] varbinary (16)
    , [VATRate] nvarchar (10)
    --, [ResidueStorageUnit] varbinary (16)
    --, [UnitForReports] varbinary (16)
    --, [BaseUnit] varbinary (16)
    , [Unit] nvarchar (150)
    , [Weight] decimal (15,3)
  	, [boxTypeID]    binary(16)
	, [boxTypeName]  nvarchar(200)
	, [packTypeID]   binary(16)
	, [packTypeName] nvarchar(200)
	, [SerializedRepresentationObject] xml

	,[ID] [int] IDENTITY(-2147483648,1) NOT NULL  
	,[CreateDate] datetime NOT NULL
	,[DLM] datetime  NOT NULL
	,[StatusID] int  NOT NULL
	,[InformationSystemID] int  NOT NULL
	,[CodeInIS] varbinary(16)  NOT NULL

	-- MDM
	,RelevanceStatus tinyint default 0
	,NormalizationStatus tinyint default 0
	,MasterId int NULL
	,CONSTRAINT chkDimNomenclaturesNormalizationStatus CHECK (NormalizationStatus in (0,1,2,3))
	,CONSTRAINT chkDimNomenclaturesRelevanceStatus CHECK (RelevanceStatus in (0,1,2))
	--,CONSTRAINT FK_Nomenclatures_MasterIdId FOREIGN KEY (MasterId) REFERENCES [DW].[DimNomenclatures] (ID)

	,PRIMARY KEY CLUSTERED ([InformationSystemID] ASC, [CodeInIS] ASC, [ID] ASC)
 
) on [DIMFileGroup]
GO

