CREATE FUNCTION [db_scales].[GetPrinterResources]
(
	@PrinterId INT,
	@Type varchar(3) = 'GRF'
)
RETURNS TABLE AS RETURN
(
	SELECT
		ref.[ID],
		res.[Name],	
		convert(varchar(max),res.[ImageData],0) [ImageData]
	FROM [db_scales].[TemplateResources] res
	INNER JOIN [db_scales].[ZebraPrinterResourceRef] ref ON res.ID = ref.[ResourceID] 
	WHERE ref.[PrinterID] = @PrinterId AND res.[Type] = @Type
)
GO
GRANT SELECT
    ON OBJECT::[db_scales].[GetPrinterResources] TO [db_scales_users]
    AS [scales_owner];

