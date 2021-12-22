-- Table TASKS. Actions
declare @delete bit = 0
declare @insert bit = 0
declare @update bit = 0
declare @enabled bit = 0
declare @scale_id int = 5
declare @task_type_uid uniqueidentifier = 'F3C70A24-ABF1-4834-8685-0A5186A6E5A1'

if (@insert=1) begin
	if not exists (select 1 from [db_scales].[TASKS] where [TASK_UID]=@task_type_uid and [SCALE_ID]=@scale_id) begin
		insert into [db_scales].[TASKS]([TASK_UID],[SCALE_ID],[ENABLED])
		values(@task_type_uid,@scale_id,@enabled)
	end
	select [UID] from [db_scales].[TASKS] where [TASK_UID]=@task_type_uid and [SCALE_ID]=@scale_id
end

if (@delete=1) begin
	delete from [db_scales].[TASKS] where [TASK_UID]=@task_type_uid and [SCALE_ID]=@scale_id
end

if (@update=1) begin
	update [db_scales].[TASKS] set [ENABLED]=@enabled where [TASK_UID]=@task_type_uid and [SCALE_ID]=@scale_id
end
