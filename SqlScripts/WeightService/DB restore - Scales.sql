-- DB restore - Scales 
USE [master]
RESTORE DATABASE [SCALES] FROM  DISK = N'E:\Microsoft SQL Server\MSSQL14.INS1\MSSQL\Backup\SCALES.BAK' WITH  FILE = 1, 
MOVE N'ScalesDB' TO N'E:\Microsoft SQL Server\MSSQL14.INS1\MSSQL\DATA\SCALES.mdf',  
MOVE N'ScalesFileGroup_14836DBD' TO N'E:\Microsoft SQL Server\MSSQL14.INS1\MSSQL\DATA\SCALES_ScalesFileGroup.mdf',  
MOVE N'ScalesFileGroupLargeData_355FE721' TO N'E:\Microsoft SQL Server\MSSQL14.INS1\MSSQL\DATA\SCALES_ScalesFileGroupLargeData.mdf',  
MOVE N'ScalesFileGroupJJ_42A4152A' TO N'E:\Microsoft SQL Server\MSSQL14.INS1\MSSQL\DATA\SCALES_ScalesFileGroupJJ.mdf',  
MOVE N'ScalesDB_log' TO N'E:\Microsoft SQL Server\MSSQL14.INS1\MSSQL\DATA\SCALES.ldf',  
NOUNLOAD,  REPLACE,  STATS = 5
