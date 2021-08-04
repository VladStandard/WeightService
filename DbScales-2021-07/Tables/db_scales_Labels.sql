CREATE TABLE [db_scales].[Labels]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[WeithingFactId] int NOT NULL FOREIGN KEY REFERENCES [db_scales].[WeithingFact] (Id),
	[Label] varbinary(max), 
    [CreateDate] DATETIME NOT NULL DEFAULT GETDATE()

) ON [ScalesFileGroupLargeData]
