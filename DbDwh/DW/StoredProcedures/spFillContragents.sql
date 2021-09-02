CREATE PROCEDURE [DW].[spFillContragents]
	@Marked			bit,
	@Code			nvarchar(15),
	@Name			nvarchar(200),
	@FullName		nvarchar(max),
	@IsBuyer		bit,
	@IsSupplier		bit,
	@GLN			nvarchar(30) ,
	@GUID_Mercury	nvarchar(36),
	@INN			nvarchar(15),
	@KPP			nvarchar(15),
	@Comment		nvarchar(max),
	@Parents		nvarchar(max),
	@OKPO			nvarchar(10),
	@ContragentType nvarchar(10),
	@ContactInfo	nvarchar(max),
	@ManagerID		varbinary(16),
	@NumberDebtDays	int , 	
	@AmountDue		numeric(15,3),
	@DaysDeferment	int, 	
	@CommercialNetworkID	varbinary(16),
	@CommercialNetworkName	nvarchar(max),

	@StatusID		int,
	@InformationSystemID  int,
	@CodeInIS		varbinary(16)

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();

	MERGE [DW].[DimContragents] as target
	USING ( 
	SELECT 
		@Marked,
		@Code,
		@Name,
		@FullName,
		@IsBuyer,
		@IsSupplier,
		@GLN,
		@GUID_Mercury,
		@INN,
		@KPP,
		@Comment,
		@Parents,
		@OKPO,
		@ContragentType,
		@ContactInfo,
		@ManagerID,

		@NumberDebtDays, 	
		@AmountDue,
		@DaysDeferment, 	
		@CommercialNetworkID,
		@CommercialNetworkName,

		@CreateDate,
		@DLM,
		@StatusID,
		@InformationSystemID,
		@CodeInIS 

	) AS source (
		[Marked]
		,[Code]
		,[Name]
		,[FullName]
		,[IsBuyer]
		,[IsSupplier]
		,[GLN]
		,[GUID_Mercury]
		,[INN]
		,[KPP]
		,[Comment]
		,[Parents]
		,[OKPO]
		,[ContragentType]
		,[ContactInfo]
		,[ManagerID]

		,[NumberDebtDays] 	
		,[AmountDue]		
		,[DaysDeferment]	
		,[CommercialNetworkID]
		,[CommercialNetworkName]

		,[CreateDate]
		,[DLM]
		,[StatusID]
		,[InformationSystemID]
		,[CodeInIS]
	)  
	ON (target.[CodeInIs] = source.[CodeInIs] AND target.[InformationSystemID] = source.[InformationSystemID]) 

	WHEN MATCHED THEN  UPDATE SET 
		[Marked]			= @Marked,
		[Code]				= @Code,
		[Name]				= @Name,
		[FullName]			= @FullName,
		[IsBuyer]			= @IsBuyer,
		[IsSupplier]		= @IsSupplier,
		[GLN]				= @GLN,
		[GUID_Mercury]		= @GUID_Mercury,
		[INN]				= @INN,
		[KPP]				= @KPP,
		[Comment]			= @Comment,
		[Parents]			= @Parents,
		[OKPO]				= @OKPO,
		[ContragentType]	= @ContragentType,
		[ContactInfo]		= @ContactInfo,
		[ManagerID]			= @ManagerID,
		[NumberDebtDays]	= @NumberDebtDays, 	
		[AmountDue]			= @AmountDue,
		[DaysDeferment]		= @DaysDeferment, 	
		[CommercialNetworkID]  = @CommercialNetworkID,
		[CommercialNetworkName] = @CommercialNetworkName,

		[StatusID]			= @StatusID,
		[DLM]				= @DLM
		
	WHEN NOT MATCHED THEN INSERT (
			[Marked]
			,[Code]
			,[Name]
			,[FullName]
			,[IsBuyer]
			,[IsSupplier]
			,[GLN]
			,[GUID_Mercury]
			,[INN]
			,[KPP]
			,[Comment]
			,[Parents]
			,[OKPO]
			,[ContragentType]
			,[ContactInfo]
			,[ManagerID]

			,[NumberDebtDays]
			,[AmountDue]		
			,[DaysDeferment]	
			,[CommercialNetworkID] 
			,[CommercialNetworkName]

			,[CreateDate]
			,[DLM]
			,[StatusID]
			,[InformationSystemID]
			,[CodeInIs]
		) VALUES  (
			@Marked,
			@Code,
			@Name,
			@FullName,
			@IsBuyer,
			@IsSupplier,
			@GLN,
			@GUID_Mercury,
			@INN,
			@KPP,
			@Comment,
			@Parents,
			@OKPO,
			@ContragentType,
			@ContactInfo,
			@ManagerID,
			@NumberDebtDays, 	
			@AmountDue		,
			@DaysDeferment	, 	
			@CommercialNetworkID,
			@CommercialNetworkName,

			@CreateDate,
			@DLM,
			@StatusID,
			@InformationSystemID,
			@CodeInIS 
		);

END

GO

