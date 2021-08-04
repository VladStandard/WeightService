CREATE PROCEDURE [db_scales].[SetOrderStatus]
	@OrderId varchar(36),
	@StatusName nvarchar(15),
	@Status tinyint OUTPUT
AS
BEGIN	

	SET @Status = 
	(
		CASE @StatusName 
			WHEN 'New'			THEN 0
			WHEN 'InProgress'	THEN 1
			WHEN 'Paused'		THEN 2
			WHEN 'Performed'	THEN 3
			WHEN 'Canceled'		THEN 4
			ELSE NULL
		END
	);

	INSERT INTO [db_scales].OrderStatus ([OrderId],[CurrentDate],[CurrentStatus]) 
	VALUES (@OrderId,GETDATE(),@Status);

	RETURN @status;
END

GO

GRANT EXECUTE ON [db_scales].[SetOrderStatus]
    TO  [db_scales_users]; 
GO
