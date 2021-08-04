CREATE TABLE [db_scales].[WeithingFact] (
    [Id]           INT              IDENTITY (-2147483648, 1) NOT NULL,
    [PluId]        INT              NOT NULL,
    [ScaleId]      INT              NOT NULL,
    [SeriesId]     INT              NULL,
    [OrderId]      INT              NULL,
    [SSCC]         VARCHAR (50)     NOT NULL,
    [WeithingDate] DATETIME2 (7)    NULL,
    [NetWeight]    NUMERIC (15, 3)  NOT NULL,
    [TareWeight]   NUMERIC (15, 3)  NULL,
    [UUID]         UNIQUEIDENTIFIER DEFAULT (newid()) NULL,
    [ProductDate]  DATE             NULL,
    [RegNum]       INT              NULL,
    [Kneading]     INT              NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) ON [ScalesFileGroupJJ],
    FOREIGN KEY ([OrderId]) REFERENCES [db_scales].[Orders] ([Id]),
    FOREIGN KEY ([ScaleId]) REFERENCES [db_scales].[Scales] ([Id]),
    FOREIGN KEY ([SeriesId]) REFERENCES [db_scales].[ProductSeries] ([Id])
) ON [ScalesFileGroupJJ];


GO
CREATE NONCLUSTERED INDEX [WeithingFact_IDX1_PackageIDProductDate]
    ON [db_scales].[WeithingFact]([SeriesId] ASC, [ProductDate] ASC, [Id] ASC)
    ON [ScalesFileGroupJJ];


GO
GRANT UPDATE
    ON OBJECT::[db_scales].[WeithingFact] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT SELECT
    ON OBJECT::[db_scales].[WeithingFact] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT INSERT
    ON OBJECT::[db_scales].[WeithingFact] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT DELETE
    ON OBJECT::[db_scales].[WeithingFact] TO [db_scales_users]
    AS [scales_owner];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'замес', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'WeithingFact', @level2type = N'COLUMN', @level2name = N'Kneading';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'дата производства', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'WeithingFact', @level2type = N'COLUMN', @level2name = N'ProductDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'вес тары', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'WeithingFact', @level2type = N'COLUMN', @level2name = N'TareWeight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'вес нетто', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'WeithingFact', @level2type = N'COLUMN', @level2name = N'NetWeight';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'дата взвешивания', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'WeithingFact', @level2type = N'COLUMN', @level2name = N'WeithingDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'SSCC код', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'WeithingFact', @level2type = N'COLUMN', @level2name = N'SSCC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ссылка на задание (если есть)', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'WeithingFact', @level2type = N'COLUMN', @level2name = N'OrderId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ссылка на производственную серию', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'WeithingFact', @level2type = N'COLUMN', @level2name = N'SeriesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ссылка на весы', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'WeithingFact', @level2type = N'COLUMN', @level2name = N'ScaleId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ссылка на PLU', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'WeithingFact', @level2type = N'COLUMN', @level2name = N'PluId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Регистрация факта взвешивания', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'WeithingFact';

