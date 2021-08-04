CREATE FUNCTION [db_scales].[GetWeithingFactsByOrderID]
(
	@OrderId int
)
RETURNS TABLE AS RETURN
(
	SELECT 
		f.[Id],
		p.[NomenclatureID] as RRefGoods,
		f.[OrderID],
		f.[SSCC],
		f.[WeithingDate],
		f.[NetWeight],
		f.[TareWeight],
		f.ScaleID,
		f.UUID,
		f.PluId,
		f.ProductDate,
		f.SeriesID,
		f.RegNum,
		f.Kneading,
		s.UUID as UuidPack,
		s.SSCC as SsccPack
	FROM [db_scales].[WeithingFact] f
		INNER JOIN [db_scales].[PLU] p 
		ON f.[ScaleID] = p.[ScaleID] AND f.PluId = p.PLU	
		INNER JOIN [db_scales].[Orders] o 
		ON  f.[ScaleID] = o.[ScaleID] AND f.[OrderID] = o.Id	
		LEFT JOIN [db_scales].[ProductSeries] s
		ON f.SeriesID = s.[Id]

	WHERE o.[Id] = @OrderId

);
GO
GRANT SELECT
    ON OBJECT::[db_scales].[GetWeithingFactsByOrderID] TO [db_scales_users]
    AS [scales_owner];

