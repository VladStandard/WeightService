-- Scales - PLU insert new
-- 1. Connect from PALYCH\LUTON
-- Before see the script: Scales - Import nomenclatures
set nocount on
use [ScalesDB]
declare @products table ([code] nvarchar(255), [plu] int, [num] int)
declare @template_id int
-- 2. Setup settings.
declare @update bit = 0
declare @commit bit = 0
declare @host nvarchar(255) = N'SCALES-MON-005'
declare @template_name nvarchar(255) = 'I2OF5 Template80x100unit_I2OF5_300dpi_tsc'
-- Set.
declare @scale_id int = (select [s].[Id] from [db_scales].[Scales] [s] left join [db_scales].[Hosts] [h] on [s].[HostId]=[h].[Id] where [h].[Name] = @host)
set @template_id = (select [Id] from [db_scales].[Templates] where [Title]=@template_name)
-- 3. Fill PLU list from nomenclatures.
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
-- Select local data.
--select * from [ScalesDB].[db_scales].[Nomenclature] [N] where [N].[Code] in (select [Code] from @products)
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
	,[AdditionalDescriptionOfNomenclature] [Days]
FROM [VSDWH].[DW].[DimNomenclatures] 
WHERE [Marked]=0
AND [Code] in (' + @codes + ')
ORDER BY [Name]
'
-- Table-variable.
declare @remote_data table ([ID] int, [Code] nvarchar(30), [Name] nvarchar(150), [IdRRef] uniqueidentifier, [SerializedRepresentationObject] nvarchar(1024),
	[CreateDate] datetime, [ModifiedDate] datetime, [Weighted] bit, [Days] nvarchar(1024))
-- Push remote data into table-variable.
declare @tsql nvarchar(max)
set @tsql = 'SELECT * FROM OPENQUERY([SQLSRSP01\LEEDS], ''' + @cmd_select + ''')'
insert into @remote_data exec(@tsql)
-- Cusrsor.
begin tran
	declare @id int
	declare @name nvarchar(300)
	declare @plu int
	declare @createDate datetime
	declare @modifiedDate datetime
	declare @weighted bit
	declare @days_str nvarchar(1024)
	declare @days int
	declare cur cursor for select [n].[Id], [n].[Name], (select [plu] from @products where [code]=[n].[Code]), 
		[Weighted] from [db_scales].[Nomenclature] [n] where [Code] in (select [code] from @products)
	open cur
	fetch next from cur into @id, @name, @plu, @weighted
	while @@fetch_status = 0 begin
		--set @days_str = 'Срок годности: 30 суток при температуре от 0°С до +6°С и относительной влажности воздуха 75%-78%. Упаковано под вакуумом.'
		set @days_str = (select [Days] from @remote_data where [ID] = @id)
		set @days_str = SUBSTRING(SUBSTRING(@days_str, 
		PATINDEX('%[0-9]%', @days_str), LEN(@days_str)), 0, PATINDEX('%[^0-9]%', 
			SUBSTRING(@days_str, 
				PATINDEX('%[0-9]%', @days_str), LEN(@days_str))))
		set @days = cast (@days_str as int)
		print @days
		insert into [db_scales].[PLU] ([GoodsName],[GoodsFullName],[GoodsDescription],[TemplateID],[GTIN],[EAN13],[ITF14]
			,[GoodsShelfLifeDays],[GoodsTareWeight],[GoodsBoxQuantly],[ScaleId],[NomenclatureId],[Plu]
			,[CreateDate],[ModifiedDate],[Active],[UpperWeightThreshold],[NominalWeight],[LowerWeightThreshold]
			,[CheckWeight],[Marked])
		values (@name,@name,null,@template_id,null,null,null,0,0,0,@scale_id,@id,@plu
			,getdate(),getdate(),1,0,0,0,@weighted,0)
		fetch next from cur into @id, @name, @plu, @weighted
	end
	close cur
	deallocate cur
-- Uncommitted local data.
select * from [ScalesDB].[db_scales].[PLU] where [ScaleId]=@scale_id
-- Settings.
if (@commit = 1) begin
	commit tran
end else begin
	rollback tran
end
set nocount off
-- Committed local data.
select * from [ScalesDB].[db_scales].[PLU] where [ScaleId]=@scale_id
