CREATE PROCEDURE [DW].[spFillDocJournal]
	@DocNum					nvarchar(15),
	@DocDate				datetime,
	@DocType				nvarchar(30),
	@Marked					bit,
	@Posted					bit,
    @OrgID					varbinary(16),
	@StatusID				int,
	@InformationSystemID	int,
	@CodeInIS				varbinary(16) 

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();
	DECLARE @DateID int = CONVERT(INT,FORMAT(@DocDate,'yyyyMMdd'));

	MERGE [DW].[DocJournal] as target
	USING ( 
	SELECT 
		@DateID
		,@Marked
		,@Posted
		,@DocNum
		,@DocDate
		,@DocType
		,@OrgID
		,@CreateDate
		,@DLM
		,@StatusID
		,@InformationSystemID
		,@CodeInIS
	) AS source (
		[DateKey]
        ,[Marked]
        ,[Posted]
        ,[DocNum]
        ,[DocDate]
        ,[DocType]
        ,[OrgID]
        ,[CreateDate]
        ,[DLM]
        ,[StatusID]
        ,[InformationSystemID]
        ,[CodeInIS]
	)  
	ON (target.[CodeInIs] = source.[CodeInIs] AND target.[InformationSystemID] = source.[InformationSystemID]) 

	WHEN MATCHED AND 
	CHECKSUM(
		target.[DateKey]
        ,target.[Marked]
        ,target.[Posted]
        ,target.[DocNum]
        ,target.[DocDate]
        ,target.[DocType]
        ,target.[OrgID]
		,target.[InformationSystemID]
        ,target.[CodeInIS]
	) <> CHECKSUM 
	(
		source.[DateKey]
        ,source.[Marked]
        ,source.[Posted]
        ,source.[DocNum]
        ,source.[DocDate]
        ,source.[DocType]
        ,source.[OrgID]
		,source.[InformationSystemID]
        ,source.[CodeInIS]
		-- @DateID
		--,@Marked
		--,@Posted
		--,@DocNum
		--,@DocDate
		--,@DocType
		--,@OrgID
		--,@InformationSystemID
		--,@CodeInIS
	)
	
	THEN UPDATE SET 
		[DateKey]              = @DateID
        ,[Marked]			   = @Marked
        ,[Posted]			   = @Posted
        ,[DocNum]			   = @DocNum
        ,[DocDate]			   = @DocDate
        ,[DocType]			   = @DocType
        ,[OrgID]			   = @OrgID
        ,[CreateDate]		   = @CreateDate
        ,[DLM]				   = @DLM
        ,[StatusID]			   = @StatusID
		
	WHEN NOT MATCHED THEN INSERT (
		[DateKey]
        ,[Marked]
        ,[Posted]
        ,[DocNum]
        ,[DocDate]
        ,[DocType]
        ,[OrgID]
        ,[CreateDate]
        ,[DLM]
        ,[StatusID]
        ,[InformationSystemID]
        ,[CodeInIs]
	) VALUES (
		@DateID
		,@Marked
		,@Posted
		,@DocNum
		,@DocDate
		,@DocType
		,@OrgID
		,@CreateDate
		,@DLM
		,@StatusID
		,@InformationSystemID
		,@CodeInIS
	);

END;
GO

