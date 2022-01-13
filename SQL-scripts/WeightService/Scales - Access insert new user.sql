-- Scales - Access insert new user
DECLARE @user NVARCHAR(255) = N'KOLBASA-VS\popov_aiu'
DECLARE @level BIT = 1
IF NOT EXISTS (SELECT
			1
		FROM [db_scales].[ACCESS]
		WHERE [user] = @user)
BEGIN
	INSERT INTO [db_scales].[ACCESS] ([user], [LEVEL])
		VALUES (@user, @level)
END
ELSE
BEGIN
	UPDATE [db_scales].[ACCESS]
	SET [LEVEL] = @level
	WHERE [user] = @user
END
SELECT
	*
FROM [db_scales].[ACCESS]
WHERE [user] = @user
