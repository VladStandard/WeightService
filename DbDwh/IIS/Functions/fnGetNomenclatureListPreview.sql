-- [IIS].[fnGetNomenclatureListPreview]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetNomenclatureList_Preview]
DROP FUNCTION IF EXISTS [IIS].[fnGetNomenclatureListPreview]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnGetNomenclatureListPreview] (@StartDate DATETIME, @EndDate DATETIME = NULL, @Offset INT = 0, @RowCount INT = 10) RETURNS XML
AS
BEGIN
	-- DECLARE.
	DECLARE @xml XML = (SELECT '' FOR XML PATH(''), ROOT('Response'), BINARY BASE64)
	DECLARE @check NVARCHAR(1024) = NULL
	DECLARE @check_xml XML = NULL
	DECLARE @ResultCount INT = 0
	SET @EndDate = ISNULL(@EndDate, GETDATE())
	-- CHECKS.
	SET @check = (select [dbo].[fnCheckDates] (@StartDate, @EndDate))
	IF (@check IS NULL) BEGIN
		SET @check = (select [dbo].[fnCheckRowCount] (@RowCount))
	END 
	IF (@check IS NOT NULL) BEGIN
		SET @check_xml = (SELECT [dbo].[fnGetXmlMessage] (NULL, 'Error', 'Description', @check))
		SET @xml.modify('insert sql:variable("@check_xml") as first into (/Response)[1]')
	END
	ELSE BEGIN
		-- Переменная с таблицей типов номенклатур
		DECLARE @tableNomenclatureTypes TABLE 
			([Name] VARCHAR(150), [GoodsForSale] BIT, [ID] INT, [CreateDate] DATETIME, [DLM] DATETIME, [StatusID] INT, [InformationSystemID] INT, [CodeInIS] VARBINARY(16))
		INSERT INTO @tableNomenclatureTypes
			SELECT [Name], [GoodsForSale], [ID], [CreateDate], [DLM], [StatusID], [InformationSystemID], [CodeInIS]
			FROM [DW].[DimTypesOfNomenclature] READUNCOMMITTED 
		-- Переменная с таблицей результатов
		DECLARE @RESULT TABLE ([ID] INT)
		INSERT INTO @RESULT ([ID])
			SELECT [N].[ID]
			FROM [DW].[DimNomenclatures] AS [N]
			LEFT JOIN @tableNomenclatureTypes [NT] ON [N].NomenclatureType = [NT].[CodeInIS]
			WHERE [NT].[GoodsForSale]=1
				AND COALESCE([N].[Marked], 0)=0
				AND (([N].[DLM]>=@StartDate) OR (@StartDate IS NULL))
				AND (([N].[DLM]<@EndDate) OR (@EndDate IS NULL))
			ORDER BY [N].[ID] OFFSET @Offset ROWS FETCH NEXT @RowCount ROWS ONLY
		-- Переменная с таблицей себестоимости
		DECLARE @tableSalfeCosts TABLE 
			([PriceType] VARCHAR(5), [DateID] INT, [NomenclatureID] INT, [Nomenclature] NVARCHAR(150), [Price] DECIMAL(15,2)
			,[StartDate] DATETIME, [DLM] DATETIME)
		INSERT INTO @tableSalfeCosts
			SELECT [PriceType], [DateID], [NomenclatureID], [Nomenclature], [Price], [StartDate], [DLM]
			FROM [VSDWH].[DW].[vwSelfCosts] READUNCOMMITTED
			ORDER BY [PriceType] ASC
		-- Переменная с таблицей брендов
		DECLARE @tableBrands TABLE 
			([Code] NVARCHAR(15), [Name] NVARCHAR(150), [ID] INT, [CreateDate] DATETIME, [DLM] DATETIME, [StatusID] INT, [InformationSystemID] INT, [CodeInIS] VARBINARY(16))
		INSERT INTO @tableBrands
			SELECT [Code], [Name], [ID], [CreateDate], [DLM], [StatusID], [InformationSystemID], [CodeInIS]
			FROM [DW].[DimBrands] READUNCOMMITTED
		-- Переменная с таблицей плановой себестоимости
		DECLARE @tableCurrentPlannedCosts TABLE 
			([Marked] BIT, [Posted] BIT, [DocNum] VARCHAR(15), [DocDate] DATETIME, [Price] DECIMAL(15,3), [NomenclatureID] INT, [NomenclatureName] VARCHAR(150), [ID] BIGINT)
		INSERT INTO @tableCurrentPlannedCosts
			SELECT [Marked], [Posted], [DocNum], [DocDate], [Price], [NomenclatureID], [NomenclatureName], [ID]
			FROM [DW].[vwCurrentPlannedCost] READUNCOMMITTED 
		-- Переменная с таблицей групп номенклатур
		DECLARE @tableNomenclatureGroups TABLE 
			([Name] NVARCHAR(150), [ID] INT, [CreateDate] DATETIME, [DLM] DATETIME, [StatusID] INT, [InformationSystemID] INT, [CodeInIS] VARBINARY(16))
		INSERT INTO @tableNomenclatureGroups
			SELECT [Name], [ID], [CreateDate], [DLM], [StatusID], [InformationSystemID],[CodeInIS]
			FROM [DW].[DimNomenclatureGroups] READUNCOMMITTED 
		-- Переменная с таблицей фактических прайсов
		DECLARE @tableFactPrices TABLE 
			([DateID] INT, [DocNum] NVARCHAR(15), [DocDate] DATETIME, [DocType] NVARCHAR(100), [Marked] BIT, [Posted] BIT, [NomenclatureID] VARBINARY(16)
			,[PriceTypeID] VARBINARY(16), [DeliveryPlaceID] VARBINARY(16), [_DateID] DATE, [_NomenclatureID] INT, [_PriceTypeID] INT, [_ContragentID] INT
			,[_DeliveryPlaceID] INT, [Price] DECIMAL(15,2), [IsAction] BIT, [StartDate] DATETIME, [EndDate] DATETIME, [ID] BIGINT, [CreateDate] DATETIME, [DLM] DATETIME
			,[StatusID] INT, [InformationSystemID] INT, [CodeInIS] VARBINARY(16), [_LineNo] INT, [CHECKSUMM] BIGINT, [Comment] NVARCHAR(1000)
			)
		INSERT INTO @tableFactPrices
			SELECT [DateID], [DocNum], [DocDate], [DocType], [Marked], [Posted], [NomenclatureID], [PriceTypeID], [DeliveryPlaceID], [_DateID], [_NomenclatureID], [_PriceTypeID], [_ContragentID]
			  , [_DeliveryPlaceID], [Price], [IsAction], [StartDate], [EndDate], [ID], [CreateDate], [DLM], [StatusID], [InformationSystemID], [CodeInIS], [_LineNo], [CHECKSUMM], [Comment]
			FROM [DW].[FactPrices] READUNCOMMITTED 
			WHERE [PriceTypeID]=0xBA6D90E6BA17BDD711E297052E5C534D -- Оптовые
				AND [DocType]='DocumentRef.УстановкаЦенНоменклатуры' AND [IsAction]=0
				AND [Marked]=0 AND [Posted]=1
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
			,CAST((SELECT * FROM ((
				SELECT [PriceType] [@PriceType], [Price] [@Price], [StartDate] [@StartDate], [DLM] [@DLM]
				FROM @tableSalfeCosts
				WHERE [N].[ID]=[NomenclatureId]
			)) [SelfCost] FOR XML PATH ('SelfCost'), BINARY BASE64) AS XML) [SelfCosts]
			-- Раздел <Prices>
			,CAST((SELECT
						[Price] [@Price]
					,[IsAction] [@IsAction]
					,[StartDate] [@StartDate]
				FROM @tableFactPrices [FP]
				WHERE [FP].[PriceTypeID]=0xBA6D90E6BA17BDD711E297052E5C534D -- Оптовые
					AND [FP].[DocType]='DocumentRef.УстановкаЦенНоменклатуры' AND [FP].[IsAction]=0
					AND [FP].[Marked]=0 AND [FP].[Posted]=1
					AND [N].[CodeInIS]=[FP].[NomenclatureID]
				ORDER BY [StartDate]
				FOR XML PATH ('Price'), BINARY BASE64)
			AS XML) [Prices]
			FROM [DW].[DimNomenclatures] [N]
			LEFT JOIN @tableNomenclatureTypes [NT] ON [N].NomenclatureType=[NT].[CodeInIS]
			LEFT JOIN @tableCurrentPlannedCosts [vCPC] ON [N].[ID] = [vCPC].[NomenclatureID]
			LEFT JOIN @tableCurrentPlannedCosts cost ON [N].[ID]=cost.[NomenclatureID]
			LEFT JOIN @tableNomenclatureGroups AS [NG] ON [N].[NomenclatureGroup]=[NG].[CodeInIS]
			LEFT JOIN @tableBrands [B] ON [N].[Brand]=[B].[CodeInIS]
			WHERE
				--JSON_VALUE(Nomenclature.[Parents], '$.parents[0]') IN ('Колбасные изделия','Мясные продукты','Рыбная продукция')
				[NT].[GoodsForSale] = 1
				AND COALESCE([N].[Marked], 0) = 0
				AND [N].[ID] IN (SELECT id FROM @RESULT)
		) [DATA] FOR XML PATH ('Nomenclature'), ROOT('Goods'), BINARY BASE64)
	END
	-- RESULT.
	DECLARE @Version NVARCHAR(100) = 'v.0.6.160'
	DECLARE @Api NVARCHAR(1000) = '/api/nomenclaturescosts/?StartDate=' + CONVERT(NVARCHAR(255), @StartDate, 126) + 
		'&EndDate=' + CONVERT(NVARCHAR(255), @EndDate, 126) + '&Offset=' + CAST(@Offset AS NVARCHAR(100)) + '&RowCount=' + CAST(@RowCount AS NVARCHAR(100))
	IF (@xml IS NULL) BEGIN
		SET @xml = (SELECT '' FOR XML PATH(''), ROOT('Response'), BINARY BASE64)
		SET @xml.modify ('insert <Information /> as first into (./Response)[1] ')
		SET @xml.modify ('insert attribute Version{sql:variable("@Version")} as first into (./Response/Information)[1] ')
		SET @xml.modify ('insert (attribute Api{sql:variable("@Api")}, attribute ResultCount{0}) as last into (./Response/Information)[1] ')
	END ELSE BEGIN
		SET @xml.modify ('insert <Information /> as first into (./Goods)[1] ')
		SET @xml.modify ('insert attribute Version{sql:variable("@Version")} as first into (./Goods/Information)[1] ')
		SET @xml.modify ('insert (attribute Api{sql:variable("@Api")}, attribute ResultCount{count(.//Nomenclature)}) as last into (./Goods/Information)[1] ')
	END
	RETURN @xml
END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[fnGetNomenclatureListPreview] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @StartDate DATETIME = '2022-01-01T00:00:00'
DECLARE @EndDate DATETIME = '2023-01-01T00:00:00'
DECLARE @Offset INT = 0
DECLARE @RowCount INT = 1000000
SELECT [IIS].[fnGetNomenclatureListPreview] (@StartDate, @EndDate, @Offset, @RowCount) [fnGetNomenclatureListPreview]
