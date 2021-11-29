-- Scales - LOGS diagram summary
-- 1. Connect from PALYCH\LUTON
-- use [ScalesDB]
SET NOCOUNT ON
DECLARE @count INT
DECLARE @rows INT
DECLARE @host_id INT
-- 2. User settings
DECLARE @select BIT = 1
DECLARE @delete BIT = 0
DECLARE @commit BIT = 0
DECLARE @create_dt DATETIME = '2021-11-26 00:00'
DECLARE @host NVARCHAR(255) = N'SCALES-MON-PC208'
----------------------------------------------------------------------------------------------------
SET @host_id = (SELECT
		[id]
	FROM [db_scales].[Hosts]
	WHERE [Name] = @host)
SET @rows = (SELECT
		[p].[rows]
	FROM sys.tables [t]
	INNER JOIN sys.indexes [i]
		ON [t].[object_id] = [i].[object_id]
	INNER JOIN sys.partitions [p]
		ON [i].[object_id] = [p].[object_id]
		AND [i].[index_id] = [p].[index_id]
	WHERE [t].[name] = 'LOGS')
PRINT N'[v] All rows count before run script: ' + CAST(@rows AS NVARCHAR(255))
----------------------------------------------------------------------------------------------------
-- Delete & commit
IF (@delete = 1)
BEGIN
	PRINT N'[v] Enabled delete mode'
	PRINT N'[v] Value for @create_dt: ' + CAST(@create_dt AS NVARCHAR(255))
	PRINT N'[v] Value for @host: ' + @host
	BEGIN TRAN
	SET @count = (SELECT
			COUNT(1)
		FROM [db_scales].[LOGS]
		WHERE [CREATE_DT] < @create_dt
		AND [host_id] = (CASE
			WHEN @host_id > 0 THEN @host_id
		END))
	DELETE FROM [db_scales].[LOGS]
	WHERE [CREATE_DT] < @create_dt
		AND [host_id] = (CASE
			WHEN @host_id > 0 THEN @host_id
		END)
	PRINT N'[v] Found ' + CAST(@count AS NVARCHAR(255)) + ' rows for deleted'
	IF (@commit = 1)
	BEGIN
		PRINT N'[v] Enabled commit mode'
		COMMIT TRAN
		PRINT N'[v] Commit ' + CAST(@count AS NVARCHAR(255)) + ' rows for deleted'
	END
	ELSE
	BEGIN
		PRINT N'[ ] Disabled commit mode'
		ROLLBACK TRAN
	END
END
ELSE
BEGIN
	PRINT N'[ ] Disabled delete mode'
END
----------------------------------------------------------------------------------------------------
-- Select
IF (@select = 1)
BEGIN
	PRINT N'[v] Enabled select mode'
	SELECT
		[l].[UID]
	   ,[l].[CREATE_DT]
	   ,[s].[Description] [SCALE]
	   ,[h].[Name] [HOST]
	   ,[a].[NAME] [APP]
	   ,[l].[VERSION]
	   ,[l].[FILE]
	   ,[l].[LINE]
	   ,[l].[MEMBER]
	   ,[lt].[ICON]
	   ,[l].[MESSAGE]
	FROM [db_scales].[LOGS] [l]
	LEFT JOIN [db_scales].[Hosts] [h]
		ON [h].[id] = [l].[host_id]
	LEFT JOIN [db_scales].[Scales] [s]
		ON [s].[HostId] = [h].[id]
	LEFT JOIN [db_scales].[APPS] [a]
		ON [a].[UID] = [l].[APP_UID]
	LEFT JOIN [db_scales].[LOG_TYPES] [lt]
		ON [lt].[UID] = [l].[LOG_TYPE_UID]
	WHERE [CREATE_DT] > @create_dt and [HOST_ID] = (case when @host_id > 0 then @host_id end)
	ORDER BY [l].[CREATE_DT] DESC
END
ELSE
BEGIN
	PRINT N'[ ] Disabled select mode'
END
----------------------------------------------------------------------------------------------------
SET @rows = (SELECT
		[p].[rows]
	FROM sys.tables [t]
	INNER JOIN sys.indexes [i]
		ON [t].[object_id] = [i].[object_id]
	INNER JOIN sys.partitions [p]
		ON [i].[object_id] = [p].[object_id]
		AND [i].[index_id] = [p].[index_id]
	WHERE [t].[name] = 'LOGS')
PRINT N'[v] All rows count after run script: ' + CAST(@rows AS NVARCHAR(255))
----------------------------------------------------------------------------------------------------
SET NOCOUNT OFF
