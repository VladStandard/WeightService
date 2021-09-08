/*
Do not change the database path or name variables.
Any sqlcmd variables will be properly substituted during 
build and deployment.
*/
ALTER DATABASE [$(DatabaseName)]
	ADD FILEGROUP [FACTFileGrooup]
GO
ALTER DATABASE [$(DatabaseName)]
	ADD FILE
	(
		NAME = [FACTStorage0],
		FILENAME = '$(DefaultDataPath)$(DefaultFilePrefix)_FACTStorage0.ndf',
		SIZE = 1024MB,  
		FILEGROWTH = 5% 
	),
	(
		NAME = [FACTStorage1],
		FILENAME = '$(DefaultDataPath)$(DefaultFilePrefix)_FACTStorage1.ndf',
		SIZE = 1024MB,  
		FILEGROWTH = 5% 
	),
	(
		NAME = [FACTStorage2],
		FILENAME = '$(DefaultDataPath)$(DefaultFilePrefix)_FACTStorage2.ndf',
		SIZE = 1024MB,  
		FILEGROWTH = 5% 
	)
TO FILEGROUP [FACTFileGrooup]
	
