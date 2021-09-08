CREATE FUNCTION [DW].[fnGetBinary1C] (@GUID1С varchar(36)) 
returns binary(16)
as 
begin 
	declare @GUIDasStr char(36)
	select @GUIDasStr ='0x'+SUBSTRING(@GUID1С,20,4)+SUBSTRING(@GUID1С,25,13)+SUBSTRING(@GUID1С,15,4)+SUBSTRING(@GUID1С,10,4)+SUBSTRING(@GUID1С,1,8)
	return ( CONVERT(binary(16),@GUIDasStr,1) ) 
end; 
GO

GRANT EXECUTE ON [DW].[fnGetBinary1C] TO [RenterRole]; 
GO