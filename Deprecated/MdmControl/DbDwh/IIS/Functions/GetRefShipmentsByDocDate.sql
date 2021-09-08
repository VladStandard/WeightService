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
	set @EndDate = isnull(@EndDate, getdate())
	declare @xml xml
	if (datediff(hour, @StartDate, @EndDate) > 25) begin
		set @xml = (select 'Error. Interval too long (more than 25 hours).' as [MESSAGE] for xml raw, root('Shipments'), binary base64)
		return cast (@xml as nvarchar(max))
	end
	if (@RowCount > 15) begin
		set @xml = (select 'Error. Value @RowCount Not more than 15.' as [MESSAGE] for xml raw, root('Shipments'), binary base64)
		return cast (@xml as nvarchar(max))
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
go
