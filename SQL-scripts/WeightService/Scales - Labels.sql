-- Table Labels
-- Connect from PALYCH\LUTON
select 
	[L].[Id]
   ,[L].[CreateDate]
   ,[S].[Description]
   ,[WF].[PluId]
   ,[WF].[ScaleId]
   ,[WF].[WeithingDate]
   ,[WF].[NetWeight]
   ,[WF].[TareWeight]
   ,[WF].[ProductDate]
   ,[WF].[RegNum]
   ,[WF].[Kneading]
   ,[L].[ZPL]
   ,replace(replace([L].[ZPL], char(13), ''), char(10), '') [ZPL_STR]
from [db_scales].[Labels] [L]
left join [db_scales].[WeithingFact] [WF] on [L].[WeithingFactId]=[WF].[Id]
left join [db_scales].[Scales] [S] on [WF].[ScaleId]=[S].[ID]
order by [CreateDate] desc
