CREATE TABLE [db_scales].[AttributeValues]
(
	
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[AttrDefinionID] INT NOT NULL,
	[OrderID] INT NOT NULL,
	[Value] nvarchar(max)

) ON [ScalesFileGroup]
GO