CREATE ROLE [scsl_datawriter]
GO
GRANT CONNECT TO [scsl_datawriter]
GO

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE ON [SCSL].[DimDistrAgents] TO [scsl_datawriter]
GO
GRANT EXECUTE ON [SCSL].[spFillDimDistrAgents] TO [scsl_datawriter]; 
GO

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE ON [SCSL].[DimDistrContragents] TO [scsl_datawriter]
GO
GRANT EXECUTE ON [SCSL].[spFillDimDistrContragents] TO [scsl_datawriter]; 
GO

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE ON [SCSL].[DimDistrNomenclatures] TO [scsl_datawriter]
GO
GRANT EXECUTE ON [SCSL].[spFillDimDistrNomenclatures] TO [scsl_datawriter]; 
GO

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE ON [SCSL].[DimDistrTradePoints] TO [scsl_datawriter]
GO
GRANT EXECUTE ON [SCSL].[spFillDimDistrTradePoints] TO [scsl_datawriter]; 
GO

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE ON [SCSL].[DimNormAgents] TO [scsl_datawriter]
GO
GRANT EXECUTE ON [SCSL].[spFillDimNormAgents] TO [scsl_datawriter]; 
GO

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE ON [SCSL].[DimNormContragents] TO [scsl_datawriter]
GO
GRANT EXECUTE ON [SCSL].[spFillDimNormContragents] TO [scsl_datawriter]; 
GO

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE ON [SCSL].[DimNormNomenclatures] TO [scsl_datawriter]
GO
GRANT EXECUTE ON [SCSL].[spFillDimNormNomenclatures] TO [scsl_datawriter]; 
GO

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE ON [SCSL].[DimNormTradePoints] TO [scsl_datawriter]
GO
GRANT EXECUTE ON [SCSL].[spFillDimNormTradePoints] TO [scsl_datawriter]; 
GO

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE ON [SCSL].[DimSubdivisions] TO [scsl_datawriter]
GO
GRANT EXECUTE ON [SCSL].[spFillDimSubdivisions] TO [scsl_datawriter]; 
GO

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE ON [SCSL].[FactSales] TO [scsl_datawriter]
GO
GRANT EXECUTE ON [SCSL].[spFillFactSales] TO [scsl_datawriter]; 
GO

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE ON [SCSL].[FactReturns] TO [scsl_datawriter]
GO
GRANT EXECUTE ON [SCSL].[spFillFactReturns] TO [scsl_datawriter]; 
GO

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE ON [SCSL].[FactOrders] TO [scsl_datawriter]
GO
GRANT EXECUTE ON [SCSL].[spFillFactOrders] TO [scsl_datawriter]; 
GO


