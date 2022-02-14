-- [IIS].[fnGetSummaryList]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetSummaryList]
DROP FUNCTION IF EXISTS [IIS].[fnGetSummaryListv2]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnGetSummaryList] (@StartDate DATETIME, @EndDate DATETIME = NULL) RETURNS XML
AS BEGIN
	-- DECLARE.
	DECLARE @xml XML = '<Summary />'
	DECLARE @check NVARCHAR(1024) = NULL
	DECLARE @check_xml XML = NULL
	DECLARE @ResultCount INT = 0
	SET @EndDate = ISNULL(@EndDate, GETDATE())
	-- CHECKS.
	SET @check = (select [dbo].[fnCheckDates] (@StartDate, @EndDate))
	IF (@check IS NOT NULL) BEGIN
		SET @check_xml = (SELECT [dbo].[fnGetXmlMessage] (NULL, 'Error', 'Description', @check))
		SET @xml.modify('insert sql:variable("@check_xml") as first into (/Summary)[1]')
	END
	ELSE 
	BEGIN
		DECLARE @Table TABLE (
			[Tag] int, 
			[Parent] int,
			[Contragent!1!ID] bigint,
			[Contragent!1!DLM] datetime,
			[Contragent!1!GUID_1C] nvarchar(38),
			[Nomenclature!2!ID]bigint, 
			[Nomenclature!2!DLM]datetime,
			[Nomenclature!2!GUID_1C] nvarchar(38),
			[Shipment!3!ID]bigint, 
			[Shipment!3!Posted] bit, 
			[Shipment!3!Marked] bit, 
			[Shipment!3!Docdate] datetime,
			[Shipment!3!DLM]datetime,
			[Shipment!3!GUID_1C] nvarchar(38),
			[Shipment!3!CHECKSUMM] int,
			[Aggregation!4!Contragents] int, 
			[Aggregation!4!Nomenclatures]int,
			[Aggregation!4!Shipments] int
		)
		insert into @Table ([Tag], [Parent], [Contragent!1!ID], [Contragent!1!DLM], [Contragent!1!GUID_1C], [Nomenclature!2!ID],  [Nomenclature!2!DLM], [Nomenclature!2!GUID_1C],
				[Shipment!3!ID], [Shipment!3!Posted], [Shipment!3!Marked], [Shipment!3!Docdate], [Shipment!3!DLM], [Shipment!3!GUID_1C], [Shipment!3!CHECKSUMM])
			SELECT DISTINCT
				1 [Tag], 
				NULL [Parent],
				Contragent.[ID] [Contragent!1!ID],
				Contragent.[DLM] [Contragent!1!DLM],
				[DW].[fnGetGuid1Cv2] (Contragent.[CodeInIS]) [Contragent!1!GUID_1C],
				NULL [Nomenclature!2!ID], 
				NULL [Nomenclature!2!DLM],
				NULL [Nomenclature!2!GUID_1C],
				NULL [Shipment!3!ID], 
				NULL [Shipment!3!Posted], 
				NULL [Shipment!3!Marked], 
				NULL [Shipment!3!Docdate],
				NULL [Shipment!3!DLM],
				NULL [Shipment!3!GUID_1C],
				NULL [Shipment!3!CHECKSUMM]
			FROM DW.DimContragents Contragent
			WHERE 
				--Contragent.[IsBuyer] = 1  
				Contragent.[Marked] = 0 
				AND Contragent.[DLM] between @StartDate and @EndDate
			UNION ALL
			SELECT DISTINCT
				2 [Tag], 
				NULL [Parent],
				NULL [Contragent!1!ID],
				NULL [Contragent!1!DLM],
				NULL [Contragent!1!GUID_1C],
				Nomenclature.[ID] [Nomenclature!2!ID], 
				Nomenclature.[DLM] [Nomenclature!2!DLM],
				[DW].[fnGetGuid1Cv2] (Nomenclature.[CodeInIS]) [Nomenclature!2!GUID_1C],
				NULL [Shipment!3!ID], 
				NULL [Shipment!3!Posted], 
				NULL [Shipment!3!Marked], 
				NULL [Shipment!3!Docdate],
				NULL [Shipment!3!DLM],
				NULL [Shipment!3!GUID_1C],
				NULL [Shipment!3!CHECKSUMM]
			FROM [DW].[DimNomenclatures] Nomenclature
				  INNER JOIN [DW].[DimTypesOfNomenclature] t
				  ON Nomenclature.NomenclatureType = t.[CodeInIS] 
				  AND Nomenclature.[InformationSystemID] = t.[InformationSystemID]
			WHERE
				Nomenclature.[DLM] between @StartDate and @EndDate 
				AND t.[GoodsForSale] = 1

			UNION ALL

			SELECT DISTINCT
				3 [Tag], 
				NULL [Parent],
				NULL [Contragent!1!ID],
				NULL [Contragent!1!DLM],
				NULL [Contragent!1!GUID_1C],

				NULL [Nomenclature!2!ID], 
				NULL [Nomenclature!2!DLM],
				NULL [Nomenclature!2!GUID_1C],

				[Doc].ID [Shipment!3!ID], 
				[Doc].Posted [Shipment!3!Posted], 
				[Doc].Marked [Shipment!3!Marked], 
				[Doc].DocDate [Shipment!3!Docdate],
				[Doc].[DLM] [Shipment!3!DLM],
				[DW].[fnGetGuid1Cv2] (Shipments.[CodeInIS]) [Shipment!3!GUID_1C],
				CHECKSUM_AGG(cast(Shipments.[CHECKSUMM] as int)) [Shipment!3!CHECKSUMM]

				FROM [DW].[FactSalesOfGoods] Shipments
				INNER JOIN [DW].[DocJournal] doc
					ON Shipments.[CodeInIS] = [Doc].[CodeInIS] 
					AND Shipments.InformationSystemID = [Doc].InformationSystemID
				WHERE 
					Shipments.[DLM] between @StartDate and @EndDate 
				GROUP BY [Doc].ID, [Doc].[DLM], Shipments.[CodeInIS], [Doc].Posted, [Doc].Marked, [Doc].Docdate

		insert into @Table ([Tag], [Parent], [Shipment!3!ID], [Shipment!3!Posted], [Shipment!3!Marked], [Shipment!3!Docdate], [Shipment!3!DLM], [Shipment!3!GUID_1C], 
			[Shipment!3!CHECKSUMM])
		SELECT DISTINCT
			3 [Tag], 
			NULL [Parent],
			[Doc].ID [Shipment!3!ID], 
			[Doc].Posted [Shipment!3!Posted], 
			[Doc].Marked [Shipment!3!Marked], 
			[Doc].DocDate [Shipment!3!Docdate],
			[Doc].[DLM] [Shipment!3!DLM],
			[DW].[fnGetGuid1Cv2] (Shipments.[CodeInIS]) [Shipment!3!GUID_1C],
			CHECKSUM_AGG(cast(Shipments.[CHECKSUMM] as int)) [Shipment!3!CHECKSUMM]

			FROM [DW].[FactReturns] Shipments
			INNER JOIN [DW].[DocJournal] doc
				ON Shipments.[CodeInIS] = [Doc].[CodeInIS] 
				AND Shipments.InformationSystemID = [Doc].InformationSystemID
			WHERE [Doc].[DLM] between @StartDate and @EndDate 

			GROUP BY [Doc].ID ,[Doc].[DLM],Shipments.[CodeInIS], [Doc].Posted, [Doc].Marked, [Doc].Docdate

		INSERT INTO @Table (
			[Tag], 
			[Parent],
			[Aggregation!4!Contragents], 
			[Aggregation!4!Nomenclatures],
			[Aggregation!4!Shipments])
		SELECT 	
			4,
			NULL,	
			COUNT([Contragent!1!ID]),	
			COUNT([Nomenclature!2!ID]), 
			COUNT([Shipment!3!ID]) 
		FROM  @Table
		-- IMPORT.
		SET @ResultCount = (SELECT COUNT (1) FROM @Table)
		SET @xml = (SELECT DISTINCT * from  @Table ORDER BY [Aggregation!4!Shipments] DESC FOR XML EXPLICIT, ROOT('Summary'), BINARY BASE64)
	END
	-- ATTRIBUTES.
	IF (@xml IS NULL) BEGIN
		SET @xml = '<Summary />'
	END
	SET @xml.modify ('insert attribute StartDate{sql:variable("@StartDate")} into (/Summary)[1] ')
	SET @xml.modify ('insert attribute EndDate{sql:variable("@EndDate")} into (/Summary)[1] ')
	SET @xml.modify ('insert attribute ResultCount{sql:variable("@ResultCount")} into (/Summary)[1] ')
	-- RESULT.
	RETURN @xml
END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[fnGetSummaryList] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @StartDate DATETIME = '2021-04-01T00:00:00'
DECLARE @EndDate DATETIME = '2021-05-30T00:00:00'
SELECT [IIS].[fnGetSummaryList](@StartDate,@EndDate) [fnGetSummaryList]
