CREATE PROCEDURE [dbo].[ArchiveIrrelevantData]	
	@ExpiresDate datetime,
	@DeleteData bit = 0
AS
BEGIN

	IF DATEDIFF(day,@ExpiresDate,getdate())<30 
	BEGIN
		RAISERROR ('Еhe @ExpiresDate value must be at least 30 days from the current one.',10,1);
		RETURN 0;
	END;
		
	DECLARE @curdate datetime = DATEADD(DAY, DATEDIFF(DAY,0, @ExpiresDate),0);

	DECLARE @dbnamearc nvarchar(255) = N'ScalesDB_'+format(@curdate,'yyyyMMdd')+'_'+ left(convert(nvarchar(36),newid()),8);
	DECLARE @exec nvarchar(max) = QUOTENAME(@dbnamearc) + N'.[sys].[sp_executesql]';

	DECLARE @sql nvarchar(max) = 
	N'IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = '''+QUOTENAME(@dbnamearc)+''')
	 BEGIN
		CREATE DATABASE '+QUOTENAME(@dbnamearc)+';
		ALTER AUTHORIZATION ON DATABASE::'+QUOTENAME(@dbnamearc)+' TO [sa];
		ALTER DATABASE '+QUOTENAME(@dbnamearc)+' SET RECOVERY SIMPLE;
	 END;';
	EXEC (@sql);

	SET @sql = N'
		CREATE SCHEMA [db_scales] AUTHORIZATION [dbo];
		';
		print @sql;
	EXEC @exec @sql;

	SET @sql = N'
		begin tran

		declare @t table (Id int)
		declare @tt table (Id int)

		insert into @t (id) select Id 
		from [ScalesDB].[db_scales].[ProductSeries]
		where [CreateDate] < @curdate;

		select * into '+QUOTENAME(@dbnamearc)+'.[db_scales].[ProductSeries]
		from [ScalesDB].[db_scales].[ProductSeries]
		where [Id] in (select Id from @t);
	
		insert into @tt (id)
		select Id
		from [ScalesDB].[db_scales].[WeithingFact]
		where [SeriesId] in (select Id from @t);

		select * into '+QUOTENAME(@dbnamearc)+'.[db_scales].[WeithingFact]
		from [ScalesDB].[db_scales].[WeithingFact]
		where [Id] in (select Id from @tt);

		select * into '+QUOTENAME(@dbnamearc)+'.[db_scales].[Labels]
		from [ScalesDB].[db_scales].[Labels]
		where [WeithingFactId] in (select Id from @tt);

	';

	if @DeleteData = 1 begin
		SET @sql = @sql + N'

			delete from [ScalesDB].[db_scales].[Labels]
			where [WeithingFactId] in (select Id from @tt);

			delete from [ScalesDB].[db_scales].[WeithingFact]
			where [Id] in (select Id from @tt);

			delete from [ScalesDB].[db_scales].[ProductSeries]
			where [Id] in (select Id from @t);

		';
	end;
	SET @sql = @sql + N'
		commit tran;
		';
	EXEC @exec @sql, N'@curdate datetime', @curdate;

	RETURN 1;

END
GO