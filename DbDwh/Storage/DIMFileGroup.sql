/*
Do not change the database path or name variables.
Any sqlcmd variables will be properly substituted during 
build and deployment.
*/
ALTER DATABASE [$(DatabaseName)]
	ADD FILEGROUP [DIMFileGroup];
GO

ALTER DATABASE [$(DatabaseName)]
	ADD FILE
	(
		NAME = [DIMStorage],
		FILENAME = '$(DefaultDataPath)$(DefaultFilePrefix)_DIMStorage.ndf',
		SIZE = 10MB,  
		FILEGROWTH = 5% 
	)
TO FILEGROUP [DIMFileGroup]; 

GO