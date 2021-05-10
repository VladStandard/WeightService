CREATE FUNCTION [db_scales].[GetWeithingFactsByPLU]
(
	@ScaleID	VARCHAR(38),
	@PLU		int,
	@StartDate	datetime,
	@EndDate	datetime
)
RETURNS TABLE AS RETURN
(
	SELECT 
		f.[Id],
		o.NomenclatureId RRefGoods,
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
		x.UUID as UuidPack,
		x.SSCC as SsccPack


	FROM [db_scales].[WeithingFact] f
		INNER JOIN [db_scales].[PLU] o 	ON f.[ScaleID] = o.[ScaleID] AND f.PluId = o.PLU	
		LEFT JOIN [db_scales].[ProductSeries] as x ON f.SeriesID = x.[Id]

	WHERE 
		f.ScaleID	= @ScaleID
		AND f.PluId = @PLU 
		AND f.WeithingDate BETWEEN @StartDate and @EndDate
);

GO

GRANT SELECT ON [db_scales].[GetWeithingFactsByPLU] TO [db_scales_users]; 
GO
