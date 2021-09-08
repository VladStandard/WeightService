CREATE PROCEDURE [ETL].[GetTCVarID]

	@ObjectName varchar(255),
	@InformationSystemsID int,
	@PackageID  varchar(38),
	@VarID bigint OUTPUT

AS
BEGIN
	
	SET NOCOUNT ON;

	IF NOT EXISTS (
		SELECT [LastID] 
		FROM [ETL].[ObjectStatuses] 
		WHERE [InformationSystemID] = @InformationSystemsID AND PackageID = @PackageID AND [Name] = @ObjectName 
	) 
	BEGIN
		SET @VarID = 0;
		--SET @VarID = -9223372036854775808;
	
		MERGE [ETL].[ObjectStatuses] as target
		USING (SELECT 
			@InformationSystemsID, 
			@ObjectName,
			1,
			@VarID,
			@PackageID
			) AS source 
			(
			[InformationSystemsID],
			[ObjectName],
			[StatusID],
			[LastID],
			[PackageID]
			)  
		ON (
			target.InformationSystemID = source.[InformationSystemsID] 
			AND target.[Name] = source.[ObjectName]  
			AND target.PackageID = source.PackageID) 

		WHEN NOT MATCHED THEN INSERT  
			(
			[InformationSystemID]
			,[Name]
			,[PackageID]
			,[StatusID]
			,[LastID]
			)
			 VALUES
			(
			 source.[InformationSystemsID]
			,source.[ObjectName]
			,source.[PackageID]
			,source.[StatusID]
			,source.[LastID]
			);

	END

	SELECT TOP(1) @VarID = [LastID]
	FROM [ETL].[ObjectStatuses]
	WHERE 
		[InformationSystemID] = @InformationSystemsID 
		AND PackageID = @PackageID 
		AND [Name] = @ObjectName;

	RETURN 0

END
GO

GRANT EXECUTE ON [ETL].[GetTCVarID] TO [guest] ;
GO