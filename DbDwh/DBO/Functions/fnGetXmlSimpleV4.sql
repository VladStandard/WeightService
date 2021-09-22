-- [dbo].[fnGetXmlSimpleV4]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [dbo].[fnGetXmlSimpleV4]
GO

-- CREATE FUNCTION
CREATE FUNCTION [dbo].[fnGetXmlSimpleV4]()
RETURNS xml
AS
BEGIN
	DECLARE @xml XML = '<Response />'
	SET @xml.modify ('insert <Items /> into (/Response)[1] ')
	SET @xml.modify ('insert <Simple /> into (/Response/Items)[1] ')
	SET @xml.modify ('insert <Simple /> into (/Response/Items)[1] ')
	SET @xml.modify ('insert <Simple /> into (/Response/Items)[1] ')
	SET @xml.modify ('insert attribute Description{("First message")} into (/Response/Items/Simple)[1] ')
	SET @xml.modify ('insert attribute Description{("Second message")} into (/Response/Items/Simple)[2] ')
	SET @xml.modify ('insert attribute Description{("Third message")} into (/Response/Items/Simple)[3] ')
	RETURN @xml
END
GO

-- ACCESS
GRANT EXECUTE ON [dbo].[fnGetXmlSimpleV4] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
SELECT [dbo].[fnGetXmlSimpleV4]() [fnGetXmlSimpleV4]
