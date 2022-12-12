------------------------------------------------------------------------------------------------------------------------
-- {table_name} TRUNC
------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
DECLARE @DB_NAME_CURRENT VARCHAR(128) = NULL;
DECLARE @DB_NAME_TEMPLATE_DEVELOP VARCHAR(128) = 'SCALES';
DECLARE @DB_NAME_TEMPLATE_RELEASE VARCHAR(128) = 'ScalesDB';
DECLARE @IS_TRUNC BIT = 1;
DECLARE @IS_COMMIT BIT = 0;
------------------------------------------------------------------------------------------------------------------------
DECLARE @CONSTRAINT_NAME VARCHAR(128)
IF (@IS_TRUNC = 1) BEGIN
	BEGIN TRAN
		PRINT N'[i] JOB IS STARTED';
		-- CHECK DB
		SET @DB_NAME_CURRENT = (SELECT DB_NAME());
		IF NOT (@DB_NAME_CURRENT = @DB_NAME_TEMPLATE_DEVELOP OR @DB_NAME_CURRENT = @DB_NAME_TEMPLATE_RELEASE) BEGIN
			PRINT N'[!] CURRENT DB IS NOT CORRECT: ' + @DB_NAME_CURRENT;
		END ELSE BEGIN
			PRINT N'[i] CURRENT DB IS CORRECT: ' + @DB_NAME_CURRENT;
			-- CHECK TABLE
			IF NOT EXISTS (SELECT 1 FROM [sys].[tables] WHERE [name] = N'{table_name}]') BEGIN
				PRINT N'[!] TABLE [{table_name}] IS NOT EXISTS';
			END ELSE BEGIN
			    -- TRUNC TABLE
				PRINT N'[i] TABLE [{table_name}] IS EXISTS';
                TRUNCATE TABLE [db_scales].[{table_name}];
                PRINT N'[i] TRUNCATE TABLE [{table_name}] IS COMPLETED';
			END;
		END;
        IF (@IS_COMMIT = 1) BEGIN
            COMMIT TRAN;
            PRINT N'[i] JOB IS COMMITTED';
        END ELSE BEGIN
            ROLLBACK TRAN;
            PRINT N'[!] JOB IS ROLL-BACKED';
        END;
END;
------------------------------------------------------------------------------------------------------------------------
