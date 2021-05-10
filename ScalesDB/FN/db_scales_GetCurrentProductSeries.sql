CREATE FUNCTION [db_scales].[GetCurrentProductSeries]
(
	 @ScaleID VARCHAR(38) 
)
RETURNS @OutputTable TABLE (
	Id int, 
	CreateDate DATETIME, 
	UUID UNIQUEIDENTIFIER, 
	SSCC  VARCHAR(50), 
	CountUnit INT,
	TotalNetWeight numeric(15,3), 
	TotalTareWeight numeric(15,3)
)

AS
BEGIN

	INSERT INTO @OutputTable (Id, CreateDate, UUID, SSCC,CountUnit,TotalNetWeight, TotalTareWeight) 
	SELECT
		s.Id,
		s.CreateDate,
		s.UUID,
		s.SSCC,
		COUNT(w.Id) CountUnit ,
		SUM(w.NetWeight)/1000.0 TotalNetWeight, 
		SUM(w.TareWeight)/1000.0 TotalTareWeight 

	FROM [db_scales].[ProductSeries] s
	LEFT JOIN [db_scales].[WeithingFact] w 
	ON s.Id = w.SeriesID
	WHERE 
		s.[IsClose] = 0
		AND s.[ScaleID] = @ScaleID
	GROUP BY s.Id,s.CreateDate,s.UUID,s.SSCC;

	RETURN;

END
GO

GRANT SELECT ON [db_scales].[GetCurrentProductSeries] TO [db_scales_users]; 
GO