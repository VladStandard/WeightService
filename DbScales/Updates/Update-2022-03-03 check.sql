---- Update-2022-03-03 check
--UPDATE [db_scales].[CONTRAGENTS_V2] SET [NAME]='Тест 1', [FULL_NAME]='Тест 2' WHERE [DWH_ID]=-2147482505
--UPDATE [db_scales].[Nomenclature] SET [NAME]='Тест 1' WHERE [Id]=-2147481876
-- Run SSIS
SELECT * FROM [db_scales].[CONTRAGENTS_V2] [C] WHERE [C].[DWH_ID]=-2147482505
SELECT * FROM [db_scales].[Nomenclature] [n] WHERE [n].[Id]=-2147481876
