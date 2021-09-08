-- [IIS].[fnGetContragentDiscounts]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetContragentDiscounts]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnGetContragentDiscounts]
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
				  cnt.ID as  [ContragentID]
				  ,dp.ID as [DeliveryPlaceID]
				  ,[DocNumber]
				  ,[DocumentDate]
				  ,[DateStart]
				  ,[DateEnd]
				  ,[DiscountPercent]
				  ,t.[Comment]
			  FROM [DW].[FactInstallationDiscountsNomenclatures] as t
			  INNER JOIN [DW].[DimContragents] as cnt ON t.[ContragentID] = cnt.CodeInIS
			  LEFT JOIN [DW].[DimDeliveryPlaces] as dp ON t.[DeliveryPlaceID] = dp.CodeInIS
			  WHERE  t.[Marked] = 0 
				AND ((t.[DLM] >= @StartDate)  OR (@StartDate is null))
				AND ((t.[DLM] <  @EndDate) OR (@EndDate is null))

			  ORDER BY t.[ID]
				OFFSET @Offset ROWS
				FETCH NEXT @RowCount ROWS ONLY
		) as D

		FOR XML PATH ('ContragentDiscount')
		,ROOT('Discounts')
		--,ELEMENTS XSINIL
		,BINARY BASE64 
		--AUTO
		--,XMLSCHEMA 
		)
END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[fnGetContragentDiscounts] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @StartDate DATETIME = '2021-01-01T00:00:00'
DECLARE @EndDate DATETIME = '2021-07-10T00:00:00'
DECLARE @Offset INT = 0
DECLARE @RowCount INT = 100
SELECT [IIS].[fnGetContragentDiscounts](@StartDate,@EndDate,@Offset,@RowCount) [fnGetContragentDiscounts]
