create function [DW].[fnCheckINN]
(
	@INN	varchar(12)		-- проверяемое ИНН (10 или 12 символов)
)
returns	tinyint
as
begin
declare	@S_CHECK_ARR	varchar(64)

declare	@ret	tinyint,
	@len	tinyint,
	@i	tinyint,
	@n	int,
	@ctrld	int

	-- Begin AAG, 22.02.2005
	set	@len	= datalength(@INN)
	if @len < 10
	begin
		set	@ret	= 0
		return	@ret
	end
	-- End AAG, 22.02.2005


	set	@S_CHECK_ARR	= '03,07,02,04,10,03,05,09,04,06,08,00,00'	

	set	@i	= 1
	set	@n	= case when @len < 11 then 7 else 4 end
	set	@ctrld	= 0
	
	while	@i < @len
	begin
		set	@ctrld	= @ctrld + convert(int, substring(@INN, @i, 1)) * convert(int, substring(@S_CHECK_ARR, @n, 2))
		set	@i	= @i + 1
		set	@n	= @n + 3
	end
	set	@ret	= case when ((@ctrld % 11) % 10) = convert(int, substring(@INN, case when @len < 11 then 10 else 11 end, 1)) then 1 else 0 end
	
	
	if @len > 11 and @ret = 1
	begin
		set	@i	= 1
		set	@n	= 1
		set	@ctrld	= 0

		while	@i < @len
		begin
			set	@ctrld	= @ctrld + convert(int, substring(@INN, @i, 1)) * convert(int, substring(@S_CHECK_ARR, @n, 2))
			set	@i	= @i + 1
			set	@n	= @n + 3
		end
		set	@ret	= case when ((@ctrld % 11) % 10) = convert(int, substring(@INN, @i, 11)) then 1 else 0 end
	end

	return	@ret
end
GO

