CREATE PROCEDURE [DW].[spFillBrands]

	@Code nvarchar(15),
	@Name nvarchar(150),
	@StatusID int ,
	@InformationSystemID  int,
	@CodeInIS varbinary(16)

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();

	MERGE [DW].[DimBrands] as target
	USING ( 
	SELECT 
		@Code,
		@Name,
		@CreateDate,
		@DLM,
		@StatusID,
		@InformationSystemID,
		@CodeInIS 

	) AS source (
		[Code],
		[RegionName],
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
		[StatusID] = @StatusID,
		[DLM] = @DLM
		
	WHEN NOT MATCHED THEN INSERT (
			[Code],
			[Name],
			[CreateDate],
			[DLM],
			[StatusID],
			[InformationSystemID],
			[CodeInIs]
		) VALUES  (
			@Code,
			@Name,
			@CreateDate,
			@DLM,
			@StatusID,
			@InformationSystemID,
			@CodeInIS 
		);

END

GO
