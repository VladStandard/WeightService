-- [dbo].[fnGetXmlError]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [dbo].[fnGetXmlError]
GO

-- CREATE FUNCTION
CREATE FUNCTION [dbo].[fnGetXmlError] (@value NVARCHAR(1024)) RETURNS XML
AS BEGIN
	RETURN (SELECT [dbo].[fnGetXmlMessage] (NULL, 'Error', 'Description', @value))
END
GO

-- ACCESS
GRANT EXECUTE ON [dbo].[fnGetXmlError] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
SELECT [dbo].[fnGetXmlError] ('Test error message!') [fnGetXmlError]
