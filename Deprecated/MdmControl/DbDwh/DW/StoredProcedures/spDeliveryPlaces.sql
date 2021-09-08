CREATE PROCEDURE [DW].[spDeliveryPlaces]
	@Marked bit,
	@ContragentID binary(16),
	@Code nvarchar(15),
	@Name nvarchar(150),
	@GUID_Mercury	  nvarchar(36),
	@KPP	  nvarchar(15),			
	@GLN	 nvarchar(15),		
	@Address	 nvarchar(1024)	,
	@FormatStoreID	 binary(16),	
	@RegionStoreID	 binary(16),	
	@FormatStoreName  nvarchar(150),
	@RegionStoreName  nvarchar(150),
	@StatusID int ,
	@InformationSystemID  int,
	@CodeInIS varbinary(16)

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();

	MERGE [DW].[DimDeliveryPlaces] as target
	USING ( 
	SELECT 
		@Marked,
		@ContragentID,
		@Code,
		@Name,
		@GUID_Mercury,
		@KPP,			
		@GLN,		
		@Address,
		@FormatStoreID,	
		@RegionStoreID,
		@FormatStoreName,
		@RegionStoreName,
		@CreateDate,
		@DLM,
		@StatusID,
		@InformationSystemID,
		@CodeInIS 

	) AS source (
		[Marked],
		[ContragentID],
		[Code],
		[Name],
		[GUID_Mercury],
		[KPP],			
		[GLN],		
		[Address],
		[FormatStoreID],	
		[RegionStoreID],
		[FormatStoreName],
		[RegionStoreName],
		[CreateDate],
		[DLM],
		[StatusID],
		[InformationSystemID],
		[CodeInIS]
	)  
	ON (target.[CodeInIs] = source.[CodeInIs] AND target.[InformationSystemID] = source.[InformationSystemID]) 

	WHEN MATCHED THEN  UPDATE SET 
		[Marked]             			= @Marked,
		[ContragentID]		 			= @ContragentID,
		[Code]				 			= @Code,
		[Name]				 			= @Name,
		[GUID_Mercury]		 			= @GUID_Mercury,
		[KPP]				 			= @KPP,			
		[GLN]				 			= @GLN,		
		[Address]			 			= @Address,
		[FormatStoreID]		 			= @FormatStoreID,	
		[RegionStoreID]		 			= @RegionStoreID,
		[FormatStoreName]	 			= @FormatStoreName,
		[RegionStoreName]	 			= @RegionStoreName,
		[StatusID]	= @StatusID,
		[DLM]		= @DLM
		
	WHEN NOT MATCHED THEN INSERT (
			[Marked],
			[ContragentID],
			[Code],
			[Name],
			[GUID_Mercury],
			[KPP],			
			[GLN],		
			[Address],
			[FormatStoreID],	
			[RegionStoreID],
			[FormatStoreName],
			[RegionStoreName],
			[CreateDate],
			[DLM],
			[StatusID],
			[InformationSystemID],
			[CodeInIs]
		) VALUES  (
			@Marked,
			@ContragentID,
			@Code,
			@Name,
			@GUID_Mercury,
			@KPP,			
			@GLN,		
			@Address,
			@FormatStoreID,	
			@RegionStoreID,
			@FormatStoreName,
			@RegionStoreName,
			@CreateDate,
			@DLM,
			@StatusID,
			@InformationSystemID,
			@CodeInIS 
		);

END

GO

