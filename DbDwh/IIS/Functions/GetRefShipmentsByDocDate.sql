-- [IIS].[GetRefShipmentsByDocDate]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[GetRefShipmentsByDocDate]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[GetRefShipmentsByDocDate] (@StartDate DATETIME, @EndDate DATETIME = NULL, @Offset INT = 0, @RowCount INT = 10) RETURNS NVARCHAR(MAX)
AS BEGIN
	-- DECLARE.
	DECLARE @json NVARCHAR(MAX)
	DECLARE @check NVARCHAR(1024) = NULL
	DECLARE @check_xml XML = NULL
	DECLARE @ResultCount INT = 0
	SET @EndDate = ISNULL(@EndDate, GETDATE())
	-- CHECKS.
	SET @check = (select [dbo].[fnCheckDates] (@StartDate, @EndDate))
	IF (@check IS NULL) BEGIN
		SET @check = (select [dbo].[fnCheckRowCount] (@RowCount))
	END 
	IF (@check IS NOT NULL) BEGIN
		SET @check_xml = (SELECT [dbo].[fnGetXmlMessage] (NULL, 'Error', 'Description', @check))
	END
	ELSE BEGIN
		SET @json = (
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
	END
	RETURN @json
END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[GetRefShipmentsByDocDate] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @StartDate DATETIME = '2021-01-01T00:00:00'
DECLARE @EndDate DATETIME = '2021-09-01T00:00:00'
DECLARE @Offset INT = 0
DECLARE @RowCount INT = 10
SELECT [IIS].[GetRefShipmentsByDocDate](@StartDate, @EndDate, @Offset, @RowCount) [GetRefShipmentsByDocDate]
