CREATE TABLE [db_scales].[AttributeDefinationList]
(
	
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[AttDefDescription] [nvarchar](250) NULL,
	[Code] [nvarchar](50) NULL,
	[DefaultValue] [nvarchar](1000) NULL,
	[Notes] [nvarchar](1000) NULL

) ON [ScalesFileGroup]
GO
