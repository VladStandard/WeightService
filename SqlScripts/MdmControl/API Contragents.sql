-- API /api/Contragents/
-- https://t1000.kolbasa-vs.local:4432/api/Contragents/?StartDate=2021-07-07T00:00:00&EndDate=2021-07-08T00:00:00&Offset=4200&RowCount=1000
declare @StartDate datetime = '2021-01-01T00:00:00'
declare @EndDate datetime = '2021-07-08T00:00:00'
declare @Offset int = 0
declare @RowCount int = 10000
select [IIS].[fnGetContragentChangesList] (@StartDate, @EndDate, @Offset, @RowCount)

select [c].[ID],[c].*
from [DW].[DimContragents] [c]
where 
	[c].[Marked] = 0 
	and (([c].[DLM] >= @StartDate) or (@StartDate is null))
	and (([c].[DLM] < @EndDate) or (@EndDate is null))
order by [c].[ID]
offset @Offset rows fetch next @RowCount rows only;
