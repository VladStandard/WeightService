------------------------------------------------------------------------------------------------------------------------
-- Table Select ZebraPrinter
------------------------------------------------------------------------------------------------------------------------
SELECT
	[PRINT].[ID]
   ,[PRINT].[CREATEDATE]
   ,[PRINT].[MODIFIEDDATE]
   ,[PRINT].[NAME]
   ,[PRINT].[IP]
   ,[PRINT].[PORT]
   ,[PRINT].[PASSWORD]
   ,[PRINT].[PRINTERTYPEID]
   ,[PRINT].[MAC]
   ,[PRINT].[PEELOFFSET]
   ,[PRINT].[DARKNESSLEVEL]
   ,[TYPE].[NAME] [TYPE_NAME]
FROM [DB_SCALES].[ZEBRAPRINTER] [PRINT]
LEFT JOIN [DB_SCALES].[ZEBRAPRINTERTYPE] [TYPE] ON [PRINT].[PRINTERTYPEID] = [TYPE].[ID]
------------------------------------------------------------------------------------------------------------------------
