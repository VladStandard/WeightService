-- Scales - PLU list
use [ScalesDB]
declare @host nvarchar(255) = N'SCALES-MON-004'
declare @scale_id int = (select [s].[Id] from [db_scales].[Scales] [s] left join [db_scales].[Hosts] [h] on [s].[HostId]=[h].[Id] where [h].[Name] = @host)
select * from [ScalesDB].[db_scales].[PLU] where [ScaleId]=@scale_id
