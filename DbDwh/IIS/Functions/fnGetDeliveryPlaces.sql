-- [IIS].[fnGetDeliveryPlaces]

-- DROP FUNCTION.
DROP FUNCTION IF EXISTS [IIS].[fnGetDeliveryPlaces]
GO

-- CREATE FUNCTION.
CREATE FUNCTION [IIS].[fnGetDeliveryPlaces] (@StartDate DATETIME, @EndDate DATETIME = NULL, @Offset INT = 0, @RowCount INT = 1000) RETURNS XML
AS BEGIN
	-- DECLARE.
	DECLARE @xml XML = '<Response />'
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
		-- DECLARE TABLE.
		DECLARE @TABLE TABLE (
			[Marked] [bit] NULL,
			[ContragentID] [varbinary](16) NULL,
			[Code] [nvarchar](15) NULL,
			[Name] [nvarchar](150) NULL,
			[GUID_Mercury] [nvarchar](36) NULL,
			[KPP] [nvarchar](15) NULL,
			[GLN] [nvarchar](15) NULL,
			[Address] [nvarchar](1024) NULL,
			[FormatStoreID] [varbinary](16) NULL,
			[RegionStoreID] [varbinary](16) NULL,
			[FormatStoreName] [nvarchar](150) NULL,
			[RegionStoreName] [nvarchar](150) NULL,
			[ID] [int] NOT NULL,
			[CreateDate] [datetime] NOT NULL,
			[DLM] [datetime] NOT NULL,
			[StatusID] [int] NOT NULL,
			[InformationSystemID] [int] NOT NULL,
			[CodeInIS] [varbinary](16) NOT NULL,
			[RelevanceStatus] [tinyint] NULL,
			[NormalizationStatus] [tinyint] NULL,
			[MasterId] [int] NULL
		)
		-- INSERT.
		INSERT INTO @TABLE([Marked], [ContragentID], [Code], [Name], [GUID_Mercury], [KPP], [GLN], [Address], [FormatStoreID], [RegionStoreID], [FormatStoreName], 
			[RegionStoreName], [ID], [CreateDate], [DLM], [StatusID], [InformationSystemID], [CodeInIS], [RelevanceStatus], [NormalizationStatus], [MasterId])
		SELECT [Marked], [ContragentID], [Code], [Name], [GUID_Mercury], [KPP], [GLN], [Address], [FormatStoreID], [RegionStoreID], [FormatStoreName], 
			[RegionStoreName], [ID], [CreateDate], [DLM], [StatusID], [InformationSystemID], [CodeInIS], [RelevanceStatus], [NormalizationStatus], [MasterId]
		FROM [DW].[DimDeliveryPlaces]
		WHERE [DLM] BETWEEN @StartDate AND @EndDate
		ORDER BY [DLM]
		OFFSET (@Offset*@RowCount) ROWS FETCH NEXT @RowCount ROWS ONLY
		-- IMPORT.
		SET @ResultCount = (SELECT COUNT (1) FROM @TABLE)
		SET @xml = (
			SELECT [DLM] [@DLM], [CreateDate] [@CreateDate], [ID] [@ID], [Marked] [@Marked], [ContragentID] [@ContragentID], [Code] [@Code], [Name] [@Name], [GUID_Mercury] [@GUID_Mercury]
				,[KPP] [@KPP], [GLN] [@GLN], [Address] [@Address], [FormatStoreID] [@FormatStoreID]
				,[RegionStoreID] [@RegionStoreID], [FormatStoreName] [@FormatStoreName], [RegionStoreName] [@RegionStoreName]
				,[StatusID] [@StatusID], [InformationSystemID] [@InformationSystemID]
				,[CodeInIS] [@CodeInIS], [RelevanceStatus] [@RelevanceStatus], [NormalizationStatus] [@NormalizationStatus], [MasterId] [@MasterId]
			FROM @TABLE FOR XML PATH('DeliveryPlace')
			,ROOT('DeliveryPlaces')
			,BINARY BASE64)
	END
	-- ATTRIBUTES.
	IF (@xml IS NULL) BEGIN
		SET @xml = '<Response />'
	END
	SET @xml.modify ('insert attribute StartDate{sql:variable("@StartDate")} into (/Response)[1] ')
	SET @xml.modify ('insert attribute EndDate{sql:variable("@EndDate")} into (/Response)[1] ')
	SET @xml.modify ('insert attribute Offset{sql:variable("@Offset")} into (/Response)[1] ')
	SET @xml.modify ('insert attribute RowCount{sql:variable("@RowCount")} into (/Response)[1] ')
	SET @xml.modify ('insert attribute ResultCount{sql:variable("@ResultCount")} into (/Response)[1] ')
	-- RESULT.
	RETURN @xml
END
GO

-- ACCESS.
GRANT EXECUTE ON [IIS].[fnGetDeliveryPlaces] TO [TerraSoftRole]
GO

-- CHECK FUNCTION.
DECLARE @StartDate DATETIME = '2021-01-01T00:00:00'
DECLARE @EndDate DATETIME = '2021-12-30T00:00:00'
DECLARE @Offset INT = 0
DECLARE @RowCount INT = 1000
SELECT [IIS].[fnGetDeliveryPlaces] (@StartDate, @EndDate, @Offset, @RowCount) [fnGetDeliveryPlaces]
