CREATE PROCEDURE [DW].[spFillFactAccountsReceivableXML]
	@Data					xml,
	@StatusID				int,
	@InformationSystemID	int 

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();

	DECLARE @Active bit = 1;
	DECLARE @DocDate datetime;
	DECLARE @DateID int;
	--SELECT @DocDate = @Data.value('(/Rows/@DocDate)[1]','datetime');
	--SET @DateID = CONVERT(INT,FORMAT(@DocDate,'yyyyMMdd'));

	SELECT TOP (1) 
		@DateID = [DW].[fnDateTimeToInt](T.c.value('@DocDate', 'datetime')),
		@DocDate = (T.c.value('@DocDate', 'datetime'))
	FROM @Data.nodes('/Rows/Row') T(c);

	DELETE FROM [DW].[FactAccountsReceivable] WHERE [DateID] = @DateID

	INSERT INTO [DW].[FactAccountsReceivable]
    ([DateID]
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
    ,[Active])
	SELECT 
		[DW].[fnDateTimeToInt](T.c.value('@DocDate', 'datetime')) DateID,
		[DW].[fnGetBinary1C](T.c.value('@OrgID', 'varchar(38)')) OrgID,
		[DW].[fnGetBinary1C](T.c.value('@ContragentID', 'varchar(38)')) ContragentID,
		[DW].[fnGetBinary1C](T.c.value('@EmployeeID', 'varchar(38)')) EmployeeID,
		T.c.value('@AmountReceivable', 'varchar(38)') AmountReceivable,
		T.c.value('@OverdueReceivables', 'varchar(38)') OverdueReceivables,
		T.c.value('@InterestOverdueReceivables', 'varchar(38)') InterestOverdueReceivables,
		T.c.value('@DaysOfDelay', 'varchar(38)') DaysOfDelay,
		T.c.value('@MaxDaysOfDelay', 'varchar(38)') MaxDaysOfDelay,
		T.c.value('@AmountDueUpto7days', 'varchar(38)') AmountDueUpto7days,
		T.c.value('@AmountDueUpto14days', 'varchar(38)') AmountDueUpto14days,
		T.c.value('@AmountDueOver14days', 'varchar(38)') AmountDueOver14days,
        @CreateDate,
        @DLM,
        @StatusID,
        @InformationSystemID,
	    CHECKSUM(
			@DateID,
			T.c.value('@OrgID', 'varchar(38)') ,
			T.c.value('@ContragentID', 'varchar(38)') ,
			T.c.value('@EmployeeID', 'varchar(38)') ,
			T.c.value('@AmountReceivable', 'varchar(38)') ,
			T.c.value('@OverdueReceivables', 'varchar(38)') ,
			T.c.value('@InterestOverdueReceivables', 'varchar(38)') ,
			T.c.value('@DaysOfDelay', 'varchar(38)') ,
			T.c.value('@MaxDaysOfDelay', 'varchar(38)') ,
			T.c.value('@AmountDueUpto7days', 'varchar(38)') ,
			T.c.value('@AmountDueUpto14days', 'varchar(38)') ,
			T.c.value('@AmountDueOver14days', 'varchar(38)') 
		),
        @Active

	FROM @Data.nodes('/Rows/Row') T(c);


END

GO
