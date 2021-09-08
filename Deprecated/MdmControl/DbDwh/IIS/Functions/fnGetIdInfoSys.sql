-- [IIS].[fnGetIdInfoSys]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [IIS].[fnGetIdInfoSys]
GO

-- CREATE FUNCTION
CREATE FUNCTION [IIS].[fnGetIdInfoSys](@INFO_SYS NVARCHAR(255))
RETURNS INT
AS
BEGIN
	DECLARE @ID INT -- код информационной системы
	SET @ID = (SELECT [IS].[InformationSystemID] FROM [ETL].[InformationSystems] [IS] WHERE [IS].[Name] = @INFO_SYS)
	RETURN @ID 
END
GO

-- ACCESS
GRANT EXECUTE ON [IIS].[fnGetIdInfoSys] TO [TerraSoftRole]
GO

-- CHECK FUNCTION
DECLARE @INFO_SYS NVARCHAR(255) = N'1С:Мясокомбинат' -- информационная система
SELECT [IIS].[fnGetIdInfoSys](@INFO_SYS) [fnGetIdInfoSys]
