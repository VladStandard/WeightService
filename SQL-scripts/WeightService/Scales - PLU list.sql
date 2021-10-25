-- Scales - PLU list
-- 1. Connect from PALYCH\LUTON
declare @host nvarchar(255) = N'SCALES-MON-PC208'
declare @scale_id int = (select [s].[Id] from [db_scales].[Scales] [s] left join [db_scales].[Hosts] [h] on [s].[HostId]=[h].[Id] where [h].[Name] = @host)
select * from [ScalesDB].[db_scales].[PLU] where [ScaleId]=@scale_id
order by [GoodsName]