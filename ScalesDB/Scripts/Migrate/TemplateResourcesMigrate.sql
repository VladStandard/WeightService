
DELETE FROM [ScalesDB].[db_scales].[TemplateResourceRef]
DELETE FROM [ScalesDB].[db_scales].[TemplateResources]
GO

INSERT INTO [ScalesDB].[db_scales].[TemplateResources]([Name],[Type],[ImageData])
SELECT [Name],[Type],CONVERT(varbinary(max),[ImageData])
  FROM [ScaleDB_old].[db_scales].[TemplateResources]
  GROUP BY [Name],[Type],[ImageData]

GO

INSERT INTO [ScalesDB].[db_scales].[TemplateResourceRef]([ResourceID],[TemplateID])
SELECT r.ID,t.ID FROM [ScalesDB].[db_scales].[Templates] t, [ScalesDB].[db_scales].[TemplateResources] r

GO