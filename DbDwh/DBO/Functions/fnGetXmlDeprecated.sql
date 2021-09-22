-- [dbo].[fnGetXmlDeprecated]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetDeprecated1]
DROP FUNCTION IF EXISTS [dbo].[fnGetXmlDeprecated]
GO

-- CREATE FUNCTION
CREATE FUNCTION [dbo].[fnGetXmlDeprecated]() RETURNS XML
AS BEGIN
	RETURN (SELECT [dbo].[fnGetXmlMessage] ('Response', 'Result', 'Message', 'Deprecated method'))
END
GO

-- ACCESS
GRANT EXECUTE ON [dbo].[fnGetXmlDeprecated] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
SELECT [dbo].[fnGetXmlDeprecated]() [fnGetDeprecated]
