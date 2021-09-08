CREATE ROLE [MDMWriter];
GO
GRANT CONNECT TO [MDMWriter];
GO

GRANT EXECUTE ON [MDM].[NomenclatureMasterRowMake] TO [MDMWriter]; 
GO
GRANT EXECUTE ON [MDM].[NomenclatureUnderRowInclude] TO [MDMWriter]; 
GO
GRANT EXECUTE ON [MDM].[NomenclatureSetNotRelevance] TO [MDMWriter]; 
GO
GRANT EXECUTE ON [MDM].[NomenclatureMasterRowRemove] TO [MDMWriter]; 
GO

GRANT EXECUTE ON [MDM].[ContragentMasterRowMake] TO [MDMWriter]; 
GO
GRANT EXECUTE ON [MDM].[ContragentUnderRowInclude] TO [MDMWriter]; 
GO
GRANT EXECUTE ON [MDM].[ContragentSetNotRelevance] TO [MDMWriter]; 
GO
GRANT EXECUTE ON [MDM].[ContragentMasterRowRemove] TO [MDMWriter]; 
GO


GRANT SELECT,UPDATE ON [DW].[DimNomenclatures] TO [MDMWriter];
GO

GRANT SELECT,UPDATE ON [DW].[DimContragents] TO [MDMWriter];
GO
