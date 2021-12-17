-- Scales - Import 1 Nomenclature
-- 1. Connect from PALYCH\LUTON
-- use [ScalesDB]
-- After all see the script: Scales - PLU insert new
set nocount on
declare @products table ([code] nvarchar(255), [plu] int, [num] int)
-- 2. Setup settings.
declare @commit bit = 0
declare @update bit = 1
-- 3. Fill PLU table from DWH - 1C.
declare @nom_changed table ([Id] int)
---- Чудо печка.
--insert into @products([code],[plu]) values(N'ЦБД00053844',631) -- Упаковка "Чудо печка" Куриная голень
--insert into @products([code],[plu]) values(N'ЦБД00053845',655) -- Упаковка "Чудо печка" Куриные крылышки
--insert into @products([code],[plu]) values(N'ЦБД00053846',679) -- Упаковка "Чудо печка" Угорь Унаги
--insert into @products([code],[plu]) values(N'ЦБД00053847',662) -- Упаковка "Чудо печка" Колбаса Хуторская
--insert into @products([code],[plu]) values(N'ЦБД00053848',648) -- Упаковка "Чудо печка" Куриная грудка
---- СЦ Линия №10 - SCALES-MON-004.
--insert into @products([code],[plu],[num]) values(N'ЦБД00008197', 102, 5) -- Московская  в.к. ГОСТ
--insert into @products([code],[plu],[num]) values(N'ЦБД00002181', 103, 5) -- Мадейра в.к.
--insert into @products([code],[plu],[num]) values(N'000В0000431', 104, 5) -- Дворянская в.к.
--insert into @products([code],[plu],[num]) values(N'000В0000477', 106, 5) -- Суздальский в.к.
--insert into @products([code],[plu],[num]) values(N'000В0000472', 107, 5) -- Кремлевский в.к.
--insert into @products([code],[plu],[num]) values(N'000В0000478', 108, 5) -- Юбилейный в.к.
--insert into @products([code],[plu],[num]) values(N'000В0000470', 109, 5) -- Зернистый в.к.
--insert into @products([code],[plu],[num]) values(N'ЦБД00012800', 112, 5) -- Коньячный в.к.
--insert into @products([code],[plu],[num]) values(N'ЦБД00011413', 113, 5) -- Владимирский в.к.
--insert into @products([code],[plu],[num]) values(N'000В0000474', 114, 5) -- Салями "Домашняя" п.к.
--insert into @products([code],[plu],[num]) values(N'000В0000379', 115, 5) -- Кремлёвская с грудинкой
--insert into @products([code],[plu],[num]) values(N'000В0000475', 117, 5) -- Салями по-Фински п.к.
--insert into @products([code],[plu],[num]) values(N'000В0000386', 120, 5) -- Чесночная стандарт
--insert into @products([code],[plu],[num]) values(N'ЦБД00037388', 137, 5) -- Сервелат с телятиной в.к.
--insert into @products([code],[plu],[num]) values(N'ЦБД00011404', 141, 5) -- Сочная п/а
--insert into @products([code],[plu],[num]) values(N'ЦБД00011405', 142, 4) -- Сочная п/а 400 г
--insert into @products([code],[plu],[num]) values(N'ЦБД00041861', 143, 5) -- Российский в.к. 1 сорт
--insert into @products([code],[plu],[num]) values(N'ЦБД00041860', 144, 5) -- Городской стандарт в.к. 1 сорт
--insert into @products([code],[plu],[num]) values(N'ЦБД00041859', 145, 5) -- Деликатесная п.к. 1 сорт
--insert into @products([code],[plu],[num]) values(N'ЦБД00053241', 146, 5) -- С беконом по-деревенски
--insert into @products([code],[plu],[num]) values(N'ЦБД00026237', 147, 5) -- Ореховый п.к.
--insert into @products([code],[plu],[num]) values(N'ЦБД00049186', 149, 5) -- Московская в.к. ГОСТ ВС
---- СЦ Линия №11 - SCALES-MON-005.
--insert into @products([code],[plu],[num]) values(N'ЦБД00008197', 102, 5) -- Московская  в.к. ГОСТ
--insert into @products([code],[plu],[num]) values(N'ЦБД00002181', 103, 5) -- Мадейра в.к.
--insert into @products([code],[plu],[num]) values(N'000В0000431', 104, 5) -- Дворянская в.к.
--insert into @products([code],[plu],[num]) values(N'000В0000477', 106, 5) -- Суздальский в.к.
--insert into @products([code],[plu],[num]) values(N'000В0000472', 107, 5) -- Кремлевский в.к.
--insert into @products([code],[plu],[num]) values(N'000В0000478', 108, 5) -- Юбилейный в.к.
--insert into @products([code],[plu],[num]) values(N'000В0000470', 109, 5) -- Зернистый в.к.
--insert into @products([code],[plu],[num]) values(N'ЦБД00012800', 112, 5) -- Коньячный в.к.
--insert into @products([code],[plu],[num]) values(N'ЦБД00011413', 113, 5) -- Владимирский в.к.
--insert into @products([code],[plu],[num]) values(N'000В0000474', 114, 5) -- Салями "Домашняя" п.к.
--insert into @products([code],[plu],[num]) values(N'000В0000379', 115, 5) -- Кремлёвская с грудинкой
--insert into @products([code],[plu],[num]) values(N'000В0000475', 117, 5) -- Салями по-Фински п.к.
--insert into @products([code],[plu],[num]) values(N'000В0000386', 120, 5) -- Чесночная стандарт
--insert into @products([code],[plu],[num]) values(N'ЦБД00026237', 132, 5) -- Ореховый п.к.
--insert into @products([code],[plu],[num]) values(N'ЦБД00037388', 137, 5) -- Сервелат с телятиной в.к.
--insert into @products([code],[plu],[num]) values(N'ЦБД00041861', 147, 5) -- Российский в.к. 1 сорт
--insert into @products([code],[plu],[num]) values(N'ЦБД00041860', 148, 5) -- Городской стандарт в.к. 1 сорт
--insert into @products([code],[plu],[num]) values(N'ЦБД00041859', 149, 5) -- Деликатесная п.к. 1 сорт
--insert into @products([code],[plu],[num]) values(N'ЦБД00053241', 153, 5) -- С беконом по-деревенски
--insert into @products([code],[plu],[num]) values(N'ЦБД00049186', 155, 5) -- Московская в.к. ГОСТ ВС
--insert into @products([code],[plu],[num]) values(N'ЦБД00011404', 156, 5) -- Сочная п/а
---- Камешково - Линия 3
--insert into @products([code],[plu],[num]) values(N'ЦБД00041851', 106, 6) -- Докторская стандарт ц/ф 1 сорт ВС
--insert into @products([code],[plu],[num]) values(N'ЦБД00041854', 107, 6) -- Любительская стандарт ц/ф 1 сорт ВС
--insert into @products([code],[plu],[num]) values(N'ЦБД00041852', 108, 6) -- Русская стандарт ц/ф 1 сорт ВС
--insert into @products([code],[plu],[num]) values(N'ЦБД00005846', 109, 7) -- яВладимирская ц/ф
--insert into @products([code],[plu],[num]) values(N'ЦБД00004307', 111, 7) -- Молочная Владимирский стандарт ц/ф
--insert into @products([code],[plu],[num]) values(N'000В0000324', 114, 6) -- Оливье п/а
--insert into @products([code],[plu],[num]) values(N'ЦБД00000433', 115, 6) -- яНежная стандарт п/а сетка
--insert into @products([code],[plu],[num]) values(N'ЦБД00010500', 116, 7) -- яАроматная стандарт п/а сетка
--insert into @products([code],[plu],[num]) values(N'ЦБД00013621', 117, 7) -- яВот такая с грудинкой по-деревенски
--insert into @products([code],[plu],[num]) values(N'ЦБД00013619', 118, 7) -- яВот такая из вырезки по-деревенски
--insert into @products([code],[plu],[num]) values(N'ЦБД00041852', 102, 7) -- Русская стандарт ц/ф 1 сорт ВС
--insert into @products([code],[plu],[num]) values(N'ЦБД00014862', 113, 5) -- Докторская  стандарт  п/а 1 сорт
--insert into @products([code],[plu],[num]) values(N'ЦБД00041851', 101, 7) -- Докторская стандарт ц/ф 1 сорт ВС
--insert into @products([code],[plu],[num]) values(N'000В0000324', 126, 5) -- Оливье п/а
--insert into @products([code],[plu],[num]) values(N'ЦБД00004307', 128, 6) -- Молочная Владимирский стандарт ц/ф
--insert into @products([code],[plu],[num]) values(N'ЦБД00014862', 130, 6) -- Докторская  стандарт  п/а 1 сорт
--insert into @products([code],[plu],[num]) values(N'ЦБД00014865', 131, 6) -- Русская  стандарт  п/а 1 сорт
--insert into @products([code],[plu],[num]) values(N'000В0000317', 132, 6) -- Муромская  стандарт 4-х слойный ц/ф
--insert into @products([code],[plu],[num]) values(N'ЦБД00014865', 133, 6) -- Русская  стандарт  п/а 1 сорт
--insert into @products([code],[plu],[num]) values(N'ЦБД00048987', 135, 6) -- Русская стандарт п/а 1 сорт ВС
--insert into @products([code],[plu],[num]) values(N'ЦБД00048988', 136, 6) -- Докторская стандарт п/а 1 сорт ВС
--insert into @products([code],[plu],[num]) values(N'ЦБД00048989', 137, 6) -- Любительская стандарт п/а 1 сорт ВС
--insert into @products([code],[plu],[num]) values(N'ЦБД00043809', 138, 6) -- яДокторская Оригинальная ц/ф
--insert into @products([code],[plu],[num]) values(N'ЦБД00014699', 139, 6) -- Докторская ГОСТ ц/ф
--insert into @products([code],[plu],[num]) values(N'ЦБД00000000', 140, 6) -- Русская стандарт 500 г ц/ф 1 сорт
--insert into @products([code],[plu],[num]) values(N'ЦБД00052445', 141, 6) -- яЛюбительская стандарт ц/ф 1 сорт ВС (ВН)
--insert into @products([code],[plu],[num]) values(N'ЦБД00052380', 142, 6) -- яДокторская стандарт п/а 1 сорт (ВН)
--insert into @products([code],[plu],[num]) values(N'ЦБД00052379', 143, 6) -- яРусская стандарт п/а 1 сорт (ВН)
--insert into @products([code],[plu],[num]) values(N'ЦБД00053560', 145, 6) -- Докторская Оригинальная
--insert into @products([code],[plu],[num]) values(N'ЦБД00054059', 151, 6) -- Докторская Особая
--insert into @products([code],[plu],[num]) values(N'ЦБД00054058', 152, 6) -- Русская Особая
--insert into @products([code],[plu],[num]) values(N'ЦБД00054244', 153, 6) -- Русская Оригинальная
--insert into @products([code],[plu],[num]) values(N'ЦБД00054871', 154, 6) -- яДокторская стандарт ц/ф 1 сорт ВС (ВН)
--insert into @products([code],[plu],[num]) values(N'ЦБД00014863', 155, 4) -- Докторская  стандарт  п/а 500 г 1 сорт
--insert into @products([code],[plu],[num]) values(N'ЦБД00030102', 156, 4) -- Со шпиком 500 г
--insert into @products([code],[plu],[num]) values(N'ЦБД00030102', 157, 8) -- Со шпиком 500 г
--insert into @products([code],[plu],[num]) values(N'ЦБД00045427', 158, 4) -- Оливье п/а 450 г
--insert into @products([code],[plu],[num]) values(N'ЦБД00048864', 159, 4) -- Докторская стандарт п/а 430 г
--insert into @products([code],[plu],[num]) values(N'ЦБД00014868', 160, 4) -- Русская  стандарт  п/а 500 г 1 сорт
--insert into @products([code],[plu],[num]) values(N'ЦБД00045427', 161, 8) -- Оливье п/а 450 г
--insert into @products([code],[plu],[num]) values(N'ЦБД00056574', 162, 6) -- Оливье стандарт
-- Remote data.
declare @codes nvarchar(max)
select @codes = coalesce(@codes + ''''', N''''', 'N''''') + [code] from @products
set @codes = @codes + ''''''
declare @cmd_select nvarchar(max) = '
SELECT 
	 [ID]
	,[Code]
	,[Name] 
	,UPPER([VSDWH].[DW].[fnGetGuid1Cv2] ([CodeInIS])) [IdRRef]
    ,[Parents] [Parents]
	,cast([SerializedRepresentationObject] as nvarchar(4000)) [SRO]
	,[CreateDate]
	,[DLM] [ModifiedDate]
	,[Weighted]
FROM [VSDWH].[DW].[DimNomenclatures] 
WHERE [Marked]=0
AND [Code] in (' + @codes + ')
ORDER BY [Name]
'
-- Table-variable.
declare @remote_data table ([ID] int, [Code] nvarchar(30), [Name] nvarchar(150), [IdRRef] uniqueidentifier, [Parents] nvarchar(1024),
	[SRO] nvarchar(4000), [CreateDate] datetime, [ModifiedDate] datetime, [Weighted] bit)
-- Push remote data into table-variable.
declare @tsql nvarchar(max)
set @tsql = 'SELECT * FROM OPENQUERY([SQLSRSP01\LEEDS], ''' + @cmd_select + ''')'
insert into @remote_data exec(@tsql)
-- Cusrsor for @remote_data.
begin tran
	declare @id int
	declare @code nvarchar(30)
	declare @name nvarchar(150)
	declare @idRRef uniqueidentifier
	declare @parents nvarchar(1024)
	declare @sro xml
	declare @createDate datetime
	declare @modifiedDate datetime
	declare @weighted bit
	declare cur cursor for select [ID], [Code], [Name], [IdRRef], [Parents], [SRO], [CreateDate], [ModifiedDate], [Weighted] 
		from @remote_data
	open cur
	fetch next from cur into @id, @code, @name, @idRRef, @parents, @sro, @createDate, @modifiedDate, @weighted
	while @@fetch_status = 0 begin
		-- Check exists local data.
		if (@update = 1) begin
			if (exists (select 1 from [db_scales].[Nomenclature] where [Id]=@id)) begin
				update [db_scales].[Nomenclature] set 
					 [Code]=@code
					,[Name]=@name
					,[IdRRef]=@idRRef
					,[SerializedRepresentationObject]=@sro
					,[CreateDate]=@createDate
					,[ModifiedDate]=@modifiedDate
					,[Weighted]=@weighted
				where [Id]=@id
				print N'[*] [db_scales].[Nomenclature] updated where [Id]=' + cast(@id as nvarchar(255))
			end else begin
				insert into @nom_changed([Id]) values (@id)
				print N'[!] [db_scales].[Nomenclature] not updated where [Id]=' + cast(@id as nvarchar(255))
			end
		end else begin
			if not(exists (select 1 from [db_scales].[Nomenclature] where [Id]=@id)) begin
				insert into [db_scales].[Nomenclature]([ID], [Code], [Name], [IdRRef], [SerializedRepresentationObject], 
					[CreateDate], [ModifiedDate], [Weighted])
					values (@id, @code, @name, @idRRef, @sro, @createDate, @modifiedDate, @weighted)
				print N'[+] [db_scales].[Nomenclature] inserted where [Id]=' + cast(@id as nvarchar(255))
			end else begin
				insert into @nom_changed([Id]) values (@id)
				print N'[!] [db_scales].[Nomenclature] not inserted where [Id]=' + cast(@id as nvarchar(255))
			end
		end
		-- Next cursor record.
		fetch next from cur into @id, @code, @name, @idRRef, @parents, @sro, @createDate, @modifiedDate, @weighted
	end
	close cur
	deallocate cur
-- Uncommitted local data.
select * from [db_scales].[Nomenclature] [N] 
where [N].[Code] in (select [Code] from @products) and [N].[Id] not in (select [Id] from @nom_changed) order by [Id]
-- Settings.
if (@commit = 1) begin
	commit tran
	print N'[+] Commit is enabled'
end else begin
	rollback tran
	print N'[-] Commit is disabled'
end
set nocount off
-- Committed local data.
select * from [db_scales].[Nomenclature] [N] where [N].[Code] in (select [Code] from @products) order by [Id]
