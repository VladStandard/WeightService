:setvar id -2147462407
:setvar id2 -2147462398
:setvar id4 -2147462017


USE [VSDWH]
GO

-- восстановить исходные состояния
BEGIN TRAN;

DECLARE @Id int = $(id)

DECLARE @MasterId int;
SELECT  @MasterId = n0.MasterId  
FROM [DW].[DimNomenclatures] n0
WHERE ID = @Id;

DELETE
FROM [DW].[DimNomenclatures] 
WHERE ID = @MasterId;

UPDATE [DW].[DimNomenclatures]
SET 
	[NormalizationStatus] = NULL
	,[RelevanceStatus] = NULL
	,[MasterId] = NULL
WHERE ID = @Id;

DECLARE @Id4 int = $(id4)
UPDATE [DW].[DimNomenclatures]
SET 
	[NormalizationStatus] = NULL
	,[RelevanceStatus] = NULL
	,[MasterId] = NULL
WHERE ID = @Id4;

DECLARE @Id2 int = $(id2)
UPDATE [DW].[DimNomenclatures]
SET 
	[NormalizationStatus] = NULL
	,[RelevanceStatus] = NULL
	,[MasterId] = NULL
WHERE ID = @Id2;

COMMIT TRAN;

SELECT * FROM [DW].[DimNomenclatures] n0
WHERE ID IN (@Id,@Id2,@Id4)
ORDER BY [ID] ASC
;

GO


-- сделать мастер
BEGIN TRAN;
DECLARE @Id int = $(id)
EXECUTE [MDM].[NomenclatureMasterRowMake] @Id;

DECLARE @MasterId int;
SELECT  @MasterId = n0.MasterId  
FROM [DW].[DimNomenclatures] n0
WHERE ID = @Id;

SELECT * FROM [DW].[DimNomenclatures] n0
WHERE MasterID = @MasterId
ORDER BY [InformationSystemID] DESC;
COMMIT TRAN;

-- добавить запись
BEGIN TRAN;
DECLARE @Id2 int = $(id2)
EXECUTE [MDM].[NomenclatureUnderRowInclude] @Id2, @MasterId;
SELECT * FROM [DW].[DimNomenclatures] n0
WHERE MasterID = @MasterId
ORDER BY [InformationSystemID] DESC;
COMMIT TRAN;

-- исключить запись
BEGIN TRAN;
DECLARE @Id3 int = $(id2)
EXECUTE [MDM].[NomenclatureUnderRowInclude] @Id3, NULL;
SELECT * FROM [DW].[DimNomenclatures] n0
WHERE MasterID = @MasterId
ORDER BY [InformationSystemID] DESC;
COMMIT TRAN;

-- ненормируемая запись 
BEGIN TRAN;
DECLARE @Id4 int = $(id4)
EXECUTE [MDM].[NomenclatureSetNotRelevance] @Id4;
SELECT * FROM [DW].[DimNomenclatures] n0
WHERE [NormalizationStatus] = 3;
COMMIT TRAN;
GO