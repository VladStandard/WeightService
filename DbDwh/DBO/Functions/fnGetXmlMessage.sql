-- [dbo].[fnGetXmlMessage]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [dbo].[fnGetXmlMessage]
GO

-- CREATE FUNCTION
CREATE FUNCTION [dbo].[fnGetXmlMessage] (@root NVARCHAR(1024), @node NVARCHAR(1024), @attribute NVARCHAR(1024), @value NVARCHAR(1024)) RETURNS XML
AS BEGIN
	DECLARE @result XML = NULL
	IF (@root IS NULL) BEGIN
		SET @result =
'<' + @node + ' ' + @attribute + '="' + @value + '" />'
	END ELSE BEGIN
		SET @result =
'<' + @root + '>
  <' + @node + ' ' + @attribute + '="' + @value + '" />
</' + @root + '>'
	END
	RETURN @result
END
GO

-- ACCESS
GRANT EXECUTE ON [dbo].[fnGetXmlMessage] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
SELECT [dbo].[fnGetXmlMessage] ('Response', 'Result', 'Description', 'Test error message!') [fnGetXmlMessage]
SELECT [dbo].[fnGetXmlMessage] (NULL, 'Result', 'Description', 'Test error message!') [fnGetXmlMessage]
