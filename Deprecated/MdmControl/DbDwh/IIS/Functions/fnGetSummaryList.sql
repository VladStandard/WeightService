-- [IIS].[fnGetSummaryList]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetSummaryList]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnGetSummaryList]
(
	@StartDate datetime,
	@EndDate datetime = null
)
RETURNS xml
AS
BEGIN
	SET @EndDate = ISNULL(@EndDate,GETDATE());
	declare @xml xml
	if (datediff(day, @StartDate, @EndDate) > 10) begin
		set @xml =  (select 'Error. Interval too long (more than 10 days).' as [MESSAGE] for xml raw, root('Summary'), binary base64)
		return @xml
	end
	DECLARE @t TABLE (

		[Tag] int, 
		[Parent] int,
		[Contragent!1!ID] bigint,
		[Contragent!1!DLM] datetime,
		[Contragent!1!GUID] varchar(38),

		[Nomenclature!2!ID]bigint, 
		[Nomenclature!2!DLM]datetime,
		[Nomenclature!2!GUID]varchar(38),

		[Shipment!3!ID]bigint, 
		[Shipment!3!Posted] bit, 
		[Shipment!3!Marked] bit, 
		[Shipment!3!Docdate] datetime,
		[Shipment!3!DLM]datetime,
		[Shipment!3!GUID]varchar(38),
		[Shipment!3!CHECKSUMM] int,

		[Aggregation!4!Contragents] int, 
		[Aggregation!4!Nomenclatures]int,
		[Aggregation!4!Shipments] int

)

insert into @t (
		[Tag], 
		[Parent],
		[Contragent!1!ID],
		[Contragent!1!DLM],
		[Contragent!1!GUID],
		[Nomenclature!2!ID], 
		[Nomenclature!2!DLM],
		[Nomenclature!2!GUID],
		[Shipment!3!ID], 
		[Shipment!3!Posted], 
		[Shipment!3!Marked], 
		[Shipment!3!Docdate],
		[Shipment!3!DLM],
		[Shipment!3!GUID],
		[Shipment!3!CHECKSUMM]

	)

	SELECT DISTINCT
		1 as [Tag], 
		null as [Parent],
		Contragent.[ID]  as [Contragent!1!ID],
		Contragent.[DLM] as [Contragent!1!DLM],
		[DW].[fnGetGuid1C](Contragent.[CodeInIS]) as [Contragent!1!GUID],

		null as [Nomenclature!2!ID], 
		null as [Nomenclature!2!DLM],
		null as [Nomenclature!2!GUID],

		null as [Shipment!3!ID], 
		null as [Shipment!3!Posted], 
		null as [Shipment!3!Marked], 
		null as [Shipment!3!Docdate],

		null as [Shipment!3!DLM],
		null as [Shipment!3!GUID],
		null as [Shipment!3!CHECKSUMM]

	FROM DW.DimContragents as Contragent
	WHERE 
		--Contragent.[IsBuyer] = 1  
		Contragent.[Marked] = 0 
		AND Contragent.[DLM] between @StartDate and @EndDate
	UNION ALL
	SELECT DISTINCT
		2 as [Tag], 
		null as [Parent],
		null as [Contragent!1!ID],
		null as [Contragent!1!DLM],
		null as [Contragent!1!GUID],

		Nomenclature.[ID] as [Nomenclature!2!ID], 
		Nomenclature.[DLM] as [Nomenclature!2!DLM],
		[DW].[fnGetGuid1C](Nomenclature.[CodeInIS]) as [Nomenclature!2!GUID],


		null as [Shipment!3!ID], 
		null as [Shipment!3!Posted], 
		null as [Shipment!3!Marked], 
		null as [Shipment!3!Docdate],
		null as [Shipment!3!DLM],
		null as [Shipment!3!GUID],
		null as [Shipment!3!CHECKSUMM]

	FROM [DW].[DimNomenclatures] as Nomenclature
		  INNER JOIN [DW].[DimTypesOfNomenclature] t
		  ON Nomenclature.NomenclatureType = t.[CodeInIS] 
		  AND Nomenclature.[InformationSystemID] = t.[InformationSystemID]
	WHERE
		Nomenclature.[DLM] between @StartDate and @EndDate 
		AND t.[GoodsForSale] = 1

	UNION ALL

	SELECT DISTINCT
		3 as [Tag], 
		null as [Parent],
		null as [Contragent!1!ID],
		null as [Contragent!1!DLM],
		null as [Contragent!1!GUID],

		null as [Nomenclature!2!ID], 
		null as [Nomenclature!2!DLM],
		null as [Nomenclature!2!GUID],

		doc.ID as [Shipment!3!ID], 
		doc.Posted as [Shipment!3!Posted], 
		doc.Marked as [Shipment!3!Marked], 
		doc.DocDate as [Shipment!3!Docdate],
		doc.[DLM] as [Shipment!3!DLM],
		[DW].[fnGetGuid1C](Shipments.[CodeInIS]) as [Shipment!3!GUID],
		CHECKSUM_AGG(cast(Shipments.[CHECKSUMM] as int)) [Shipment!3!CHECKSUMM]

		FROM [DW].[FactSalesOfGoods] Shipments
		INNER JOIN [DW].[DocJournal] as doc
			ON Shipments.[CodeInIS] = doc.[CodeInIS] 
			AND Shipments.InformationSystemID = doc.InformationSystemID
		WHERE 
			Shipments.[DLM] between @StartDate and @EndDate 

		GROUP BY doc.ID ,doc.[DLM],Shipments.[CodeInIS], doc.Posted, doc.Marked, doc.Docdate
/*
	UNION ALL

	SELECT DISTINCT
		3 as [Tag], 
		null as [Parent],
		null as [Contragent!1!ID],
		null as [Contragent!1!DLM],
		null as [Contragent!1!GUID],

		null as [Nomenclature!2!ID], 
		null as [Nomenclature!2!DLM],
		null as [Nomenclature!2!GUID],

		doc.ID as [Shipment!3!ID], 
		doc.Posted as [Shipment!3!Posted], 
		doc.Marked as [Shipment!3!Marked], 
		doc.DocDate as [Shipment!3!Docdate],
		doc.[DLM] as [Shipment!3!DLM],
		[DW].[fnGetGuid1C](Shipments.[CodeInIS]) as [Shipment!3!GUID],
		CHECKSUM_AGG(cast(Shipments.[CHECKSUMM] as int)) [Shipment!3!CHECKSUMM]

		FROM [DW].[FactSalesOfGoods] Shipments
		INNER JOIN [DW].[DocJournal] as doc
			ON Shipments.[CodeInIS] = doc.[CodeInIS] 
			AND Shipments.InformationSystemID = doc.InformationSystemID
		WHERE 
			doc.ID IN (
				SELECT DISTINCT [_SalesCodeID] FROM [DW].[FactReturns] ret
				WHERE ret.[DLM] between @StartDate and @EndDate 
			)

		GROUP BY doc.ID ,doc.[DLM],Shipments.[CodeInIS], doc.Posted, doc.Marked, doc.Docdate
*/

insert into @t (
		[Tag], 
		[Parent],
		[Shipment!3!ID], 
		[Shipment!3!Posted], 
		[Shipment!3!Marked], 
		[Shipment!3!Docdate],
		[Shipment!3!DLM],
		[Shipment!3!GUID],
		[Shipment!3!CHECKSUMM]
	)
	SELECT DISTINCT
		3 as [Tag], 
		null as [Parent],
		doc.ID as [Shipment!3!ID], 
		doc.Posted as [Shipment!3!Posted], 
		doc.Marked as [Shipment!3!Marked], 
		doc.DocDate as [Shipment!3!Docdate],
		doc.[DLM] as [Shipment!3!DLM],
		[DW].[fnGetGuid1C](Shipments.[CodeInIS]) as [Shipment!3!GUID],
		CHECKSUM_AGG(cast(Shipments.[CHECKSUMM] as int)) [Shipment!3!CHECKSUMM]

		FROM [DW].[FactReturns] Shipments
		INNER JOIN [DW].[DocJournal] as doc
			ON Shipments.[CodeInIS] = doc.[CodeInIS] 
			AND Shipments.InformationSystemID = doc.InformationSystemID
		WHERE doc.[DLM] between @StartDate and @EndDate 

		GROUP BY doc.ID ,doc.[DLM],Shipments.[CodeInIS], doc.Posted, doc.Marked, doc.Docdate

	INSERT INTO @t (
		[Tag], 
		[Parent],
		[Aggregation!4!Contragents], 
		[Aggregation!4!Nomenclatures],
		[Aggregation!4!Shipments])
	SELECT 	
		4,
		null,	
		COUNT([Contragent!1!ID]),	
		COUNT([Nomenclature!2!ID]), 
		COUNT([Shipment!3!ID]) 
	FROM  @t
	SET @xml = (SELECT DISTINCT * from  @t ORDER BY [Aggregation!4!Shipments] DESC FOR XML EXPLICIT, ROOT('Summary'), BINARY BASE64);
	RETURN @xml;
END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[fnGetSummaryList] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @StartDate DATETIME = '2021-01-01T00:00:00'
DECLARE @EndDate DATETIME = '2021-01-10T00:00:00'
SELECT [IIS].[fnGetSummaryList](@StartDate,@EndDate) [fnGetSummaryList]
