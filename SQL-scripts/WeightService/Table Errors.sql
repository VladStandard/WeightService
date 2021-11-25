----------------------------------------------------------------------------------------------------
-- Таблица Errors
----------------------------------------------------------------------------------------------------
IF NOT EXISTS (SELECT *
		FROM [sys].[tables]
		WHERE [name] = 'Errors'
		AND type = 'U')
BEGIN
	CREATE TABLE [db_scales].[Errors] (
		[Id] INT IDENTITY (1, 1) NOT NULL
	   ,[CreatedDate] DATETIME DEFAULT (GETDATE()) NOT NULL
	   ,[ModifiedDate] DATETIME DEFAULT (GETDATE()) NOT NULL
	   ,[FilePath] NVARCHAR(1024) NULL
	   ,[LineNumber] SMALLINT NULL
	   ,[MemberName] NVARCHAR(128) NULL
	   ,[Exception] NVARCHAR(4000) NOT NULL
	   ,[InnerException] NVARCHAR(4000) NULL
	   ,PRIMARY KEY CLUSTERED ([Id] ASC) ON [ScalesFileGroup]
	   ,
	) ON [ScalesFileGroup]
END
----------------------------------------------------------------------------------------------------
SELECT
	*
FROM [db_scales].[Errors]
ORDER BY [CreatedDate] DESC
----------------------------------------------------------------------------------------------------
