-- [IIS].[fnCheckDates]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnCheckDates]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnCheckDates] (@StartDate DATETIME = NULL, @EndDate DATETIME = NULL) RETURNS XML
AS BEGIN
	-- DECLARE VARS.
	DECLARE @xml XML = '<Response />'
	SET @EndDate = ISNULL(@EndDate, GETDATE())
	DECLARE @days_limit INT = 14
	DECLARE @days_diff INT = DATEDIFF(DAY, @StartDate, @EndDate)
	DECLARE @err NVARCHAR(1024)
	-- Date comparison.
	IF (@EndDate < @StartDate) begin
		SET @err = '@EndDate must be be more than @StartDate!'
		SET @xml.modify ('insert <Error/> as last into (/Response)[1] ')
		SET @xml.modify ('insert attribute Description{sql:variable("@err")} into (/Response/Error)[1] ')
		RETURN @xml
	END
	-- Days limit.
	IF (@days_diff > @days_limit) begin
		SET @err = 'Interval between @StartDate and @EndDate is too much: ' + CAST(@days_diff AS NVARCHAR(255)) + ' days (limit is ' + CAST(@days_limit AS NVARCHAR(255)) + ' days)!'
		SET @xml.modify ('insert <Error/> as last into (/Response)[1] ')
		SET @xml.modify ('insert attribute Description{sql:variable("@err")} into (/Response/Error)[1] ')
		RETURN @xml
	END
	RETURN @xml
END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[fnCheckDates] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @StartDate DATETIME = '2021-10-01T00:00:00'
DECLARE @EndDate DATETIME = '2021-09-21T00:00:00'
DECLARE @Offset INT = 0
DECLARE @RowCount INT = 30
SELECT [IIS].[fnCheckDates] (@StartDate, @EndDate) [fnCheckDates]
