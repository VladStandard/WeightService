-- Exec LoadResourceToDB
declare @select bit = 1
declare @load_resource_to_db bit = 0
declare @load_resource_to_zebra bit = 0
declare @print_resource_from_zebra bit = 1
----------------------------------------------------------------------------------------------------
-- 1. Загрузить ресурс в таблицу.
if (@load_resource_to_db = 1) begin
	use [ScalesDB]
	declare @id int = 69
	declare @name nvarchar(255) = N'Имя 1'
	declare @description nvarchar(255) = N'Описание'
	declare @type varchar(3) = 'TTF'
	declare @imagedata varchar(max) = 'Текст' -- binary[] --> base64
	declare @marked bit = 0
	exec [db_scales].[LoadResourceToDB] @id, @name, @description, @type, @imagedata, @marked
end
----------------------------------------------------------------------------------------------------
if (@select = 1) begin
	use [ScalesDB]
	select * from [db_scales].[TemplateResources]
end
----------------------------------------------------------------------------------------------------
-- 2. Загрузить логотип в принтер Зебра.
if (@load_resource_to_zebra = 1) begin
	use [ScalesDB]
	declare @ip varchar(100) = '192.168.7.126'
	declare @port int = 9100
	declare @id2 int = 69
	execute [db_scales].[UploadLogo] @ip, @port, @id2
end
----------------------------------------------------------------------------------------------------
-- 3. Печать логотипа с принтера Зебра.
if (@print_resource_from_zebra = 1) begin
	use [ScalesDB]
	declare @ip3 varchar(100) = '192.168.7.126'
	declare @port3 int = 9100
	declare @zplCommand nvarchar(max) =
	'^XA
	^CI28
	^CWK,E:COURB.TTF
	^CWL,E:COURBI.TTF
	^CWM,E:COURBD.TTF
	^CWN,E:COUR.TTF
	^CWZ,E:ARIAL.TTF
	^CWW,E:ARIALBI.TTF
	^CWE,E:ARIALBD.TTF
	^CWR,E:ARIALI.TTF

	^LH0,10
	^FWR

	^LL1180
	^PW944

	^FO820,50
	^CFZ,24,20
	^FB1100,4,0,C,0
	^FH^FD_d0_98_d0_b7_d0_b3_d0_be_d1_82_d0_be_d0_b2_d0_b8_d1_82_d0_b5_d0_bb_d1_8c: _d0_9e_d0_9e_d0_9e "_d0_92_d0_bb_d0_b0_d0_b4_d0_b8_d0_bc_d0_b8_d1_80_d1_81_d0_ba_d0_b8_d0_b9 _d1_81_d1_82_d0_b0_d0_bd_d0_b4_d0_b0_d1_80_d1_82" _d0_a0_d0_be_d1_81_d1_81_d0_b8_d1_8f, 600910 _d0_92_d0_bb_d0_b0_d0_b4_d0_b8_d0_bc_d0_b8_d1_80_d1_81_d0_ba_d0_b0_d1_8f _d0_be_d0_b1_d0_bb. _d0_b3._d0_a0_d0_b0_d0_b4_d1_83_d0_b6_d0_bd_d1_8b_d0_b9 _d0_ba_d0_b2_d0_b0_d1_80_d1_82_d0_b0_d0_bb 13/13 _d0_b4_d0_be_d0_bc 20^FS

	^FO200,666
	^XGE:EAC.GRF,1,1^FS
	^FO315,888
	^XGE:FISH.GRF,1,1^FS
	^FO435,888
	^XGE:TEMP6.GRF,1,1^FS

	^XZ';

	execute [db_scales].[ZplPipe] 
	   @ip
	  ,@port
	  ,@zplCommand
end
----------------------------------------------------------------------------------------------------
