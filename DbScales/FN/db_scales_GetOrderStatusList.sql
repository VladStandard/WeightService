CREATE FUNCTION [db_scales].[GetOrderStatusList]
(
	@OrderList xml,
	@CurrentDate datetime = null
)
RETURNS @OutputTable TABLE (OrderID varchar(38), CurrentStatus tinyint, CurrentStatusName varchar(50))
AS
BEGIN

	IF @CurrentDate IS NULL 
		SET @CurrentDate = GetDate();

	DECLARE @CurStat int;

	INSERT INTO @OutputTable (OrderID,CurrentStatus,CurrentStatusName) 
	SELECT
		s.OrderId,
		s.CurrentStatus,
		CASE s.CurrentStatus
			WHEN 0 THEN 'New'
			WHEN 1 THEN 'InProgress'
			WHEN 2 THEN 'Paused'
			WHEN 3 THEN 'Performed' 
			WHEN 4 THEN 'Canceled' 
			ELSE NULL
		END  CurrentStatusName

	FROM [db_scales].[OrderStatus] s
	WHERE s.OrderId IN (SELECT T.c.query('.').value('.','varchar(38)') FROM @OrderList.nodes('/root/IDRRef') As T(c)) 
	AND
	s.CurrentDate = 
	(
		SELECT MAX(z.CurrentDate) 
		FROM [db_scales].[OrderStatus] z 
		WHERE z.OrderId IN (SELECT T.c.query('.').value('.','varchar(38)') FROM @OrderList.nodes('/root/IDRRef') As T(c))  
		AND z.CurrentDate < @CurrentDate
	);

	RETURN;

END
GO

GRANT select ON [db_scales].[GetOrderStatusList] TO [db_scales_users]
GO