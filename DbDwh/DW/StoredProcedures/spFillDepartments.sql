CREATE PROCEDURE [DW].[spFillDepartments]
	@Code nvarchar(15),
	@Name nvarchar(150),
	@OrgID varbinary(16),
	@Level int,
	@StatusID int ,
	@InformationSystemID  int,
	@CodeInIS varbinary(16),
	@ParentCodeInIS varbinary(16)

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();

	MERGE [DW].[DimDepartments] as target
	USING ( 
	SELECT 
		@Code,
		@Name,
		@OrgID,
		@Level,
		@ParentCodeInIS,
		@CreateDate,
		@DLM,
		@StatusID,
		@InformationSystemID,
		@CodeInIS 

	) AS source (
		[Code],
		[Name],
		[OrganizationID],
		[Level],
		[ParentCodeInIS],
		[CreateDate],
		[DLM],
		[StatusID],
		[InformationSystemID],
		[CodeInIS]
	)  
	ON (target.[CodeInIs] = source.[CodeInIs] AND target.[InformationSystemID] = source.[InformationSystemID]) 

	WHEN MATCHED THEN  UPDATE SET 
		[Code] = @Code,
		[Name] = @Name,
		[OrganizationID] = @OrgID,
		[Level] = @Level,
		[ParentCodeInIS] = @ParentCodeInIS,
		[StatusID] = @StatusID,
		[DLM] = @DLM
		
	WHEN NOT MATCHED THEN INSERT (
			[Code],
			[Name],
			[OrganizationID],
			[Level],
			[ParentCodeInIS],
			[CreateDate],
			[DLM],
			[StatusID],
			[InformationSystemID],
			[CodeInIs]
		) VALUES  (
			@Code,
			@Name,
			@OrgID,
			@Level,
			@ParentCodeInIS,
			@CreateDate,
			@DLM,
			@StatusID,
			@InformationSystemID,
			@CodeInIS 
		);

END

GO

