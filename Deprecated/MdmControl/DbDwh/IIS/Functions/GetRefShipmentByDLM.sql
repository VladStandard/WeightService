CREATE FUNCTION [IIS].[GetRefShipmentsByDLM] 
(
	@StartDate datetime = null, 
	@EndDate datetime = null, 
	@Offset int = 0, 
	@RowCount int = 10
)
RETURNS NVARCHAR(MAX)
AS
BEGIN
	
	SET @EndDate = ISNULL(@EndDate,GETDATE());

	IF (DATEDIFF(hour, @StartDate, @EndDate) > 25) BEGIN
		RETURN 
			(
			SELECT 'Error. Interval too long (more than 25 hours).' AS [MESSAGE] 
			FOR XML RAW 
			,ROOT('Shipments')
			,BINARY BASE64
			)
	END

	IF ( @RowCount > 15) BEGIN
		RETURN 
			(
			SELECT 'Error. Value @RowCount Not more than 15.' AS [MESSAGE] 
			FOR XML RAW 
			,ROOT('Shipments')
			,BINARY BASE64
			)
	END

	DECLARE @jsonVariable NVARCHAR(MAX);	

	SET @jsonVariable = (
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
	ORDER BY [ID],[IDC] OFFSET (@Offset*@RowCount) ROWS FETCH NEXT @RowCount ROWS ONLY
	FOR JSON PATH);

	RETURN @jsonVariable;

END
GO