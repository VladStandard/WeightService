/*
Do not change the database path or name variables.
Any sqlcmd variables will be properly substituted during 
build and deployment.
*/
ALTER DATABASE [$(DatabaseName)]
	ADD FILEGROUP [ETLFileGroup]

GO

ALTER DATABASE [$(DatabaseName)]
	ADD FILE
	(
		NAME = [ETLStorege],
		FILENAME = '$(DefaultDataPath)$(DefaultFilePrefix)_ETLStorege.ndf',
		SIZE = 10MB,  
		FILEGROWTH = 5% 
	)
TO FILEGROUP [ETLFileGroup]