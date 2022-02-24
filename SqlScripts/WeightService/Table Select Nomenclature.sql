------------------------------------------------------------------------------------------------------------------------
-- Table Select Nomenclature
------------------------------------------------------------------------------------------------------------------------
SELECT [N].[Id]
	  ,[N].[Code]
	  ,[N].[Name]
	  ,[N].[IdRRef]
	  ,[N].[SerializedRepresentationObject]
	  ,[N].[CreateDate]
	  ,[N].[ModifiedDate]
	  ,[N].[Weighted]
FROM [db_scales].[Nomenclature] [N]
--WHERE [n].[Id]=-2147422336
ORDER BY [N].[CreateDate] DESC
