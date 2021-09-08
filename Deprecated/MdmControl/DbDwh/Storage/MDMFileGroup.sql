ALTER DATABASE [$(DatabaseName)]
	ADD FILEGROUP [MDMFileGroup];
GO

ALTER DATABASE [$(DatabaseName)]
	ADD FILE
	(
		NAME = [MDMStorage],
		FILENAME = '$(DefaultDataPath)$(DefaultFilePrefix)_MDMStorage.ndf',
		SIZE = 10MB,  
		FILEGROWTH = 5% 
	)
TO FILEGROUP [MDMFileGroup]; 

GO