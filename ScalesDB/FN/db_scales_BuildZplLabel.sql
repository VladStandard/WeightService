CREATE FUNCTION [db_scales].[BuildZplLabel]
(
	@TemplateId		int,
	@Variables		xml
)
RETURNS nvarchar(max)
AS
BEGIN

	DECLARE @Template nvarchar(max);
	SELECT @Template = CONVERT(VARCHAR(max), ImageData, 0) 
		FROM [db_scales].[Templates] WHERE Id = @TemplateId;

	DECLARE @Key varchar(150), @Value varchar(1000);
	DECLARE c CURSOR FOR 
	SELECT 
			--T.c.query('fn:local-name(.)').value('.','varchar(max)') [XmlName],
			T.c.value('@key','varchar(150)') [Key],
			T.c.value('@value','nvarchar(max)') [Value]
		 FROM @Variables.nodes('/*/*') T(c);
	OPEN c;
	FETCH NEXT FROM c INTO @Key, @Value;
	WHILE @@FETCH_STATUS = 0 BEGIN

		SET @Template = REPLACE(@Template,@Key,@Value);

		FETCH NEXT FROM c INTO @Key, @Value;
	END;
	CLOSE c;
	DEALLOCATE c;

	RETURN @Template;

END
GO

GRANT EXECUTE ON [db_scales].[BuildZplLabel] TO  [db_scales_users]; 
GO
