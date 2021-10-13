-- [dbo].[fnCheckDates]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [dbo].[fnCheckDates]
GO

-- CREATE FUNCTION
CREATE FUNCTION [dbo].[fnCheckDates] (@StartDate DATETIME, @EndDate DATETIME) RETURNS NVARCHAR(1024)
AS BEGIN
	-- DECLARE VARS.
	DECLARE @days_limit INT = 10000
	DECLARE @days_diff INT = DATEDIFF(DAY, @StartDate, @EndDate)
	DECLARE @err NVARCHAR(1024)
	-- Date comparison.
	IF (@EndDate < @StartDate) begin
		RETURN '@EndDate must be be more than @StartDate!'
	END
	-- Days limit.
	IF (@days_diff > @days_limit) begin
		RETURN 'Interval between @StartDate and @EndDate is too much: ' + CAST(@days_diff AS NVARCHAR(255)) + ' days (limit is ' + CAST(@days_limit AS NVARCHAR(255)) + ' days)!'
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

SET @StartDate = '2010-01-01T00:00:00'
SET @EndDate = '2021-12-31T00:00:00'
SELECT [dbo].[fnCheckDates] (@StartDate, @EndDate) [fnCheckDates]

SET @StartDate = '1990-01-01T00:00:00'
SET @EndDate = '2021-12-31T00:00:00'
SELECT [dbo].[fnCheckDates] (@StartDate, @EndDate) [fnCheckDates]
