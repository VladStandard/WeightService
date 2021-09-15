-- [IIS].[fnGetContragentChangesList]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetContragentChangesList]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnGetContragentChangesList] (@StartDate DATETIME = NULL, @EndDate DATETIME = NULL, @Offset INT = 0, @RowCount INT = 10)
RETURNS XML
AS
BEGIN
	DECLARE @RESULT TABLE ([ID] INT)
	INSERT INTO @RESULT ([ID])
		SELECT
			[c].[ID]
		FROM [DW].[DimContragents] [c]
		WHERE [c].[Marked] = 0
		AND (([c].[DLM] >= @StartDate)
		OR (@StartDate IS NULL))
		AND (([c].[DLM] < @EndDate)
		OR (@EndDate IS NULL))
		ORDER BY [c].[ID]
		OFFSET @Offset ROWS FETCH NEXT @RowCount ROWS ONLY;
	RETURN (SELECT
			*
		FROM (SELECT
				[CONTRAGENTS].[ID] "@ID"
			   ,[CONTRAGENTS].[Name] "@Name"
			   ,[CONTRAGENTS].[Code] "@Code"
			   ,[CONTRAGENTS].[FullName] "@FullName"
			   ,[CONTRAGENTS].[ContragentType] "@ContragentType"
			   ,[CONTRAGENTS].[INN] "@INN"
			   ,[CONTRAGENTS].[KPP] "@KPP"
			   ,[CONTRAGENTS].[OKPO] "@OKPO"
			   ,[CONTRAGENTS].[GUID_Mercury] "@GUID_Mercury"
			   ,[CONTRAGENTS].[ConsolidatedClientID] "@ConsolidatedClientID"
			   ,[CONTRAGENTS].[Comment] "@Comment"
			   ,[CONTRAGENTS].[InformationSystemID] "@InformationSystemID"
			   ,[DW].[fnGetGuid1Cv2] ([CONTRAGENTS].[CodeInIS]) "@GUID_1C"
			   ,CAST((SELECT
						[DP].[ID] "@DeliveryPlaceID"
					   ,[DocNumber] "@DocNumber"
					   ,[DocumentDate] "@DocumentDate"
					   ,[DateStart] "@DateStart"
					   ,[DateEnd] "@DateEnd"
					   ,[DiscountPercent] "@DiscountPercent"
					   ,[FIDN].[Comment] "@Comment"
					FROM [DW].[FactInstallationDiscountsNomenclatures] AS [FIDN]
					LEFT JOIN [DW].[DimDeliveryPlaces] [DP]
						ON [FIDN].[DeliveryPlaceID] = [DP].[CodeInIS]
					WHERE [FIDN].[ContragentID] = [CONTRAGENTS].CodeInIS
					FOR XML PATH ('Discount'), BINARY BASE64)
				AS XML) [Discounts]
			   ,CAST([CONTRAGENTS].[ContactInfo] AS XML) "ContactInformations"
			   ,(SELECT
						[dp].[ID] [@ID]
					   ,[dp].[Name] [@Name]
					   ,[dp].[Address] [@Address]
					   ,[dp].[FormatStoreName] [@FormatName]
					   ,[dp].[RegionStoreName] [@RegionName]
					   ,[Region].[Code] [@RegionCode]
					   ,[DW].[fnGetGuid1C]([dp].[CodeInIS]) "@CodeInIS"
					   ,CASE
							WHEN [dp].[RegionStoreID] = 0x00000000000000000000000000000000 THEN NULL
							ELSE [DW].[fnGetGuid1C]([dp].[RegionStoreID])
						END [@RegionId]
					FROM [DW].[DimDeliveryPlaces] [dp]
					LEFT JOIN [DW].[DimRegions] Region
						ON [dp].[RegionStoreID] = [Region].[CodeInIS]
					WHERE [dp].[ContragentID] = [CONTRAGENTS].[CodeInIS]
					AND [dp].[Marked] = 0
					FOR XML PATH ('DeliveryPlaces'), TYPE)
				AS DeliveryPlaces
			FROM [DW].[DimContragents] [CONTRAGENTS]
			WHERE [CONTRAGENTS].[ID] IN (SELECT
					[ID]
				FROM @RESULT)) AS D
		FOR XML PATH ('Contragent')
		, ROOT ('Contragents'), BINARY BASE64)
END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[fnGetContragentChangesList] to [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @StartDate DATETIME = '2021-01-01T00:00:00'
DECLARE @EndDate DATETIME = '2021-07-10T00:00:00'
DECLARE @Offset INT = 0
DECLARE @RowCount INT = 100
SELECT [IIS].[fnGetContragentChangesList](@StartDate, @EndDate, @Offset, @RowCount) [fnGetContragentChangesList]
