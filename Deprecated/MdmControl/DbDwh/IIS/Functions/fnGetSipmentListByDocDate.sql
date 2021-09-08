-- [IIS].[fnGetSipmentListByDocDate]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetSipmentListByDocDate]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnGetSipmentListByDocDate] (@StartDate DATETIME, @EndDate DATETIME = NULL)
RETURNS XML
AS
BEGIN
	DECLARE @xml XML;
	SET @EndDate = ISNULL(@EndDate, GETDATE());
	IF (DATEDIFF(DAY, @StartDate, @EndDate) > 10)
	BEGIN
		SET @xml = (SELECT
				'Error. Interval too long (more than 10 days).' AS [MESSAGE]
			FOR XML RAW
			, ROOT ('Summary')
			, BINARY BASE64);
		RETURN @xml;
	END;
	DECLARE @t TABLE (
		[Tag] INT
	   ,[Parent] INT
	   ,[Shipment!3!ID] BIGINT
	   ,[Shipment!3!DLM] DATETIME
	   ,[Shipment!3!DateDoc] DATETIME
	   ,[Shipment!3!Marked] BIT
	   ,[Shipment!3!Posted] BIT
	   ,[Shipment!3!CHECKSUMM] INT
	   ,[Shipment!3!GUID] VARCHAR(38)
	   ,[Aggregation!4!Contragents] INT
	   ,[Aggregation!4!Nomenclatures] INT
	   ,[Aggregation!4!Shipments] INT
	)
	INSERT INTO @t ([Tag],
	[Parent],
	[Shipment!3!ID],
	[Shipment!3!DLM],
	[Shipment!3!DateDoc],
	[Shipment!3!Marked],
	[Shipment!3!Posted],
	[Shipment!3!CHECKSUMM],
	[Shipment!3!GUID])
		SELECT DISTINCT
			3 AS [Tag]
		   ,NULL AS [Parent]
		   ,doc.ID AS [Shipment!3!ID]
		   ,doc.[DLM] AS [Shipment!3!DLM]
		   ,doc.[DocDate] AS [Shipment!3!DateDoc]
		   ,doc.Marked AS [Shipment!3!Marked]
		   ,doc.Posted AS [Shipment!3!Posted]
		   ,CHECKSUM_AGG(CAST(Shipments.[CHECKSUMM] AS INT)) AS [Shipment!3!CHECKSUMM]
		   ,[DW].[fnGetGuid1C](Shipments.[CodeInIS]) AS [Shipment!3!GUID]
		FROM [DW].[FactSalesOfGoods] Shipments
		INNER JOIN [DW].[DocJournal] AS doc
			ON Shipments.[CodeInIS] = doc.[CodeInIS]
				AND Shipments.InformationSystemID = doc.InformationSystemID
		WHERE [doc].[DocDate] BETWEEN @StartDate AND @EndDate
		GROUP BY doc.ID
				,doc.[DLM]
				,doc.Marked
				,doc.Posted
				,doc.[DocDate]
				,Shipments.[CodeInIS];
	INSERT INTO @t ([Tag], [Parent], [Aggregation!4!Shipments])
		SELECT
			4
		   ,NULL
		   ,COUNT([Shipment!3!ID])
		FROM @t
	SET @xml = (SELECT DISTINCT *
		FROM @t
		ORDER BY [Aggregation!4!Shipments] DESC
		FOR XML EXPLICIT, ROOT ('Summary'), BINARY BASE64);
	RETURN @xml;
END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[fnGetSipmentListByDocDate] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @StartDate DATETIME = '2021-01-01T00:00:00'
DECLARE @EndDate DATETIME = '2021-01-10T00:00:00'
SELECT [IIS].[fnGetSipmentListByDocDate](@StartDate, @EndDate) [fnGetSipmentListByDocDate]
