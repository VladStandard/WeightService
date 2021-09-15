-- [IIS].[fnGetContragentByID]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetContragentByID]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnGetContragentByID] (@ID BIGINT)
RETURNS XML
AS
BEGIN
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
						dp.ID AS "@DeliveryPlaceID"
					   ,[DocNumber] AS "@DocNumber"
					   ,[DocumentDate] AS "@DocumentDate"
					   ,[DateStart] AS "@DateStart"
					   ,[DateEnd] AS "@DateEnd"
					   ,[DiscountPercent] AS "@DiscountPercent"
					   ,t.[Comment] AS "@Comment"

					FROM [DW].[FactInstallationDiscountsNomenclatures] AS t
					LEFT JOIN [DW].[DimDeliveryPlaces] dp
						ON t.[DeliveryPlaceID] = dp.[CodeInIS]
					WHERE t.[ContragentID] = [C].CodeInIS

					FOR XML PATH ('Discount'), BINARY BASE64)
				AS XML) AS Discounts
			   ,CAST([C].[ContactInfo] AS XML) "ContactInformations"
			   ,(SELECT
						DeliveryPlaces.[ID] [@ID]
					   ,DeliveryPlaces.[Name] [@Name]
					   ,DeliveryPlaces.[Address] [@Address]
					   ,DeliveryPlaces.[FormatStoreName] [@FormatName]
					   ,DeliveryPlaces.[RegionStoreName] [@RegionName]
					   ,[Region].[Code] [@RegionCode]
					   ,CASE
							WHEN DeliveryPlaces.[RegionStoreID] = 0x00000000000000000000000000000000 THEN NULL
							ELSE [DW].[fnGetGuid1C](DeliveryPlaces.[RegionStoreID])
						END [@RegionId]
					FROM [DW].[DimDeliveryPlaces] DeliveryPlaces
					LEFT JOIN [DW].[DimRegions] Region
						ON [DeliveryPlaces].[RegionStoreID] = [Region].[CodeInIS]
						AND [DeliveryPlaces].InformationSystemID = [Region].InformationSystemID
					WHERE DeliveryPlaces.[ContragentID] = [C].[CodeInIS]
					AND DeliveryPlaces.[Marked] = 0
					FOR XML PATH ('DeliveryPlaces'), TYPE)
				AS DeliveryPlaces
			FROM DW.DimContragents [C]
			WHERE
			--Contragent.[IsBuyer]	= 1 
			[C].[Marked] = 0
			AND [C].ID = @ID) AS D
		FOR XML PATH ('Contragent')
		, ROOT ('Contragents')
		--,ELEMENTS XSINIL
		, BINARY BASE64
	--AUTO
	--,XMLSCHEMA 
	)
END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[fnGetContragentByID] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @ID BIGINT = -2147482821
SELECT [IIS].[fnGetContragentByID](@ID) [fnGetContragentByID]
