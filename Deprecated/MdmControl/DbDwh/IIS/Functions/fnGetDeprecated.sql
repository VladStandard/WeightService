-- [IIS].[fnGetDeprecated]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetDeprecated]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnGetDeprecated]()
RETURNS xml
AS
BEGIN
	-- Deprecated method!
	RETURN (SELECT N'Deprecated method!' [Message] 
	FOR XML RAW('Result'), ROOT('Response'), BINARY BASE64)
END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[fnGetDeprecated] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
SELECT [IIS].[fnGetDeprecated]() [fnGetDeprecated]
