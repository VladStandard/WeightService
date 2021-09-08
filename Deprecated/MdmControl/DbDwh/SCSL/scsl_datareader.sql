CREATE ROLE [scsl_datareader]
GO
GRANT CONNECT TO [scsl_datareader]
GO

GRANT SELECT ON [SCSL].[DimDistrAgents] TO [scsl_datareader] ;
GO

GRANT SELECT ON [SCSL].[DimDistrContragents] TO [scsl_datareader];
GO

GRANT SELECT ON [SCSL].[DimDistrNomenclatures] TO [scsl_datareader];
GO

GRANT SELECT ON [SCSL].[DimDistrTradePoints] TO [scsl_datareader];
GO

GRANT SELECT ON [SCSL].[DimNormAgents] TO [scsl_datareader];
GO

GRANT SELECT ON [SCSL].[DimNormContragents]TO [scsl_datareader];
GO

GRANT SELECT ON [SCSL].[DimNormNomenclatures]TO [scsl_datareader];
GO

GRANT SELECT ON [SCSL].[DimNormTradePoints] TO [scsl_datareader];
GO

GRANT SELECT ON [SCSL].[DimSubdivisions] TO [scsl_datareader];
GO

GRANT SELECT ON [SCSL].[FactSales] TO [scsl_datareader];
GO 

GRANT SELECT ON [SCSL].[FactReturns] TO [scsl_datareader];
GO

GRANT SELECT ON [SCSL].[FactOrders] TO [scsl_datareader];
GO