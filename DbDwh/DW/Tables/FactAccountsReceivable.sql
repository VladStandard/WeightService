CREATE TABLE [DW].[FactAccountsReceivable]
(
	[DateID]				int NOT NULL,
    [OrgID]					varbinary(16) NOT NULL,
    [ContragentID]			varbinary(16) NOT NULL,
    [EmployeeID]			varbinary(16) NOT NULL,

	[_DateID]				date,
    [_OrgID]				int,
    [_ContragentID]			int,
    [_EmployeeID]			int,
	
	[AmountReceivable]		decimal(15,2),
	[OverdueReceivables]	decimal(15,2),
	[InterestOverdueReceivables] decimal(15,2),
	[DaysOfDelay]			int,
	[MaxDaysOfDelay]		int,
	[AmountDueUpto7days]	decimal(15,2),
	[AmountDueUpto14days]	decimal(15,2),
	[AmountDueOver14days]	decimal(15,2),

	[ID] BIGINT NOT NULL IDENTITY(-9223372036854775808,1),
	[CreateDate]	datetime NOT NULL,
	[DLM]			datetime NOT NULL,
	[StatusID]		int  NOT NULL,
	[InformationSystemID] int NOT NULL,
    [CHECKSUMM] BIGINT NOT NULL,
	[Active] BIT NULL DEFAULT 1, 

    PRIMARY KEY CLUSTERED ([InformationSystemID] ASC, [ContragentID] ASC, [DateID] ASC, [ID] ASC)
)
GO
