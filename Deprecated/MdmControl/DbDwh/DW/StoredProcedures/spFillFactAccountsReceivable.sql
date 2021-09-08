CREATE PROCEDURE [DW].[spFillFactAccountsReceivable]

	@DocDate				datetime,
    @OrgID					varbinary(16),
    @ContragentID			varbinary(16),
	@EmployeeID				varbinary(16),

	@AmountReceivable		decimal(15,2),
	@OverdueReceivables		decimal(15,2),
	@InterestOverdueReceivables decimal(15,2),
	@DaysOfDelay			int,
	@MaxDaysOfDelay			int,
	@AmountDueUpto7days		decimal(15,2),
	@AmountDueUpto14days	decimal(15,2),
	@AmountDueOver14days	decimal(15,2),

	@CHECKSUMM				bigint,
	@StatusID				int,
	@InformationSystemID	int 

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();
	DECLARE @DateID int = CONVERT(INT,FORMAT(@DocDate,'yyyyMMdd'));

	MERGE [DW].[FactAccountsReceivable] as target
	USING ( 
	SELECT 
        @DateID
		,@OrgID
		,@ContragentID
		,@EmployeeID
		,@AmountReceivable
		,@OverdueReceivables
		,@InterestOverdueReceivables
		,@DaysOfDelay
		,@MaxDaysOfDelay
		,@AmountDueUpto7days
		,@AmountDueUpto14days
		,@AmountDueOver14days

        ,@CreateDate
        ,@DLM
        ,@StatusID
        ,@InformationSystemID
		,@CHECKSUMM
	) AS source (
        [DateID]
		,[OrgID]
		,[ContragentID]
		,[EmployeeID]
		,[AmountReceivable]
		,[OverdueReceivables]
		,[InterestOverdueReceivables]
		,[DaysOfDelay]
		,[MaxDaysOfDelay]
		,[AmountDueUpto7days]
		,[AmountDueUpto14days]
		,[AmountDueOver14days]

        ,[CreateDate]
        ,[DLM]
        ,[StatusID]
        ,[InformationSystemID]
		,[CHECKSUMM]
	)  
	ON (	target.[InformationSystemID] = source.[InformationSystemID]
		AND target.[OrgID]				 = source.[OrgID]
		AND target.[ContragentID]		 = source.[ContragentID]
		AND target.[DateID]				 = source.[DateID]
	) 

	WHEN MATCHED AND target.CHECKSUMM <> @CHECKSUMM
	THEN UPDATE SET 
         [DateID]		= source.DateID 
        ,[OrgID]		= source.OrgID
        ,[ContragentID] = source.ContragentID
		,[EmployeeID]	= source.[EmployeeID]

		,[AmountReceivable]                = source.AmountReceivable
		,[OverdueReceivables]			   = source.OverdueReceivables
		,[InterestOverdueReceivables]	   = source.InterestOverdueReceivables
		,[DaysOfDelay]					   = source.DaysOfDelay
		,[MaxDaysOfDelay]				   = source.MaxDaysOfDelay
		,[AmountDueUpto7days]			   = source.AmountDueUpto7days
		,[AmountDueUpto14days]			   = source.AmountDueUpto14days
		,[AmountDueOver14days]			   = source.AmountDueOver14days

        ,[CreateDate]	= source.CreateDate
        ,[DLM]			= source.DLM
        ,[StatusID]		= source.StatusID
		,[CHECKSUMM]    = source.CHECKSUMM
		,[Active] = 1


 --   WHEN NOT MATCHED BY SOURCE          
 --       AND [InformationSystemID] = @InformationSystemID
 --       AND [CodeInIS] = @CodeInIS
 --       AND [LineNo] = @LineNo
	--THEN DELETE 

	WHEN NOT MATCHED BY TARGET THEN INSERT (

        [DateID]
		,[OrgID]
		,[ContragentID]
		,[EmployeeID]

		,[AmountReceivable]
		,[OverdueReceivables]
		,[InterestOverdueReceivables]
		,[DaysOfDelay]
		,[MaxDaysOfDelay]
		,[AmountDueUpto7days]
		,[AmountDueUpto14days]
		,[AmountDueOver14days]

        ,[CreateDate]
        ,[DLM]
        ,[StatusID]
        ,[InformationSystemID]
		,[CHECKSUMM]
		,[Active]

	) VALUES (
        source.[DateID]
		,source.[OrgID]
		,source.[ContragentID]
		,source.[EmployeeID]

		,source.[AmountReceivable]
		,source.[OverdueReceivables]
		,source.[InterestOverdueReceivables]
		,source.[DaysOfDelay]
		,source.[MaxDaysOfDelay]
		,source.[AmountDueUpto7days]
		,source.[AmountDueUpto14days]
		,source.[AmountDueOver14days]

        ,source.[CreateDate]
        ,source.[DLM]
        ,source.[StatusID]
        ,source.[InformationSystemID]
		,source.[CHECKSUMM]
		,1
	);

END

GO
