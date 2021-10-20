-- Scales - Import nomenclatures
-- 1. Connect from PALYCH\LUTON
-- After all see the script: Scales - PLU insert new
set nocount on
use [ScalesDB]
declare @products table ([code] nvarchar(255), [plu] int, [num] int)
-- 2. Setup settings.
declare @commit bit = 0
-- 3. Fill PLU table from DWH - 1C.
-- Чудо печка.
--insert into @products([code]) values(N'ЦБД00053844') -- Упаковка "Чудо печка" Куриная голень
--insert into @products([code]) values(N'ЦБД00053845') -- Упаковка "Чудо печка" Куриные крылышки
--insert into @products([code]) values(N'ЦБД00053846') -- Упаковка "Чудо печка" Угорь Унаги
--insert into @products([code]) values(N'ЦБД00053847') -- Упаковка "Чудо печка" Колбаса Хуторская
--insert into @products([code]) values(N'ЦБД00053848') -- Упаковка "Чудо печка" Куриная грудка
-- СЦ Линия №10 - SCALES-MON-004.
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
--insert into @products([code],[plu],[num]) values(N'ЦБД00049186', 147, 5) -- Ореховый п.к.
-- СЦ Линия №11 - SCALES-MON-005.
insert into @products([code],[plu],[num]) values(N'ЦБД00008197', 102, 5) -- Московская  в.к. ГОСТ
insert into @products([code],[plu],[num]) values(N'ЦБД00002181', 103, 5) -- Мадейра в.к.
insert into @products([code],[plu],[num]) values(N'000В0000431', 104, 5) -- Дворянская в.к.
insert into @products([code],[plu],[num]) values(N'000В0000477', 106, 5) -- Суздальский в.к.
insert into @products([code],[plu],[num]) values(N'000В0000472', 107, 5) -- Кремлевский в.к.
insert into @products([code],[plu],[num]) values(N'000В0000478', 108, 5) -- Юбилейный в.к.
insert into @products([code],[plu],[num]) values(N'000В0000470', 109, 5) -- Зернистый в.к.
insert into @products([code],[plu],[num]) values(N'ЦБД00012800', 112, 5) -- Коньячный в.к.
insert into @products([code],[plu],[num]) values(N'ЦБД00011413', 113, 5) -- Владимирский в.к.
insert into @products([code],[plu],[num]) values(N'000В0000474', 114, 5) -- Салями "Домашняя" п.к.
insert into @products([code],[plu],[num]) values(N'000В0000379', 115, 5) -- Кремлёвская с грудинкой
insert into @products([code],[plu],[num]) values(N'000В0000475', 117, 5) -- Салями по-Фински п.к.
insert into @products([code],[plu],[num]) values(N'000В0000386', 120, 5) -- Чесночная стандарт
insert into @products([code],[plu],[num]) values(N'ЦБД00026237', 132, 5) -- Ореховый п.к.
insert into @products([code],[plu],[num]) values(N'ЦБД00037388', 137, 5) -- Сервелат с телятиной в.к.
insert into @products([code],[plu],[num]) values(N'ЦБД00041861', 147, 5) -- Российский в.к. 1 сорт
insert into @products([code],[plu],[num]) values(N'ЦБД00041860', 148, 5) -- Городской стандарт в.к. 1 сорт
insert into @products([code],[plu],[num]) values(N'ЦБД00041859', 149, 5) -- Деликатесная п.к. 1 сорт
insert into @products([code],[plu],[num]) values(N'ЦБД00053241', 153, 5) -- С беконом по-деревенски
insert into @products([code],[plu],[num]) values(N'ЦБД00049186', 155, 5) -- Московская в.к. ГОСТ ВС
insert into @products([code],[plu],[num]) values(N'ЦБД00011404', 156, 5) -- Сочная п/а
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
    ,[Parents] [SerializedRepresentationObject]
	,[CreateDate]
	,[DLM] [ModifiedDate]
	,[Weighted]
FROM [VSDWH].[DW].[DimNomenclatures] 
WHERE [Marked]=0
AND [Code] in (' + @codes + ')
ORDER BY [Name]
'
-- Table-variable.
declare @remote_data table ([ID] int, [Code] nvarchar(30), [Name] nvarchar(150), [IdRRef] uniqueidentifier, [SerializedRepresentationObject] nvarchar(1024),
	[CreateDate] datetime, [ModifiedDate] datetime, [Weighted] bit)
-- Push remote data into table-variable.
declare @tsql nvarchar(max)
set @tsql = 'SELECT * FROM OPENQUERY([SQLSRSP01\LEEDS], ''' + @cmd_select + ''')'
insert into @remote_data exec(@tsql)
-- Cusrsor.
begin tran
	declare @id int
	declare @code nvarchar(30)
	declare @name nvarchar(150)
	declare @idRRef uniqueidentifier
	declare @sro nvarchar(1024)
	declare @createDate datetime
	declare @modifiedDate datetime
	declare @weighted bit
	declare cur cursor for select [ID], [Code], [Name], [IdRRef], [SerializedRepresentationObject], [CreateDate], [ModifiedDate], [Weighted] from @remote_data
	open cur
	fetch next from cur into @id, @code, @name, @idRRef, @sro, @createDate, @modifiedDate, @weighted
	while @@fetch_status = 0 begin
		-- Check exists local data.
		if not(exists (select 1 from [ScalesDB].[db_scales].[Nomenclature] [N] where [N].[Code] = @code)) begin
			insert into [ScalesDB].[db_scales].[Nomenclature]([ID], [Code], [Name], [IdRRef], [SerializedRepresentationObject], [CreateDate], [ModifiedDate], [Weighted])
				values (@id, @code, @name, @idRRef, @sro, @createDate, @modifiedDate, @weighted)
		end
		-- Next cursor record.
		fetch next from cur into @id, @code, @name, @idRRef, @sro, @createDate, @modifiedDate, @weighted
	end
	close cur
	deallocate cur
-- Uncommitted local data.
select * from [ScalesDB].[db_scales].[Nomenclature] [N] where [N].[Code] in (select [Code] from @products)
-- Settings.
if (@commit = 1) begin
	commit tran
end else begin
	rollback tran
end
set nocount off
-- Committed local data.
select * from [ScalesDB].[db_scales].[Nomenclature] [N] where [N].[Code] in (select [Code] from @products)