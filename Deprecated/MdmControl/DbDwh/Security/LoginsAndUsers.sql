CREATE LOGIN [RenterLogin] 
WITH 
	PASSWORD = 'Pa$$w0rd',
	DEFAULT_DATABASE = [$(DatabaseName)] ,
	CHECK_EXPIRATION=OFF, 
	CHECK_POLICY=OFF
GO


CREATE USER [RenterUser] FOR LOGIN [RenterLogin] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [OLAPReaderRole] ADD MEMBER [RenterUser]
GO
ALTER ROLE [RenterRole] ADD MEMBER [RenterUser]
GO
ALTER ROLE [TerraSoftRole] ADD MEMBER [RenterUser]
GO
ALTER ROLE [scsl_datawriter] ADD MEMBER [RenterUser]
GO

-----------------------------------------------------------------------------
CREATE LOGIN [KOLBASA-VS\#GG-10001-RO] 
	FROM WINDOWS WITH DEFAULT_DATABASE=[$(DatabaseName)]
GO

CREATE USER [KOLBASA-VS\#GG-10001-RO] 
	FOR LOGIN [KOLBASA-VS\#GG-10001-RO] 
	WITH DEFAULT_SCHEMA=[DW]
GO

--ALTER ROLE [OLAPReaderRole] ADD MEMBER [KOLBASA-VS\#GG-10001-RO];
EXEC sp_addrolemember N'TerraSoftRole', N'KOLBASA-VS\#GG-10001-RO';
GO
EXEC sp_addrolemember N'db_datareader', N'KOLBASA-VS\#GG-10001-RO';
GO
EXEC sp_addrolemember N'OLAPReaderRole', N'KOLBASA-VS\#GG-10001-RO';
GO
EXEC sp_addrolemember N'scsl_datareader', N'KOLBASA-VS\#GG-10001-RO';
GO


-----------------------------------------------------------------------------
--CREATE LOGIN [$(iisuser)] 
--	FROM WINDOWS WITH DEFAULT_DATABASE=[$(DatabaseName)]
--GO

--CREATE USER [$(iisuser)] 
--	FOR LOGIN [$(iisuser)] 
--	WITH DEFAULT_SCHEMA=[IIS]
--GO

--ALTER ROLE [TerraSoftRole] ADD MEMBER [$(iisuser)]
--GO
