-- [IIS].[GetRefShipmentsByDocDate]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[GetRefShipmentsByDocDate]
GO

-- CREATE FUNCTION
create function [IIS].[GetRefShipmentsByDocDate] 
(
	@StartDate datetime = null, 
	@EndDate datetime = null, 
	@Offset int = 0, 
	@RowCount int = 10
)
returns nvarchar(max)
as
begin
	declare @days_limit int = 5
	declare @rows_limit int = 15
	set @EndDate = isnull(@EndDate, getdate())
	declare @result nvarchar(1024)
	if (datediff(day, @StartDate, @EndDate) > @days_limit) begin
		return '{ "Error": "Interval ' + cast(datediff(day, @StartDate, @EndDate) as nvarchar(255)) + ' days is can not be more than ' + cast(@days_limit as nvarchar(255)) + ' days!" }'
	end
	if (@RowCount > @rows_limit) begin
		return '{ "Error": "Value ' + cast(@RowCount as nvarchar(255)) + ' is more than ' + cast(@rows_limit as nvarchar(255)) + '!" }'
	end
	declare @jsonVariable nvarchar(max)
	set @jsonVariable = (
	select * from 
	(
		select distinct doc.ID as "id", null "idc"
		from [DW].[FactSalesOfGoods] [fs]
		inner join [DW].[DocJournal] [doc]
		on [fs].[CodeInIS] = [doc].[CodeInIS] and [fs].InformationSystemID = [doc].InformationSystemID
		where (([doc].[DocDate] >= @StartDate) or @StartDate is null) and ([doc].[DocDate] < @EndDate or @EndDate is null)
		group by doc.[ID]
		union all
		select distinct null "id", doc.ID as "idc"
		from [DW].[FactReturns] [fr]
		inner join [DW].[DocJournal] [doc]
		on [fr].[CodeInIS] = doc.[CodeInIS] and [fr].InformationSystemID = [doc].InformationSystemID
		where ([doc].[DocDate] >= @StartDate or @StartDate is null) and ([doc].[DocDate] < @EndDate or @EndDate is null)
		group by doc.[ID]
	) as XXX 
	order by [ID],[IDC] OFFSET (@Offset*@RowCount) rows fetch next @RowCount rows ONLY
	for json path)
	return @jsonVariable
end
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[GetRefShipmentsByDocDate] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @StartDate DATETIME = '2021-09-10T00:00:00'
DECLARE @EndDate DATETIME = '2021-09-16T00:00:00'
DECLARE @Offset INT = 0
DECLARE @RowCount INT = 10
SELECT [IIS].[GetRefShipmentsByDocDate](@StartDate, @EndDate, @Offset, @RowCount) [GetRefShipmentsByDocDate]
