CREATE PROCEDURE [MDM].[NomenclatureMasterRowMake]
	@Id int
AS
BEGIN
	
    DECLARE @MasterID int;

    SELECT @MasterID = [MasterId]
    FROM [DW].[DimNomenclatures]
    WHERE [ID] = @Id;

    IF @MasterID IS NOT NULL BEGIN
        RAISERROR ('Запись уже нормализована.',11,1);
        RETURN 0;
    END;

	INSERT INTO [DW].[DimNomenclatures]
           ([Code]
           ,[Marked]
           ,[Name]
           ,[Parents]
           ,[Article]
           ,[Weighted]
           ,[GUID_Mercury]
           ,[KeepTrackOfCharacteristics]
           ,[NameFull]
           ,[Comment]
           ,[IsService]
           ,[IsProduct]
           ,[AdditionalDescriptionOfNomenclature]
           ,[NomenclatureGroupCost]
           ,[NomenclatureGroup]
           ,[ArticleCost]
           ,[Brand]
           ,[NomenclatureType]
           ,[VATRate]
           ,[Unit]
           ,[Weight]
           ,[boxTypeID]
           ,[boxTypeName]
           ,[packTypeID]
           ,[packTypeName]
           ,[SerializedRepresentationObject]
           ,[InformationSystemID]
           ,[NormalizationStatus]
           ,[CodeInIS]
           ,[RelevanceStatus]
           ,[CreateDate]
           ,[DLM]
           ,[StatusID]
            )
    SELECT [Code]
          ,[Marked]
          ,[Name]
          ,[Parents]
          ,[Article]
          ,[Weighted]
          ,[GUID_Mercury]
          ,[KeepTrackOfCharacteristics]
          ,[NameFull]
          ,[Comment]
          ,[IsService]
          ,[IsProduct]
          ,[AdditionalDescriptionOfNomenclature]
          ,[NomenclatureGroupCost]
          ,[NomenclatureGroup]
          ,[ArticleCost]
          ,[Brand]
          ,[NomenclatureType]
          ,[VATRate]
          ,[Unit]
          ,[Weight]
          ,[boxTypeID]
          ,[boxTypeName]
          ,[packTypeID]
          ,[packTypeName]
          ,[SerializedRepresentationObject]
          ,7
          ,2
          ,[CodeInIS]
          ,1
          ,GETDATE()
          ,GETDATE()
          ,1
    FROM [DW].[DimNomenclatures]
    WHERE [ID] = @Id;

    SELECT @MasterID = @@IDENTITY;

    UPDATE [DW].[DimNomenclatures]
	SET 
		[NormalizationStatus] = 1
		,[RelevanceStatus] = 1
		,[MasterId] = @MasterId
	WHERE 
		[Id]  = @Id;

    UPDATE [DW].[DimNomenclatures]
	SET 
		[MasterId] = @MasterId
	WHERE 
		[Id]  = @MasterId;

	RETURN 1;
END
GO