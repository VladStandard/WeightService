CREATE PROCEDURE [ETL].[SetTCVarID]

	@ObjectName varchar(255),
	@InformationSystemsID int,
	@PackageID  varchar(38),
	@VarID bigint 

AS
BEGIN
	
	SET NOCOUNT ON;

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

	WHEN MATCHED THEN UPDATE SET 
		[LastID] = source.[LastID],
		[DLM] = GETDATE()

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

	RETURN 0

END
GO

GRANT EXECUTE ON [ETL].[SetTCVarID] TO [guest] ;
GO