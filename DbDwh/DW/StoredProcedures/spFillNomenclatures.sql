CREATE PROCEDURE [DW].[spFillNomenclatures]

     @Code nvarchar(15)
	,@Name nvarchar (150)
	,@Parents nvarchar (1024)
    ,@Article nvarchar (25)
    ,@Weighted bit
    ,@GUID_Mercury nvarchar (36)
    ,@KeepTrackOfCharacteristics bit
    ,@NameFull nvarchar (512)
    ,@Comment nvarchar (512)
    ,@IsService bit
    ,@AdditionalDescriptionOfNomenclature nvarchar (max)
    ,@NomenclatureGroupCost varbinary (16)
    ,@NomenclatureGroup varbinary (16)
    ,@ArticleCost varbinary (16)
    ,@Brand varbinary (16)
    ,@NomenclatureType varbinary (16)
    ,@VATRate nvarchar (10)
    --,@ResidueStorageUnit varbinary (16)
    --,@UnitForReports varbinary (16)
    --,@BaseUnit varbinary (16)
    ,@Unit nvarchar (150)
    ,@Weight decimal (15,3)

  	,@boxTypeID    binary(16)
	,@boxTypeName  nvarchar(200)
	,@packTypeID   binary(16)
	,@packTypeName nvarchar(200)
	,@SerializedRepresentationObject xml
	,@Marked	   bit


	,@StatusID int 
	,@InformationSystemID  int
	,@CodeInIS varbinary(16)

AS
BEGIN

    SET NOCOUNT ON;  
	DECLARE @DLM datetime = GetDate();
	DECLARE @CreateDate datetime = GetDate();

	SET @NomenclatureGroupCost = CASE WHEN @NomenclatureGroupCost = 0x00000000000000000000000000000000 THEN NULL ELSE @NomenclatureGroupCost END;
    SET @NomenclatureGroup  = CASE WHEN @NomenclatureGroup = 0x00000000000000000000000000000000 THEN NULL ELSE @NomenclatureGroup END;
    SET @ArticleCost  = CASE WHEN @ArticleCost = 0x00000000000000000000000000000000 THEN NULL ELSE @ArticleCost END;
    SET @Brand  = CASE WHEN @Brand = 0x00000000000000000000000000000000 THEN NULL ELSE @Brand END;
    SET @NomenclatureType  = CASE WHEN @NomenclatureType = 0x00000000000000000000000000000000 THEN NULL ELSE @NomenclatureType END;

	MERGE [DW].[DimNomenclatures] as target
	USING ( 
	SELECT 
		 @Code
		,@Name
		,@Parents
    	,@Article 
    	,@Weighted 
    	,@GUID_Mercury 
    	,@KeepTrackOfCharacteristics 
    	,@NameFull 
    	,@Comment 
    	,@IsService 
    	,@AdditionalDescriptionOfNomenclature 
    	,@NomenclatureGroupCost 
    	,@NomenclatureGroup 
    	,@ArticleCost 
    	,@Brand 
    	,@NomenclatureType 
    	,@VATRate 
    	--,@ResidueStorageUnit 
    	--,@UnitForReports
    	--,@BaseUnit 
    	,@Unit 
    	,@Weight

  		,@boxTypeID    
		,@boxTypeName  
		,@packTypeID   
		,@packTypeName 
		,@Marked

		,@CreateDate
		,@DLM
		,@StatusID
		,@InformationSystemID
		,@CodeInIS 
		,@SerializedRepresentationObject

	) AS source (
		 [Code]
		,[Name]
		,[Parents]
    	,[Article]
    	,[Weighted]
    	,[GUID_Mercury]
    	,[KeepTrackOfCharacteristics]
    	,[NameFull]
    	,[Comment]
    	,[IsService]
    	,[AdditionalDescriptionOfNomenclature]
    	,[NomenclatureGroupCost]
    	,[NomenclatureGroup]
    	,[ArticleCost]
    	,[Brand]
    	,[NomenclatureType]
    	,[VATRate]
    	--,[ResidueStorageUnit]
    	--,[UnitForReports]
    	--,[BaseUnit] 
    	,[Unit]
    	,[Weight]

  		,[boxTypeID]   
		,[boxTypeName] 
		,[packTypeID]  
		,[packTypeName]
		,[Marked]

		,[CreateDate]
		,[DLM]
		,[StatusID]
		,[InformationSystemID]
		,[CodeInIS]
		,[SerializedRepresentationObject]
	)  
	ON (target.[CodeInIs] = source.[CodeInIs] AND target.[InformationSystemID] = source.[InformationSystemID]) 

	WHEN MATCHED THEN  UPDATE SET 
		 [Code]													= @Code
		,[Name]													= @Name
		,[Parents]												= @Parents
    	,[Article]									    		= @Article 
    	,[Weighted]									    		= @Weighted 
    	,[GUID_Mercury]								    		= @GUID_Mercury 
    	,[KeepTrackOfCharacteristics]				    		= @KeepTrackOfCharacteristics 
    	,[NameFull]									    		= @NameFull 
    	,[Comment]									    		= @Comment 
    	,[IsService]								    		= @IsService 
    	,[AdditionalDescriptionOfNomenclature]		    		= @AdditionalDescriptionOfNomenclature
    	,[NomenclatureGroupCost]					    		= @NomenclatureGroupCost 
    	,[NomenclatureGroup]						    		= @NomenclatureGroup 
    	,[ArticleCost]								    		= @ArticleCost 
    	,[Brand]									    		= @Brand 
    	,[NomenclatureType]							    		= @NomenclatureType 
    	,[VATRate]									    		= @VATRate 
    	--,[ResidueStorageUnit]						    		= @ResidueStorageUnit 
    	--,[UnitForReports]							    		= @UnitForReports
    	--,[BaseUnit] 								    		= @BaseUnit 
    	,[Unit]										    		= @Unit 
    	,[Weight]									    		= @Weight

  		,[boxTypeID]		= @boxTypeID
		,[boxTypeName]		= @boxTypeName
		,[packTypeID]		= @packTypeID
		,[packTypeName]		= @packTypeName
		,[Marked]			= @Marked

		,[StatusID]												= @StatusID
		,[DLM]													= @DLM
		,[SerializedRepresentationObject]						= source.SerializedRepresentationObject
		,[IsProduct] = IIF(source.[Brand] is null,0,1)

	WHEN NOT MATCHED THEN INSERT (
			 [Code]									
			,[Name]
			,[Parents]
    		,[Article]
    		,[Weighted]
    		,[GUID_Mercury]
    		,[KeepTrackOfCharacteristics]
    		,[NameFull]
    		,[Comment]
    		,[IsService]
    		,[AdditionalDescriptionOfNomenclature]
    		,[NomenclatureGroupCost]
    		,[NomenclatureGroup]
    		,[ArticleCost]
    		,[Brand]
    		,[NomenclatureType]
    		,[VATRate]
    		--,[ResidueStorageUnit]
    		--,[UnitForReports]
    		--,[BaseUnit] 
    		,[Unit]
    		,[Weight]

  			,[boxTypeID]   
			,[boxTypeName] 
			,[packTypeID]  
			,[packTypeName]
			,[Marked]

			,[CreateDate]
			,[DLM]
			,[StatusID]
			,[InformationSystemID]
			,[CodeInIs]
			,[SerializedRepresentationObject]
			,[IsProduct]

		) VALUES  (
			 @Code
			,@Name
			,@Parents
    		,@Article 
    		,@Weighted 
    		,@GUID_Mercury 
    		,@KeepTrackOfCharacteristics 
    		,@NameFull 
    		,@Comment 
    		,@IsService 
    		,@AdditionalDescriptionOfNomenclature 
    		,@NomenclatureGroupCost 
    		,@NomenclatureGroup 
    		,@ArticleCost 
    		,@Brand 
    		,@NomenclatureType 
    		,@VATRate 
    		--,@ResidueStorageUnit 
    		--,@UnitForReports
    		--,@BaseUnit 
    		,@Unit 
    		,@Weight

  			,@boxTypeID
			,@boxTypeName
			,@packTypeID
			,@packTypeName
			,@Marked

			,@CreateDate
			,@DLM
			,@StatusID
			,@InformationSystemID
			,@CodeInIS 
			,source.[SerializedRepresentationObject]
			,IIF(source.[Brand] is null,0,1) 
		);

END

GO
