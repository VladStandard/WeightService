CREATE FUNCTION [ETL].[fnGetLastDateID]
(
	@ObjectName varchar(255),
	@InformationSystemsID int,
	@PackageID  varchar(38)

)
RETURNS INT
AS
BEGIN
	declare @varid bigint;
	SELECT TOP(1) @varid = [LastDateID] FROM (
	SELECT 0 id, COALESCE([LastDateID], 20100101) [LastDateID] FROM [ETL].[ObjectStatuses]
	WHERE [Name] = @ObjectName AND [InformationSystemID] = @InformationSystemsID AND PackageID = @PackageID
	UNION ALL 
	SELECT 1,20100101 AS [LastID]
	) AS X
	ORDER BY id;

	RETURN @varid;
END;
GO


GRANT EXECUTE ON [ETL].[fnGetLastDateID] TO [guest] ;
GO