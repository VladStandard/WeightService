------------------------------------------------------------------------------------------------------------------------
--Update-2022-03-11 - NomenclatureGroups
------------------------------------------------------------------------------------------------------------------------
-- SQLSRSP01\LEEDS OR CREATIO\INS1
DECLARE @LAST_ID BIGINT
SET @LAST_ID = -9223372036633886334
DECLARE @SELECT_FN_GET_NOMENCLATURE_GROUPS BIT = 1
DECLARE @SELECT_DIM_CONTRAGENTS BIT = 0
DECLARE @TEST_INSERT BIT = 0
DECLARE @UPDATE_DIM_CONTRAGENTS BIT = 0
------------------------------------------------------------------------------------------------------------------------
-- SQLDBEP01\YORK + [meat_vs_prod]
IF (@SELECT_FN_GET_NOMENCLATURE_GROUPS = 1) BEGIN
SELECT 
	 CAST([ID] AS VARBINARY(16)) [MEAT_CODE_IN_IS]
	,[Code] [MEAT_CODE]
	,[Description] [MEAT_DESCRIPTION]
	,[VerID] [MEAT_VER_ID]
	,1 [MEAT_STATUS_ID]
	,1 [MEAT_INFORMATION_SYSTEM_ID]
	,GETDATE() [MEAT_CREATE_DT]
	,GETDATE() [MEAT_CHANGE_DT]
FROM [Source1C].[fnGetNomenclatureGroups](9223372036854775807) [FN_GET_NOMENCLATURE_GROUPS]
ORDER BY [MEAT_DESCRIPTION]
END
------------------------------------------------------------------------------------------------------------------------
-- CREATIO\INS1 + VSDWH
IF (@TEST_INSERT = 1) BEGIN
	DECLARE @CODE NVARCHAR(15) = N'ЦБД001627'
	DECLARE @CODE_IN_IS VARBINARY(36) = 0x80BB001E6722B44911E5EB78F4D2AB64
	IF NOT EXISTS (SELECT 1 FROM [DW].[DimContragents] WHERE [CodeInIS] = @CODE_IN_IS AND [InformationSystemID] = 1 AND [Code] = @CODE) BEGIN
		INSERT INTO [DW].[DimContragents] ([Marked],[Code],[Name],[FullName],[IsBuyer],[IsSupplier],[GLN],[GUID_Mercury],[INN],[KPP],[Comment],[Parents]
			,[OKPO],[ContragentType],[ContactInfo],[ManagerID],[ConsolidatedClientID],[NumberDebtDays],[AmountDue],[DaysDeferment],[CommercialNetworkID]
			,[CommercialNetworkName],[CreateDate],[DLM],[StatusID],[InformationSystemID],[CodeInIS],[RelevanceStatus],[NormalizationStatus],[MasterId])
		VALUES (
		--0x93FA001E6722B44911E29B65FA184EB8
		0,@CODE,'Фомальгаут ПТП ООО','ООО "ПТП Фомальгаут"',1,1,'','','5022040430','503301001','отсрочка 10 кл. дней','{"parents":["Покупатели","Фомальгаут ПТП ООО"]}'
			,'','ЮрЛицо',NULL,0,NULL,0,0.000,NULL,NULL
			,NULL,GETDATE(),GETDATE(),1,1,@CODE_IN_IS,NULL,NULL,NULL)
	END
	SELECT * FROM [DW].[DimContragents] WHERE [CodeInIS] = @CODE_IN_IS --AND [InformationSystemID] = 1 --AND [Code] = @CODE
	IF EXISTS (SELECT 1 FROM [DW].[DimContragents] WHERE [CodeInIS] = @CODE_IN_IS AND [InformationSystemID] = 1 AND [Code] = @CODE) BEGIN
		DELETE FROM [DW].[DimContragents] WHERE [CodeInIS] = @CODE_IN_IS AND [InformationSystemID] = 1 AND [Code] = @CODE
	END
END
------------------------------------------------------------------------------------------------------------------------
IF (@SELECT_DIM_CONTRAGENTS = 1) BEGIN
	SELECT [C].[CreateDate], [C].[DLM], *
	FROM [DW].[DimContragents] [C]
	ORDER BY [C].[DLM] DESC
END
------------------------------------------------------------------------------------------------------------------------
IF (@UPDATE_DIM_CONTRAGENTS = 1) BEGIN
	--UPDATE [DW].[DimContragents] SET [FullName] = 'Test' WHERE [CodeInIS] =0x93F6001E6722B44911E26C4E140B6989
	SELECT [C].*
	FROM [DW].[DimContragents] [C]
	WHERE [C].[CodeInIS] =0x93F6001E6722B44911E26C4E140B6989
END
------------------------------------------------------------------------------------------------------------------------
