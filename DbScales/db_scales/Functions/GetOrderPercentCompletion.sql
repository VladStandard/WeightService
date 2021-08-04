
CREATE FUNCTION [db_scales].[GetOrderPercentCompletion]
(
	@OrderID varchar(36)
)
RETURNS INT
AS
BEGIN

	DECLARE @CNT INT;
	SELECT TOP(1) @CNT = COUNT(*) 
		FROM [db_scales].[WeithingFact]
		WHERE OrderID = @OrderID

	RETURN @CNT

END
GO
GRANT EXECUTE
    ON OBJECT::[db_scales].[GetOrderPercentCompletion] TO [db_scales_users]
    AS [scales_owner];

