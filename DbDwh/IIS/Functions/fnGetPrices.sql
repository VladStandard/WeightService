-- [IIS].[fnGetPrices]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetPrices]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnGetPrices]
(
	@StartDate datetime = null, @EndDate datetime = null, @Offset int = 0, @RowCount int = 10
)
RETURNS xml
AS
BEGIN
	-- Deprecated method!
	RETURN (SELECT N'Deprecated method!' [Message] 
	FOR XML RAW('Result'), ROOT('Response'), BINARY BASE64)

	RETURN 	(
		SELECT * FROM (
			SELECT 
				n.ID as NomenclatureID
				,[Price]
				,IsAction
				,[StartDate] as StartDate
				--,case when lead ([StartDate]) over(partition by [NomenclatureId] order by DateID,[NomenclatureId]) is NULL 
				--then cast(getdate() as date)
				--else lead ([StartDate]) over(partition by [NomenclatureId] order by [StartDate], [NomenclatureId]) END as EndDate

			  FROM [DW].[FactPrices] as t 
			  INNER JOIN [DW].[DimNomenclatures] as n 
			  ON t.NomenclatureID = n.CodeInIS
			  WHERE 
				[PriceTypeID] = 0xBA6D90E6BA17BDD711E297052E5C534D 
				and [DocType] = 'DocumentRef.УстановкаЦенНоменклатуры'
				and IsAction = 0
				AND ((t.[DLM] >= @StartDate)  OR (@StartDate is null))
				AND ((t.[DLM] <  @EndDate) OR (@EndDate is null))

			  ORDER BY t.[ID]
				OFFSET @Offset ROWS
				FETCH NEXT @RowCount ROWS ONLY

		) as D

		FOR XML PATH ('Price')
		,ROOT('Prices')
		--,ELEMENTS XSINIL
		,BINARY BASE64 
		--AUTO
		--,XMLSCHEMA 
		)
END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[fnGetPrices] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @StartDate DATETIME = '2021-01-01T00:00:00'
DECLARE @EndDate DATETIME = '2021-07-10T00:00:00'
DECLARE @Offset INT = 0
DECLARE @RowCount INT = 100
SELECT [IIS].[fnGetPrices](@StartDate,@EndDate,@Offset,@RowCount) [fnGetPrices]
