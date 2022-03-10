------------------------------------------------------------------------------------------------------------------------
--Update-2022-03-05
------------------------------------------------------------------------------------------------------------------------
-- SQLSRSP01\LEEDS OR CREATIO\INS1
DECLARE @PACKAGE_ID UNIQUEIDENTIFIER = '{E4030FC4-7097-458C-8C8C-CCE4FCAE813B}' -- DimContragents.dtsx
DECLARE @LAST_ID BIGINT
--SET @LAST_ID = (SELECT [LastID] FROM [ETL].[ObjectStatuses] WHERE PackageID = @PACKAGE_ID AND [InformationSystemID] = 1)
SET @LAST_ID = -9223372036633886334
DECLARE @REF_ID BIGINT = 9223372036854775807 -- пароль на запуск
DECLARE @SELECT_FNGETCONTRAGENTS BIT = 0
DECLARE @SELECT_DIM_CONTRAGENTS BIT = 1
DECLARE @TEST_INSERT BIT = 0
DECLARE @UPDATE_DIM_CONTRAGENTS BIT = 0
------------------------------------------------------------------------------------------------------------------------
-- SQLDBEP01\YORK
IF (@SELECT_FNGETCONTRAGENTS = 1) BEGIN
	--USE [meat_vs_prod]
	SELECT 
		 CAST([ID] AS VARBINARY(36)) [MEAT_CODE_IN_IS]
		,[Marked] [MEAT_MARKED]
		,[Code] [MEAT_CODE]
		,[Description] [MEAT_DESCRIPTION]
		,[FullName] [MEAT_FULL_NAME]
		,[IsBuyer] [MEAT_IS_BUYER]
		,[IsSupplier] [MEAT_IS_SUPPLIER]
		,[GLN] [MEAT_GLN]
		,[GUID_Mercury] [MEAT_GUID_MERCURY]
		,[INN] [MEAT_INN]
		,[KPP] [MEAT_KPP]
		,[Comment] [MEAT_COMMENT]
		,[Parents] [MEAT_PARENTS]
		,CAST([OKPO] AS NVARCHAR(10)) [MEAT_OKPO_LEN10]
		,CAST([ContragentType] AS NVARCHAR(10)) [MEAT_CONTRAGENT_TYPE_LEN10]
		,[VerID] [MEAT_VER_ID]
		,[ManagerID] [MEAT_MANAGER_ID]
		,[ContactInfo] [MEAT_CONTACT_INFO]
		,CAST([ContactInfo] AS XML) [MEAT_CONTACT_INFO_XML]
		,[NumberDebtDays] [MEAT_NUMBER_DEBT_DAYS]
		,[AmountDue] [MEAT_AMOUNT_DUE]
		,[DaysDeferment] [MEAT_DAYS_DEFERMENT]
		,[CommercialNetworkName] [MEAT_COMMERCIAL_NETWORK_NAME]
		,[CommercialNetworkID] [MEAT_COMMERCIAL_NETWORK_ID]
		,0 [MEAT_RELEVANCE_STATUS] -- Unknown
		,0 [MEAT_NORMALIZATION_STATUS] -- NotNormilized
		,1 [MEAT_INFORMATION_SYSTEM_ID]
		,GETDATE() [MEAT_CREATE_DT]
		,GETDATE() [MEAT_CHANGE_DT]
	FROM [Source1C].[fnGetContragents](@REF_ID) [FN_GET_CONTRAGENTS]
	WHERE [FN_GET_CONTRAGENTS].[Code] = 'ЦБД001627'
	--WHERE [FN_GET_CONTRAGENTS].[ContragentType] <> CAST([FN_GET_CONTRAGENTS].[ContragentType] AS NVARCHAR(10))
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
