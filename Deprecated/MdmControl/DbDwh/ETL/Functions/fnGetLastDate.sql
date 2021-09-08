CREATE FUNCTION [ETL].[fnGetLastDate]
(
	@ObjectName varchar(255),
	@InformationSystemsID int,
	@PackageID  varchar(38)

)
RETURNS DATETIME
AS
BEGIN
	declare @varid datetime;
	SELECT TOP(1) @varid = [LastDate] FROM (
	SELECT 0 id, COALESCE([LastDate], CONVERT(datetime,'20200101',112)) [LastDate] FROM [ETL].[ObjectStatuses]
	WHERE [Name] = @ObjectName AND [InformationSystemID] = @InformationSystemsID AND PackageID = @PackageID
	UNION ALL 
	SELECT 1, CONVERT(datetime,'20200101',112) AS [LastDate]
	) AS X
	ORDER BY id;

	RETURN @varid;
END;
GO


GRANT EXECUTE ON [ETL].[fnGetLastDate] TO [guest] ;
GO