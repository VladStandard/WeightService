CREATE ROLE [MDMReader];
GO
GRANT CONNECT TO [MDMReader];
GO

GRANT SELECT ON [DW].[DimNomenclatures] TO [MDMReader];
GO

GRANT SELECT ON [DW].[DimContragents] TO [MDMReader];
GO
