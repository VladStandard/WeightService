-- [IIS].[GetRefShipmentsById]

-- DROP FUNCTION.
DROP FUNCTION IF EXISTS [IIS].[GetRefShipmentsById]
DROP FUNCTION IF EXISTS [IIS].[GetRefShipmentsByIdv2]
GO

-- CREATE FUNCTION.
CREATE FUNCTION [IIS].[GetRefShipmentsById] (@id BIGINT) RETURNS NVARCHAR(MAX)
AS BEGIN
	DECLARE @json NVARCHAR(MAX)
	SET @json = (
		SELECT @ID as "id", @ID as "idc"
		--select * from 
		--(
		--	select distinct doc.ID as "id", null "idc"
		--	from [DW].[FactSalesOfGoods] [fs]
		--	inner join [DW].[DocJournal] [doc]
		--	on [fs].[CodeInIS] = [doc].[CodeInIS] and [fs].InformationSystemID = [doc].InformationSystemID
		--	where [fs].[Id] = @id
		--	--group by doc.[ID]
		--	union all
		--	select distinct null "id", doc.ID as "idc"
		--	from [DW].[FactReturns] [fr]
		--	inner join [DW].[DocJournal] [doc]
		--	on [fr].[CodeInIS] = doc.[CodeInIS] and [fr].InformationSystemID = [doc].InformationSystemID
		--	where [fr].[Id] = @id
		--	--group by doc.[ID]
		--) as XXX 
		--order by [ID],[IDC]
		for json path)
	RETURN @json
END
GO

-- ACCESS.
GRANT EXECUTE ON [IIS].[GetRefShipmentsById] TO [TerraSoftRole]
GO

-- CHECK FUNCTION.
DECLARE @id BIGINT = -9223372036853859140
SELECT [IIS].[GetRefShipmentsById] (@id) [GetRefShipmentsById]
