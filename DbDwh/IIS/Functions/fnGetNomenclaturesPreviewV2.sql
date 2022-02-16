-- [IIS].[fnGetNomenclaturesV2Preview]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetNomenclaturesPreview]
DROP FUNCTION IF EXISTS [IIS].[fnGetNomenclaturesPreviewV2]
DROP FUNCTION IF EXISTS [IIS].[fnGetNomenclaturesV2Preview]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnGetNomenclaturesV2Preview] (
	@code NVARCHAR(255) = NULL,
	@id BIGINT = NULL,
	@start_date DATETIME = NULL,
	@end_date DATETIME = NULL,
	@offset INT = NULL,
	@row_count INT = NULL)
RETURNS XML
BEGIN
	-- DECLARE.
	DECLARE @xml XML = (SELECT '' FOR XML PATH(''), ROOT('Response'), BINARY BASE64);
	DECLARE @id_count INT = 0;
	DECLARE @result_count INT = 0;
	SET @end_date = ISNULL(@end_date, DATEADD(DD, 1, CAST(GETDATE() AS DATE)));
	-- Переменная с таблицей типов номенклатур
	DECLARE @tableNomenclatureTypes TABLE 
		([Name] VARCHAR(150), [GoodsForSale] BIT, [ID] INT, [CreateDate] DATETIME, [DLM] DATETIME, [StatusID] INT, 
		[InformationSystemID] INT, [CodeInIS] VARBINARY(16));
	INSERT INTO @tableNomenclatureTypes
		SELECT [Name], [GoodsForSale], [ID], [CreateDate], [DLM], [StatusID], [InformationSystemID], [CodeInIS]
		FROM [DW].[DimTypesOfNomenclature] READUNCOMMITTED;
	-- Переменная с таблицей id
	DECLARE @table_id TABLE ([ID] INT);
	IF (@code IS NOT NULL) BEGIN
		INSERT INTO @table_id ([ID])
			SELECT [N].[ID]
			FROM [DW].[DimNomenclatures] [N]
			LEFT JOIN @tableNomenclatureTypes [NT] ON [N].[NomenclatureType] = [NT].[CodeInIS] 
			WHERE [NT].[GoodsForSale] = 1 AND COALESCE([N].[Marked], 0) = 0
				AND [N].[Code] = @code;
	END ELSE IF (@id IS NOT NULL) BEGIN
		INSERT INTO @table_id ([ID])
			SELECT [N].[ID]
			FROM [DW].[DimNomenclatures] [N]
			LEFT JOIN @tableNomenclatureTypes [NT] ON [N].[NomenclatureType] = [NT].[CodeInIS] 
			WHERE [NT].[GoodsForSale] = 1 AND COALESCE([N].[Marked], 0) = 0
				AND [N].[ID] = @id;
	END ELSE IF (@start_date IS NOT NULL AND @offset IS NOT NULL AND @row_count IS NOT NULL) BEGIN
		INSERT INTO @table_id ([ID])
			SELECT [N].[ID]
			FROM [DW].[DimNomenclatures] [N]
			LEFT JOIN @tableNomenclatureTypes [NT] ON [N].[NomenclatureType] = [NT].[CodeInIS]
			WHERE [NT].[GoodsForSale] = 1 AND COALESCE([N].[Marked], 0) = 0
				AND (([N].[DLM] >= @start_date) OR (@start_date IS NULL))
				AND (([N].[DLM] < @end_date) OR (@end_date IS NULL))
			ORDER BY [N].[ID] OFFSET @offset ROWS FETCH NEXT @row_count ROWS ONLY;
	END ELSE IF (@start_date IS NOT NULL AND @offset IS NULL AND @row_count IS NULL) BEGIN
		INSERT INTO @table_id ([ID])
			SELECT [N].[ID]
			FROM [DW].[DimNomenclatures] [N]
			LEFT JOIN @tableNomenclatureTypes [NT] ON [N].[NomenclatureType] = [NT].[CodeInIS]
			WHERE [NT].[GoodsForSale] = 1 AND COALESCE([N].[Marked], 0) = 0
				AND (([N].[DLM] >= @start_date) OR (@start_date IS NULL))
				AND (([N].[DLM] < @end_date) OR (@end_date IS NULL));
	END ELSE IF (@start_date IS NULL AND @offset IS NULL AND @row_count IS NULL) BEGIN
		INSERT INTO @table_id ([ID])
			SELECT [N].[ID]
			FROM [DW].[DimNomenclatures] [N]
			LEFT JOIN @tableNomenclatureTypes [NT] ON [N].[NomenclatureType] = [NT].[CodeInIS]
			WHERE [NT].[GoodsForSale] = 1 AND COALESCE([N].[Marked], 0) = 0
	END
	-- @id_count
	SET @id_count = (SELECT COUNT(*) FROM @table_id);
	-- Переменная с таблицей себестоимости
	DECLARE @tableSalfeCosts TABLE 
		([PriceType] VARCHAR(5), [DateID] INT, [NomenclatureID] INT, [Nomenclature] NVARCHAR(150), [Price] DECIMAL(15,2)
		,[StartDate] DATETIME, [DLM] DATETIME);
	INSERT INTO @tableSalfeCosts
		SELECT [PriceType], [DateID], [NomenclatureID], [Nomenclature], [Price], [StartDate], [DLM]
		FROM [DW].[vwSelfCosts] READUNCOMMITTED
		ORDER BY [PriceType] ASC;
	-- Переменная с таблицей брендов
	DECLARE @tableBrands TABLE 
		([Code] NVARCHAR(15), [Name] NVARCHAR(150), [ID] INT, [CreateDate] DATETIME, [DLM] DATETIME, [StatusID] INT, 
		[InformationSystemID] INT, [CodeInIS] VARBINARY(16));
	INSERT INTO @tableBrands
		SELECT [Code], [Name], [ID], [CreateDate], [DLM], [StatusID], [InformationSystemID], [CodeInIS]
		FROM [DW].[DimBrands] READUNCOMMITTED;
	-- Переменная с таблицей плановой себестоимости
	DECLARE @tableCurrentPlannedCosts TABLE
		([Marked] BIT, [Posted] BIT, [DocNum] VARCHAR(15), [DocDate] DATETIME, [Price] DECIMAL(15,3), 
		[NomenclatureID] INT, [NomenclatureName] VARCHAR(150), [ID] BIGINT);
	INSERT INTO @tableCurrentPlannedCosts
		SELECT [Marked], [Posted], [DocNum], [DocDate], [Price], [NomenclatureID], [NomenclatureName], [ID]
		FROM [DW].[vwCurrentPlannedCost] READUNCOMMITTED;
	-- Переменная с таблицей групп номенклатур
	DECLARE @tableNomenclatureGroups TABLE
		([Name] NVARCHAR(150), [ID] INT, [CreateDate] DATETIME, [DLM] DATETIME, [StatusID] INT, 
		[InformationSystemID] INT, [CodeInIS] VARBINARY(16));
	INSERT INTO @tableNomenclatureGroups
		SELECT [Name], [ID], [CreateDate], [DLM], [StatusID], [InformationSystemID],[CodeInIS]
		FROM [DW].[DimNomenclatureGroups] READUNCOMMITTED;
	-- Переменная с таблицей фактических прайсов
	DECLARE @tableFactPrices TABLE 
		([DateID] INT, [DocNum] NVARCHAR(15), [DocDate] DATETIME, [DocType] NVARCHAR(100), [Marked] BIT, [Posted] BIT, [NomenclatureID] VARBINARY(16)
		,[PriceTypeID] VARBINARY(16), [DeliveryPlaceID] VARBINARY(16), [_DateID] DATE, [_NomenclatureID] INT, [_PriceTypeID] INT, [_ContragentID] INT
		,[_DeliveryPlaceID] INT, [Price] DECIMAL(15,2), [IsAction] BIT, [StartDate] DATETIME, [EndDate] DATETIME, [ID] BIGINT, [CreateDate] DATETIME, [DLM] DATETIME
		,[StatusID] INT, [InformationSystemID] INT, [CodeInIS] VARBINARY(16), [_LineNo] INT, [CHECKSUMM] BIGINT, [Comment] NVARCHAR(1000)
		);
	INSERT INTO @tableFactPrices
		SELECT [DateID], [DocNum], [DocDate], [DocType], [Marked], [Posted], [NomenclatureID], [PriceTypeID], [DeliveryPlaceID], [_DateID], [_NomenclatureID], [_PriceTypeID], [_ContragentID]
			, [_DeliveryPlaceID], [Price], [IsAction], [StartDate], [EndDate], [ID], [CreateDate], [DLM], [StatusID], [InformationSystemID], [CodeInIS], [_LineNo], [CHECKSUMM], [Comment]
		FROM [DW].[FactPrices] READUNCOMMITTED 
		WHERE [PriceTypeID] = 0xBA6D90E6BA17BDD711E297052E5C534D -- Оптовые
			AND [DocType] = 'DocumentRef.УстановкаЦенНоменклатуры' AND [IsAction] = 0
			AND [Marked] = 0 AND [Posted] = 1;
	-- Результат XML.
	SET @xml = (SELECT * FROM (SELECT
		 [N].[ID] "@ID"
		,[N].[Name] "@Name"
		,[N].[Code] "@Code"
		,[N].[MasterId] "@MasterId"
		,[N].[InformationSystemID] "@InformationSystemID"
		,[DW].[fnGetGuid1Cv2] ([N].[CodeInIS]) [@GUID_1C]
		,[N].[NameFull] "FullName"
		,[N].[CreateDate] "CreateDate"
		,[N].[DLM] "DLM"
		,[NG].[Name] "NomenclatureGroup"
		,JSON_VALUE([N].[Parents], '$.parents[0]') "Category"
		,[B].[Name] "Brand"
		,[N].[boxTypeName] "boxTypeName"
		,[N].[packTypeName] "packTypeName"
		,[N].[Unit] "Unit"
		-- Раздел <PlannedCost>
		,[vCPC].[Price] [PlannedCost]
		-- Раздел <SelfCosts>
		,CAST((SELECT [PriceType] [@PriceType], [Price] [@Price], [StartDate] [@StartDate], [DLM] [@DLM]
			FROM @tableSalfeCosts
			WHERE [N].[ID] = [NomenclatureID]
			ORDER BY [StartDate]
			FOR XML PATH ('SelfCost'), BINARY BASE64) AS XML) [SelfCosts]
		-- Раздел <Prices>
		,CAST((SELECT [Price] [@Price], [IsAction] [@IsAction], [StartDate] [@StartDate]
			FROM @tableFactPrices [FP]
			WHERE [N].[CodeInIS] = [FP].[NomenclatureID]
			ORDER BY [StartDate]
			FOR XML PATH ('Price'), BINARY BASE64) AS XML) [Prices]
		FROM [DW].[DimNomenclatures] [N]
		LEFT JOIN @tableNomenclatureGroups [NG] ON [N].[NomenclatureGroup] = [NG].[CodeInIS]
		LEFT JOIN @tableBrands [B] ON [N].[Brand] = [B].[CodeInIS]
		LEFT JOIN @tableCurrentPlannedCosts [vCPC] ON [N].[ID] = [vCPC].[NomenclatureID]
		WHERE [N].[ID] IN (SELECT [ID] FROM @table_id)
	) [DATA] FOR XML PATH ('Nomenclature'), ROOT('Goods'), BINARY BASE64);
	-- RESULT.
	DECLARE @Version NVARCHAR(100) = 'v.0.6.170';
	DECLARE @Api NVARCHAR(1000);
	IF (@code IS NOT NULL) BEGIN
		SET @api = '/api/v2/nomenclature/?code=' + @code;
	END ELSE IF (@id IS NOT NULL) BEGIN
		SET @api = '/api/v2/nomenclature/?id=' + CAST(@id AS NVARCHAR(100));
	END ELSE IF (@start_date IS NOT NULL AND @offset IS NOT NULL AND @row_count IS NOT NULL) BEGIN
		SET @api = '/api/v2/nomenclatures/?StartDate=' + CONVERT(NVARCHAR(255), @start_date, 126) + 
			'&EndDate=' + CONVERT(NVARCHAR(255), @end_date, 126) + '&Offset=' + CAST(@offset AS NVARCHAR(100)) + 
			'&RowCount=' + CAST(@row_count AS NVARCHAR(100));
	END ELSE IF (@start_date IS NOT NULL AND @offset IS NULL AND @row_count IS NULL) BEGIN
		SET @api = '/api/v2/nomenclatures/?StartDate=' + CONVERT(NVARCHAR(255), @start_date, 126) + 
			'&EndDate=' + CONVERT(NVARCHAR(255), @end_date, 126);
	END ELSE IF (@start_date IS NULL AND @offset IS NULL AND @row_count IS NULL) BEGIN
		SET @api = '/api/v2/nomenclatures/';
	END
	IF (@xml IS NULL) BEGIN
		SET @xml = (SELECT '' FOR XML PATH(''), ROOT('Response'), BINARY BASE64);
		SET @xml.modify ('insert <Information /> as first into (./Response)[1] ');
		SET @xml.modify ('insert attribute Version{sql:variable("@Version")} as first into (./Response/Information)[1] ');
		SET @xml.modify ('insert attribute Api{sql:variable("@Api")} as last into (./Response/Information)[1] ');
		SET @xml.modify ('insert attribute ResultCount{0} as last into (./Response/Information)[1] ');
	END ELSE BEGIN
		SET @xml.modify ('insert <Information /> as first into (./Goods)[1] ');
		SET @xml.modify ('insert attribute Version{sql:variable("@Version")} as first into (./Goods/Information)[1] ');
		SET @xml.modify ('insert attribute Api{sql:variable("@Api")} as last into (./Goods/Information)[1] ');
		SET @xml.modify ('insert attribute ResultCount{count(.//Nomenclature)} as last into (./Goods/Information)[1] ');
	END
	SET @xml.modify ('insert attribute IdCount{sql:variable("@id_count")} as last into (./Goods/Information)[1] ');
	RETURN @xml;
END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[fnGetNomenclaturesV2Preview] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @code NVARCHAR(255) = N'ЦБД00018851'
DECLARE @id BIGINT = -2147460739
DECLARE @start_date DATETIME = '2022-01-01T00:00:00'
DECLARE @end_date DATETIME = '2023-01-01T00:00:00'
--SELECT [IIS].[fnGetNomenclaturesV2Preview] (@code, DEFAULT, DEFAULT, DEFAULT, DEFAULT, DEFAULT) [fnGetNomenclaturesV2Preview]
--SELECT [IIS].[fnGetNomenclaturesV2Preview] (DEFAULT, @id, DEFAULT, DEFAULT, DEFAULT, DEFAULT) [fnGetNomenclaturesV2Preview]
--SELECT [IIS].[fnGetNomenclaturesV2Preview] (DEFAULT, DEFAULT, @start_date, DEFAULT, DEFAULT, DEFAULT) [fnGetNomenclaturesV2Preview]
--SELECT [IIS].[fnGetNomenclaturesV2Preview] (DEFAULT, DEFAULT, @start_date, @end_date, DEFAULT, DEFAULT) [fnGetNomenclaturesV2Preview]
--SELECT [IIS].[fnGetNomenclaturesV2Preview] (DEFAULT, DEFAULT, DEFAULT, DEFAULT, DEFAULT, DEFAULT) [fnGetNomenclaturesV2Preview]
