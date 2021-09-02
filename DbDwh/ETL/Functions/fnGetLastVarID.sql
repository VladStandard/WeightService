CREATE FUNCTION [ETL].[fnGetLastVarID]
(
	@ObjectName varchar(255),
	@InformationSystemsID int,
	@PackageID  varchar(38)

)
RETURNS BIGINT
AS
BEGIN
	declare @varid bigint;
	SELECT TOP(1) @varid = [LastID] FROM (
	SELECT 0 id, COALESCE([LastID], -9223372036854775808) [LastID] FROM [ETL].[ObjectStatuses]
	WHERE [Name] = @ObjectName AND [InformationSystemID] = @InformationSystemsID AND PackageID = @PackageID 
	UNION ALL 
	SELECT 1,9223372036854775807 AS [LastID]
	) AS X
	ORDER BY id;

	RETURN @varid;
END;
GO

GRANT EXECUTE ON [ETL].[fnGetLastVarID] TO [guest] ;
GO