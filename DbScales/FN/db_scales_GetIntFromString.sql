-- [db_scales].[GetIntFromString]

-- DROP FUNCTION
DROP FUNCTION IF EXISTS [db_scales].[GetIntFromString]
GO

-- CREATE FUNCTION
CREATE FUNCTION [db_scales].[GetIntFromString] (@str nvarchar(1024), @row smallint)
RETURNS smallint
AS
BEGIN
	declare @result smallint = 0
	set @result = (select [value] from (
	select row_number() over (order by (select null)) [row], [value]
	from string_split((select substring(@str, patindex('%[0-9]%', @str), 16)), ' ')) Q
	where [row] = @row)
	return @result
END
GO

-- ACCESS
--GRANT EXECUTE ON [IIS].[GetIntFromString] to [db_scales_users]
GO

-- CHECK FUNCTION
declare @str nvarchar(255) = N'Коробка №4 360 грамм (380*230*170)'
select [db_scales].[GetIntFromString](@str, 2) [GetIntFromString]
