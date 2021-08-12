-- Table Nomenclature. ЧудоПечка
select
	 [Id]
	,[CreateDate]
	,[ModifiedDate]
	,[Code]
	,[IdRRef]
	,[Name]
	,[SerializedRepresentationObject]
from [db_scales].[Nomenclature]
where name like '%Чудо печка%'
order by [Nomenclature].[Id]
