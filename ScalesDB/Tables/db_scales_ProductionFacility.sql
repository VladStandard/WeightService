CREATE TABLE [db_scales].[ProductionFacility]
(

	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[Name]				[nvarchar](150) NULL,
	[CreateDate]		[datetime] NULL DEFAULT(GETDATE()),
	[ModifiedDate]		[datetime] NULL DEFAULT(GETDATE()), 
	[IdRRef] UNIQUEIDENTIFIER NULL,
    [Marked] BIT NOT NULL DEFAULT 0

) ON [ScalesFileGroup]

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Площадки предприятия (изменяется очень редко, не требует интерфейса пользователя для ввода)',
    @level0type = N'SCHEMA',
    @level0name = N'db_scales',
    @level1type = N'TABLE',
    @level1name = N'ProductionFacility',
    @level2type = NULL,
    @level2name = NULL