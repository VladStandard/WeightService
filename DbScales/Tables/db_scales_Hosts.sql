CREATE TABLE [db_scales].[HOSTS]
(
	[ID]            INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[NAME]	        NVARCHAR(150) NULL,
	[IP]		    VARCHAR(15) NULL,
	[MAC]	    	VARCHAR(35) NULL,
	[IDRREF]        UNIQUEIDENTIFIER NOT NULL UNIQUE,
	[CREATEDATE]    DATETIME NOT NULL DEFAULT GETDATE(),
	[MODIFIEDDATE]  DATETIME NOT NULL DEFAULT GETDATE(),
    [MARKED]        BIT NOT NULL DEFAULT 0,
    [SETTINGSFILE]  XML, 
	[ACCESS_DT]     DATETIME NOT NULL DEFAULT GETDATE(),
) ON [ScalesFileGroup]
GO

EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Справочник моноблоков',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'Hosts',
    @level2type = NULL,
    @level2name = NULL
GO
