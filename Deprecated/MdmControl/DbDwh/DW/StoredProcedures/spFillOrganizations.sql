CREATE PROCEDURE [DW].[spFillOrganizations]

	@Name nvarchar(150),
	@StatusID int ,
	@InformationSystemID  int,
	@CodeInIS varbinary(16)

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();

	MERGE [DW].[DimOrganizations] as target
	USING ( 
	SELECT 
		@Name,
		@CreateDate,
		@DLM,
		@StatusID,
		@InformationSystemID,
		@CodeInIS 

	) AS source (
		[Name],
		[CreateDate],
		[DLM],
		[StatusID],
		[InformationSystemID],
		[CodeInIS]
	)  
	ON (target.[CodeInIs] = source.[CodeInIs] AND target.[InformationSystemID] = source.[InformationSystemID]) 

	WHEN MATCHED THEN  UPDATE SET 
		[Description] = @Name,
		[StatusID] = @StatusID,
		[DLM] = @DLM
		
	WHEN NOT MATCHED THEN INSERT (
			[Description],
			[CreateDate],
			[DLM],
			[StatusID],
			[InformationSystemID],
			[CodeInIS]
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

