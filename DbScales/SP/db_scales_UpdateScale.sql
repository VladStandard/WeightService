-- [db_scales].[UpdateScale]

-- DROP PROCEDURE
DROP PROCEDURE IF EXISTS [db_scales].[UpdateScale]
GO

-- CREATE PROCEDURE
CREATE PROCEDURE [db_scales].[UpdateScale] (
@ID INT = NULL,
@Description NVARCHAR(150) = NULL,
@IP VARCHAR(15) = NULL,
@Port SMALLINT = NULL,
@MAC VARCHAR(35) = NULL,
@SendTimeout SMALLINT = NULL,
@ReceiveTimeout SMALLINT = NULL,
@ComPort VARCHAR(5) = NULL,
@UseOrder SMALLINT = 0,
@VerScalesUI VARCHAR(30) = NULL,
@ScaleFactor INT = NULL)
AS
BEGIN
	UPDATE [db_scales].[SCALES]
	SET [Description] = @Description
	   ,[DeviceIP] = @IP
	   ,[DevicePort] = @Port
	   ,[DeviceMAC] = @MAC
	   ,[DeviceSendTimeout] = @SendTimeout
	   ,[DeviceReceiveTimeout] = @ReceiveTimeout
	   ,[DeviceComPort] = @ComPort
	   ,[UseOrder] = @UseOrder
	   ,[VerScalesUI] = @VerScalesUI
	   ,[ModifiedDate] = GETDATE()
	   ,[ScaleFactor] = @ScaleFactor
	WHERE [Id] = @ID
END
GO

-- ACCESS
GRANT EXECUTE ON [db_scales].[UpdateScale] TO [db_scales_users]
GO

-- CHECK PROCEDURE
DECLARE @DESCRIPTION NVARCHAR(255) = N'Test'
IF NOT EXISTS (SELECT 1 FROM [db_scales].[Scales] [S] WHERE [S].[Description]=@DESCRIPTION) BEGIN  
	INSERT INTO [db_scales].[Scales] ([Description], [CreateDate], [ModifiedDate], [WorkShopId], [Marked])
	VALUES (@DESCRIPTION, GETDATE(), GETDATE(), 5, 0)
END
DECLARE @ID INT = (SELECT [ID] FROM [db_scales].[Scales] [S] WHERE [S].[Description]=@DESCRIPTION)
EXECUTE [db_scales].[UpdateScale] @ID
								 ,@DESCRIPTION
								 ,NULL
								 ,NULL
								 ,NULL
								 ,NULL
								 ,NULL
								 ,NULL
								 ,NULL
								 ,N'v.0.0.1'
								 ,1000
SELECT *
FROM [db_scales].[SCALES] [S]
WHERE [S].[Id] = @ID
