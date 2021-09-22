-- [dbo].[fnCheckRowCount]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [dbo].[fnCheckRowCount]
GO

-- CREATE FUNCTION
CREATE FUNCTION [dbo].[fnCheckRowCount] (@RowCount INT = 100) RETURNS NVARCHAR(1024)
AS BEGIN
	-- DECLARE VARS.
	DECLARE @err NVARCHAR(1024)
	DECLARE @RowCountLimit INT = 1000
	-- Negative value.
	IF (@RowCount <= 0) BEGIN
		RETURN '@RowCount must be more than 0!' 
	END
	IF (@RowCount > @RowCountLimit) BEGIN
		RETURN '@RowCount is too much: ' + cast(@RowCount as nvarchar(255)) + ' (limit is ' + cast(@RowCountLimit as nvarchar(255)) + ')!'
	END
	RETURN NULL
END
GO

-- ACCESS
GRANT EXECUTE ON [dbo].[fnCheckRowCount] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @RowCount INT = 3000
SELECT [dbo].[fnCheckRowCount] (@RowCount) [fnCheckRowCount]
