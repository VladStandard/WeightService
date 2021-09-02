
CREATE PROCEDURE [ETL].[SetObjectStatus]
	@InformationSystemID int,
	@DocumentName nvarchar(250),  
	@PackageID varchar(38),
	@MaxID bigint
AS
BEGIN
	SET NOCOUNT ON;

	MERGE [ETL].[ObjectStatuses] as target

	USING (SELECT @InformationSystemID,@DocumentName,1,@MaxID,@PackageID) AS source 
	([InformationSystemID],[Name],[StatusID],[LastID],[PackageID])  
	ON ( target.InformationSystemID = source.InformationSystemID AND target.[Name] = source.[Name]  AND target.PackageID = source.PackageID) 

	WHEN MATCHED THEN  UPDATE SET 
		[LastID] = @MaxID
		
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
		@InformationSystemID
		,@DocumentName
		,@PackageID
		,1
		,@MaxID
		);
END

GO

