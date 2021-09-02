CREATE PROCEDURE [ETL].[spSetLastDate]
	@InformationSystemID int,
	@DocumentName nvarchar(150),  
	@PackageID varchar(38),
	@DateID datetime
AS
BEGIN
	SET NOCOUNT ON;

	MERGE [ETL].[ObjectStatuses] as target

	USING (SELECT @InformationSystemID,@DocumentName,1,@DateID,@PackageID) AS source 
	([InformationSystemID],[Name],[StatusID],[LastID],[PackageID])  
	ON ( target.InformationSystemID = source.InformationSystemID AND target.[Name] = source.[Name] AND target.[PackageID] = source.[PackageID] ) 

	WHEN MATCHED THEN  UPDATE SET 
		LastDate = @DateID
		
	WHEN NOT MATCHED THEN INSERT  
		(
		[InformationSystemID]
		,[Name]
		,[PackageID]
		,[StatusID]
		,LastDate
		)
		 VALUES
		(
		@InformationSystemID
		,@DocumentName
		,@PackageID
		,1
		,@DateID
		);
END

GO

