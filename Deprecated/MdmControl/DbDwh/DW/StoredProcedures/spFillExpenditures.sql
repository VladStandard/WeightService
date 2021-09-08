CREATE PROCEDURE [DW].[spFillExpenditures]
	@Name nvarchar(150),
	@StatusID int ,
	@InformationSystemID  int,
	@CodeInIS varbinary(16)

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();

	MERGE [DW].[DimExpenditures] as target
	USING ( 
	SELECT 
		@Name,
		@CreateDate,
		@DLM,
		@StatusID,
		@InformationSystemID,
		@CodeInIS 

	) AS source (
		[RegionName],
		[CreateDate],
		[DLM],
		[StatusID],
		[InformationSystemID],
		[CodeInIS]
	)  
	ON (target.[CodeInIs] = source.[CodeInIs] AND target.[InformationSystemID] = source.[InformationSystemID]) 

	WHEN MATCHED THEN  UPDATE SET 
		[Name] = @Name,
		[StatusID] = @StatusID,
		[DLM] = @DLM
		
	WHEN NOT MATCHED THEN INSERT (
			[Name],
			[CreateDate],
			[DLM],
			[StatusID],
			[InformationSystemID],
			[CodeInIs]
		) VALUES  (
			@Name,
			@CreateDate,
			@DLM,
			@StatusID,
			@InformationSystemID,
			@CodeInIS 
		);

END

GO
