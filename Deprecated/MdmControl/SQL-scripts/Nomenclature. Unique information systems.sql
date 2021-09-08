-- Nomenclature. Unique information systems
-- ISEXCD02\INS1
use [VSDWH]
select [is].[InformationSystemID], [is].[Name], [c].[Count]
from [ETL].[InformationSystems] [is]
join 
(select [DW].[DimNomenclatures].[InformationSystemID]
from [DW].[DimNomenclatures]
group by [DW].[DimNomenclatures].[InformationSystemID]
) [n] on [n].[InformationSystemID] = [is].[InformationSystemID]
join 
(select [c].[InformationSystemID], count([c].[InformationSystemID]) [Count] from [DW].[DimNomenclatures] [c] group by [c].[InformationSystemID]) 
[c] on [c].[InformationSystemID] = [is].[InformationSystemID]
order by [is].[InformationSystemID]
