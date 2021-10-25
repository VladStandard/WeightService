-- Scales - Nomenclature WeightTara
-- Connect from SQLSRSP01\LEEDS
use [VSDWH]
SELECT [Name], [Code], [n].[boxTypeName], [n].[packTypeName]
FROM [DW].[DimNomenclatures] [n]
where [n].[Code]='ЦБД00028396'
--where [n].[Name]='Дворянская в.к. 350 г'

-- boxWeight (360) + quantly (20) * packWeight (5) = 460 гр / 1000 = 0,46 кг
