CREATE TABLE [db_scales].[ProductionFacility] (
    [Id]           INT              IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (150)   NULL,
    [CreateDate]   DATETIME         DEFAULT (getdate()) NULL,
    [ModifiedDate] DATETIME         DEFAULT (getdate()) NULL,
    [IdRRef]       UNIQUEIDENTIFIER NULL,
    [Marked]       BIT              DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) ON [ScalesFileGroup]
) ON [ScalesFileGroup];


GO
GRANT UPDATE
    ON OBJECT::[db_scales].[ProductionFacility] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT SELECT
    ON OBJECT::[db_scales].[ProductionFacility] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT INSERT
    ON OBJECT::[db_scales].[ProductionFacility] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT DELETE
    ON OBJECT::[db_scales].[ProductionFacility] TO [db_scales_users]
    AS [scales_owner];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Площадки предприятия (изменяется очень редко, не требует интерфейса пользователя для ввода)', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'ProductionFacility';

