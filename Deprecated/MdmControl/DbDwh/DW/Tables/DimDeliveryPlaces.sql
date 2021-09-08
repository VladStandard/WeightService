CREATE TABLE [DW].[DimDeliveryPlaces] (
	Marked bit
	,ContragentID varbinary(16)
	,Code nvarchar(15)
	,[Name] nvarchar(150)
	,[GUID_Mercury]	  nvarchar(36)		--Справочник.МестаДоставки.Реквизит.GUID_Меркурий
	,[KPP]	  nvarchar(15)				--Справочник.МестаДоставки.Реквизит.КПП
	,[GLN]	 nvarchar(15)				--Справочник.МестаДоставки.Реквизит.GLN
	,[Address]	 nvarchar(1024)			--Справочник.МестаДоставки.Реквизит.ФактАдрес
	,[FormatStoreID]	 varbinary(16)		--Справочник.МестаДоставки.Реквизит.ФорматМагазинаТС
	,[RegionStoreID]	 varbinary(16)		--Справочник.МестаДоставки.Реквизит.РегионМагазинаТС
	,[FormatStoreName]  nvarchar(150)
	,[RegionStoreName]  nvarchar(150)
	
	,[ID] [int] NOT NULL IDENTITY(-2147483648,1)
	,[CreateDate] datetime NOT NULL
	,[DLM] datetime  NOT NULL
	,[StatusID] int  NOT NULL
	,[InformationSystemID] int  NOT NULL
	,[CodeInIS] varbinary(16)  NOT NULL

	-- MDM
	,RelevanceStatus tinyint default 0
	,NormalizationStatus tinyint default 0
	,MasterId int  NULL
	,CONSTRAINT chkDimDeliveryPlacesRelevanceStatus CHECK (NormalizationStatus in (0,1,2,3))
	,CONSTRAINT chkDimDeliveryPlacesNormalizationStatus CHECK (RelevanceStatus in (0,1,2))


	,PRIMARY KEY CLUSTERED ([InformationSystemID] ASC,[CodeInIS] ASC, [ID] ASC)


) on [DIMFileGroup]

GO


