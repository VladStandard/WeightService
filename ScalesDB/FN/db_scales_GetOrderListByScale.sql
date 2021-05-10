CREATE FUNCTION [db_scales].[GetOrderListByScale]
(
	@ScaleID	int,
	@StartDate	datetime,
	@EndDate	datetime
)
RETURNS TABLE
AS

RETURN

	SELECT 
		o.[Id],
		o.[IdRRef]	as [RRefID],
		o.[PLU]	as [PLU],
		o.[PlaneBoxCount],
		o.[PlanePalletCount],
		o.[PlanePackingOperationBeginDate],
		o.[PlanePackingOperationEndDate],
		o.[ProductDate],
		o.TemplateId,
		o.[CreateDate],
		o.[OrderType],
		o.[ScaleId]	,
		s.[CurrentStatus]

		FROM [db_scales].[Orders] o
			INNER JOIN [db_scales].[OrderStatus] s
				ON o.Id = s.[OrderId]  AND
				s.CurrentDate = (
					SELECT MAX(z.CurrentDate) 
					FROM [db_scales].[OrderStatus] z
					WHERE z.OrderId = s.OrderId AND z.CurrentDate <= GETDATE())

		WHERE 
			 o.[CreateDate] BETWEEN @StartDate AND @EndDate
			 AND o.[ScaleID] = @ScaleID


GO

GRANT SELECT ON [db_scales].[GetOrderListByScale] TO  [db_scales_users]; 
GO