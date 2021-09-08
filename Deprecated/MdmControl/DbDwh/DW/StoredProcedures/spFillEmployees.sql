CREATE PROCEDURE [DW].[spFillEmployees]

	@Name [nvarchar](150),
	@PositionID varbinary(36),
	@PositionName [nvarchar](150),
	@DepartmentID varbinary(36),
	@OrgID varbinary(36),
	@Birthday date,
	@ДатаПриема			date,
	@ДатаУвольнения		date,

	@StatusID int,
	@InformationSystemID int,
	@CodeInIS varbinary(36) 

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();

	MERGE [DW].[DimEmployees] as target
	USING ( 
	SELECT 
		@Name,
		@PositionID,
		@PositionName,
		@DepartmentID,
		@OrgID,
		@CreateDate,
		@DLM,
		@StatusID,
		@InformationSystemID,
		@CodeInIS,
		@Birthday,
		@ДатаПриема,		
		@ДатаУвольнения	

	) AS source (
		[Name],
		[PositionID],
		[PositionName],
		[DepartmentID],
		[OrgID],
		[CreateDate],
		[DLM],
		[StatusID],
		[InformationSystemID],
		[CodeInIS],
		[Birthday],
		[ДатаПриема],		
		[ДатаУвольнения]
	)  
	ON (target.[CodeInIs] = source.[CodeInIs] AND target.[InformationSystemID] = source.[InformationSystemID]) 

	WHEN MATCHED THEN  UPDATE SET 
		[FullName] = @Name,
		[PositionID] = @PositionID,
		[PositionName] = @PositionName,
		[DepartmentID] = @DepartmentID,
		[OrgID] = @OrgID,
		[StatusID] = @StatusID,
		[DLM] = @DLM,
		[Birthday] = @Birthday,
		[ДатаПриема]	 = source.[ДатаПриема],		
		[ДатаУвольнения] = source.[ДатаУвольнения]


	WHEN NOT MATCHED THEN INSERT (
			[FullName],
			[PositionID],
			[PositionName],
			[DepartmentID],
			[OrgID],
			[CreateDate],
			[DLM],
			[StatusID],
			[InformationSystemID],
			[CodeInIs],
			[Birthday],
			[ДатаПриема],	
			[ДатаУвольнения]
		) VALUES  (
			@Name,
			@PositionID,
			@PositionName,
			@DepartmentID,
			@OrgID,
			@CreateDate,
			@DLM,
			@StatusID,
			@InformationSystemID,
			@CodeInIS,
			@Birthday,
			source.[ДатаПриема],		
			source.[ДатаУвольнения]
		);

END

GO
