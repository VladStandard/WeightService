create procedure [IIS].[GetShipments]
	@jsonListId nvarchar(max)
	,@xml xml output
as
begin
    set nocount on;  
	declare @IDTbl table (ID bigint, IDC bigint)
	insert into @IDTbl 
		select * 
		from openjson ( @jsonListId , N'$')
		with (ID bigint N'$.id', IDC bigint N'$.idc')
	drop table if exists #Shipments;
	create table #Shipments (
		 [ParentDoc] bigint
		,[Marked] bit
		,[Posted] bit
		,[DocNum] nvarchar(15)
		,[DocDate] datetime
		,[DocType] nvarchar(50)
		,[ID] bigint
		,[CodeInIS] varbinary(16)
		,[DLM] datetime
		,[OrgID] int
		,[OrgName] nvarchar(150) 
		,[ContragentID] int
		,[ContragentName] nvarchar(150) 
		,[DeliveryPlaceID] int
		,[DeliveryPlace] nvarchar(150) 
		,[NomenclatureID] int
		,[NomenclatureMasterID] int
		,[NomenclatureName] nvarchar(150) 
		,[VATRate] nvarchar(10) 
		,[Qty] numeric(15,3)
		,[Price] numeric(15,2)
		,[CostWithDiscounts] numeric(15,2)
		,[Cost] numeric(15,2)
		,[CostVAT] numeric(15,2)
		,[BasePrice] numeric(15,2)
		,[DiscountCondition] nvarchar(20) 
		,[PercentageDiscounts] numeric(5,2)

	)
	insert into #Shipments
	select 
		null [ParentDoc]
		,[doc].[Marked]
		,[doc].[Posted]
		,[doc].[DocNum]
		,[doc].[DocDate]
		,[doc].[DocType]
		,[doc].[ID]
		,[doc].[CodeInIS]
		,[doc].[DLM]
		,[ss].[_OrgID] OrgID
		,(select [Description] from [DW].[DimOrganizations] where ID = [ss].[_OrgID]) as OrgName
		,[ss].[_ContragentID]
		,(select [Name] from [DW].[DimContragents] where ID = [ss].[_ContragentID]) as ContragentName
		,[ss].[_DeliveryPlaceID]
		,(select [Name] from [DW].[DimDeliveryPlaces] where ID = [ss].[_DeliveryPlaceID]) as DeliveryPlace
		,[ss].[_NomenclatureID]
		,(select [MasterId] from [DW].[DimNomenclatures] where ID = [ss].[_NomenclatureID]) as NomenclatureMasterID
		,(select [Name] from [DW].[DimNomenclatures] where ID = [ss].[_NomenclatureID]) as NomenclatureName
		,[ss].[VATRate]
		,[ss].[Qty] * COALESCE( (select [Weight] from [DW].[DimNomenclatures] where ID = [ss].[_NomenclatureID]) ,1) as [Qty]
		,[ss].[Price]
		,[ss].[Cost] as [CostWithDiscounts]
		,([ss].[Qty]*[ss].[Price]) as [Cost]
		,[ss].[CostVAT]
		,[ss].[BasePrice]
		,[ss].[DiscountCondition]
		,[ss].[PercentageDiscounts]
	from [DW].[FactSalesOfGoods] [ss]
	inner join [DW].[DocJournal] as [doc]
	on [ss].[CodeInIS] = [doc].[CodeInIS] and [ss].InformationSystemID = [doc].InformationSystemID
	where [doc].[ID] in (select ID from @IDTbl)
	union all
	select 
		 [ss].[_SalesCodeID] ParentDoc
		,[doc].[Marked]
		,[doc].[Posted]
		,[doc].[DocNum]
		,[doc].[DocDate]
		,[doc].[DocType]
		,[doc].[ID]
		,[doc].[CodeInIS]
		,[doc].[DLM]
		,[ss].[_OrgID] OrgID
		,(select [Description] from [DW].[DimOrganizations] where ID = [ss].[_OrgID]) as OrgName
		,[ss].[_ContragentID]
		,(select [Name] from [DW].[DimContragents] where ID = [ss].[_ContragentID]) as ContragentName
		,[ss].[_DeliveryPlaceID]
		,(select [Name] from [DW].[DimDeliveryPlaces] where ID = [ss].[_DeliveryPlaceID]) as DeliveryPlace
		,[ss].[_NomenclatureID]
		,(select [MasterId] from [DW].[DimNomenclatures] where ID = [ss].[_NomenclatureID]) as NomenclatureMasterId
		,(select [Name] from [DW].[DimNomenclatures] where ID = [ss].[_NomenclatureID]) as NomenclatureName
		,[ss].[VATRate]
		,([ss].Qty - [ss].[QtyBeforeChange]) * COALESCE( (select [Weight] from [DW].[DimNomenclatures] where ID = [ss].[_NomenclatureID]) ,1) as [Qty]
		,([ss].[Price] - [ss].[PriceBeforeChange])	as [Price]
		,([ss].[Cost] - [ss].[CostBeforeChange])	as [CostWithDiscounts]
		,(([ss].Qty - [ss].[QtyBeforeChange]) * ([ss].[Price] - [ss].[PriceBeforeChange]))	as [Cost]
		,([ss].[CostVAT] - [ss].[CostVATBeforeChange]) as [CostVAT]
		,null as [BasePrice]
		,null as [DiscountCondition]
		,null as [PercentageDiscounts]
	from [DW].[FactReturns] [ss]
	inner join [DW].[DocJournal] as [doc] 
	on [ss].[CodeInIS] = [doc].[CodeInIS] and [ss].InformationSystemID = [doc].InformationSystemID
	where [doc].[ID] in (SELECT IDC FROM @IDTbl)
	and
		([ss].Qty - [ss].[QtyBeforeChange])
		+([ss].[Price] - [ss].[PriceBeforeChange])
		+([ss].[Cost] - [ss].[CostBeforeChange])
		+([ss].[CostVAT]-[ss].[CostVATBeforeChange]) <> 0
	;
	select * from #Shipments
	declare @Shipments table (
		 [Tag] int
		,[Parent] int
		,[Shipment!1!ID] bigint
		,[Shipment!1!DocType] nvarchar(50)
		,[Shipment!1!Marked] bit
		,[Shipment!1!Posted] bit
		,[Shipment!1!DocNum] nvarchar(15)
		,[Shipment!1!DocDate] datetime
		,[Shipment!1!OrgID] bigint
		,[Shipment!1!OrgName] nvarchar(150)
		,[Shipment!1!ContragentID] bigint
		,[Shipment!1!ContragentName] nvarchar(150)
		,[Shipment!1!DeliveryPlaceID] bigint
		,[Shipment!1!DeliveryPlace] nvarchar(150)
		,[Shipment!1!ShippingDate] datetime
		,[Shipment!1!DLM] datetime
		,[Shipment!1!Cost] numeric(15,2)
		,[Shipment!1!CostWithDiscounts] numeric(15,2)
		,[Shipment!2!NomenclatureID] int
		,[Shipment!2!NomenclatureMasterID] int
		,[Shipment!2!NomenclatureName] nvarchar(150)
		,[Shipment!2!Qty] numeric(15,3)
		,[Shipment!2!Price] numeric(15,2)
		,[Shipment!2!Cost] numeric(15,2)
		,[Shipment!2!CostWithDiscounts] numeric(15,2)
		,[Shipment!2!PercentageDiscounts] numeric(15,2)
		,[Shipment!2!BasePrice] numeric(15,2)
		,[Shipment!2!OrderDocDate] datetime
		,[Shipment!2!Marked] bit
		,[Shipment!2!Posted] bit
	)
	insert into @Shipments (
		[Tag],[Parent]		
		,[Shipment!1!ID]
		,[Shipment!1!DocType]
		,[Shipment!1!Marked]	
		,[Shipment!1!Posted]	
		,[Shipment!1!DocNum]
		,[Shipment!1!DocDate]
		,[Shipment!1!OrgID]
		,[Shipment!1!OrgName]	
		,[Shipment!1!ContragentID]
		,[Shipment!1!ContragentName]
		,[Shipment!1!DeliveryPlaceID]
		,[Shipment!1!DeliveryPlace]
		,[Shipment!1!ShippingDate]
		,[Shipment!1!DLM]
		,[Shipment!1!Cost]
		,[Shipment!1!CostWithDiscounts]
	)
		SELECT
			1					[Tag]
			,NULL				[Parent]
			,[ID]				[Shipment!1!ID]
			,[DocType]			[Shipment!1!DocType]
			,Marked				[Shipment!1!Posted]
			,Posted				[Shipment!1!Marked]
			,[DocNum]				[Shipment!1!DocNum]	
			,[DocDate]				[Shipment!1!DocDate]
			,OrgID					[Shipment!1!OrgID]
			,OrgName				[Shipment!1!OrgName]
			,ContragentID			[Shipment!1!ContragentID]
			,ContragentName			[Shipment!1!ContragentName]
			,DeliveryPlaceID		[Shipment!1!DeliveryPlaceID]
			,DeliveryPlace			[Shipment!1!DeliveryPlace]
			,[DocDate]				[Shipment!1!ShippingDate]
			,MAX([DLM])				[Shipment!1!DLM]
			,SUM(Cost)				[Shipment!1!Cost]
			,SUM(CostWithDiscounts)	[Shipment!1!CostWithDiscounts]
		from #Shipments
		group by 
			ID
			,[DocType]
			,Marked		
			,Posted			
			,[DocNum]		
			,[DocDate]		
			,OrgID			
			,OrgName		
			,ContragentID	
			,ContragentName	
			,DeliveryPlaceID
			,DeliveryPlace	
			,[DocDate]		
	insert into @Shipments (
		 [Tag]
		,[Parent]
		,[Shipment!1!ID]
		,[Shipment!2!OrderDocDate]	
		,[Shipment!2!NomenclatureID]
		,[Shipment!2!NomenclatureMasterId]
		,[Shipment!2!NomenclatureName]
		,[Shipment!2!Qty]
		,[Shipment!2!Price]
		,[Shipment!2!Cost]
		,[Shipment!2!CostWithDiscounts]
		,[Shipment!2!PercentageDiscounts]
		,[Shipment!2!BasePrice]
		,[Shipment!2!Marked]
		,[Shipment!2!Posted]
	)
	select 
		 2							[Tag]
		,1							[Parent]
		,[ID]						[Shipment!1!ID]
		,NULL						[Shipment!2!OrderDocDate]	
		,NomenclatureID				[Shipment!2!NomenclatureID]
		,NomenclatureMasterID		[Shipment!2!NomenclatureMasterID]
		,NomenclatureName			[Shipment!2!NomenclatureName]
		,SUM(Qty)					[Shipment!2!Qty]
		,[Price]					[Shipment!2!Price]
		,SUM([Cost])				[Shipment!2!Cost]
		,SUM( CostWithDiscounts)	[Shipment!2!CostWithDiscounts]
		,[PercentageDiscounts]		[Shipment!2!PercentageDiscounts]
		,[BasePrice]				[Shipment!2!BasePrice]
		,[Marked]					[Shipment!2!Marked]
		,[Posted]					[Shipment!2!Posted]
	from #Shipments
	group by 
		 [ID]
		,[NomenclatureID]
		,[NomenclatureMasterID]
		,[NomenclatureName]
		,[Price]
		,[PercentageDiscounts]
		,[BasePrice]
		,[Marked]
		,[Posted]

	set @xml = (select * from @Shipments order by [Shipment!1!ID],[Tag] for xml explicit, root('Shipments'), binary base64)
	---SELECT @xml
	drop table if EXISTS #Shipments;
	return 0
end