-- ХП NomenclatureMasterRowMake
-- Создать мастер-запись
use [VSDWH]
declare @RC int
declare @Id int = 0
execute @RC = [MDM].[NomenclatureMasterRowMake] @Id

--execute [MDM].[NomenclatureMasterRowMake] -2147437994
--execute [MDM].[NomenclatureMasterRowRemove] -2147424990
