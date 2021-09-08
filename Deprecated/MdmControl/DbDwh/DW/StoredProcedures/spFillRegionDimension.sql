CREATE PROCEDURE [DW].[spFillRegionDimension]

	@RegionName nvarchar(150),
	@Code nvarchar(9),
	@StatusID int ,
	@InformationSystemID  int,
	@CodeInIS varbinary(16)

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();

	MERGE [DW].[DimRegions] as target
	USING ( 
	SELECT 
		@RegionName,
		@CreateDate,
		@DLM,
		@StatusID,
		@InformationSystemID,
		@CodeInIS,
		@Code

	) AS source (
		[RegionName],
		[CreateDate],
		[DLM],
		[StatusID],
		[InformationSystemID],
		[CodeInIS],
		[Code]
	)  
	ON (target.[CodeInIs] = source.[CodeInIs] AND target.[InformationSystemID] = source.[InformationSystemID]) 

	WHEN MATCHED THEN  UPDATE SET 
		[RegionName] = @RegionName,
		[StatusID]   = @StatusID,
		[DLM]        = @DLM,
		[Code]       = @Code
		
	WHEN NOT MATCHED THEN INSERT (
			[RegionName],
			[CreateDate],
			[DLM],
			[StatusID],
			[InformationSystemID],
			[CodeInIs],
			[Code]
		) VALUES  (
			@RegionName,
			@CreateDate,
			@DLM,
			@StatusID,
			@InformationSystemID,
			@CodeInIS,
			@Code
		);

END

GO

