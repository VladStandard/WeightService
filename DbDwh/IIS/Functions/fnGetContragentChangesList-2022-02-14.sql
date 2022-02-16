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
				[C].[ID] "@ID"
			   ,[C].[Name] "@Name"
			   ,[C].[Code] "@Code"
			   ,[C].[FullName] "@FullName"
			   ,[C].[ContragentType] "@ContragentType"
			   ,[C].[INN] "@INN"
			   ,[C].[KPP] "@KPP"
			   ,[C].[OKPO] "@OKPO"
			   ,[C].[GUID_Mercury] "@GUID_Mercury"
			   ,[C].[ConsolidatedClientID] "@ConsolidatedClientID"
			   ,[C].[Comment] "@Comment"
			   ,[C].[InformationSystemID] "@InformationSystemID"
			   ,[DW].[fnGetGuid1Cv2] ([C].[CodeInIS]) "@GUID_1C"
			   ,CAST((SELECT
						 [FIDN].[ID] "@Id"
						,[DP].[ID] "@DeliveryPlaceID"
						,[FIDN].[DocNumber] "@DocNumber"
						,[FIDN].[DocumentDate] "@DocumentDate"
						,[FIDN].[DateStart] "@DateStart"
						,[FIDN].[DateEnd] "@DateEnd"
						,[FIDN].[DiscountPercent] "@DiscountPercent"
						,[FIDN].[Comment] "@Comment"
						-- А. Попов #1692
						,[FIDN].Marked "@Marked"
						,[FIDN].Posted "@Posted"
					FROM [DW].[FactInstallationDiscountsNomenclatures] AS [FIDN]
					LEFT JOIN [DW].[DimDeliveryPlaces] [DP]
						ON [FIDN].[DeliveryPlaceID] = [DP].[CodeInIS]
					WHERE [FIDN].[ContragentID] = [C].CodeInIS
					FOR XML PATH ('Discount'), BINARY BASE64)
				AS XML) [Discounts]
			   ,CAST([C].[ContactInfo] AS XML) "ContactInformations"
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
					WHERE [dp].[ContragentID] = [C].[CodeInIS]
					AND [dp].[Marked] = 0
					FOR XML PATH ('DeliveryPlaces'), TYPE)
				AS DeliveryPlaces
			FROM [DW].[DimContragents] [C]
			WHERE [C].[ID] IN (SELECT
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
DECLARE @StartDate DATETIME = '2021-10-01T00:00:00'
DECLARE @EndDate DATETIME = '2021-12-30T00:00:00'
DECLARE @Offset INT = 0
DECLARE @RowCount INT = 100
SELECT [IIS].[fnGetContragentChangesList](@StartDate, @EndDate, @Offset, @RowCount) [fnGetContragentChangesList]
