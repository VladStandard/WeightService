-- [dbo].[fnGetXmlSimpleV2]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [dbo].[fnGetXmlSimpleV2]
GO

-- CREATE FUNCTION
CREATE FUNCTION [dbo].[fnGetXmlSimpleV2]()
RETURNS xml
AS
BEGIN
	-- Deprecated method!
	RETURN (SELECT N'Response message' [Description]
	FOR XML RAW('Simple'), ROOT('Response'), BINARY BASE64)
END
GO

-- ACCESS
GRANT EXECUTE ON [dbo].[fnGetXmlSimpleV2] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
SELECT [dbo].[fnGetXmlSimpleV2]() [fnGetXmlSimpleV2]
