-- [db_scales].[GetCountDays]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [db_scales].[GetCountDays]
GO

-- CREATE FUNCTION
CREATE FUNCTION [db_scales].[GetCountDays] (@str nvarchar(1024), @method smallint = 1)
RETURNS smallint
AS
BEGIN
	declare @result smallint = 0
	if (@method = 1) begin
		set @result = substring(substring(@str, 
		patindex('%[0-9]%', @str), len(@str)), 0, patindex('%[^0-9]%', 
			substring(@str, patindex('%[0-9]%', @str), len(@str))))

	end else if (@method = 2) begin
		set @result = (select [value] from (
		select row_number() over (order by (select null)) [row], [value]
		from string_split((select substring(@str, patindex('%[0-9]%', @str), 16)), ' ')) Q
		where [row] = 1)
	end else if (@method = 3) begin
		set @result = (select substring (@str
			,patindex('%[0-9]%', @str)
			,patindex('% суток%', @str) - patindex('%[0-9]%', @str)
		))
	end
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
