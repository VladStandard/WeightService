-- [IIS].[GetShipments]

-- DROP PROCEDURE
DROP PROCEDURE IF EXISTS [IIS].[GetShipments]
GO

-- CREATE PROCEDURE
CREATE PROCEDURE [IIS].[GetShipments] @jsonListId NVARCHAR(MAX), @xml XML OUTPUT
AS BEGIN
	DECLARE @TableIds TABLE (ID BIGINT, IDC bigint)
	INSERT INTO @TableIds SELECT * FROM OPENJSON(@jsonListId, N'$') WITH (ID BIGINT N'$.id', IDC BIGINT N'$.idc')
	DROP TABLE IF EXISTS #Shipments
	CREATE TABLE #Shipments (
		[ParentDoc] BIGINT
	   ,[Marked] BIT
	   ,[Posted] BIT
	   ,[DocNum] NVARCHAR(15)
	   ,[DocDate] DATETIME
	   ,[DocType] NVARCHAR(50)
	   ,[ID] BIGINT
	   ,[CodeInIS] VARBINARY(16)
	   ,[DLM] DATETIME
	   ,[OrgID] INT
	   ,[OrgName] NVARCHAR(150)
	   ,[ContragentID] INT
	   ,[ContragentName] NVARCHAR(150)
	   ,[DeliveryPlaceID] INT
	   ,[DeliveryPlace] NVARCHAR(150)
	   ,[NomenclatureID] INT
	   ,[NomenclatureMasterID] INT
	   ,[NomenclatureName] NVARCHAR(150)
	   ,[VATRate] NVARCHAR(10)
	   ,[Qty] NUMERIC(15, 3)
	   ,[Price] NUMERIC(15, 2)
	   ,[CostWithDiscounts] NUMERIC(15, 2)
	   ,[Cost] NUMERIC(15, 2)
	   ,[CostVAT] NUMERIC(15, 2)
	   ,[BasePrice] NUMERIC(15, 2)
	   ,[DiscountCondition] NVARCHAR(20)
	   ,[PercentageDiscounts] NUMERIC(5, 2)
	   ,[InformationSystemID] INT
	)
	INSERT INTO #Shipments
		SELECT
			NULL [ParentDoc]
		   ,[doc].[Marked]
		   ,[doc].[Posted]
		   ,[doc].[DocNum]
		   ,[doc].[DocDate]
		   ,[doc].[DocType]
		   ,[doc].[ID]
		   ,[doc].[CodeInIS]
		   ,[doc].[DLM]
		   ,[ss].[_OrgID] OrgID
		   ,(SELECT [Description] FROM [DW].[DimOrganizations] WHERE ID = [ss].[_OrgID]) AS [OrgName]
		   ,[ss].[_ContragentID]
		   ,(SELECT [Name] FROM [DW].[DimContragents] WHERE ID = [ss].[_ContragentID]) AS [ContragentName]
		   ,[ss].[_DeliveryPlaceID]
		   ,(SELECT [Name] FROM [DW].[DimDeliveryPlaces] WHERE ID = [ss].[_DeliveryPlaceID]) AS [DeliveryPlace]
		   ,[ss].[_NomenclatureID]
		   ,(SELECT [MasterId] FROM [DW].[DimNomenclatures] WHERE ID = [ss].[_NomenclatureID]) AS [NomenclatureMasterID]
		   ,(SELECT [Name] FROM [DW].[DimNomenclatures] WHERE ID = [ss].[_NomenclatureID]) AS [NomenclatureName]
		   ,[ss].[VATRate]
		   ,[ss].[Qty] * COALESCE((SELECT [Weight] FROM [DW].[DimNomenclatures] WHERE ID = [ss].[_NomenclatureID]) , 1) AS [Qty]
		   ,[ss].[Price]
		   ,[ss].[Cost] AS [CostWithDiscounts]
		   ,([ss].[Qty] * [ss].[Price]) AS [Cost]
		   ,[ss].[CostVAT]
		   ,[ss].[BasePrice]
		   ,[ss].[DiscountCondition]
		   ,[ss].[PercentageDiscounts]
		   ,[ss].[InformationSystemID]
		FROM [DW].[FactSalesOfGoods] [ss]
		INNER JOIN [DW].[DocJournal] AS [doc] ON [ss].[CodeInIS] = [doc].[CodeInIS] AND [ss].InformationSystemID = [doc].InformationSystemID
		WHERE [doc].[ID] IN (SELECT ID FROM @TableIds)
		UNION ALL
		SELECT
			[ss].[_SalesCodeID] ParentDoc
		   ,[doc].[Marked]
		   ,[doc].[Posted]
		   ,[doc].[DocNum]
		   ,[doc].[DocDate]
		   ,[doc].[DocType]
		   ,[doc].[ID]
		   ,[doc].[CodeInIS]
		   ,[doc].[DLM]
		   ,[ss].[_OrgID] OrgID
		   ,(SELECT [Description] FROM [DW].[DimOrganizations] WHERE ID = [ss].[_OrgID]) AS [OrgName]
		   ,[ss].[_ContragentID]
		   ,(SELECT [Name] FROM [DW].[DimContragents] WHERE ID = [ss].[_ContragentID]) AS [ContragentName]
		   ,[ss].[_DeliveryPlaceID]
		   ,(SELECT [Name] FROM [DW].[DimDeliveryPlaces] WHERE ID = [ss].[_DeliveryPlaceID]) AS [DeliveryPlace]
		   ,[ss].[_NomenclatureID]
		   ,(SELECT [MasterId] FROM [DW].[DimNomenclatures] WHERE ID = [ss].[_NomenclatureID]) AS [NomenclatureMasterId]
		   ,(SELECT [Name] FROM [DW].[DimNomenclatures] WHERE ID = [ss].[_NomenclatureID]) AS [NomenclatureName]
		   ,[ss].[VATRate]
		   ,([ss].Qty - [ss].[QtyBeforeChange]) * COALESCE((SELECT [Weight] FROM [DW].[DimNomenclatures] WHERE ID = [ss].[_NomenclatureID]), 1) AS [Qty]
		   ,([ss].[Price] - [ss].[PriceBeforeChange]) AS [Price]
		   ,([ss].[Cost] - [ss].[CostBeforeChange]) AS [CostWithDiscounts]
		   ,(([ss].Qty - [ss].[QtyBeforeChange]) * ([ss].[Price] - [ss].[PriceBeforeChange])) AS [Cost]
		   ,([ss].[CostVAT] - [ss].[CostVATBeforeChange]) AS [CostVAT]
		   ,NULL AS [BasePrice]
		   ,NULL AS [DiscountCondition]
		   ,NULL AS [PercentageDiscounts]
		   ,[ss].[InformationSystemID] AS [InformationSystemID]
		FROM [DW].[FactReturns] [ss]
		INNER JOIN [DW].[DocJournal] AS [doc] ON [ss].[CodeInIS] = [doc].[CodeInIS] AND [ss].InformationSystemID = [doc].InformationSystemID
		WHERE [doc].[ID] IN (SELECT IDC FROM @TableIds)
		AND ([ss].Qty - [ss].[QtyBeforeChange])
		+ ([ss].[Price] - [ss].[PriceBeforeChange])
		+ ([ss].[Cost] - [ss].[CostBeforeChange])
		+ ([ss].[CostVAT] - [ss].[CostVATBeforeChange]) <> 0
	DECLARE @Shipments TABLE (
		[Tag] INT
	   ,[Parent] INT
	   ,[Shipment!1!ID] BIGINT
	   ,[Shipment!1!DocType] NVARCHAR(50)
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
	   ,[Shipment!1!DLM] DATETIME
	   ,[Shipment!1!Cost] NUMERIC(15, 2)
	   ,[Shipment!1!CostWithDiscounts] NUMERIC(15, 2)
	   ,[Shipment!1!InformationSystemID] INT
	   ,[Shipment!2!NomenclatureID] INT
	   ,[Shipment!2!NomenclatureMasterID] INT
	   ,[Shipment!2!NomenclatureName] NVARCHAR(150)
	   ,[Shipment!2!Qty] NUMERIC(15, 3)
	   ,[Shipment!2!Price] NUMERIC(15, 2)
	   ,[Shipment!2!Cost] NUMERIC(15, 2)
	   ,[Shipment!2!CostWithDiscounts] NUMERIC(15, 2)
	   ,[Shipment!2!PercentageDiscounts] NUMERIC(15, 2)
	   ,[Shipment!2!BasePrice] NUMERIC(15, 2)
	   ,[Shipment!2!OrderDocDate] DATETIME
	   ,[Shipment!2!Marked] BIT
	   ,[Shipment!2!Posted] BIT
	   ,[Shipment!2!InformationSystemID] INT
	)
	INSERT INTO @Shipments ([Tag], [Parent]
		,[Shipment!1!ID]
		,[Shipment!1!DocType]
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
		,[Shipment!1!DLM]
		,[Shipment!1!Cost]
		,[Shipment!1!CostWithDiscounts]
		,[Shipment!1!InformationSystemID])
		SELECT
			1 AS [Tag]
		   ,NULL AS [Parent]
		   ,[ID] AS [Shipment!1!ID]
		   ,[DocType] AS [Shipment!1!DocType]
		   ,[Marked] AS [Shipment!1!Posted]
		   ,[Posted] AS [Shipment!1!Marked]
		   ,[DocNum] AS [Shipment!1!DocNum]
		   ,[DocDate] AS [Shipment!1!DocDate]
		   ,[OrgID] AS [Shipment!1!OrgID]
		   ,[OrgName] AS [Shipment!1!OrgName]
		   ,[ContragentID] AS [Shipment!1!ContragentID]
		   ,[ContragentName] AS [Shipment!1!ContragentName]
		   ,[DeliveryPlaceID] AS [Shipment!1!DeliveryPlaceID]
		   ,[DeliveryPlace] AS [Shipment!1!DeliveryPlace]
		   ,[DocDate] AS [Shipment!1!ShippingDate]
		   ,MAX([DLM]) AS [Shipment!1!DLM]
		   ,SUM([Cost]) AS [Shipment!1!Cost]
		   ,SUM([CostWithDiscounts]) [Shipment!1!CostWithDiscounts]
		   ,[InformationSystemID] [Shipment!1!InformationSystemID]
		FROM #Shipments
		GROUP BY [ID], [DocType], [Marked], [Posted], [DocNum], [DocDate], [OrgID], [OrgName], [ContragentID], [ContragentName],
			[DeliveryPlaceID], [DeliveryPlace], [DocDate], [InformationSystemID]
	INSERT INTO @Shipments ([Tag]
		,[Parent]
		,[Shipment!1!ID]
		,[Shipment!2!OrderDocDate]
		,[Shipment!2!NomenclatureID]
		,[Shipment!2!NomenclatureMasterId]
		,[Shipment!2!NomenclatureName]
		,[Shipment!2!Qty]
		,[Shipment!2!Price]
		,[Shipment!2!Cost]
		,[Shipment!2!CostWithDiscounts]
		,[Shipment!2!PercentageDiscounts]
		,[Shipment!2!BasePrice]
		,[Shipment!2!Marked]
		,[Shipment!2!Posted]
		,[Shipment!2!InformationSystemID])
		SELECT
			2 AS [Tag]
		   ,1 AS [Parent]
		   ,[ID] AS [Shipment!1!ID]
		   ,NULL AS [Shipment!2!OrderDocDate]
		   ,[NomenclatureID] AS [Shipment!2!NomenclatureID]
		   ,[NomenclatureMasterID] AS [Shipment!2!NomenclatureMasterID]
		   ,[NomenclatureName] AS [Shipment!2!NomenclatureName]
		   ,SUM([Qty]) AS [Shipment!2!Qty]
		   ,[Price] AS [Shipment!2!Price]
		   ,SUM([Cost]) AS [Shipment!2!Cost]
		   ,SUM([CostWithDiscounts]) AS [Shipment!2!CostWithDiscounts]
		   ,[PercentageDiscounts] AS [Shipment!2!PercentageDiscounts]
		   ,[BasePrice] AS [Shipment!2!BasePrice]
		   ,[Marked] AS [Shipment!2!Marked]
		   ,[Posted] AS [Shipment!2!Posted]
		   ,[InformationSystemID] AS [Shipment!2!InformationSystemID]
		FROM #Shipments
		GROUP BY [ID], [NomenclatureID], [NomenclatureMasterID], [NomenclatureName], [Price], [PercentageDiscounts], [BasePrice], 
			[Marked], [Posted], [InformationSystemID]
	SET @xml = (SELECT * FROM @Shipments ORDER BY [Shipment!1!ID], [Tag] FOR XML EXPLICIT, ROOT ('Shipments'), BINARY BASE64)
	--SELECT * FROM #Shipments
	DROP TABLE IF EXISTS #Shipments
	RETURN 0
	END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[GetShipments] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @ID BIGINT = -9223372036854773994
DECLARE @JSON NVARCHAR(MAX) = (SELECT [IIS].[GetRefShipmentsById](@ID) [GetRefShipmentsById])
DECLARE @XML XML
EXEC [IIS].[GetShipments] @jsonListId = @JSON, @xml = @XML OUTPUT
SELECT @XML [XML]
