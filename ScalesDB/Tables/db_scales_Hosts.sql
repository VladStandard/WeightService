CREATE TABLE [db_scales].[Hosts]
(
	[Id]            INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Name]	        NVARCHAR(150),
	[IP]		    VARCHAR(15),
	[MAC]	    	VARCHAR(35),
	[IdRRef]        UNIQUEIDENTIFIER NOT NULL UNIQUE,
	[CreateDate]    DATETIME NOT NULL DEFAULT GETDATE(),
	[ModifiedDate]  DATETIME NOT NULL DEFAULT GETDATE(),
    [Marked]        BIT NOT NULL DEFAULT 0,
    [SettingsFile]  xml

) ON [ScalesFileGroup]
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Справочник моноблоков.',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'Hosts',
    @level2type = NULL,
    @level2name = NULL
GO
