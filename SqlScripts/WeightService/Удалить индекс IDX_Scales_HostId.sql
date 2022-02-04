-- Удалить индекс IDX_Scales_HostId
use [ScalesDB]
drop index if exists [IDX_Scales_HostId] on [db_scales].[Scales]
