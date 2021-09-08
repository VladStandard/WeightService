:setvar dbname "AgentP" 
:setvar ChangeRetention "5 DAYS"
------------------------------------------------------------------------------------

USE $(dbname);

IF NOT EXISTS (SELECT 1 FROM sys.schemas WHERE name = 'Source1C')
BEGIN
	EXEC( 'CREATE SCHEMA [Source1C]');
END
GO

------------------------------------------------------------------------------------

	USE [master];
	IF NOT Exists (select loginname from master.dbo.syslogins where name = 'SsisUserR')
	BEGIN
		CREATE LOGIN [SsisUserR] 
		WITH PASSWORD = 'Pa$$w0rd',DEFAULT_DATABASE = $(dbname),CHECK_EXPIRATION=OFF,CHECK_POLICY=OFF;
	END

	USE $(dbname);
	IF EXISTS (SELECT 1 FROM sys.database_principals WHERE name = N'SsisUserR') DROP USER [SsisUserR];
	CREATE USER [SsisUserR] FOR LOGIN [SsisUserR];
	ALTER USER [SsisUserR] WITH DEFAULT_SCHEMA=[Source1C];
	ALTER ROLE [db_datareader] ADD MEMBER [SsisUserR];

------------------------------------------------------------------------------------

IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_databases WHERE database_id=DB_ID(N'$(dbname)'))
BEGIN
	ALTER DATABASE $(dbname) SET 
	CHANGE_TRACKING = ON 
	(CHANGE_RETENTION = $(ChangeRetention), AUTO_CLEANUP = ON);
END

------------------------------------------------------------------------------------
-- Справочник торговых точек
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type = 'PK' AND  parent_object_id = OBJECT_ID ('_Reference76'))
BEGIN
	ALTER TABLE [dbo].[_Reference76] ADD CONSTRAINT [PK_Reference76_IDRRef] PRIMARY KEY ([_IDRRef]);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference76]'))
BEGIN
	ALTER TABLE [dbo].[_Reference76] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = ON);
END
GO

DROP FUNCTION IF EXISTS [Source1C].[fnGetDistrTradePoints];
GO
CREATE FUNCTION [Source1C].[fnGetDistrTradePoints]( @vEndVersionID bigint = 0)
RETURNS TABLE 
AS
RETURN 
	SELECT 
		 [DistrTradePoints].[_IDRRef]		as [DistrTradePointIDinIS]
		,CONVERT(bit,[_Marked])				as [Marked]
		,[_Fld1061RRef]						as [NormTradePointID]
		,[_Code]							as [DistrCodeTradePoint]
		,iif([_Description] is null or len([_Description])<3,'Не указан',[_Description]) as [DistrNameTradePoint]
		,iif([_Fld1068] is null or len([_Fld1068])<3,'Не указан',[_Fld1068]) as [DistrAddressTradePoint]
		,1 as [UseInReport]
		,CHECKSUM(
			[DistrTradePoints].[_IDRRef],
			[_Marked],
			[_Fld1061RRef],
			[_Description],
			[_Fld1068]
		)  as CHECKSUMM
		,CT.SYS_CHANGE_VERSION
		,CT.SYS_CHANGE_CREATION_VERSION
		,CT.SYS_CHANGE_OPERATION
		,CT.SYS_CHANGE_COLUMNS
		,CT.SYS_CHANGE_CONTEXT
	FROM   
	CHANGETABLE(CHANGES [dbo].[_Reference76], @vEndVersionID) AS CT
	LEFT JOIN [dbo].[_Reference76] as [DistrTradePoints]  
	ON CT.[_IDRRef] = [DistrTradePoints].[_IDRRef]
GO
GRANT SELECT ON [Source1C].[fnGetDistrTradePoints] TO [Guest]; 
GO

-----------------------------------------------------------------------------------------------------------
-- Справочник номенклатуры

IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type = 'PK' AND  parent_object_id = OBJECT_ID ('_Reference52'))
BEGIN
	ALTER TABLE [dbo].[_Reference52] ADD CONSTRAINT [PK_Reference52_IDRRef] PRIMARY KEY ([_IDRRef]);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference52]'))
BEGIN
	ALTER TABLE [dbo].[_Reference52] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = ON);
END
GO

DROP FUNCTION IF EXISTS [Source1C].[fnGetDistrNomenclatures];
GO
CREATE FUNCTION [Source1C].[fnGetDistrNomenclatures]( @vEndVersionID bigint = 0)
RETURNS TABLE 
AS
RETURN 
	SELECT 
		[DistrNomenclatures].[_IDRRef] AS [DistrNomenclatureIDinIS],
		CONVERT(bit,[_Marked])		as [Marked],
        [_Fld790RRef] as [NormNomenclatureID],
	    iif([_Code] is null or len([_Code])<3,'Не указан',[_Code]) AS [DistrCodeNomenclature], 
	    iif([_Description] is null or len([_Description])<3,'Не указан',[_Description]) AS [DistrNameNomenclature],
	    iif([_Fld794] is null or len([_Fld794])<3,'Не указан',[_Fld794]) as [DistrArticleNomenclature],
	    1 as [UseInReport],
		[_Fld790RRef] as [NormNomenclatureIdInIS]
		,CHECKSUM(
			[DistrNomenclatures].[_IDRRef],
			[_Marked],
			[_Fld790RRef],
			[_Code],
			[_Description],
			[_Fld794]
		)  as CHECKSUMM

		,CT.SYS_CHANGE_VERSION
		,CT.SYS_CHANGE_CREATION_VERSION
		,CT.SYS_CHANGE_OPERATION
		,CT.SYS_CHANGE_COLUMNS
		,CT.SYS_CHANGE_CONTEXT

	FROM   
	CHANGETABLE(CHANGES [dbo].[_Reference52], @vEndVersionID) AS CT
	LEFT JOIN [dbo].[_Reference52] as [DistrNomenclatures]  
	ON CT.[_IDRRef] = [DistrNomenclatures].[_IDRRef]
GO
GRANT SELECT ON [Source1C].[fnGetDistrNomenclatures] TO [Guest]; 
GO

-----------------------------------------------------------------------------------------------------------
--Справочник агентов

IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type = 'PK' AND  parent_object_id = OBJECT_ID ('_Reference68'))
BEGIN
	ALTER TABLE [dbo].[_Reference68] ADD CONSTRAINT [PK_Reference68_IDRRef] PRIMARY KEY ([_IDRRef]);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference68]'))
BEGIN
	ALTER TABLE [dbo].[_Reference68] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = ON);
END
GO

DROP FUNCTION IF EXISTS [Source1C].[fnGetDistrAgents];
GO
CREATE FUNCTION [Source1C].[fnGetDistrAgents]( @vEndVersionID bigint = 0)
RETURNS TABLE 
AS
RETURN 
	SELECT 
		[DistrAgents].[_IDRRef] as [DistrAgentCodeInIS]
		,CONVERT(bit,[_Marked])		as [Marked]
		,[_Fld925RRef] as [NormAgentCodeInIS]
		,[_Code] as [DistrAgentCode]
		,[_Description] as [DistrAgentName]
		,1 as [UseInReport]

		,CHECKSUM(
			[DistrAgents].[_IDRRef],
			[_Marked],
			[_Fld925RRef],
			[_Code],
			[_Description]
		)  as CHECKSUMM

		,CT.SYS_CHANGE_VERSION
		,CT.SYS_CHANGE_CREATION_VERSION
		,CT.SYS_CHANGE_OPERATION
		,CT.SYS_CHANGE_COLUMNS
		,CT.SYS_CHANGE_CONTEXT
	FROM  CHANGETABLE(CHANGES [dbo].[_Reference68], @vEndVersionID) AS CT
	LEFT JOIN [dbo].[_Reference68] as [DistrAgents]  
	ON CT.[_IDRRef] = [DistrAgents].[_IDRRef]
GO
GRANT SELECT ON [Source1C].[fnGetDistrAgents] TO [Guest]; 
GO

-----------------------------------------------------------------------------------
-- Справочник контрагентов

IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type = 'PK' AND  parent_object_id = OBJECT_ID ('_Reference47'))
BEGIN
	ALTER TABLE [dbo].[_Reference47] ADD CONSTRAINT [PK_Reference47_IDRRef] PRIMARY KEY ([_IDRRef]);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference47]'))
BEGIN
	ALTER TABLE [dbo].[_Reference47] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = ON);
END
GO
DROP FUNCTION IF EXISTS [Source1C].[fnGetDistrContragents];
GO
CREATE FUNCTION [Source1C].[fnGetDistrContragents]( @vEndVersionID bigint = 0)
RETURNS TABLE 
AS
RETURN 
	SELECT 
		[DistrContragents].[_IDRRef] as [DistrContragentIDInIS]
		,CONVERT(bit,[_Marked])		as [Marked]
		,[_Fld730RRef] as [NormContragentIDinIS]
		,[_Code] as [DistrCodeContragent]
		,iif([_Description] is null or len([_Description])<3,'Не указан',[_Description]) as [DistrNameContragent]
		,iif([_Fld732] is null or len([_Fld732])<3,'Не указан',[_Fld732]) as [DistrNameFullContragent]
		,iif([_Fld733] is null or len([_Fld733])<3,'Не указан',[_Fld733]) as [INN]
		,iif([_Fld734] is null or len([_Fld734])<3,'Не указан',[_Fld734]) as [KPP]
		,1 as [UseInReport]
		,CHECKSUM(
			[DistrContragents].[_IDRRef]
			,[_Marked]
			,[_Fld730RRef]
			,[_Code]
			,[_Description]
				,[_Fld732] 
			,[_Fld733] 
			,[_Fld734] 
		)  as CHECKSUMM
		,CT.SYS_CHANGE_VERSION
		,CT.SYS_CHANGE_CREATION_VERSION
		,CT.SYS_CHANGE_OPERATION
		,CT.SYS_CHANGE_COLUMNS
		,CT.SYS_CHANGE_CONTEXT

	FROM  CHANGETABLE(CHANGES [dbo].[_Reference47], @vEndVersionID) AS CT
	LEFT JOIN [dbo].[_Reference47] as [DistrContragents]  
	ON CT.[_IDRRef] = [DistrContragents].[_IDRRef]
GO
GRANT SELECT ON [Source1C].[fnGetDistrContragents] TO [Guest]; 
GO


-----------------------------------------------------------------------------------
-- Справочник подразделений

IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type = 'PK' AND  parent_object_id = OBJECT_ID ('_Reference156'))
BEGIN
	ALTER TABLE [dbo].[_Reference156] ADD CONSTRAINT [PK_Reference156_IDRRef] PRIMARY KEY ([_IDRRef]);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference156]'))
BEGIN
	ALTER TABLE [dbo].[_Reference156] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = ON);
END
GO
DROP FUNCTION IF EXISTS [Source1C].[fnGetSubdivisions];
GO
CREATE FUNCTION [Source1C].[fnGetSubdivisions]( @vEndVersionID bigint = 0)
RETURNS TABLE 
AS
RETURN 
	SELECT 
		[Subdivisions].[_IDRRef] as [SubdivisionIDinIS]
		,CONVERT(bit,[_Marked])		as [Marked]
		,[_Code] as [SubdivisionCode]
		,[_Description] as [SubdivisionName]
		,1 as [UseInReport]
		,CHECKSUM(
			[Subdivisions].[_IDRRef]
			,[_Marked]
			,[_Code]
			,[_Description]
		)  as CHECKSUMM

		,CT.SYS_CHANGE_VERSION
		,CT.SYS_CHANGE_CREATION_VERSION
		,CT.SYS_CHANGE_OPERATION
		,CT.SYS_CHANGE_COLUMNS
		,CT.SYS_CHANGE_CONTEXT

	FROM  CHANGETABLE(CHANGES [dbo].[_Reference156], @vEndVersionID) AS CT
	LEFT JOIN [dbo].[_Reference156] as [Subdivisions]  
	ON CT.[_IDRRef] = [Subdivisions].[_IDRRef]
GO
GRANT SELECT ON [Source1C].[fnGetSubdivisions] TO [Guest]; 
GO


-----------------------------------------------------------------------------------
-- Загрузка нормализованных торговых точек
IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type = 'PK' AND  parent_object_id = OBJECT_ID ('_Reference75'))
BEGIN
	ALTER TABLE [dbo].[_Reference75] ADD CONSTRAINT [PK_Reference75_IDRRef] PRIMARY KEY ([_IDRRef]);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference75]'))
BEGIN
	ALTER TABLE [dbo].[_Reference75] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = ON);
END
GO
DROP FUNCTION IF EXISTS [Source1C].[fnGetTradePoints];
GO
USE [AgentP]
GO
/****** Object:  UserDefinedFunction [Source1C].[fnGetTradePoints]    Script Date: 19.11.2020 9:17:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER FUNCTION [Source1C].[fnGetTradePoints]( @vEndVersionID bigint = 0)
RETURNS @result TABLE 
(

NormTradePointIDinIS	binary(16)   ,
Marked	bit							  ,
NormCodeTradePoint	nvarchar(15)	  ,
NormAddressTradePoint	nvarchar(500) ,
NormNameTradePoint	nvarchar(250)	  ,
TradeAgent	nvarchar(250)			  ,
ChainName	nvarchar(250)			  ,
SalesChannel	nvarchar(250)		  ,
FormatTT	nvarchar(250)			  ,
CHECKSUMM	int						  ,
SYS_CHANGE_VERSION	bigint			  ,
SYS_CHANGE_CREATION_VERSION	bigint	  ,
SYS_CHANGE_OPERATION	nchar		  ,
SYS_CHANGE_COLUMNS	varbinary(max)	  ,
SYS_CHANGE_CONTEXT	varbinary(max)	  

)

AS 
BEGIN

	DECLARE @tbl TABLE ([_Fld1038] nvarchar(500),[_Reference75_IDRRef] binary(16),[_LineNo1035] numeric(5,0))
	insert into @tbl
	select [_Fld1038],[_Reference75_IDRRef],[_LineNo1035]
	from [dbo].[_Reference75_VT1034] (nolock);
			

	DECLARE @tbl1 TABLE ([_Fld1031RRef] binary(16),[_IDRRef] binary(16),[_Reference75_IDRRef] binary(16),[_Description] nvarchar(100))
	insert into @tbl1
	select  [_Fld1031RRef],	[_IDRRef],	[_Reference75_IDRRef],[_Description]
	from [dbo].[_Reference75_VT1029] TP0 (nolock)
				inner join [dbo].[_Reference109] as NameTP (nolock)
				on TP0.[_Fld1032_RRRef] = NameTP._IDRRef


	insert into @result
	SELECT 
		[NormTradePointIDinIS]
		,[Marked]
        ,[NormCodeTradePoint]
        ,[NormAddressTradePoint]
        ,[NormNameTradePoint]
        ,[TradeAgent]
        ,[ChainName]
        ,[SalesChannel]
		,[FormatTT]

		,CHECKSUM(
			[NormTradePointIDinIS]
			,[Marked]
			,[NormCodeTradePoint]
			,[NormAddressTradePoint]
			,[NormNameTradePoint]
			,[TradeAgent]
			,[ChainName]
			,[SalesChannel]
			,[FormatTT]
		)  as CHECKSUMM

		,CT.SYS_CHANGE_VERSION
		,CT.SYS_CHANGE_CREATION_VERSION
		,CT.SYS_CHANGE_OPERATION
		,CT.SYS_CHANGE_COLUMNS
		,CT.SYS_CHANGE_CONTEXT

	FROM  CHANGETABLE(CHANGES [dbo].[_Reference75], @vEndVersionID) AS CT
	LEFT JOIN 
	(

	SELECT 
	   [NormTradePointIDinIS]
	  ,CONVERT(bit,[Marked]) as [Marked]
      ,[NormCodeTradePoint]
	  ,iif(len(coalesce([NormAddressTradePoint],''))<5,'Не указан',[NormAddressTradePoint]) as [NormAddressTradePoint]
      ,iif(len(coalesce([NormNameTradePoint],''))<5,'Не указан',[NormNameTradePoint]) as [NormNameTradePoint]
	  ,[TradeAgent]
	  ,[ChainName]
	  ,iif(len(coalesce([SalesChannel],''))<2,'Не указан',[SalesChannel]) as [SalesChannel]
	  ,COALESCE([FormatTT],'Не указан') as [FormatTT]

	FROM
		(
		SELECT 
		  [TradePoints].[_IDRRef] as [NormTradePointIDinIS]
		  ,[TradePoints].[_Marked] as [Marked]
		  ,[TradePoints].[_Code] as [NormCodeTradePoint]
		  ,AddressTT.[_Fld1038] as [NormAddressTradePoint]
		  ,[TradePoints].[_Description] as [NormNameTradePoint]
		  ,TP._Description as [TradeAgent]
		  ,Chain._Description as [ChainName]
		  ,SalesChannels._Description as [SalesChannel]
		  ,([FTT]._Description) as [FormatTT]

		FROM  
			   [dbo].[_Reference75] as [TradePoints] (nolock)
			   left join [dbo].[_Reference42]  as SalesChannels (nolock)
			   on [TradePoints].[_Fld1014RRef] = SalesChannels._IDRRef 
			   left join [dbo].[_Reference168]  as [FTT] (nolock)
			   on [TradePoints].[_Fld1019RRef] = [FTT]._IDRRef
		    
			   left join
			   (select [_Fld1038],[_Reference75_IDRRef] 
				from @tbl 
				where _LineNo1035 = 1) as AddressTT 
				on [TradePoints].[_IDRRef] = AddressTT._Reference75_IDRRef 
		   
			   left join 
			   (select [_Fld1038],[_Reference75_IDRRef] 
			   from @tbl 
				where _LineNo1035 = 2) as AddressTTu 
			   on  [TradePoints].[_IDRRef]   = AddressTTu._Reference75_IDRRef 

   			   left join (
					select [_Fld1031RRef],[_Description],[_IDRRef],[_Reference75_IDRRef] 
					from @tbl1
					where [_Fld1031RRef] = 0x8D6900155D463A0111E93421C7D49BC8  ) as TP 
			   on   [TradePoints]._IDRRef    = TP.[_Reference75_IDRRef]

   			   left join (
					select [_Fld1031RRef],[_Description],[_IDRRef],[_Reference75_IDRRef] 
					from @tbl1
					where [_Fld1031RRef] = 0xBD2D00155D46420311E9303E1B032835  ) as Chain 
			   on   [TradePoints]._IDRRef   = Chain.[_Reference75_IDRRef]
		) as cccv

	) as [TradePoints] 
	
	ON CT.[_IDRRef] = [TradePoints].[NormTradePointIDinIS];

	RETURN 
END
GO
GRANT SELECT ON [Source1C].[fnGetTradePoints] TO [Guest]; 
GO



-----------------------------------------------------------------------------------
-- Загрузка нормализованных контрагентов

IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type = 'PK' AND  parent_object_id = OBJECT_ID ('_Reference120'))
BEGIN
	ALTER TABLE [dbo].[_Reference120] ADD CONSTRAINT [PK_Reference120_IDRRef] PRIMARY KEY ([_IDRRef]);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference120]'))
BEGIN
	ALTER TABLE [dbo].[_Reference120] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = ON);
END
GO
DROP FUNCTION IF EXISTS [Source1C].[fnGetNormContragents];
GO
CREATE FUNCTION [Source1C].[fnGetNormContragents]( @vEndVersionID bigint = 0)
RETURNS TABLE 
AS
RETURN 
	SELECT 
		[NormContragents].[_IDRRef] as [NormContragentIDinIS]
		,CONVERT(bit,[_Marked])		as [Marked]
		,[_Code] as [NormCodeContragent]
		,iif([_Description] is null or len([_Description])<3,'Не указан',[_Description]) as [NormNameContragent]
		,iif([_Fld1878] is null or len([_Fld1878])<3,'Не указан',[_Fld1878]) as [NormNameFullContragent]
		,iif([_Fld1881] is null or len([_Fld1881])<3,'Не указан',[_Fld1881]) as [INN]
		,iif([_Fld1883] is null or len([_Fld1883])<3,'Не указан',[_Fld1883]) as [KPP]

		,CHECKSUM(
			[NormContragents].[_IDRRef]
			,[_Marked]
			,[_Code]
			,[_Description]
			,[_Fld1878]
			,[_Fld1881]
			,[_Fld1883]
		)  as CHECKSUMM

		,CT.SYS_CHANGE_VERSION
		,CT.SYS_CHANGE_CREATION_VERSION
		,CT.SYS_CHANGE_OPERATION
		,CT.SYS_CHANGE_COLUMNS
		,CT.SYS_CHANGE_CONTEXT

	FROM  CHANGETABLE(CHANGES [dbo].[_Reference120], @vEndVersionID) AS CT
	LEFT JOIN [dbo].[_Reference120] as [NormContragents]
	ON CT.[_IDRRef] = [NormContragents].[_IDRRef]
GO
GRANT SELECT ON [Source1C].[fnGetNormContragents] TO [Guest]; 
GO

-----------------------------------------------------------------------------------
-- Загрузка нормализованных агентов

IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type = 'PK' AND  parent_object_id = OBJECT_ID ('_Reference67'))
BEGIN
	ALTER TABLE [dbo].[_Reference67] ADD CONSTRAINT [PK_Reference67_IDRRef] PRIMARY KEY ([_IDRRef]);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference67]'))
BEGIN
	ALTER TABLE [dbo].[_Reference67] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = ON);
END
GO
DROP FUNCTION IF EXISTS [Source1C].[fnGetNormAgents];
GO
CREATE FUNCTION [Source1C].[fnGetNormAgents]( @vEndVersionID bigint = 0)
RETURNS TABLE 
AS
RETURN 
	SELECT 
		[NormAgents].[_IDRRef] as [NormAgentIDInIS]
		,CONVERT(bit,[_Marked])		as [Marked]
		,[_Code] as [NormCodeAgent]
		,[_Description] as [NormNameAgent]
		,iif(year([_Fld916])< 4000,'01.01.2020',dateadd(year,-2000,[_Fld916])) as [WorkStartDate]

		,CHECKSUM(
			[NormAgents].[_IDRRef]
			,[_Marked]
			,[_Code]
			,[_Description]
			,[_Fld916]
		)  as CHECKSUMM

		,CT.SYS_CHANGE_VERSION
		,CT.SYS_CHANGE_CREATION_VERSION
		,CT.SYS_CHANGE_OPERATION
		,CT.SYS_CHANGE_COLUMNS
		,CT.SYS_CHANGE_CONTEXT

	FROM  CHANGETABLE(CHANGES [dbo].[_Reference67], @vEndVersionID) AS CT
	LEFT JOIN [dbo].[_Reference67] as [NormAgents]
	ON CT.[_IDRRef] = [NormAgents].[_IDRRef]
GO
GRANT SELECT ON [Source1C].[fnGetNormAgents] TO [Guest]; 
GO

-----------------------------------------------------------------------------------
-- Загрузка справочника нормализованной номенклатуры

IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type = 'PK' AND  parent_object_id = OBJECT_ID ('_Reference124'))
BEGIN
	ALTER TABLE [dbo].[_Reference124] ADD CONSTRAINT [PK_Reference124_IDRRef] PRIMARY KEY ([_IDRRef]);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Reference124]'))
BEGIN
	ALTER TABLE [dbo].[_Reference124] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = ON);
END
GO
DROP FUNCTION IF EXISTS [Source1C].[fnGetNormNomenclatures];
GO
CREATE FUNCTION [Source1C].[fnGetNormNomenclatures]( @vEndVersionID bigint = 0)
RETURNS TABLE 
AS
RETURN 
	SELECT 
		[NormNomenclatures].[_IDRRef] as [NormNomenclatureIDinIS], 
		CONVERT(bit,[_Marked])		as [Marked],
	    [_Code] AS [NormCodeNomenclature], 
	    [_Description] AS [NormNameNomenclature],
	    iif([_Fld1933] is null or len([_Fld1933])<3,'Не указан',[_Fld1933]) AS [NormArticleNomenclature], 
        [_Fld1954] AS [NormWeightNomenclature]

		,CHECKSUM(
			[NormNomenclatures].[_IDRRef],[_Marked]
			,[_Code]
			,[_Description]
			,[_Fld1933]
			,[_Fld1954]
		)  as CHECKSUMM

		,CT.SYS_CHANGE_VERSION
		,CT.SYS_CHANGE_CREATION_VERSION
		,CT.SYS_CHANGE_OPERATION
		,CT.SYS_CHANGE_COLUMNS
		,CT.SYS_CHANGE_CONTEXT

	FROM  CHANGETABLE(CHANGES [dbo].[_Reference124], @vEndVersionID) AS CT
	LEFT JOIN [dbo].[_Reference124] as [NormNomenclatures]
	ON CT.[_IDRRef] = [NormNomenclatures].[_IDRRef]
GO
GRANT SELECT ON [Source1C].[fnGetNormNomenclatures] TO [Guest]; 
GO

-----------------------------------------------------------------------------------
-- Загрузка данных по продажам из Агент + --

IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type = 'PK' AND  parent_object_id = OBJECT_ID ('_Document206'))
BEGIN
	ALTER TABLE [dbo].[_Document206] ADD CONSTRAINT [PK_Document206_IDRRef] PRIMARY KEY ([_IDRRef]);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Document206]'))
BEGIN
	ALTER TABLE [dbo].[_Document206] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = ON);
END
GO
DROP FUNCTION IF EXISTS [Source1C].[fnGetSales];
GO

CREATE FUNCTION [Source1C].[fnGetSales]( @vEndVersionID bigint = 0)
RETURNS TABLE 
AS
RETURN 
	SELECT 
		[_Document206_IDRRef]		as [IDinIS]
		,CONVERT(bit,[_Marked])		as [Marked]
		,CONVERT(bit,[_Posted])		as [Posted]
		,[_Number] as [DocumentNumber]
		,cast(dateadd(year,-2000,[_Date_Time]) as date)	as [DocumentDate]
		,cast(dateadd(year,-2000,[_Fld3879]) as date)		as [DeliveryDate]
		,[_Fld3898RRef] as [DistrTradePointIDinIS]
		,[_Fld3920RRef] as [DistrNomenclatureIDinIS]
		,[_Fld3890RRef] as [SubdivisionIDinIS]
		,[_Fld3901RRef] as [AgentDistrIDinIS]
		,[_Fld3900RRef] as [ContragentIDinIS]
		,[_Fld3910] as [Qty]
		,[_Fld3911] as [Price]
		,[_Fld3917] as [Cost]
		,[_Fld3924] as [SalesVol]
		,iif([_Fld3880]=1,'1','0') as [isSecondarySales]
		,iif([_Fld3884]=1,'1','0') as [UseNDS]
		,iif([_Fld3896]=1,'1','0') as [PriceWithNDS]
		,CAST([_LineNo3905] as INT)	as [LineNo100]

		,CHECKSUM(
			[_Document206_IDRRef],[_Marked]
			,[_Number]
			,[_Date_Time]
			,[_Fld3879]
			,[_Fld3898RRef]
			,[_Fld3920RRef]
			,[_Fld3890RRef]
			,[_Fld3901RRef]
			,[_Fld3900RRef]
			,[_Fld3910]  
			,[_Fld3911] 
			,[_Fld3917] 
			,[_Fld3924] 
			,[_Fld3880]
			,[_Fld3884]
			,[_Fld3896]
		)  as CHECKSUMM

		,CT.SYS_CHANGE_VERSION
		,CT.SYS_CHANGE_CREATION_VERSION
		,CT.SYS_CHANGE_OPERATION
		,CT.SYS_CHANGE_COLUMNS
		,CT.SYS_CHANGE_CONTEXT

	FROM  CHANGETABLE(CHANGES [dbo].[_Document206], @vEndVersionID) AS CT
	LEFT JOIN [dbo].[_Document206] as [SalesHeader] 
		ON CT.[_IDRRef] = [SalesHeader].[_IDRRef]
	LEFT JOIN [dbo].[_Document206_VT3904] as [SalesLines] 
		ON [SalesHeader].[_IDRRef] = [SalesLines].[_Document206_IDRRef]
	WHERE  [_Document206_IDRRef]  is not null


GO
GRANT SELECT ON [Source1C].[fnGetSales] TO [Guest]; 
GO


-----------------------------------------------------------------------------------
-- Загрузка данных по заказам из Агент + --

IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type = 'PK' AND  parent_object_id = OBJECT_ID ('_Document196'))
BEGIN
	ALTER TABLE [dbo].[_Document196] ADD CONSTRAINT [PK_Document196_IDRRef] PRIMARY KEY ([_IDRRef]);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Document196]'))
BEGIN
	ALTER TABLE [dbo].[_Document196] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = ON);
END
GO
DROP FUNCTION IF EXISTS [Source1C].[fnGetOrders];
GO

CREATE FUNCTION [Source1C].[fnGetOrders]( @vEndVersionID bigint = 0)
RETURNS TABLE 
AS
RETURN 
	SELECT 
		[_Document196_IDRRef] as [IDinIS]
		,CONVERT(bit,[_Marked])		as [Marked]
		,CONVERT(bit,[_Posted])		as [Posted]
		,[_Number] as [DocumentNumber]
		,cast(dateadd(year,-2000,[_Date_Time]) as date) as [DocumentDate]
		,cast(dateadd(year,-2000,[_Fld3567]) as date) as [DeliveryDate]
		,[_Fld3571RRef] as [DistrTradePointIDinIS]
		,[_Fld3591RRef] as [DistrNomenclatureIDinIS]
		,[_Fld3559RRef] as [SubdivisionIDinIS]
		,[_Fld3573RRef] as [AgentDistrIDinIS]
		,[_Fld3570RRef] as [ContragentIDinIS]
		,[_Fld3581] as [Qty]
		,[_Fld3582] as [Price]
		,[_Fld3590] as [Cost]
		,[_Fld3595] as [Volume]
		,iif([_Fld3564]=1,'1','0') as [UseNDS]
		,iif([_Fld3560]=1,'1','0') as [PriceWithNDS]
		,CAST([_LineNo3576] as INT)	as [LineNo100]

		,CHECKSUM(
			[_Document196_IDRRef],[_Marked]
			,[_Number]
			,[_Date_Time]
			,[_Fld3567]
			,[_Fld3571RRef]
			,[_Fld3591RRef]
			,[_Fld3559RRef]
			,[_Fld3573RRef]
			,[_Fld3570RRef]
			,[_Fld3581]
			,[_Fld3582] 
			,[_Fld3590]
			,[_Fld3595]
			,[_Fld3564]
			,[_Fld3560]
		)  as CHECKSUMM


		,CT.SYS_CHANGE_VERSION
		,CT.SYS_CHANGE_CREATION_VERSION
		,CT.SYS_CHANGE_OPERATION
		,CT.SYS_CHANGE_COLUMNS
		,CT.SYS_CHANGE_CONTEXT

	FROM  CHANGETABLE(CHANGES [dbo].[_Document196], @vEndVersionID) AS CT
	LEFT JOIN [dbo].[_Document196] as [OrderHeaders] 
		ON CT.[_IDRRef] = [OrderHeaders].[_IDRRef]
	LEFT JOIN [dbo].[_Document196_VT3575] as [OrderLines] 
		ON [OrderHeaders].[_IDRRef] = [OrderLines].[_Document196_IDRRef]
	WHERE  [_Document196_IDRRef]  is not null
GO
GRANT SELECT ON [Source1C].[fnGetOrders] TO [Guest]; 
GO

-----------------------------------------------------------------------------------
-- Загрузка данных по возвратам из Агент + --

IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE type = 'PK' AND  parent_object_id = OBJECT_ID ('_Document194'))
BEGIN
	ALTER TABLE [dbo].[_Document194] ADD CONSTRAINT [PK_Document194_IDRRef] PRIMARY KEY ([_IDRRef]);
END
GO

IF NOT EXISTS (SELECT 1 FROM sys.change_tracking_tables WHERE object_id = OBJECT_ID('[dbo].[_Document194]'))
BEGIN
	ALTER TABLE [dbo].[_Document194] ENABLE CHANGE_TRACKING WITH (TRACK_COLUMNS_UPDATED = ON);
END
GO
DROP FUNCTION IF EXISTS [Source1C].[fnGetReturns];
GO

CREATE FUNCTION [Source1C].[fnGetReturns]( @vEndVersionID bigint = 0)
RETURNS TABLE 
AS
RETURN 
	SELECT 
		[_Document194_IDRRef]		as [IDinIS]
		,CONVERT(bit,[_Marked])		as [Marked]
		,CONVERT(bit,[_Posted])		as [Posted]
		,[_Number] as [DocumentNumber]
		,cast(dateadd(year,-2000,[_Date_Time]) as date) as [DocumentDate]
		,[_Fld3476RRef] as [DistrTradePointIDinIS]
		,[_Fld3498RRef] as [DistrNomenclatureIDinIS]
		,[_Fld3467RRef] as [SubdivisionIDinIS]
		,[_Fld3480RRef] as [AgentDistrIDinIS]
		,[_Fld3479RRef] as [ContragentIDinIS]
		,[_Fld3488] as [Qty]
		,[_Fld3489] as [Price]
		,[_Fld3492] as [Cost]
		,[_Fld3502] as [Volume]
		,iif([_Fld3461]=1,'1','0') as [UseNDS]
		,iif([_Fld3474]=1,'1','0') as [PriceWithNDS] 
		,CAST([_LineNo3483] as INT)	as [LineNo100]

		,CHECKSUM(
			[_Document194_IDRRef],[_Marked]
			,[_Number]
			,[_Date_Time]
			,[_Fld3476RRef]
			,[_Fld3498RRef]
			,[_Fld3467RRef]
			,[_Fld3480RRef]
			,[_Fld3479RRef]
			,[_Fld3488] 
			,[_Fld3489] 
			,[_Fld3492] 
			,[_Fld3502] 
			,[_Fld3461]
			,[_Fld3474]
		)  as CHECKSUMM


		,CT.SYS_CHANGE_VERSION
		,CT.SYS_CHANGE_CREATION_VERSION
		,CT.SYS_CHANGE_OPERATION
		,CT.SYS_CHANGE_COLUMNS
		,CT.SYS_CHANGE_CONTEXT

	FROM  CHANGETABLE(CHANGES [dbo].[_Document194], @vEndVersionID) AS CT
	LEFT JOIN [dbo].[_Document194] as [ReturnHeaders] 
		ON CT.[_IDRRef] = [ReturnHeaders].[_IDRRef]
	LEFT JOIN [dbo].[_Document194_VT3482] as [ReturnLines] 
		ON [ReturnHeaders].[_IDRRef] = [ReturnLines].[_Document194_IDRRef]
	WHERE  [_Document194_IDRRef]  is not null

GO
GRANT SELECT ON [Source1C].[fnGetReturns] TO [Guest]; 
GO
