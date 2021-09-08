Create function [DW].[fnCalcINN] (@inn varchar(12)) 
returns varchar(12) as 
begin 
declare @c table(pos int, m10 int, m11 int, m12 int)
insert @c
select 1, 2, 7, 3
union select 2,	4 ,2, 7
union select 3, 10, 4, 2
union select 4, 3, 10, 4
union select 5, 5, 3, 10
union select 6, 9, 5, 3
union select 7, 4, 9, 5
union select 8, 6, 4, 9
union select 9, 8, 6, 4
union select 10, 0, 8, 6
union select 11, 0, 0, 8


declare @s10  int, @s11 int, @s12 int

select 
  @s10 = sum(convert(int,substring(@inn+'00', pos,1))*m10),
  @s11 = sum(convert(int,substring(@inn+'00', pos,1))*m11),
  @s12 = sum(convert(int,substring(@inn+'00', pos,1))*m12)
  from @c

declare @res varchar(12)

select @s10=(@s10%11)%10, @s11=(@s11%11)%10, @s12=(@s12%11)%10
select @res=
  case datalength(@inn) 
    when 10 then left(@inn, 9)+convert(char, @s10)
    when 12 then left(@inn, 10)+convert(varchar, @s11)+convert(varchar, @s12)
    else 'length error'
  end

return @res 
end
GO

GRANT EXECUTE ON [DW].[fnDateTimeToInt] TO [RenterRole]; 
GO