------------------------------------------------------------------------------------------------------------------------
-- Update-2022-03-01
------------------------------------------------------------------------------------------------------------------------
DECLARE @IS_FIX_PLU BIT = 0  -- исправить ошибки
DECLARE @IS_COMMMIT BIT = 0  -- коммит транзакции
DECLARE @IS_UPDATE_NULL BIT = 0  -- не нулевые поля [EAN13], [ITF14], [GTIN] таблицы [PLU]
DECLARE @IS_UPDATE_C_PLU_PLU BIT = 0  -- ограничение на поле [PLU] таблицы [PLU]
DECLARE @IS_UPDATE_UQ_PLU_PLU BIT = 0  -- уникальное ограничение на поля [ScaleId], [PLU] таблицы [PLU]
DECLARE @INFO NVARCHAR(1024) -- инфо
SET NOCOUNT ON
------------------------------------------------------------------------------------------------------------------------
IF (@IS_FIX_PLU = 1) BEGIN
	PRINT N'[+] IS_FIX_PLU IS ENABLED'
	BEGIN TRAN
		-- FIX 1
		--UPDATE db_scales.PLU SET PLU=101 WHERE Id=837
		--UPDATE db_scales.PLU SET PLU=102 WHERE Id=838
		--UPDATE db_scales.PLU SET PLU=103 WHERE Id=839
		--UPDATE db_scales.PLU SET PLU=104 WHERE Id=840
		--UPDATE db_scales.PLU SET PLU=105 WHERE Id=841
		--SELECT S.Description, P.PLU , P.* FROM db_scales.PLU P
		--INNER JOIN db_scales.Scales S ON p.ScaleId=s.Id
		--WHERE s.Id=31
		-- FIX 2
		--DELETE FROM db_scales.PLU
		--WHERE [Id] IN (SELECT [P].[ID]
		--	--SELECT S.Description, P.PLU , P.* 
		--	FROM db_scales.PLU P
		--	INNER JOIN db_scales.Scales S ON p.ScaleId=s.Id
		--	WHERE P.PLU < 100 OR P.PLU > 999)
		-- FIX 3
		--UPDATE db_scales.PLU SET PLU=748 WHERE Id=403
		--UPDATE db_scales.PLU SET PLU=779 WHERE Id=404
		--UPDATE db_scales.PLU SET PLU=731 WHERE Id=405
		--UPDATE db_scales.PLU SET PLU=755 WHERE Id=406
		--UPDATE db_scales.PLU SET PLU=762 WHERE Id=407
		--SELECT S.Description, P.PLU , P.* 
		--	FROM db_scales.PLU P
		--	INNER JOIN db_scales.Scales S ON p.ScaleId=s.Id
		--	WHERE S.Id=29
		-- FIX 4
		SET @INFO = (
			SELECT COUNT(*)
				FROM db_scales.PLU P
				INNER JOIN db_scales.Scales S ON p.ScaleId=s.Id
				WHERE P.Plu < 100 OR P.PLU > 999)
		DELETE FROM db_scales.PLU WHERE PLU < 100
		DELETE FROM db_scales.PLU WHERE PLU > 999
		PRINT N'[+] FIX ERRORS WAS COMPLETE: ' + @INFO
	IF (@IS_COMMMIT = 1) BEGIN
		PRINT N'[+] COMMIT IS ENABLED'
		COMMIT TRAN
	END ELSE BEGIN
		PRINT N'[-] COMMIT IS DISABLED'
		ROLLBACK TRAN
	END
END
------------------------------------------------------------------------------------------------------------------------
IF (@IS_UPDATE_NULL = 1) BEGIN
	PRINT N'[+] IS_UPDATE_NULL IS ENABLED'
	BEGIN TRAN
		DECLARE @TABLE_ID TABLE ([ID] INT)
		INSERT INTO @TABLE_ID SELECT [ID] FROM [db_scales].[PLU] [P] WHERE [P].[EAN13] IS NULL OR [P].[ITF14] IS NULL OR [P].[GTIN] IS NULL
		UPDATE [db_scales].[PLU] SET [EAN13] = '' WHERE [EAN13] IS NULL
		UPDATE [db_scales].[PLU] SET [ITF14] = '' WHERE [ITF14] IS NULL
		UPDATE [db_scales].[PLU] SET [GTIN] = '' WHERE [GTIN] IS NULL
		SET @INFO = CAST((SELECT COUNT([ID]) FROM @TABLE_ID) AS NVARCHAR(255))
		PRINT N'[+] UPDATE NULL COMPLETE WITH RESULT: ' + @INFO
	IF (@IS_COMMMIT = 1) BEGIN
		PRINT N'[+] COMMIT IS ENABLED'
		COMMIT TRAN
	END ELSE BEGIN
		PRINT N'[-] COMMIT IS DISABLED'
		ROLLBACK TRAN
	END
END
------------------------------------------------------------------------------------------------------------------------
IF (@IS_UPDATE_C_PLU_PLU = 1) BEGIN
	PRINT N'[+] IS_UPDATE_C_PLU_PLU IS ENABLED'
		IF EXISTS (SELECT 1 FROM [sys].[objects] WHERE [name] = 'CN_PLU_PLU') 
			ALTER TABLE [db_scales].[PLU] DROP CONSTRAINT [CN_PLU_PLU]
		IF EXISTS (SELECT 1 FROM [sys].[objects] WHERE [name] = 'C_PLU_PLU') 
			ALTER TABLE [db_scales].[PLU] DROP CONSTRAINT [C_PLU_PLU]
		ALTER TABLE [db_scales].[PLU] ADD CONSTRAINT [C_PLU_PLU] CHECK([PLU] > 99 AND [PLU] < 1000)
		PRINT N'[+] CONSTRAINT [C_PLU_PLU] WAS ADDED'
	BEGIN TRAN
	IF (@IS_COMMMIT = 1) BEGIN
		PRINT N'[+] COMMIT IS ENABLED'
		COMMIT TRAN
	END ELSE BEGIN
		PRINT N'[-] COMMIT IS DISABLED'
		ROLLBACK TRAN
	END
END
------------------------------------------------------------------------------------------------------------------------
IF (@IS_UPDATE_UQ_PLU_PLU = 1) BEGIN
	PRINT N'[+] IS_UPDATE_UQ_PLU_PLU IS ENABLED'
		IF EXISTS (SELECT 1 FROM [sys].[objects] WHERE [name] = 'UQ_PLU_PLU') 
			ALTER TABLE [db_scales].[PLU] DROP CONSTRAINT [UQ_PLU_PLU]
		ALTER TABLE [db_scales].[PLU] ADD CONSTRAINT [UQ_PLU_PLU] UNIQUE([ScaleId], [PLU])
		PRINT N'[+] CONSTRAINT [UQ_PLU_PLU] WAS ADDED'
	BEGIN TRAN
	IF (@IS_COMMMIT = 1) BEGIN
		PRINT N'[+] COMMIT IS ENABLED'
		COMMIT TRAN
	END ELSE BEGIN
		PRINT N'[-] COMMIT IS DISABLED'
		ROLLBACK TRAN
	END
END
------------------------------------------------------------------------------------------------------------------------
SET NOCOUNT OFF
------------------------------------------------------------------------------------------------------------------------
