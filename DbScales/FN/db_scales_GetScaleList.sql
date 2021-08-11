CREATE FUNCTION [db_scales].[GetScaleList]()
RETURNS TABLE AS RETURN
(
	SELECT 
		[DeviceNumber] as [Id] ,
		[Description],
		[IdRRef] AS [RRefID], 
		[DeviceIP],
		[DevicePort],
		[DeviceMAC],
		[VerScalesUI]

	FROM [db_scales].[Scales]
	 WHERE [Marked] = 0
)
GO

GRANT SELECT ON [db_scales].[GetScaleList]
    TO  [db_scales_users]; 
GO
