UPDATE [ScalesDB].[db_scales].[PLU] 
SET  [TemplateID]= null;

UPDATE [ScalesDB].[db_scales].[Scales] 
SET [TemplateIdDefault] = null,[TemplateIdSeries]=null;

DELETE FROM [ScalesDB].[db_scales].[Templates] ;

INSERT INTO [ScalesDB].[db_scales].[Templates]
           (
		   [CategoryID]
           ,[IdRRef]
           ,[Title]
           ,[ImageData]
           )

SELECT 
      [CategoryID]
      ,[1CTemplateID]
      ,[Title]
      ,[ImageData]
  FROM [ScaleDB_old].[db_scales].[Templates]
  WHERE LEN([ImageData])>10;

UPDATE [ScalesDB].[db_scales].[Scales] 
SET [TemplateIdDefault] = (SELECT MAX(Id) FROM [ScalesDB].[db_scales].[Templates])

UPDATE [ScalesDB].[db_scales].[PLU] 
SET [TemplateID] = (SELECT MAX(Id)-1 FROM [ScalesDB].[db_scales].[Templates])