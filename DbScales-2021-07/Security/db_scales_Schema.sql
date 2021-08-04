create user [scales_owner]
	without login
	with default_schema = db_scales
go
create schema [db_scales] authorization [scales_owner]
go 
create schema [db_sscc] authorization [scales_owner]
go