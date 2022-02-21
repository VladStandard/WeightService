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
ORDER BY [N].[CreateDate] DESC
