USE [SCALESDB]
-- ��� ���������.
SELECT
	 [DB_SCALES].[SCALES].[ID]                   -- 0
	,[DB_SCALES].[SCALES].[DESCRIPTION]          -- 1
	,[DB_SCALES].[SCALES].[IDRREF]               -- 2
	,[DB_SCALES].[SCALES].[DEVICEIP]             -- 3
	,[DB_SCALES].[SCALES].[DEVICEPORT]           -- 4
	,[DB_SCALES].[SCALES].[DEVICEMAC]            -- 5
	,[DB_SCALES].[SCALES].[DEVICESENDTIMEOUT]    -- 6
	,[DB_SCALES].[SCALES].[DEVICERECEIVETIMEOUT] -- 7
	,[DB_SCALES].[SCALES].[DEVICECOMPORT]        -- 8
	,[DB_SCALES].[SCALES].[ZEBRAIP]              -- 9
	,[DB_SCALES].[SCALES].[ZEBRAPORT]            -- 10
	,[DB_SCALES].[SCALES].[USEORDER]             -- 11
	,[DB_SCALES].[SCALES].[VERSCALESUI]          -- 12
	,[DB_SCALES].[SCALES].[DEVICENUMBER]         -- 13
FROM [DB_SCALES].[SCALES]
ORDER BY [DB_SCALES].[SCALES].[ID]
