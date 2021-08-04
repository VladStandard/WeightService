-- db_scales_Scales_Changed
----------------------------------------------------------------------------------------------------
-- Alter table
begin
	alter table [db_scales].[SCALES] with check add foreign key([LOG_TYPE_UID]) references [db_scales].[LOG_TYPES] ([UID])
end
----------------------------------------------------------------------------------------------------
