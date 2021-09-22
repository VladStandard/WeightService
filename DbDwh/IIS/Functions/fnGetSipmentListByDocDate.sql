-- [IIS].[fnGetSipmentListByDocDate]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetSipmentListByDocDate]
DROP FUNCTION IF EXISTS [IIS].[fnGetSipmentListByDocDatev2]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnGetSipmentListByDocDate] (@StartDate DATETIME, @EndDate DATETIME = NULL)
RETURNS XML
AS
BEGIN
	-- DECLARE.
	DECLARE @xml XML = '<Response />'
	DECLARE @check NVARCHAR(1024) = NULL
	DECLARE @check_xml XML = NULL
	DECLARE @ResultCount INT = 0
	SET @EndDate = ISNULL(@EndDate, GETDATE())
	-- CHECKS.
	SET @check = (select [dbo].[fnCheckDates] (@StartDate, @EndDate))
	IF (@check IS NOT NULL) BEGIN
		SET @check_xml = (SELECT [dbo].[fnGetXmlMessage] (NULL, 'Error', 'Description', @check))
		SET @xml.modify('insert sql:variable("@check_xml") as first into (/Response)[1]')
	END
	ELSE BEGIN
		-- DECLARE TABLE.
		DECLARE @TABLE TABLE (
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
		-- INSERT.
		INSERT INTO @TABLE ([Tag], [Parent], [Shipment!3!ID], [Shipment!3!DLM], [Shipment!3!DateDoc], [Shipment!3!Marked], [Shipment!3!Posted], [Shipment!3!CHECKSUMM], [Shipment!3!GUID_1C])
			SELECT DISTINCT 3 [Tag], NULL [Parent]
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
			GROUP BY [DOC].ID, [DOC].[DLM], [DOC].Marked, [DOC].Posted, [DOC].[DocDate], [SHIPMENTS].[CodeInIS]
		INSERT INTO @TABLE ([Tag], [Parent], [Aggregation!4!Shipments])
			SELECT 4, NULL, COUNT([Shipment!3!ID]) FROM @TABLE
		-- IMPORT.
		SET @ResultCount = (SELECT COUNT (1) FROM @TABLE)
		SET @xml = (SELECT *
			FROM @TABLE
			ORDER BY [Aggregation!4!Shipments] DESC, [Shipment!3!DLM]
			FOR XML EXPLICIT, ROOT ('Response'), BINARY BASE64)
	END
	-- ATTRIBUTES.
	IF (@xml IS NULL) BEGIN
		SET @xml = '<Response />'
	END
	SET @xml.modify ('insert attribute StartDate{sql:variable("@StartDate")} into (/Response)[1] ')
	SET @xml.modify ('insert attribute EndDate{sql:variable("@EndDate")} into (/Response)[1] ')
	SET @xml.modify ('insert attribute ResultCount{sql:variable("@ResultCount")} into (/Response)[1] ')
	-- RESULT.
	RETURN @xml
END
GO

-- ACCESS.
GRANT EXECUTE ON [IIS].[fnGetSipmentListByDocDate] TO [TerraSoftRole]
GO

-- CHECK FUNCTION.
DECLARE @StartDate DATETIME = '2021-08-01T00:00:00'
DECLARE @EndDate DATETIME = '2021-09-22T00:00:00'
SELECT [IIS].[fnGetSipmentListByDocDate](@StartDate, @EndDate) [fnGetSipmentListByDocDate]
