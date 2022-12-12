------------------------------------------------------------------------------------------------------------------------
-- {table_name} SELECT
------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
DECLARE @DB_NAME_CURRENT VARCHAR(128) = NULL;
DECLARE @DB_NAME_TEMPLATE_DEVELOP VARCHAR(128) = 'SCALES';
DECLARE @DB_NAME_TEMPLATE_RELEASE VARCHAR(128) = 'ScalesDB';
------------------------------------------------------------------------------------------------------------------------
-- CHECK DB
SET @DB_NAME_CURRENT = (SELECT DB_NAME());
IF NOT (@DB_NAME_CURRENT = @DB_NAME_TEMPLATE_DEVELOP OR @DB_NAME_CURRENT = @DB_NAME_TEMPLATE_RELEASE) BEGIN
	PRINT N'[!] CURRENT DB IS NOT CORRECT: ' + @DB_NAME_CURRENT;
END ELSE BEGIN
	PRINT N'[i] CURRENT DB IS CORRECT: ' + @DB_NAME_CURRENT;
	-- CHECK TABLE
	IF NOT EXISTS (SELECT 1 FROM [sys].[tables] WHERE [name] = N'{table_name}') BEGIN
		PRINT N'[!] TABLE [{table_name}] IS NOT EXISTS';
	END ELSE BEGIN
	    -- SELECT TO TABLE
		PRINT N'[i] TABLE [{table_name}] IS EXISTS';
		SELECT
            [N].[CREATE_DT]
            ,[N].[CHANGE_DT]
            ,[N].[IS_MARKED]
		FROM [db_scales].[{table_name}] [N]
		ORDER BY [N].[NAME] ASC;
	END;
END;
------------------------------------------------------------------------------------------------------------------------
