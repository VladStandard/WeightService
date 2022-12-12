------------------------------------------------------------------------------------------------------------------------
-- {table_name} STRUCTURE
------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
DECLARE @DB_NAME_CURRENT VARCHAR(128) = NULL;
DECLARE @DB_NAME_TEMPLATE_DEVELOP VARCHAR(128) = 'SCALES';
DECLARE @DB_NAME_TEMPLATE_RELEASE VARCHAR(128) = 'ScalesDB';
DECLARE @IS_UPDATE BIT = 1;
DECLARE @IS_COMMIT BIT = 0;
------------------------------------------------------------------------------------------------------------------------
IF (@IS_UPDATE = 1) BEGIN
	BEGIN TRAN
		PRINT N'[i] JOB IS STARTED';
		-- CHECK DB
		SET @DB_NAME_CURRENT = (SELECT DB_NAME());
		IF NOT (@DB_NAME_CURRENT = @DB_NAME_TEMPLATE_DEVELOP OR @DB_NAME_CURRENT = @DB_NAME_TEMPLATE_RELEASE) BEGIN
			PRINT N'[!] CURRENT DB IS NOT CORRECT: ' + @DB_NAME_CURRENT;
		END ELSE BEGIN
			PRINT N'[i] CURRENT DB IS CORRECT: ' + @DB_NAME_CURRENT;
			-- CHECK TABLE
			IF EXISTS (SELECT 1 FROM [sys].[tables] WHERE [name] = N'{table_name}') BEGIN
				PRINT N'[i] TABLE [{table_name}] IS ALREADY EXISTS';
			END ELSE BEGIN
				PRINT N'[i] TABLE [{table_name}] IS NOT EXISTS AND WILL BE CREATED';
				-- CREATE TABLE
				CREATE TABLE [db_scales].[{table_name}] (
					[UID] [UNIQUEIDENTIFIER] NOT NULL,
					[CREATE_DT] [DATETIME] NOT NULL,
					[CHANGE_DT] [DATETIME] NOT NULL,
					[IS_MARKED] [BIT] NOT NULL,
				PRIMARY KEY CLUSTERED ([UID] ASC
					) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON,
					ALLOW_PAGE_LOCKS = ON) ON [ScalesFileGroup]);
				PRINT N'[i] TABLE [{table_name}] WAS CREATED';
				EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'{table_name} reference' , @level0type=N'SCHEMA',
					@level0name=N'db_scales', @level1type=N'TABLE', @level1name=N'{table_name}';
				PRINT N'[i] ADD EXTEND PROPERTY WAS COMPLETED';
			END;
			-- ALTER TABLE
			IF EXISTS (SELECT 1 FROM [sys].[tables] WHERE [name] = N'{table_name}') BEGIN
				IF NOT EXISTS (SELECT 1 FROM [SYS].[DEFAULT_CONSTRAINTS] WHERE [NAME] = 'DF_{table_name}_UID') BEGIN
					ALTER TABLE [db_scales].[{table_name}] ADD CONSTRAINT [DF_{table_name}_UID] DEFAULT (NEWID()) FOR [UID];
					PRINT N'[i] CONSTRAINT [DF_{table_name}_UID] WAS CREATED';
				END;
				IF NOT EXISTS (SELECT 1 FROM [SYS].[DEFAULT_CONSTRAINTS] WHERE [NAME] = 'DF_{table_name}_CREATE_DT') BEGIN
					ALTER TABLE [db_scales].[{table_name}] ADD CONSTRAINT [DF_{table_name}_CREATE_DT] DEFAULT (GETDATE()) FOR [CREATE_DT];
					PRINT N'[i] CONSTRAINT [DF_{table_name}_CREATE_DT] WAS CREATED';
				END;
				IF NOT EXISTS (SELECT 1 FROM [SYS].[DEFAULT_CONSTRAINTS] WHERE [NAME] = 'DF_{table_name}_CHANGE_DT') BEGIN
					ALTER TABLE [db_scales].[{table_name}] ADD CONSTRAINT [DF_{table_name}_CHANGE_DT] DEFAULT (GETDATE()) FOR [CHANGE_DT];
					PRINT N'[i] CONSTRAINT [DF_{table_name}_CHANGE_DT] WAS CREATED';
				END;
				IF NOT EXISTS (SELECT 1 FROM [SYS].[DEFAULT_CONSTRAINTS] WHERE [NAME] = 'DF_{table_name}_IS_MARKED') BEGIN
					ALTER TABLE [db_scales].[{table_name}] ADD CONSTRAINT [DF_{table_name}_IS_MARKED] DEFAULT (0) FOR [IS_MARKED];
					PRINT N'[i] CONSTRAINT [DF_{table_name}_IS_MARKED] WAS CREATED';
				END;
			END;
		END;
		PRINT N'[i] ALTER TABLE WAS COMPLETED';
	-- COMMIT
        IF (@IS_COMMIT = 1) BEGIN
            COMMIT TRAN;
            PRINT N'[i] JOB WAS COMMITTED';
        END ELSE BEGIN
            ROLLBACK TRAN;
            PRINT N'[!] JOB WAS ROLL-BACKED';
        END;
END;
------------------------------------------------------------------------------------------------------------------------