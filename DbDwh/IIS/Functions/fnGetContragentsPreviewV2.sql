-- [IIS].[fnGetContragentsPreviewV2]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetContragentsPreviewV2]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnGetContragentsPreviewV2] (
	@code NVARCHAR(255) = NULL,
	@id BIGINT = NULL,
	@start_date DATETIME = NULL,
	@end_date DATETIME = NULL,
	@offset INT = NULL,
	@row_count INT = NULL
)
RETURNS XML
BEGIN
	-- DECLARE.
	DECLARE @xml XML = (SELECT '' FOR XML PATH(''), ROOT('Response'), BINARY BASE64);
	DECLARE @id_count INT = 0;
	DECLARE @result_count INT = 0;
	SET @end_date = ISNULL(@end_date, DATEADD(DD, 1, CAST(GETDATE() AS DATE)));
	-- Переменная с таблицей типов номенклатур
	DECLARE @table_id TABLE ([ID] INT)
	IF (@code IS NOT NULL) BEGIN
		INSERT INTO @table_id ([ID])
			SELECT [DC].[ID]
			FROM [DW].[DimContragents] [DC]
			WHERE [DC].[Marked] = 0 AND [DC].[Code] = @code
			ORDER BY [DC].[ID];
	END ELSE IF (@id IS NOT NULL) BEGIN
		INSERT INTO @table_id ([ID])
			SELECT [DC].[ID]
			FROM [DW].[DimContragents] [DC]
			WHERE [DC].[Marked] = 0 AND [DC].[ID] = @id
			ORDER BY [DC].[ID];
	END ELSE IF (@start_date IS NOT NULL AND @offset IS NOT NULL AND @row_count IS NOT NULL) BEGIN
		INSERT INTO @table_id ([ID])
			SELECT [DC].[ID]
			FROM [DW].[DimContragents] [DC]
			WHERE [DC].[Marked] = 0
				AND (([DC].[DLM] >= @start_date) OR (@start_date IS NULL))
				AND (([DC].[DLM] < @end_date) OR (@end_date IS NULL))
			ORDER BY [DC].[ID] OFFSET @offset ROWS FETCH NEXT @row_count ROWS ONLY;
	END ELSE IF (@start_date IS NOT NULL AND @offset IS NULL AND @row_count IS NULL) BEGIN
		INSERT INTO @table_id ([ID])
			SELECT [DC].[ID]
			FROM [DW].[DimContragents] [DC]
			WHERE [DC].[Marked] = 0
				AND (([DC].[DLM] >= @start_date) OR (@start_date IS NULL))
				AND (([DC].[DLM] < @end_date) OR (@end_date IS NULL))
			ORDER BY [DC].[ID];
	END ELSE IF (@start_date IS NULL AND @offset IS NULL AND @row_count IS NULL) BEGIN
		INSERT INTO @table_id ([ID])
			SELECT [DC].[ID]
			FROM [DW].[DimContragents] [DC]
			WHERE [DC].[Marked] = 0
			ORDER BY [DC].[ID];
	END
	-- Переменная с таблицей [FactInstallationDiscountsNomenclatures]
	DECLARE @tableFactInstallationDiscountsNomenclatures TABLE 
		([ID] BIGINT, [DocNumber] NVARCHAR(15), [DocumentDate] DATETIME, [DateStart] DATETIME, [DateEnd] DATETIME, 
		[DiscountPercent] DECIMAL(15,6), [Comment] NVARCHAR(1000), [Marked] BIT, [Posted] BIT, 
		[DeliveryPlaceID] VARBINARY(16), [ContragentID] VARBINARY(16));
	INSERT INTO @tableFactInstallationDiscountsNomenclatures
		SELECT [ID], [DocNumber], [DocumentDate], [DateStart], [DateEnd], [DiscountPercent], [Comment], [Marked], [Posted], 
			[DeliveryPlaceID], [ContragentID]
		FROM [DW].[FactInstallationDiscountsNomenclatures] READUNCOMMITTED;
	-- Переменная с таблицей [DimDeliveryPlaces]
	DECLARE @tableDimRegions TABLE 
		([ID] INT, [Code] NVARCHAR(9), [CodeInIS] VARBINARY(16));
	INSERT INTO @tableDimRegions
		SELECT [ID], [Code], [CodeInIS]
		FROM [DW].[DimRegions] READUNCOMMITTED;
	-- @id_count
	SET @id_count = (SELECT COUNT(*) FROM @table_id);
	-- Результат XML.
	SET @xml = (SELECT * FROM (SELECT
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
				 [FIDN].[ID] [@id]
				,[DP].[ID] [@DeliveryPlaceID]
				,[FIDN].[DocNumber] [@DocNumber]
				,[FIDN].[DocumentDate] [@DocumentDate]
				,[FIDN].[DateStart] [@DateStart]
				,[FIDN].[DateEnd] [@DateEnd]
				,[FIDN].[DiscountPercent] [@DiscountPercent]
				,[FIDN].[Comment] [@Comment]
				,[FIDN].[Marked] [@Marked]
				,[FIDN].[Posted] [@Posted]
			FROM @tableFactInstallationDiscountsNomenclatures [FIDN]
			LEFT JOIN [DW].[DimDeliveryPlaces] [DP] ON [FIDN].[DeliveryPlaceID] = [DP].[CodeInIS]
			WHERE [FIDN].[ContragentID] = [C].[CodeInIS]
			FOR XML PATH ('Discount'), BINARY BASE64) AS XML) [Discounts]
		,CAST([C].[ContactInfo] AS XML) [ContactInformations]
		,(SELECT
				[DP].[ID] [@ID]
				,[DP].[Name] [@Name]
				,[DP].[Address] [@Address]
				,[DP].[FormatStoreName] [@FormatName]
				,[DP].[RegionStoreName] [@RegionName]
				,[R].[Code] [@RegionCode]
				,[DW].[fnGetGuid1C]([DP].[CodeInIS]) "@CodeInIS"
				,CASE
					WHEN [DP].[RegionStoreID] = 0x00000000000000000000000000000000 THEN NULL
					ELSE [DW].[fnGetGuid1C]([DP].[RegionStoreID])
				END [@RegionId]
			FROM [DW].[DimDeliveryPlaces] [DP]
			LEFT JOIN @tableDimRegions [R] ON [DP].[RegionStoreID] = [R].[CodeInIS]
			WHERE [DP].[ContragentID] = [C].[CodeInIS] AND [DP].[Marked] = 0
			FOR XML PATH ('DeliveryPlaces'), TYPE)
		AS DeliveryPlaces
	FROM [DW].[DimContragents] [C]
	WHERE [C].[ID] IN (SELECT [ID] FROM @table_id)
	) [DATA] FOR XML PATH ('Contragent'), ROOT ('Contragents'), BINARY BASE64)
	-- RESULT.
	DECLARE @Version NVARCHAR(100) = 'v.0.6.170';
	DECLARE @Api NVARCHAR(1000);
	IF (@code IS NOT NULL) BEGIN
		SET @api = '/api/v2/contragent/?code=' + @code;
	END ELSE IF (@id IS NOT NULL) BEGIN
		SET @api = '/api/v2/contragent/?id=' + CAST(@id AS NVARCHAR(100));
	END ELSE IF (@start_date IS NOT NULL AND @offset IS NOT NULL AND @row_count IS NOT NULL) BEGIN
		SET @api = '/api/v2/contragents/?StartDate=' + CONVERT(NVARCHAR(255), @start_date, 126) + 
			'&EndDate=' + CONVERT(NVARCHAR(255), @end_date, 126) + '&Offset=' + CAST(@offset AS NVARCHAR(100)) + 
			'&RowCount=' + CAST(@row_count AS NVARCHAR(100));
	END ELSE IF (@start_date IS NOT NULL AND @offset IS NULL AND @row_count IS NULL) BEGIN
		SET @api = '/api/v2/contragents/?StartDate=' + CONVERT(NVARCHAR(255), @start_date, 126) + 
			'&EndDate=' + CONVERT(NVARCHAR(255), @end_date, 126);
	END ELSE IF (@start_date IS NULL AND @offset IS NULL AND @row_count IS NULL) BEGIN
		SET @api = '/api/v2/contragents/';
	END
	IF (@xml IS NULL) BEGIN
		SET @xml = (SELECT '' FOR XML PATH(''), ROOT('Response'), BINARY BASE64);
		SET @xml.modify ('insert <Information /> as first into (./Response)[1] ');
		SET @xml.modify ('insert attribute Version{sql:variable("@Version")} as first into (./Response/Information)[1] ');
		SET @xml.modify ('insert attribute Api{sql:variable("@Api")} as last into (./Response/Information)[1] ');
		SET @xml.modify ('insert attribute ResultCount{0} as last into (./Response/Information)[1] ');
	END ELSE BEGIN
		SET @xml.modify ('insert <Information /> as first into (./Contragents)[1] ');
		SET @xml.modify ('insert attribute Version{sql:variable("@Version")} as first into (./Contragents/Information)[1] ');
		SET @xml.modify ('insert attribute Api{sql:variable("@Api")} as last into (./Contragents/Information)[1] ');
		SET @xml.modify ('insert attribute ResultCount{count(.//Contragent)} as last into (./Contragents/Information)[1] ');
	END
	SET @xml.modify ('insert attribute IdCount{sql:variable("@id_count")} as last into (./Contragents/Information)[1] ');
	RETURN @xml;
END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[fnGetContragentsPreviewV2] to [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @code NVARCHAR(255) = N'000014151'
DECLARE @id BIGINT = -2147482738
DECLARE @start_date DATETIME = '2022-01-01T00:00:00'
DECLARE @end_date DATETIME = '2023-01-01T00:00:00'
--SELECT [IIS].[fnGetContragentsPreviewV2] (@code, DEFAULT, DEFAULT, DEFAULT, DEFAULT, DEFAULT) [fnGetContragentsPreviewV2]
SELECT [IIS].[fnGetContragentsPreviewV2] (DEFAULT, @id, DEFAULT, DEFAULT, DEFAULT, DEFAULT) [fnGetContragentsPreviewV2]
--SELECT [IIS].[fnGetContragentsPreviewV2] (DEFAULT, DEFAULT, @start_date, @end_date, DEFAULT, DEFAULT) [fnGetContragentsPreviewV2]
--SELECT [IIS].[fnGetContragentsPreviewV2] (DEFAULT, DEFAULT, @start_date, DEFAULT, DEFAULT, DEFAULT) [fnGetContragentsPreviewV2]
--SELECT [IIS].[fnGetContragentsPreviewV2] (DEFAULT, DEFAULT, DEFAULT, DEFAULT, DEFAULT, DEFAULT) [fnGetContragentsPreviewV2]
