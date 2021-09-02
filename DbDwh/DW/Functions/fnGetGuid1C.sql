CREATE FUNCTION [DW].[fnGetGuid1C] (@id binary(16)) 
returns char(36) 
    as 
begin 
	declare @unidentifier char(36) 
	declare @charvalue char(36) 
	select @unidentifier = CONVERT(char(36),CAST(@id as uniqueidentifier)) 
		select @charvalue = 
		right(@unidentifier, 8) 
		+ substring(@unidentifier,24,5) 
		+ substring(@unidentifier,19,5) 
		+ '-' 
		+ substring(@unidentifier,7,2) 
		+ substring(@unidentifier,5,2) 
		+ '-' 
		+ substring(@unidentifier,3,2) 
		+ left(@unidentifier,2) 
		+ substring(@unidentifier,12,2) 
		+ substring(@unidentifier,10,2) 
		+ substring(@unidentifier,17,2) 
		+ substring(@unidentifier,15,2) 
	
		return ( @charvalue ) 
end; 

GO

GRANT EXECUTE ON [DW].[fnGetGuid1C] TO [RenterRole]; 
GO