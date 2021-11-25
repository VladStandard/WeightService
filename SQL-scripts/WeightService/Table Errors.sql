----------------------------------------------------------------------------------------------------
-- Таблица Errors
----------------------------------------------------------------------------------------------------
if not exists (select * from [sys].[tables] where [name] = 'Errors' and type = 'U') begin
	create table [db_scales].[Errors] (
		[Id]             int              identity (1, 1) not null,
		[CreatedDate]    datetime         default (getdate()) not null,
		[ModifiedDate]   datetime         default (getdate()) not null,
		[FilePath]       nvarchar (1024)  null,
		[LineNumber]     smallint         null,
		[MemberName]     nvarchar (128)   null,
		[Exception]      nvarchar (4000)  not null,
		[InnerException] nvarchar (4000)  null,
		primary key clustered ([Id] ASC) ON [ScalesFileGroup],
	) on [ScalesFileGroup]
end
----------------------------------------------------------------------------------------------------
select *
from [db_scales].[Errors]
order by [CreatedDate] desc
----------------------------------------------------------------------------------------------------
