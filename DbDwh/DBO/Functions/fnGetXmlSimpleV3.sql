-- [dbo].[fnGetXmlSimpleV3]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [dbo].[fnGetXmlSimpleV3]
GO

-- CREATE FUNCTION
CREATE FUNCTION [dbo].[fnGetXmlSimpleV3]()
RETURNS xml
AS
BEGIN
	DECLARE @xml XML = '<Response />'
	SET @xml.modify ('insert <Simple /> into (/Response)[1] ')
	SET @xml.modify ('insert <Simple /> into (/Response)[1] ')
	SET @xml.modify ('insert <Simple /> into (/Response)[1] ')
	SET @xml.modify ('insert attribute Description{("First message")} into (/Response/Simple)[1] ')
	SET @xml.modify ('insert attribute Description{("Second message")} into (/Response/Simple)[2] ')
	SET @xml.modify ('insert attribute Description{("Third message")} into (/Response/Simple)[3] ')
	RETURN @xml
END
GO

-- ACCESS
GRANT EXECUTE ON [dbo].[fnGetXmlSimpleV3] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
SELECT [dbo].[fnGetXmlSimpleV3]() [fnGetXmlSimpleV3]
