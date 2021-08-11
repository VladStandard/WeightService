/*

DECLARE @Name nvarchar(50) = 'ARIALBL.TTF'
DECLARE @Description nvarchar(500) = 'DescriptionDescriptionDescriptionDescriptionDescription'
DECLARE @Type varchar(3) =  'TTF';
DECLARE @ImageData varchar(max) = CONVERT(varchar(max),'Строка(Base64Строка(шрифт.Данные.Получить()))')
DECLARE @Marked bit = 0

EXECUTE [db_scales].[SetTemplateResources] 
,@Name
,@Description
,@Type
,@ImageData
,@Marked

*/


CREATE PROCEDURE  [db_scales].[LoadResourceToDB]
(
	@ID INT ,
	@Name nvarchar(50),
	@Description nvarchar(500),
	@Type varchar(3),
	@ImageData varchar(max), -- binary[] --> base64
	@Marked bit = 0
)
AS
BEGIN
	
	DECLARE @IDS TABLE (ID INT);
	
	IF EXISTS(SELECT * FROM [db_scales].[TemplateResources] WHERE ID = @ID) 
	BEGIN

		UPDATE [db_scales].[TemplateResources]
		SET 
			[Description]=@Description,
			[Type] = @Type,
			[ImageData] = CAST(@ImageData as varbinary(max)),
			[Marked] = @Marked
		OUTPUT INSERTED.Id INTO @IDS
		WHERE ID = @ID;
		SELECT TOP 1 @ID=ID FROM @IDS;

	END
	ELSE
	BEGIN

		INSERT [db_scales].[TemplateResources]
			([Name],[Description],[Type],[ImageData],[Marked]) 
			VALUES (@Name,@Description,@Type,CAST(@ImageData as varbinary(max)),0);

		SET @ID = @@IDENTITY;

	END

	RETURN @ID;

END
GO

GRANT EXECUTE ON [db_scales].[LoadResourceToDB] TO [db_scales_users]; 
GO
