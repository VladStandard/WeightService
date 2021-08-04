CREATE FUNCTION [db_scales].[GetOrderStatus]
(
	@OrderId int,
	@CurrentDate datetime = null
)
RETURNS int
AS
BEGIN
	IF @CurrentDate IS NULL 
		SET @CurrentDate = GetDate();

	DECLARE @CurStat int;

	SET @CurStat = (SELECT TOP (1)  
		 s.CurrentStatus

		--@CurStat =
		--	CASE s.CurrentStatus
		--	WHEN 0 THEN 'New'
		--	WHEN 1 THEN 'InProgress'
		--	WHEN 2 THEN 'Paused'
		--	WHEN 3 THEN 'Performed' 
		--	WHEN 4 THEN 'Canceled' 
		--	ELSE NULL
		--	END

	FROM [db_scales].[OrderStatus] s
	WHERE s.OrderId = @OrderId AND
		s.CurrentDate = (
		SELECT MAX(z.CurrentDate) 
		FROM [db_scales].[OrderStatus] z 
		WHERE z.OrderId = @OrderId 
			AND z.CurrentDate < @CurrentDate)
	);

	RETURN @CurStat;

END
GO
GRANT EXECUTE
    ON OBJECT::[db_scales].[GetOrderStatus] TO [db_scales_users]
    AS [scales_owner];

