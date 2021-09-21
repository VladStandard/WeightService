-- [dbo].[fnGetXmlSimpleV1]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [dbo].[fnGetXmlSimpleV1]
GO

-- CREATE FUNCTION
CREATE FUNCTION [dbo].[fnGetXmlSimpleV1]()
RETURNS xml
AS
BEGIN
	-- Deprecated method!
	RETURN (SELECT N'Simple message' [Description]
	FOR XML RAW('Simple'), BINARY BASE64)
END
GO

-- ACCESS
GRANT EXECUTE ON [dbo].[fnGetXmlSimpleV1] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
SELECT [dbo].[fnGetXmlSimpleV1]() [fnGetXmlSimpleV1]
