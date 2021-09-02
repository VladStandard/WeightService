-- [IIS].[fnGetShipmentByID]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetShipmentByID]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnGetShipmentByID] (@ID BIGINT)
RETURNS XML
AS
BEGIN
	DECLARE @RESULT TABLE ([ID] VARBINARY(16))
	INSERT INTO @RESULT
		SELECT
			gg.[CodeInIS]
		FROM [DW].[DocJournal] gg
		WHERE gg.CodeInIS IN (SELECT DISTINCT
				[CodeInIS]
			FROM [DW].[FactSalesOfGoods] ss
			WHERE gg.ID = @ID)
		GROUP BY gg.[CodeInIS]
	DECLARE @sales TABLE (
		[Tag] INT
	   ,[Parent] INT
	   ,[Shipment!1!ID] BIGINT
	   ,[Shipment!1!Marked] BIT
	   ,[Shipment!1!Posted] BIT
	   ,[Shipment!1!DocNum] NVARCHAR(15)
	   ,[Shipment!1!DocDate] DATETIME
	   ,[Shipment!1!OrgID] BIGINT
	   ,[Shipment!1!OrgName] NVARCHAR(150)
	   ,[Shipment!1!ContragentID] BIGINT
	   ,[Shipment!1!ContragentName] NVARCHAR(150)
	   ,[Shipment!1!DeliveryPlaceID] BIGINT
	   ,[Shipment!1!DeliveryPlace] NVARCHAR(150)
	   ,[Shipment!1!ShippingDate] DATETIME
	   ,[Shipment!1!InformationSystemID] INT
	   ,[Shipment!2!NomenclatureID] INT
	   ,[Shipment!2!NomenclatureMasterID] INT
	   ,[Shipment!2!NomenclatureName] NVARCHAR(150)
	   ,[Shipment!2!Qty] NUMERIC(15, 3)
	   ,[Shipment!2!Price] NUMERIC(15, 2)
	   ,[Shipment!2!Cost] NUMERIC(15, 2)
	   ,[Shipment!2!CostWithDiscounts] NUMERIC(15, 2)
	   ,[Shipment!2!PercentageDiscounts] NUMERIC(15, 2)
	   ,[Shipment!2!Price2] NUMERIC(15, 2)
	   ,[Shipment!2!OrderDocDate] DATETIME
	)
	INSERT INTO @sales ([Tag], [Parent]
	, [Shipment!1!ID]
	, [Shipment!1!Marked]
	, [Shipment!1!Posted]
	, [Shipment!1!DocNum]
	, [Shipment!1!DocDate]
	, [Shipment!1!OrgID]
	, [Shipment!1!OrgName]
	, [Shipment!1!ContragentID]
	, [Shipment!1!ContragentName]
	, [Shipment!1!DeliveryPlaceID]
	, [Shipment!1!DeliveryPlace]
	, [Shipment!1!ShippingDate]
	, [Shipment!2!OrderDocDate]
	, [Shipment!2!NomenclatureID]
	, [Shipment!2!NomenclatureMasterID]
	, [Shipment!2!NomenclatureName]
	, [Shipment!2!Qty]
	, [Shipment!2!Price]
	, [Shipment!2!Cost]
	, [Shipment!2!CostWithDiscounts]
	, [Shipment!2!PercentageDiscounts]
	, [Shipment!2!Price2])
		SELECT
			1 [Tag]
		   ,NULL [Parent]
		   ,doc.[ID] [Shipment!1!CID]
		   ,doc.Marked [Shipment!1!Posted]
		   ,doc.Posted [Shipment!1!Marked]
		   ,doc.[DocNum] [Shipment!1!DocNum]
		   ,doc.[DocDate] [Shipment!1!DocDate]
		   ,org.[ID] [Shipment!1!OrgID]
		   ,org.[Description] [Shipment!1!OrgName]
		   ,cn.[ID] [Shipment!1!ContragentID]
		   ,cn.[Name] [Shipment!1!ContragentName]
		   ,dp.[ID] [Shipment!1!DeliveryPlaceID]
		   ,dp.[Name] [Shipment!1!DeliveryPlace]
		   ,doc.[DocDate] [Shipment!1!ShippingDate]
		   ,NULL AS [Shipment!2!OrderDocDate]
		   ,NULL [Shipment!2!NomenclatureID]
		   ,NULL [Shipment!2!NomenclatureMasterID]
		   ,NULL [Shipment!2!NomenclatureName]
		   ,NULL [Shipment!2!Qty]
		   ,NULL [Shipment!2!Price]
		   ,SUM(ss.[Cost]) [Shipment!2!Cost]
		   ,NULL [Shipment!2!CostWithDiscounts]
		   ,NULL [Shipment!2!PercentageDiscounts]
		   ,NULL [Shipment!2!BasePrice]
		FROM [DW].[FactSalesOfGoods] ss
		INNER JOIN [DW].[DocJournal] AS doc
			ON ss.[CodeInIS] = doc.[CodeInIS]
		--     LEFT JOIN 
		-- (SELECT MAX(jj.DocDate) DocDate, MAX(oo.OrderDate) OrderDate, MAX(oo.ShippingDate) ShippingDate, oo.CodeInIS, oo.InformationSystemID FROM [DW].[FactOrdersOfGoods] oo
		--INNER JOIN  [DW].[DocJournal] jj ON oo.CodeInIS = jj.CodeInIS
		--AND jj.InformationSystemID = oo.InformationSystemID
		--GROUP BY oo.CodeInIS,oo.InformationSystemID
		-- ) o
		-- ON ss.OrderID = o.CodeInIS
		-- AND ss.InformationSystemID = o.InformationSystemID
		LEFT JOIN [DW].[DimOrganizations] AS org
			ON doc.[OrgID] = org.[CodeInIS]
				AND doc.InformationSystemID = org.InformationSystemID
		LEFT JOIN [DW].[DimContragents] AS cn
			ON ss.[ContragentID] = cn.[CodeInIS]
				AND ss.InformationSystemID = cn.InformationSystemID
		LEFT JOIN [DW].[DimDeliveryPlaces] AS dp
			ON ss.[DeliveryPlaceID] = dp.[CodeInIS]
				AND ss.InformationSystemID = dp.InformationSystemID
		WHERE ss.[CodeInIS] IN (SELECT
				ID
			FROM @RESULT)
		GROUP BY doc.[ID]
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
		--,o.[DocDate] 
		--,o.[OrderDate]
		--,o.[ShippingDate]
		UNION ALL
		SELECT
			2 [Tag]
		   ,1 [Parent]
		   ,doc.[ID] [Shipment!1!CID]
		   ,NULL [Shipment!1!Marked]
		   ,NULL [Shipment!1!Posted]
		   ,NULL [Shipment!1!DocNum]
		   ,NULL [Shipment!1!DocDate]
		   ,NULL [Shipment!1!OrgID]
		   ,NULL [Shipment!1!OrgName]
		   ,NULL [Shipment!1!ContragentID]
		   ,NULL [Shipment!1!ContragentName]
		   ,NULL [Shipment!1!DeliveryPlaceID]
		   ,NULL [Shipment!1!DeliveryPlace]
		   ,NULL [Shipment!1!ShippingDate]
		   ,ord.DocDate [Shipment!2!OrderDocDate]
		   ,n.[ID] [Shipment!2!NomenclatureID]
		   ,n.[MasterId] [Shipment!2!NomenclatureMasterID]
		   ,n.[Name] [Shipment!2!NomenclatureName]
		   ,SUM(ss.[Qty] * COALESCE(n.[Weight], 1)) [Shipment!2!Qty]
		   ,ss.[Price] [Shipment!2!Price]
		   ,SUM(ss.[Cost]) [Shipment!2!Cost]
		   ,SUM(ss.[Cost] - (ss.[Cost] * ss.[PercentageDiscounts]) / 100) [Shipment!2!CostWithDiscounts]
		   ,ss.[PercentageDiscounts] [Shipment!2!PercentageDiscounts]
		   ,ss.[BasePrice] [Shipment!2!BasePrice]
		FROM [DW].[FactSalesOfGoods] ss
		INNER JOIN [DW].[DocJournal] AS doc
			ON ss.[CodeInIS] = doc.[CodeInIS]
				AND ss.InformationSystemID = doc.InformationSystemID
		INNER JOIN [DW].[DimNomenclatures] AS n
			ON ss.[NomenclatureID] = n.[CodeInIS]
				AND ss.InformationSystemID = n.InformationSystemID
		LEFT JOIN (SELECT DISTINCT
				jj.[DocDate]
			   ,oo.[CodeInIS]
			   ,oo.[NomenclatureID]
			   ,oo.[InformationSystemID]
			FROM [DW].[FactOrdersOfGoods] oo
			INNER JOIN [DW].[DocJournal] jj
				ON oo.CodeInIS = jj.CodeInIS
				AND jj.InformationSystemID = oo.InformationSystemID) AS ord
			ON ss.InformationSystemID = ord.InformationSystemID
				AND ss.[OrderID] = ord.[CodeInIS]
				AND ss.[NomenclatureID] = ord.[NomenclatureID]
		WHERE ss.[CodeInIS] IN (SELECT
				ID
			FROM @RESULT)
		GROUP BY doc.[ID]
				,ord.DocDate
				,n.[ID]
				,n.[MasterId]
				,n.[Name]
				,ss.[PercentageDiscounts]
				,ss.[Price]
				,ss.[BasePrice]
	DECLARE @xml XML = (SELECT *
		FROM @sales
		ORDER BY [Shipment!1!ID], Tag
		FOR XML EXPLICIT
		, ROOT ('Shipments')
		, BINARY BASE64)
	RETURN @xml;
END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[fnGetShipmentByID] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @ID BIGINT = -9223372036853859140
SELECT [IIS].[fnGetShipmentByID](@ID) [fnGetShipmentByID]
