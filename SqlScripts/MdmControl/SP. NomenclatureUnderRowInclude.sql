-- ХП NomenclatureUnderRowInclude
-- Привязать/отвязать текущую запись к мастер-записи
USE [VSDWH]
DECLARE @RC int
DECLARE @Id int
DECLARE @MasterId int
EXECUTE @RC = [MDM].[NomenclatureUnderRowInclude] @Id, @MasterId
