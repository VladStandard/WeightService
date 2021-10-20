-- [db_scales].[GetCountDays]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [db_scales].[GetCountDays]
GO

-- CREATE FUNCTION
CREATE FUNCTION [db_scales].[GetCountDays]
(
	@str nvarchar(1024),
	@method tinyint = 1
)
RETURNS tinyint
AS
BEGIN
	declare @result tinyint = 0

	return @result
END
GO

-- ACCESS
--GRANT EXECUTE ON [IIS].[GetCountDays] to [db_scales_users]
GO

-- CHECK FUNCTION
declare @str nvarchar(255) = N'Срок годности: 30 суток при температуре от 0°С до +6°С и относительной влажности воздуха 75%-78%. Упаковано под вакуумом.'
select [db_scales].[GetCountDays](@str, 1) [GetCountDays_1]
select [db_scales].[GetCountDays](@str, 2) [GetCountDays_2]
select [db_scales].[GetCountDays](@str, 3) [GetCountDays_3]
