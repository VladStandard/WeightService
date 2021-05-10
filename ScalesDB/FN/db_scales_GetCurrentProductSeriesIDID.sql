CREATE FUNCTION [db_scales].[GetCurrentProductSeriesId]
(
	 @ScaleID int 
)
RETURNS INT
AS
BEGIN

	DECLARE @ID INT = NULL;

	SELECT TOP(1) @ID = Id 
	FROM  [db_scales].[ProductSeries]
	WHERE 
		[IsClose] = 0
		AND [ScaleID] = @ScaleID;

	RETURN @ID;

END
GO

GRANT EXECUTE ON [db_scales].[GetCurrentProductSeriesId] TO [db_scales_users]; 
GO