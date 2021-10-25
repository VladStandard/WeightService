-- Scales - Import 2 PLU
-- 1. Connect from PALYCH\LUTON
-- Before see the script: Scales - Import nomenclatures
set nocount on
use [ScalesDB]
declare @products table ([code] nvarchar(255), [plu] int, [num] int, [GoodsBoxQuantly] int)
declare @template_id int
-- 2. Setup settings.
declare @commit bit = 0
declare @update bit = 0
--declare @host nvarchar(255) = N'SCALES-MON-005'
declare @host nvarchar(255) = N'SCALES-MON-PC208'
declare @template_name nvarchar(255) = 'I2OF5 Template80x100unit_I2OF5_300dpi_tsc' -- Производство
--declare @template_name nvarchar(255) = 'ЧудоПечка 30x50 шт АРМ'  -- Чудо-Печка
-- Set.
declare @scale_id int = (select [s].[Id] from [db_scales].[Scales] [s] left join [db_scales].[Hosts] [h] 
	on [s].[HostId]=[h].[Id] where [h].[Name] = @host)
set @template_id = (select [Id] from [db_scales].[Templates] where [Title]=@template_name)
declare @plu_changed table ([Id] int)
declare @plu_exists table ([Id] int)
insert into @plu_exists([Id]) (select [Id] from [db_scales].[PLU])
-- 3. Fill PLU list from nomenclatures.
-- Чудо печка.
--insert into @products([code],[plu]) values(N'ЦБД00053844',631) -- Упаковка "Чудо печка" Куриная голень
--insert into @products([code],[plu]) values(N'ЦБД00053845',655) -- Упаковка "Чудо печка" Куриные крылышки
--insert into @products([code],[plu]) values(N'ЦБД00053846',679) -- Упаковка "Чудо печка" Угорь Унаги
--insert into @products([code],[plu]) values(N'ЦБД00053847',662) -- Упаковка "Чудо печка" Колбаса Хуторская
--insert into @products([code],[plu]) values(N'ЦБД00053848',648) -- Упаковка "Чудо печка" Куриная грудка
-- СЦ Линия №10 - SCALES-MON-004.
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'ЦБД00008197', 102, 5, 25) -- Московская  в.к. ГОСТ
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'ЦБД00002181', 103, 5, 15) -- Мадейра в.к.
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'000В0000431', 104, 5, 25) -- Дворянская в.к.
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'000В0000477', 106, 5, 15) -- Суздальский в.к.
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'000В0000472', 107, 5, 25) -- Кремлевский в.к.
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'000В0000478', 108, 5, 15) -- Юбилейный в.к.
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'000В0000470', 109, 5, 15) -- Зернистый в.к.
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'ЦБД00012800', 112, 5, 25) -- Коньячный в.к.
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'ЦБД00011413', 113, 5, 25) -- Владимирский в.к.
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'000В0000474', 114, 5, 25) -- Салями "Домашняя" п.к.
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'000В0000379', 115, 5, 25) -- Кремлёвская с грудинкой
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'000В0000475', 117, 5, 25) -- Салями по-Фински п.к.
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'000В0000386', 120, 5, 25) -- Чесночная стандарт
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'ЦБД00037388', 137, 5, 25) -- Сервелат с телятиной в.к.
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'ЦБД00011404', 141, 5, 12) -- Сочная п/а
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'ЦБД00011405', 142, 4, 15) -- Сочная п/а 400 г
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'ЦБД00041861', 143, 5, 25) -- Российский в.к. 1 сорт
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'ЦБД00041860', 144, 5, 25) -- Городской стандарт в.к. 1 сорт
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'ЦБД00041859', 145, 5, 25) -- Деликатесная п.к. 1 сорт
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'ЦБД00053241', 146, 5, 25) -- С беконом по-деревенски
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'ЦБД00026237', 147, 5, 25) -- Ореховый п.к.
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'ЦБД00049186', 149, 5, 25) -- Московская в.к. ГОСТ ВС
-- СЦ Линия №11 - SCALES-MON-005.
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'ЦБД00008197', 102, 5, 25) -- Московская  в.к. ГОСТ
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'ЦБД00002181', 103, 5, 15) -- Мадейра в.к.
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'000В0000431', 104, 5, 25) -- Дворянская в.к.
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'000В0000477', 106, 5, 15) -- Суздальский в.к.
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'000В0000472', 107, 5, 25) -- Кремлевский в.к.
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'000В0000478', 108, 5, 15) -- Юбилейный в.к.
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'000В0000470', 109, 5, 15) -- Зернистый в.к.
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'ЦБД00012800', 112, 5, 25) -- Коньячный в.к.
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'ЦБД00011413', 113, 5, 25) -- Владимирский в.к.
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'000В0000474', 114, 5, 25) -- Салями "Домашняя" п.к.
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'000В0000379', 115, 5, 25) -- Кремлёвская с грудинкой
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'000В0000475', 117, 5, 25) -- Салями по-Фински п.к.
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'000В0000386', 120, 5, 25) -- Чесночная стандарт
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'ЦБД00026237', 132, 5, 25) -- Ореховый п.к.
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'ЦБД00037388', 137, 5, 25) -- Сервелат с телятиной в.к.
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'ЦБД00041861', 147, 5, 25) -- Российский в.к. 1 сорт
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'ЦБД00041860', 148, 5, 25) -- Городской стандарт в.к. 1 сорт
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'ЦБД00041859', 149, 5, 25) -- Деликатесная п.к. 1 сорт
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'ЦБД00053241', 153, 5, 25) -- С беконом по-деревенски
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'ЦБД00049186', 155, 5, 25) -- Московская в.к. ГОСТ ВС
insert into @products([code],[plu],[num],[GoodsBoxQuantly]) values(N'ЦБД00011404', 156, 5, 12) -- Сочная п/а
-- Select local data.
--select * from [db_scales].[Nomenclature] [N] where [N].[Code] in (select [Code] from @products)
-- Remote data.
declare @codes nvarchar(max)
select @codes = coalesce(@codes + ''''', N''''', 'N''''') + [code] from @products
set @codes = @codes + ''''''
declare @cmd_select nvarchar(max) = '
SELECT 
	 [ID]
	,[Code]
	,[Name]
	,[NameFull]
	,UPPER([VSDWH].[DW].[fnGetGuid1Cv2] ([CodeInIS])) [IdRRef]
    ,[Parents] [Parents]
	,cast([SerializedRepresentationObject] as nvarchar(4000)) [SRO]
	,[CreateDate]
	,[DLM] [ModifiedDate]
	,[Weighted]
	,[AdditionalDescriptionOfNomenclature] [AdditionalDescription]
	,[Unit]
	,[boxTypeName]
	,[packTypeName]
FROM [VSDWH].[DW].[DimNomenclatures] 
WHERE [Marked]=0
AND [Code] in (' + @codes + ')
ORDER BY [Name]
'
-- Table-variable.
declare @remote_data table 
	([ID] int, [Code] nvarchar(30), [Name] nvarchar(300), [NameFull] nvarchar(1024), [IdRRef] uniqueidentifier, 
	[Parents] nvarchar(1024), [SerializedRepresentationObject] nvarchar(4000),
	[CreateDate] datetime, [ModifiedDate] datetime, [Weighted] bit, [AdditionalDescription] nvarchar(1024), [Unit] nvarchar(255)
	,[boxTypeName] nvarchar(200), [packTypeName] nvarchar(200))
-- Push remote data into table-variable.
declare @tsql nvarchar(max)
set @tsql = 'SELECT * FROM OPENQUERY([SQLSRSP01\LEEDS], ''' + @cmd_select + ''')'
insert into @remote_data exec(@tsql)
-- Cusrsor for [Nomenclature].
begin tran
	declare @nomenclature_id int
	declare @name nvarchar(300)
	declare @nameFull nvarchar(1024)
	declare @plu int
	declare @createDate datetime
	declare @modifiedDate datetime
	declare @weighted bit
	declare @days_str nvarchar(1024)
	declare @additionalDescription nvarchar(1024)
	declare @days int
	declare @barcode_type nvarchar(255)
	declare @ean13 nvarchar(255)
	declare @gtin nvarchar(255)
	declare @itf14 nvarchar(255)
	declare @sro xml
	declare @goodsBoxQuantly int = null
	declare @boxTypeName nvarchar(200)
	declare @packTypeName nvarchar(200)
	declare @goodsBoxWeight decimal(10,3) = 0
	declare @goodsPackWeight decimal(10,3) = 0
	declare @goodsTareWeight decimal(10,3) = 0
	declare cur cursor for select [n].[Id], [n].[Name]
		,(select top 1 [plu] from @products where [code]=[n].[Code])
		,(select top 1 [GoodsBoxQuantly] from @products where [code]=[n].[Code])
		from [db_scales].[Nomenclature] [n] where [n].[Code] in (select [code] from @products)
	open cur
	fetch next from cur into @nomenclature_id, @name, @plu, @goodsBoxQuantly
	while @@fetch_status = 0 begin
		set @days = (select [db_scales].[GetCountDays]((select [AdditionalDescription] from @remote_data where [ID] = @nomenclature_id), 1))
		set @additionalDescription = (select [AdditionalDescription] from @remote_data where [ID] = @nomenclature_id)
		set @nameFull = (select [NameFull] from @remote_data where [ID] = @nomenclature_id)
		set @weighted = (select [Weighted] from @remote_data where [ID] = @nomenclature_id)
		set @sro = (select [SerializedRepresentationObject] from @remote_data where [ID] = @nomenclature_id)
		set @boxTypeName = (select [boxTypeName] from @remote_data where [ID] = @nomenclature_id)
		set @packTypeName = (select [packTypeName] from @remote_data where [ID] = @nomenclature_id)
		set @barcode_type = (select @sro.value('(/Product/Barcodes/Type/node())[1]', 'nvarchar(255)'))
		if (@barcode_type = 'GTIN') begin
			set @gtin = (select @sro.value('(/Product/Barcodes/Barcode/node())[1]', 'nvarchar(255)'))
		end
		if (@barcode_type = 'EAN13') begin
			set @ean13 = (select @sro.value('(/Product/Barcodes/Barcode/node())[1]', 'nvarchar(255)'))
		end
		if (@barcode_type = 'ITF14') begin
			set @itf14 = (select @sro.value('(/Product/Barcodes/Barcode/node())[1]', 'nvarchar(255)'))
		end
		-- boxWeight (360) + quantly (20) * packWeight (5) = 460 гр / 1000 = 0,46 кг
		set @goodsBoxWeight = (select [db_scales].[GetIntFromString](@boxTypeName, 2))
		if (@goodsBoxQuantly is null) or (@goodsBoxQuantly = 0) begin
			set @goodsTareWeight = @goodsBoxWeight / 1000
		end else begin
			if (@packTypeName is null) or (@packTypeName = '') begin
				set @goodsTareWeight = @goodsBoxWeight / 1000
			end else begin
				set @goodsPackWeight = (select [db_scales].[GetIntFromString](@packTypeName, 1))
				set @goodsTareWeight = (@goodsBoxWeight + (@goodsBoxQuantly * @goodsPackWeight)) / 1000
			end
		end
		--print '@goodsTareWeight = ' + cast(@goodsTareWeight as nvarchar(255))
		if (@update = 1) begin
			if (exists (select 1 from [db_scales].[PLU] where [ScaleId]=@scale_id and [NomenclatureId]=@nomenclature_id)) begin
					update [db_scales].[PLU] set 
						[GoodsName]=@name, [GoodsFullName]=@nameFull, [GoodsDescription]=@additionalDescription
						,[TemplateID]=@template_id
						--,[GTIN],[EAN13],[ITF14]
						,[GoodsShelfLifeDays]=@days
						,[GoodsTareWeight]=@goodsTareWeight
						--,[GoodsBoxQuantly]
						--,[ScaleId]=@scale_id
						,[Plu]=@plu
						--,[CreateDate]
						,[ModifiedDate]=getdate()
						--,[Active]=1,[UpperWeightThreshold],[NominalWeight],[LowerWeightThreshold]
						,[CheckWeight]=@weighted
						--,[Marked]
					where [ScaleId]=@scale_id and [NomenclatureId]=@nomenclature_id
				if (@ean13 is not null) begin
					update [db_scales].[PLU] set [EAN13]=@ean13 where [ScaleId]=@scale_id and [NomenclatureId]=@nomenclature_id
				end
				if (@gtin is not null) begin
					update [db_scales].[PLU] set [GTIN]=@gtin where [ScaleId]=@scale_id and [NomenclatureId]=@nomenclature_id
				end
				if (@itf14 is not null) begin
					update [db_scales].[PLU] set [ITF14]=@itf14 where [ScaleId]=@scale_id and [NomenclatureId]=@nomenclature_id
				end
				if (@goodsBoxQuantly is not null) begin
					update [db_scales].[PLU] set [GoodsBoxQuantly]=@goodsBoxQuantly where [ScaleId]=@scale_id and [NomenclatureId]=@nomenclature_id
				end
				print N'[*] [db_scales].[PLU] updated where [ScaleId]=' + cast(@scale_id as nvarchar(255)) + 
					' and [NomenclatureId]=' + cast(@nomenclature_id as nvarchar(255))
			end else begin
				insert into @plu_changed([Id]) values (@nomenclature_id)
				print N'[!] [db_scales].[PLU] not updated where [ScaleId]=' + cast(@scale_id as nvarchar(255)) + 
					' and [NomenclatureId]=' + cast(@nomenclature_id as nvarchar(255))
			end
		end else begin
			if not(exists (select 1 from [db_scales].[PLU] where [ScaleId]=@scale_id and [NomenclatureId]=@nomenclature_id)) begin
				insert into [db_scales].[PLU] ([GoodsName],[GoodsFullName],[GoodsDescription],[TemplateID]
					,[GTIN],[EAN13],[ITF14]
					,[GoodsShelfLifeDays],[GoodsTareWeight],[GoodsBoxQuantly]
					,[ScaleId],[NomenclatureId],[Plu]
					,[CreateDate],[ModifiedDate],[Active],[UpperWeightThreshold],[NominalWeight],[LowerWeightThreshold]
					,[CheckWeight],[Marked])
				values (@name,@nameFull,null,@template_id
					,@gtin,@ean13,@itf14
					,@days,@goodsTareWeight,@goodsBoxQuantly
					,@scale_id,@nomenclature_id,@plu
					,getdate(),getdate(),1,0,0,0,@weighted,0)
				print N'[+] [db_scales].[PLU] inserted where [ScaleId]=' + cast(@scale_id as nvarchar(255)) + 
					' and [NomenclatureId]=' + cast(@nomenclature_id as nvarchar(255))
			end else begin
				insert into @plu_changed([Id]) values (@nomenclature_id)
				print N'[!] [db_scales].[PLU] not inserted where [ScaleId]=' + cast(@scale_id as nvarchar(255)) + 
					' and [NomenclatureId]=' + cast(@nomenclature_id as nvarchar(255))
			end
		end
		fetch next from cur into @nomenclature_id, @name, @plu, @goodsBoxQuantly
	end
	close cur
	deallocate cur
-- Uncommitted local data.
select * from [db_scales].[PLU] where [ScaleId]=@scale_id 
	and [NomenclatureId] not in (select [Id] from @plu_changed) 
	and [Id] not in (select [Id] from @plu_exists)
order by [Id]
-- Settings.
if (@commit = 1) begin
	commit tran
end else begin
	rollback tran
end
set nocount off
-- Committed local data.
select * from [db_scales].[PLU] where [ScaleId]=@scale_id order by [Id]
