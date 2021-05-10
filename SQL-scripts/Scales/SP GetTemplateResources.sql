-- SP GetTemplateResources
use [ScalesDB]
set nocount on
------------------------------------------------------------------------------------------------------------------------
select [Id], [Name], [ImageData]
from [db_scales].[GetTemplateResources]
(33, 'GRF')
set nocount off

SELECT * FROM [db_scales].[TemplateResources]
