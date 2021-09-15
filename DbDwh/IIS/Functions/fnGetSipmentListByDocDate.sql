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
	   ,[Shipment!3!GUID_1C] NVARCHAR(38)
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
	[Shipment!3!GUID_1C])
		SELECT DISTINCT
			3 [Tag]
		   ,NULL [Parent]
		   ,[DOC].[ID] [Shipment!3!ID]
		   ,[DOC].[DLM] [Shipment!3!DLM]
		   ,[DOC].[DocDate] [Shipment!3!DateDoc]
		   ,[DOC].[Marked] [Shipment!3!Marked]
		   ,[DOC].[Posted] [Shipment!3!Posted]
		   ,CHECKSUM_AGG(CAST([SHIPMENTS].[CHECKSUMM] AS INT)) [Shipment!3!CHECKSUMM]
		   ,[DW].[fnGetGuid1Cv2]([SHIPMENTS].[CodeInIS]) [Shipment!3!GUID_1C]
		FROM [DW].[FactSalesOfGoods] [SHIPMENTS]
		INNER JOIN [DW].[DocJournal] [DOC] ON [SHIPMENTS].[CodeInIS]=[DOC].[CodeInIS] AND [SHIPMENTS].[InformationSystemID]=[DOC].[InformationSystemID]
		WHERE [DOC].[DocDate] BETWEEN @StartDate AND @EndDate
		GROUP BY [DOC].ID, [DOC].[DLM], [DOC].Marked, [DOC].Posted, [DOC].[DocDate], [SHIPMENTS].[CodeInIS];
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
DECLARE @StartDate DATETIME = '2021-09-10T00:00:00'
DECLARE @EndDate DATETIME = '2021-09-15T00:00:00'
SELECT [IIS].[fnGetSipmentListByDocDate](@StartDate, @EndDate) [fnGetSipmentListByDocDate]
