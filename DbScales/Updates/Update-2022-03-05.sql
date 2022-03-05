------------------------------------------------------------------------------------------------------------------------
--Update-2022-03-05
------------------------------------------------------------------------------------------------------------------------
-- SQLSRSP01\LEEDS OR CREATIO\INS1
DECLARE @PACKAGE_ID UNIQUEIDENTIFIER = '{E4030FC4-7097-458C-8C8C-CCE4FCAE813B}' -- DimContragents.dtsx
DECLARE @LAST_ID BIGINT
--SET @LAST_ID = (SELECT [LastID] FROM [ETL].[ObjectStatuses] WHERE PackageID = @PACKAGE_ID AND [InformationSystemID] = 1)
SET @LAST_ID = -9223372036633886334
DECLARE @REF_ID BIGINT = 9223372036854775807 -- пароль на запуск
------------------------------------------------------------------------------------------------------------------------
-- SQLDBEP01\YORK
SELECT *
	-- [ID] [MEAT_ID]
	--,CAST([ID] AS VARBINARY(36)) [MEAT_CODE_IN_IS]
	--,UPPER(substring(substring(sys.fn_sqlvarbasetostr([ID]),3,32),25,8) + '-' + 
	--  substring(substring(sys.fn_sqlvarbasetostr([ID]),3,32),21,4) + '-' + 
	--  substring(substring(sys.fn_sqlvarbasetostr([ID]),3,32),17,4) + '-' + 
	--  substring(substring(sys.fn_sqlvarbasetostr([ID]),3,32),1,4) + '-' + 
	--  substring(substring(sys.fn_sqlvarbasetostr([ID]),3,32),5,12)) [MEAT_CAST_ID]
	--,[Marked] [MEAT_MARKED]
	--,[Code] [MEAT_CODE]
	--,[Description] [MEAT_DESCRIPTION]
	--,[FullName] [MEAT_FULL_NAME]
	--,[IsBuyer] [MEAT_IS_BUYER]
	--,[IsSupplier] [MEAT_IS_SUPPLIER]
	--,[GLN] [MEAT_GLN]
	--,[GUID_Mercury] [MEAT_GUID_MERCURY]
	--,[INN] [MEAT_INN]
	--,[KPP] [MEAT_KPP]
	--,[Comment] [MEAT_COMMENT]
	--,[Parents] [MEAT_PARENTS]
	--,CAST([OKPO] AS NVARCHAR(10)) [MEAT_OKPO]
	--,CAST([ContragentType] AS NVARCHAR(10)) [MEAT_CONTRAGENT_TYPE]
	--,[VerID] [MEAT_VER_ID]
	--,[ManagerID] [MEAT_MANAGER_ID]
	--,CAST([ContactInfo] AS XML) [MEAT_XML]
	--,[NumberDebtDays] [MEAT_NUMBER_DEBT_DAYS]
	--,[AmountDue] [MEAT_AMOUNT_DUE]
	--,[DaysDeferment] [MEAT_DAYS_DEFERMENT]
	--,[CommercialNetworkName] [MEAT_COMMERCIAL_NETWORK_NAME]
	--,[CommercialNetworkID] [MEAT_COMMERCIAL_NETWORK_ID]
FROM [Source1C].[fnGetContragents](@REF_ID) [FN_GET_CONTRAGENTS]
WHERE [FN_GET_CONTRAGENTS].[Code] = 'ЦБД001627'
--WHERE [FN_GET_CONTRAGENTS].[ContragentType] <> CAST([FN_GET_CONTRAGENTS].[ContragentType] AS NVARCHAR(10))
------------------------------------------------------------------------------------------------------------------------
