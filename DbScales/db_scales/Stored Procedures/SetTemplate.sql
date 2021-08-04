CREATE PROCEDURE [db_scales].[SetTemplate]
	@TemplateID	int
	,@CategoryID	nVARCHAR(150)
    ,@Title			nvarchar(250)
    ,@ImageData		varbinary(max)
	,@ID int OUTPUT
AS
BEGIN

	MERGE  [db_scales].[Templates] AS target 
	USING (
	SELECT  
		@TemplateID,
		@CategoryID,
		@Title,
		@ImageData
	) AS source (
		[TemplateID],
		[CategoryID],
		[Title],
		[ImageData]
	)  
	ON (target.[Id] = source.[TemplateID])  
	WHEN MATCHED THEN
		UPDATE SET 
			[CategoryID] = source.[CategoryID],
			[Title] = source.[Title],
			[ImageData] = source.[ImageData],
			[ModifiedDate]	= GETDATE()
			

	WHEN NOT MATCHED THEN  
        INSERT (
			[Id],
			[CategoryID],
			[Title],
			[ImageData]
		) VALUES (
			source.[TemplateID],
			source.[CategoryID],
			source.[Title],
			source.[ImageData]
		);  

	SELECT @ID = @@IDENTITY;

	RETURN 0
END
GO
GRANT EXECUTE
    ON OBJECT::[db_scales].[SetTemplate] TO [db_scales_users]
    AS [scales_owner];

