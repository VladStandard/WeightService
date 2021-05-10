CREATE FUNCTION [db_scales].[GetWeithingFactsByScale]
(
	@ScaleID	VARCHAR(38),
	@StartDate	datetime,
	@EndDate	datetime
)
RETURNS TABLE AS RETURN
(
	SELECT 
		f.[Id],
		o.NomenclatureId as RRefGoods,
		f.[OrderID],
		f.[SSCC],
		f.[WeithingDate],
		f.[NetWeight],
		f.[TareWeight],
		f.[ScaleID],
		f.UUID,
		f.PluId,
		f.ProductDate,
		f.SeriesID,
		f.RegNum,
		f.Kneading,
		s.UUID as UuidPack,
		s.SSCC as SsccPack

	FROM [db_scales].[WeithingFact] f
		INNER JOIN [db_scales].[PLU] o ON f.[ScaleID] = o.[ScaleID] AND f.PluId = o.PLU	
		LEFT JOIN [db_scales].[ProductSeries] s	ON f.SeriesID = s.[Id]

	WHERE 
		f.ScaleID	= @ScaleID
		AND f.WeithingDate BETWEEN @StartDate and @EndDate
);

GO

GRANT SELECT ON [db_scales].[GetWeithingFactsByScale] TO [db_scales_users]; 
GO
