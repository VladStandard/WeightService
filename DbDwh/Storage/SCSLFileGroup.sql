/*
Do not change the database path or name variables.
Any sqlcmd variables will be properly substituted during 
build and deployment.
*/

ALTER DATABASE [$(DatabaseName)]
	ADD FILEGROUP [SCSLFileGroup];
GO

ALTER DATABASE [$(DatabaseName)]
	ADD FILE
	(
		NAME = [SCSLStorage],
		FILENAME = '$(DefaultDataPath)$(DefaultFilePrefix)_SCSLStorage.ndf',
		SIZE = 100MB,  
		FILEGROWTH = 5% 
	)
TO FILEGROUP [SCSLFileGroup]; 

GO