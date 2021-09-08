CREATE PROCEDURE [scsl].[DeactivateAnyPoints]
	
AS
begin
	update [SCSL].[FactOrders] set Active = 0 where id in (
	select id from [SCSL].[FactOrders] o
	inner join (

		select  [IDinIS],cast([DLM] as date) [DLM] from [SCSL].[FactOrders]
		group by [IDinIS],cast([DLM] as date)
		except 
		select [IDinIS], MAX([DLM]) [DLM] from (
		select  [IDinIS],cast([DLM] as date) [DLM] from [SCSL].[FactOrders]
		group by [IDinIS],cast([DLM] as date)
		) as a
		group by [IDinIS]

	) as c	on o.[IDinIS] = c.[IDinIS] and cast(o.[DLM] as date) = c.[DLM]);
-----------------------------------------------------------------------------

	update [SCSL].[FactSales] set Active = 0 where id in (
	select id from [SCSL].[FactSales] o
	inner join (

		select  [IDinIS],cast([DLM] as date) [DLM] from [SCSL].[FactSales]
		group by [IDinIS],cast([DLM] as date)
		except 
		select [IDinIS], MAX([DLM]) [DLM] from (
		select  [IDinIS],cast([DLM] as date) [DLM] from [SCSL].[FactSales]
		group by [IDinIS],cast([DLM] as date)
		) as a
		group by [IDinIS]

	) as c	on o.[IDinIS] = c.[IDinIS] and cast(o.[DLM] as date) = c.[DLM]);
-----------------------------------------------------------------------------

	update [SCSL].[FactReturns] set Active = 0 where id in (
	select id from [SCSL].[FactReturns] o
	inner join (

		select  [IDinIS],cast([DLM] as date) [DLM] from [SCSL].[FactReturns]
		group by [IDinIS],cast([DLM] as date)
		except 
		select [IDinIS], MAX([DLM]) [DLM] from (
		select  [IDinIS],cast([DLM] as date) [DLM] from [SCSL].[FactReturns]
		group by [IDinIS],cast([DLM] as date)
		) as a
		group by [IDinIS]

	) as c	on o.[IDinIS] = c.[IDinIS] and cast(o.[DLM] as date) = c.[DLM]);


RETURN 0
end
