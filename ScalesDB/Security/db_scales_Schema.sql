CREATE USER [scales_owner]
	WITHOUT LOGIN
	WITH DEFAULT_SCHEMA = db_scales
GO


CREATE SCHEMA [db_scales] AUTHORIZATION [scales_owner];
GO 

CREATE SCHEMA [db_sscc] AUTHORIZATION [scales_owner];
GO 