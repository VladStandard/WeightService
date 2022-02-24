------------------------------------------------------------------------------------------------------------------------
-- Table Select WeithingFact 1
------------------------------------------------------------------------------------------------------------------------
SELECT
	[ID]
   ,[PLUID]
   ,[SCALEID]
   ,[SERIESID]
   ,[ORDERID]
   ,[SSCC]
   ,[WEITHINGDATE]
   ,[NETWEIGHT]
   ,[TAREWEIGHT]
   ,[UUID]
   ,[PRODUCTDATE]
   ,[REGNUM]
   ,[KNEADING]
FROM [DB_SCALES].[WeithingFact]
--WHERE Kneading<>1
ORDER BY [WEITHINGDATE] DESC
------------------------------------------------------------------------------------------------------------------------
