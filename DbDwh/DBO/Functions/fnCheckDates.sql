-- [dbo].[fnCheckDates]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [dbo].[fnCheckDates]
GO

-- CREATE FUNCTION
CREATE FUNCTION [dbo].[fnCheckDates] (@StartDate DATETIME, @EndDate DATETIME) RETURNS XML
AS BEGIN
	-- DECLARE VARS.
	DECLARE @xml XML = '<Error />'
	DECLARE @days_limit INT = 365
	DECLARE @days_diff INT = DATEDIFF(DAY, @StartDate, @EndDate)
	DECLARE @err NVARCHAR(1024)
	-- Date comparison.
	IF (@EndDate < @StartDate) begin
		SET @err = '@EndDate must be be more than @StartDate!'
		SET @xml.modify ('insert attribute Description{sql:variable("@err")} into (/Error)[1] ')
		RETURN @xml
	END
	-- Days limit.
	IF (@days_diff > @days_limit) begin
		SET @err = 'Interval between @StartDate and @EndDate is too much: ' + CAST(@days_diff AS NVARCHAR(255)) + ' days (limit is ' + CAST(@days_limit AS NVARCHAR(255)) + ' days)!'
		SET @xml.modify ('insert attribute Description{sql:variable("@err")} into (/Error)[1] ')
		RETURN @xml
	END
	RETURN NULL
END
GO

-- ACCESS
GRANT EXECUTE ON [dbo].[fnCheckDates] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @StartDate DATETIME = '2021-10-10T00:00:00'
DECLARE @EndDate DATETIME = '2021-09-21T00:00:00'
SELECT [dbo].[fnCheckDates] (@StartDate, @EndDate) [fnCheckDates]
