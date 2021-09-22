-- [IIS].[GetRefShipmentsByDLM]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[GetRefShipmentsByDLM]
DROP FUNCTION IF EXISTS [IIS].[GetRefShipmentsByDLMv2]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[GetRefShipmentsByDLM] (@StartDate DATETIME, @EndDate DATETIME = NULL, @Offset INT = 0, @RowCount INT = 10) RETURNS NVARCHAR(MAX)
AS
BEGIN
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
		SELECT * FROM 
		(
				SELECT DISTINCT doc.ID as "id", null "idc"
				FROM [DW].[FactSalesOfGoods] ss
				INNER JOIN [DW].[DocJournal] doc
				ON 
					ss.[CodeInIS] = doc.[CodeInIS]
					AND ss.InformationSystemID = doc.InformationSystemID
				WHERE 
				((ss.[DLM] >= @StartDate) OR (@StartDate is null))
				AND ((ss.[DLM] < @EndDate)	OR (@EndDate is null))
				GROUP BY doc.[ID]
		
				UNION ALL

				SELECT DISTINCT null "id", doc.ID as "idc"
				FROM [DW].[FactReturns] ss
				INNER JOIN [DW].[DocJournal] doc
				ON 
					ss.[CodeInIS] = doc.[CodeInIS]
					AND ss.InformationSystemID = doc.InformationSystemID
				WHERE 
				((ss.[DLM] >= @StartDate)	OR (@StartDate is null))
				AND ((ss.[DLM] < @EndDate)	OR (@EndDate is null))
				GROUP BY doc.[ID]
		
		) AS XXX 
		ORDER BY [ID],[IDC] OFFSET (@Offset*@RowCount) ROWS FETCH NEXT @RowCount ROWS ONLY FOR JSON PATH)
	END
	RETURN @json
END
GO

-- ACCESS.
GRANT EXECUTE ON [IIS].[fnGetSipmentListByDocDate] TO [TerraSoftRole]
GO

-- CHECK FUNCTION.
DECLARE @StartDate DATETIME = '2021-01-01T00:00:00'
DECLARE @EndDate DATETIME = '2021-10-22T00:00:00'
DECLARE @Offset INT = 0
DECLARE @RowCount INT = 10
SELECT [IIS].[GetRefShipmentsByDLM](@StartDate, @EndDate, @Offset, @RowCount) [GetRefShipmentsByDLM]
