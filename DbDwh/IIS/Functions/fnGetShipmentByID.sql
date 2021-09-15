-- [IIS].[fnGetShipmentByID]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetShipmentByID]
DROP FUNCTION IF EXISTS [IIS].[fnGetShipmentByIDv2]
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
	   ,[Shipment!1!GUID_1C] NVARCHAR(38)
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
		,[Shipment!1!ID]
		,[Shipment!1!Marked]
		,[Shipment!1!Posted]
		,[Shipment!1!DocNum]
		,[Shipment!1!DocDate]
		,[Shipment!1!OrgID]
		,[Shipment!1!OrgName]
		,[Shipment!1!ContragentID]
		,[Shipment!1!ContragentName]
		,[Shipment!1!DeliveryPlaceID]
		,[Shipment!1!DeliveryPlace]
		,[Shipment!1!ShippingDate]
		,[Shipment!1!InformationSystemID]
		,[Shipment!1!GUID_1C]
		,[Shipment!2!OrderDocDate]
		,[Shipment!2!NomenclatureID]
		,[Shipment!2!NomenclatureMasterID]
		,[Shipment!2!NomenclatureName]
		,[Shipment!2!Qty]
		,[Shipment!2!Price]
		,[Shipment!2!Cost]
		,[Shipment!2!CostWithDiscounts]
		,[Shipment!2!PercentageDiscounts]
		,[Shipment!2!Price2])
		SELECT
			1 [Tag]
		   ,NULL [Parent]
		   ,[DOC].[ID] [Shipment!1!CID]
		   ,[DOC].Marked [Shipment!1!Posted]
		   ,[DOC].Posted [Shipment!1!Marked]
		   ,[DOC].[DocNum] [Shipment!1!DocNum]
		   ,[DOC].[DocDate] [Shipment!1!DocDate]
		   ,[ORG].[ID] [Shipment!1!OrgID]
		   ,[ORG].[Description] [Shipment!1!OrgName]
		   ,[CN].[ID] [Shipment!1!ContragentID]
		   ,[CN].[Name] [Shipment!1!ContragentName]
		   ,[DP].[ID] [Shipment!1!DeliveryPlaceID]
		   ,[DP].[Name] [Shipment!1!DeliveryPlace]
		   ,[DOC].[DocDate] [Shipment!1!ShippingDate]
		   ,[DOC].[InformationSystemID] [Shipment!1!InformationSystemID]
		   --,[DOC].[CodeInIS] [Shipment!1!GUID_1C]
		   ,[DW].[fnGetGuid1Cv2] ([DOC].[CodeInIS]) [Shipment!1!GUID_1C]
		   ,NULL AS [Shipment!2!OrderDocDate]
		   ,NULL [Shipment!2!NomenclatureID]
		   ,NULL [Shipment!2!NomenclatureMasterID]
		   ,NULL [Shipment!2!NomenclatureName]
		   ,NULL [Shipment!2!Qty]
		   ,NULL [Shipment!2!Price]
		   ,SUM([FSG].[Cost]) [Shipment!2!Cost]
		   ,NULL [Shipment!2!CostWithDiscounts]
		   ,NULL [Shipment!2!PercentageDiscounts]
		   ,NULL [Shipment!2!BasePrice]
		FROM [DW].[FactSalesOfGoods] [FSG]
		INNER JOIN [DW].[DocJournal] [DOC]
			ON [FSG].[CodeInIS] = [DOC].[CodeInIS]
		--     LEFT JOIN 
		-- (SELECT MAX(jj.DocDate) DocDate, MAX(oo.OrderDate) OrderDate, MAX(oo.ShippingDate) ShippingDate, oo.CodeInIS, oo.InformationSystemID FROM [DW].[FactOrdersOfGoods] oo
		--INNER JOIN  [DW].[DocJournal] jj ON oo.CodeInIS = jj.CodeInIS
		--AND jj.InformationSystemID = oo.InformationSystemID
		--GROUP BY oo.CodeInIS,oo.InformationSystemID
		-- ) o
		-- ON ss.OrderID = o.CodeInIS
		-- AND ss.InformationSystemID = o.InformationSystemID
		LEFT JOIN [DW].[DimOrganizations] [ORG] ON [DOC].[OrgID]=[ORG].[CodeInIS] AND [DOC].[InformationSystemID]=[ORG].[InformationSystemID]
		LEFT JOIN [DW].[DimContragents] [CN] ON [FSG].[ContragentID]=[CN].[CodeInIS] AND [FSG].[InformationSystemID]=[CN].[InformationSystemID]
		LEFT JOIN [DW].[DimDeliveryPlaces] [DP] ON [FSG].[DeliveryPlaceID]=[DP].[CodeInIS] AND [FSG].[InformationSystemID]=[DP].[InformationSystemID]
		WHERE [FSG].[CodeInIS] IN (SELECT ID FROM @RESULT)
		GROUP BY [DOC].[ID], [DOC].Marked, [DOC].Posted, [DOC].[DocNum], [DOC].[DocDate], [ORG].[ID], [ORG].[Description], [CN].[ID], [CN].[Name], [DP].[ID], [DP].[Name]
			,[DOC].[InformationSystemID], [DOC].[CodeInIS]
		--,o.[DocDate] 
		--,o.[OrderDate]
		--,o.[ShippingDate]
		UNION ALL
		SELECT
			2 [Tag]
		   ,1 [Parent]
		   ,[DOC].[ID] [Shipment!1!CID]
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
		   ,NULL [Shipment!1!InformationSystemID]
		   ,NULL [Shipment!1!GUID_1C]
		   ,ord.DocDate [Shipment!2!OrderDocDate]
		   ,[N].[ID] [Shipment!2!NomenclatureID]
		   ,[N].[MasterId] [Shipment!2!NomenclatureMasterID]
		   ,[N].[Name] [Shipment!2!NomenclatureName]
		   ,SUM([SS].[Qty] * COALESCE([N].[Weight], 1)) [Shipment!2!Qty]
		   ,[SS].[Price] [Shipment!2!Price]
		   ,SUM([SS].[Cost]) [Shipment!2!Cost]
		   ,SUM([SS].[Cost] - ([SS].[Cost] * [SS].[PercentageDiscounts]) / 100) [Shipment!2!CostWithDiscounts]
		   ,[SS].[PercentageDiscounts] [Shipment!2!PercentageDiscounts]
		   ,[SS].[BasePrice] [Shipment!2!BasePrice]
		FROM [DW].[FactSalesOfGoods] [SS]
		INNER JOIN [DW].[DocJournal] [DOC] ON [SS].[CodeInIS]=[DOC].[CodeInIS] AND [SS].[InformationSystemID]=[DOC].[InformationSystemID]
		INNER JOIN [DW].[DimNomenclatures] [N] ON [SS].[NomenclatureID]=[N].[CodeInIS] AND [SS].[InformationSystemID]=[N].[InformationSystemID]
		LEFT JOIN (SELECT DISTINCT
				jj.[DocDate]
			   ,oo.[CodeInIS]
			   ,oo.[NomenclatureID]
			   ,oo.[InformationSystemID]
			FROM [DW].[FactOrdersOfGoods] oo
			INNER JOIN [DW].[DocJournal] jj
				ON oo.CodeInIS = jj.CodeInIS
				AND jj.InformationSystemID = oo.InformationSystemID) AS ord
			ON [SS].InformationSystemID = ord.InformationSystemID
				AND [SS].[OrderID] = ord.[CodeInIS]
				AND [SS].[NomenclatureID] = ord.[NomenclatureID]
		WHERE [SS].[CodeInIS] IN (SELECT
				ID
			FROM @RESULT)
		GROUP BY [DOC].[ID]
				,ord.DocDate
				,[N].[ID]
				,[N].[MasterId]
				,[N].[Name]
				,[SS].[PercentageDiscounts]
				,[SS].[Price]
				,[SS].[BasePrice]
	DECLARE @xml XML = (SELECT *
		FROM @sales
		ORDER BY [Shipment!1!ID], Tag
		FOR XML EXPLICIT
		,ROOT ('Shipments')
		,BINARY BASE64)
	RETURN @xml;
END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[fnGetShipmentByID] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @ID BIGINT = -9223372036853859140
SELECT [IIS].[fnGetShipmentByID](@ID) [fnGetShipmentByIDv2]
