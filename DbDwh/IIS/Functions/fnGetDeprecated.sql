-- [dbo].[fnGetDeprecated]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [dbo].[fnGetDeprecated]
GO

-- CREATE FUNCTION
CREATE FUNCTION [dbo].[fnGetDeprecated]()
RETURNS xml
AS
BEGIN
	DECLARE @xml XML = '<Error />'
	DECLARE @err NVARCHAR(1024)
	-- Deprecated method!
	SET @err = N'Deprecated method!'
	SET @xml.modify ('insert attribute Description{sql:variable("@err")} into (/Error)[1] ')
	RETURN @xml
END
GO

-- ACCESS
GRANT EXECUTE ON [dbo].[fnGetDeprecated] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
SELECT [dbo].[fnGetDeprecated]() [fnGetDeprecated]
