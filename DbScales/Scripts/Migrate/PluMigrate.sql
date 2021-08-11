


INSERT INTO [ScalesDB].[db_scales].[PLU]
           ([GoodsName]
           ,[GoodsFullName]
           ,[GoodsDescription]
           ,[TemplateID]
           ,[GTIN]
           ,[EAN13]
           ,[ITF14]
           ,[GoodsShelfLifeDays]
           ,[GoodsTareWeight]
           ,[GoodsBoxQuantly]
           ,[ConsumerName]
           ,[Gln]
           ,[ScaleId]
           ,[NomenclatureId]
           ,[Plu]
           ,[Active])


SELECT 
	   [GoodsName]
      ,[GoodsFullName]
      ,[GoodsDescription]
      ,(
	  SELECT TOP 1 z.ID 
	  FROM [ScalesDB].[db_scales].[Templates] z
	  WHERE  [Title] IN (
	  SELECT TOP 1 CAST( [Title] COLLATE Cyrillic_General_100_CI_AS as nvarchar(250)) 
		FROM [ScaleDB_old].[db_scales].[Templates] x 
		WHERE x.[1CTemplateID] = p.[TemplateID]
	  ))
      ,[GTIN]
      ,[EAN13]
      ,[ITF14]
      ,[GoodsShelfLifeDays]
      ,[GoodsTareWeight]
      ,[GoodsBoxQuantly]
      ,[ConsumerName]
      ,[GLN]
      ,(
		SELECT TOP 1 z.ID 
		FROM [ScalesDB].[db_scales].[Scales] z
		WHERE  [Description] IN (
			SELECT CAST( [Description] COLLATE Cyrillic_General_100_CI_AS as nvarchar(150))
			FROM [ScaleDB_old].[db_scales].[Scales] 
			WHERE [1CRRefID]=p.[1CScaleID]
		)) as ScaleID
      ,1
      ,[PLU]
      ,[Active]
  FROM [ScaleDB_old].[db_scales].[PLU] p