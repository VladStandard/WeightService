-- [IIS].[fnGetSipmentListByShippingDate]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetSipmentListByShippingDate]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnGetSipmentListByShippingDate]
(
	@StartDate datetime, 
	@EndDate datetime = null
)
RETURNS xml
AS
BEGIN
	-- Deprecated method!
	RETURN (SELECT N'Deprecated method!' [Message] 
	FOR XML RAW('Result'), ROOT('Response'), BINARY BASE64)

	SET @EndDate = ISNULL(@EndDate,GETDATE());
	IF (DATEDIFF(day, @StartDate, @EndDate) > 5) BEGIN
		RETURN  
			(
			SELECT 'Error. Interval too long (more than 5 days).' AS [MESSAGE] 
			FOR XML RAW 
			,ROOT('Summary')
			,BINARY BASE64
			);
	END;
	DECLARE @t TABLE (
		[Tag] int, 
		[Parent] int,
		[Shipment!3!ID] bigint, 
		[Shipment!3!DLM] datetime,
		[Shipment!3!Marked]	bit,
		[Shipment!3!Posted]	bit,
		[Shipment!3!CHECKSUMM] int,
		[Shipment!3!ShippingDate] datetime,
		[Aggregation!4!Shipments] int
	)
	INSERT INTO @t (
		[Tag], 
		[Parent],
		[Shipment!3!ID], 
		[Shipment!3!DLM],
		[Shipment!3!Marked],
		[Shipment!3!Posted],
		[Shipment!3!CHECKSUMM],
		[Shipment!3!ShippingDate]
	)
	SELECT DISTINCT
		3				as [Tag], 
		null			as [Parent],
		doc.ID			as [Shipment!3!ID], 
		doc.[DLM]		as [Shipment!3!DLM],
		doc.Marked		as [Shipment!3!Marked],
		doc.Posted		as [Shipment!3!Posted],
		CHECKSUM_AGG(cast(Shipments.[CHECKSUMM] as int)) as [Shipment!3!CHECKSUMM],
		doc.[DocDate]	as [Shipment!3!DateDoc]
	FROM [DW].[FactSalesOfGoods] Shipments
	INNER JOIN [DW].[DocJournal] as doc
		ON Shipments.[CodeInIS] = doc.[CodeInIS] 
		AND Shipments.InformationSystemID = doc.InformationSystemID
	WHERE 
		[doc].[DocDate] between @StartDate and @EndDate
	GROUP BY doc.ID, doc.[DLM],doc.Marked,doc.Posted, doc.[DocDate];

	INSERT INTO @t ([Tag], [Parent],[Aggregation!4!Shipments])
	SELECT 	4,null, COUNT([Shipment!3!ID]) FROM  @t

	DECLARE @xml xml;
	SET @xml = (SELECT DISTINCT * FROM @t ORDER BY [Aggregation!4!Shipments] DESC FOR XML EXPLICIT, ROOT('Summary'), BINARY BASE64);

	RETURN @xml;
END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[fnGetSipmentListByShippingDate] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @StartDate DATETIME = '2021-01-01T00:00:00'
DECLARE @EndDate DATETIME = '2021-07-10T00:00:00'
SELECT [IIS].[fnGetSipmentListByShippingDate](@StartDate,@EndDate) [fnGetSipmentListByShippingDate]
