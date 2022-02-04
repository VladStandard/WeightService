-- ХП NomenclatureSetNotRelevance
-- Сделать запись не подлежащей нормализации
USE [VSDWH]
DECLARE @RC int
DECLARE @Id int
EXECUTE @RC = [MDM].[NomenclatureSetNotRelevance] @Id
