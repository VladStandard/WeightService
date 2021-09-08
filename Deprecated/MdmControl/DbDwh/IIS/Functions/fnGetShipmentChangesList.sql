-- [IIS].[fnGetShipmentChangesList]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetShipmentChangesList]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnGetShipmentChangesList]
(
	@StartDate datetime = null, @EndDate datetime = null, @Offset int = 0, @RowCount int = 10
)
RETURNS XML
AS
BEGIN
	-- Deprecated method!
	RETURN (SELECT N'Deprecated method!' [Message] 
	FOR XML RAW('Result'), ROOT('Response'), BINARY BASE64)

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

	DECLARE @IDTbl TABLE (ID varbinary(16))

	INSERT INTO @IDTbl 
		SELECT gg.[CodeInIS] 
		FROM [DW].[DocJournal] gg
		WHERE gg.CodeInIS IN
		(
			SELECT DISTINCT [CodeInIS] 
			FROM [DW].[FactSalesOfGoods] ss
			WHERE 
			((ss.[DLM] >= @StartDate)	OR (@StartDate is null))
			AND ((ss.[DLM] < @EndDate)	OR (@EndDate is null))
		)
		GROUP BY gg.[CodeInIS]
			ORDER BY  gg.[CodeInIS] OFFSET @Offset ROWS	FETCH NEXT @RowCount ROWS ONLY

	DECLARE @sales TABLE  
	(
		[Tag]				int
		,[Parent]			int
		,[Shipment!1!ID]	bigint
		,[Shipment!1!Marked]	bit
		,[Shipment!1!Posted]	bit
		,[Shipment!1!CHECKSUMM] int

		,[Shipment!1!DocNum]			nvarchar(15)
		,[Shipment!1!DocDate]			datetime

		,[Shipment!1!OrgID]				bigint
		,[Shipment!1!OrgName]			nvarchar(150)
	
		,[Shipment!1!ContragentID]		bigint
		,[Shipment!1!ContragentName]	nvarchar(150)

		,[Shipment!1!DeliveryPlaceID]	bigint
		,[Shipment!1!DeliveryPlace]		nvarchar(150)

		,[Shipment!1!OrderDocDate]		datetime
		,[Shipment!1!ShadowOrderDate]	datetime
		,[Shipment!1!ShippingDate]		datetime

		,[Shipment!1!OrderDate]				datetime
		,[Shipment!2!NomenclatureID]		int
		,[Shipment!2!NomenclatureName]		nvarchar(150)
		,[Shipment!2!Qty]					numeric(15,3)
		,[Shipment!2!Price]					numeric(15,2)
		,[Shipment!2!Cost]					numeric(15,2)
		,[Shipment!2!CostWithDiscounts]		numeric(15,2)
		,[Shipment!2!PercentageDiscounts]	numeric(15,2)
		,[Shipment!2!Price2]				numeric(15,2)
	)


--declare @StartDate	datetime = N'20200409'; 
--declare @EndDate	datetime = null;
--declare @Offset int		= 0; 
--declare @RowCount int	= 10000;


	INSERT INTO @sales (
		[Tag],[Parent]		
		,[Shipment!1!ID]
		,[Shipment!1!Marked]	
		,[Shipment!1!Posted]	
		,[Shipment!1!CHECKSUMM]
		,[Shipment!1!DocNum]
		,[Shipment!1!DocDate]

		,[Shipment!1!OrgID]
		,[Shipment!1!OrgName]	

		,[Shipment!1!ContragentID]
		,[Shipment!1!ContragentName]

		,[Shipment!1!DeliveryPlaceID]
		,[Shipment!1!DeliveryPlace]

		,[Shipment!1!OrderDocDate]
		,[Shipment!1!ShadowOrderDate]
		,[Shipment!1!ShippingDate]

		,[Shipment!1!OrderDate]
		,[Shipment!2!NomenclatureID]
		,[Shipment!2!NomenclatureName]
		,[Shipment!2!Qty]
		,[Shipment!2!Price]
		,[Shipment!2!Cost]
		,[Shipment!2!CostWithDiscounts]
		,[Shipment!2!PercentageDiscounts]
		,[Shipment!2!Price2]

	)
		SELECT
			1					[Tag]
			,NULL				[Parent]
			,doc.[ID]			[Shipment!1!CID]
			,doc.Marked         [Shipment!1!Posted]
			,doc.Posted			[Shipment!1!Marked]
			,CHECKSUM_AGG(cast(ss.[CHECKSUMM] as int)) [Shipment!1!CHECKSUMM]

			,doc.[DocNum]		[Shipment!1!DocNum]	
			,doc.[DocDate]		[Shipment!1!DocDate]
			,org.[ID]			[Shipment!1!OrgID]
			,org.[Description]	[Shipment!1!OrgName]
			,cn.[ID]			[Shipment!1!ContragentID]
			,cn.[Name]			[Shipment!1!ContragentName]
			,dp.[ID]			[Shipment!1!DeliveryPlaceID]
			,dp.[Name]			[Shipment!1!DeliveryPlace]

			,o.[DocDate]  as [Shipment!1!OrderDocDate]
			,CASE WHEN o.[OrderDate] > N'4000-01-01' 
			THEN DATEADD(year,-2000,o.[OrderDate])
			ELSE NULL END as [Shipment!1!ShadowOrderDate]

			--,DATEADD(year,-2000,o.ShippingDate)		[Shipment!1!ShippingDate]
			,doc.[DocDate]		[Shipment!1!ShippingDate]
			,NULL				[Shipment!1!OrderDate]	
			,NULL				[Shipment!2!NomenclatureID]
			,NULL				[Shipment!2!NomenclatureName]
			,NULL				[Shipment!2!Qty]
			,NULL				[Shipment!2!Price]
			,SUM(ss.[Cost])		[Shipment!2!Cost]
			,NULL				[Shipment!2!CostWithDiscounts]
			,NULL				[Shipment!2!PercentageDiscounts]
			,NULL				[Shipment!2!BasePrice]

		FROM [DW].[FactSalesOfGoods] ss
		INNER JOIN [DW].[DocJournal] as doc
		ON ss.[CodeInIS] = doc.[CodeInIS]

        LEFT JOIN 
		  (SELECT MAX(jj.DocDate) DocDate, MAX(oo.OrderDate) OrderDate, 
			MAX(oo.ShippingDate) ShippingDate, oo.CodeInIS, oo.InformationSystemID 
			FROM [DW].[FactOrdersOfGoods] oo
			INNER JOIN  [DW].[DocJournal] jj ON oo.CodeInIS = jj.CodeInIS
			AND jj.InformationSystemID = oo.InformationSystemID
			GROUP BY oo.CodeInIS,oo.InformationSystemID
		  ) o
		  ON ss.OrderID = o.CodeInIS
		  AND ss.InformationSystemID = o.InformationSystemID

		LEFT JOIN [DW].[DimOrganizations] as org
		ON doc.[OrgID] = org.[CodeInIS]
		  AND ss.InformationSystemID = org.InformationSystemID

		LEFT JOIN [DW].[DimContragents] as cn
		ON ss.[ContragentID] = cn.[CodeInIS]
		  AND doc.InformationSystemID = cn.InformationSystemID

		LEFT JOIN [DW].[DimDeliveryPlaces] as dp
		ON ss.[DeliveryPlaceID] = dp.[CodeInIS]
		  AND ss.InformationSystemID = dp.InformationSystemID

		WHERE ss.[CodeInIS] IN (SELECT ID FROM @IDTbl)

		GROUP BY 
			doc.[ID]		
			,doc.Marked
			,doc.Posted
			
			,doc.[DocNum]		
			,doc.[DocDate]		
			,org.[ID]			
			,org.[Description]	
			,cn.[ID]			
			,cn.[Name]			
			,dp.[ID]			
			,dp.[Name]			
			,o.[DocDate] 
			,o.[OrderDate]
			,o.[ShippingDate]
		UNION ALL
		SELECT 
		2				[Tag]
		,1				[Parent]
		,doc.[ID]		[Shipment!1!CID]
		,null			[Shipment!1!Marked]	
		,null			[Shipment!1!Posted]	
		,null			[Shipment!1!CHECKSUMM]
		,null			[Shipment!1!DocNum]
		,null			[Shipment!1!DocDate]
		,null			[Shipment!1!OrgID]
		,null			[Shipment!1!OrgName]
		,null			[Shipment!1!ContragentID]
		,null			[Shipment!1!ContragentName]
		,null			[Shipment!1!DeliveryPlaceID]
		,null			[Shipment!1!DeliveryPlace]
		,null as [Shipment!1!OrderDocDate]
		,null as [Shipment!1!ShadowOrderDate]
		,null as [Shipment!1!ShippingDate]
		,ord.OrderDate							[Shipment!1!OrderDate]	
		,n.[ID]									[Shipment!2!NomenclatureID]
		,n.[Name]								[Shipment!2!NomenclatureName]
		,SUM(ss.[Qty] * COALESCE(n.[Weight],1))		[Shipment!2!Qty]
		,ss.[Price]								[Shipment!2!Price]
		,SUM(ss.[Cost])								[Shipment!2!Cost]
		,SUM( ss.[Cost] - (ss.[Cost]*ss.[PercentageDiscounts])/100 )	[Shipment!2!CostWithDiscounts]
		,ss.[PercentageDiscounts]				[Shipment!2!PercentageDiscounts]
		,ss.[BasePrice]								[Shipment!2!BasePrice]

	FROM  [DW].[FactSalesOfGoods] ss
	INNER JOIN [DW].[DocJournal] as doc 
		ON ss.[CodeInIS] = doc.[CodeInIS]
		  AND ss.InformationSystemID = doc.InformationSystemID

	INNER JOIN [DW].[DimNomenclatures] as n 
		ON ss.[NomenclatureID] = n.[CodeInIS]
		AND ss.InformationSystemID = n.InformationSystemID

	LEFT JOIN 
		(SELECT DISTINCT [OrderDate],[CodeInIS],[NomenclatureID],InformationSystemID 
		FROM [DW].[FactOrdersOfGoods]) as ord 
		ON ss.[OrderID] = ord.[CodeInIS] AND ss.[NomenclatureID] = ord.[NomenclatureID]
		AND ss.InformationSystemID = ord.InformationSystemID
	WHERE ss.[CodeInIS] IN (SELECT ID FROM @IDTbl)
	GROUP BY 
		doc.[ID]
		,ord.OrderDate						
		,n.[ID]								
		,n.[Name]							
		,ss.[PercentageDiscounts]			
		,ss.[Price]							
		,ss.[BasePrice]							
	DECLARE @XML XML = (
	SELECT * FROM @sales ORDER BY [Shipment!1!ID],Tag 
	FOR XML EXPLICIT, ROOT('Shipments'), BINARY BASE64)
	RETURN @XML;
END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[fnGetShipmentChangesList] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @StartDate DATETIME = '2021-01-01T00:00:00'
DECLARE @EndDate DATETIME = '2021-07-10T00:00:00'
DECLARE @Offset INT = 0
DECLARE @RowCount INT = 100
SELECT [IIS].[fnGetShipmentChangesList](@StartDate,@EndDate,@Offset,@RowCount) [fnGetShipmentChangesList]
