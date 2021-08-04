CREATE TABLE [db_scales].[ProductSeries] (
    [Id]         INT              IDENTITY (-2147483648, 1) NOT NULL,
    [ScaleID]    INT              NOT NULL,
    [CreateDate] DATETIME2 (7)    DEFAULT (sysdatetime()) NOT NULL,
    [UUID]       UNIQUEIDENTIFIER DEFAULT (newid()) NULL,
    [IsClose]    BIT              DEFAULT ((0)) NULL,
    [SSCC]       VARCHAR (50)     NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) ON [ScalesFileGroupJJ],
    FOREIGN KEY ([ScaleID]) REFERENCES [db_scales].[Scales] ([Id])
) ON [ScalesFileGroupJJ];


GO
CREATE NONCLUSTERED INDEX [ProductSeries_IDX1_ScaleID]
    ON [db_scales].[ProductSeries]([ScaleID] ASC, [Id] ASC)
    ON [ScalesFileGroupJJ];


GO
CREATE NONCLUSTERED INDEX [ProductSeries_IDX2_SSCC]
    ON [db_scales].[ProductSeries]([SSCC] ASC, [Id] ASC)
    ON [ScalesFileGroupJJ];


GO
GRANT UPDATE
    ON OBJECT::[db_scales].[ProductSeries] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT SELECT
    ON OBJECT::[db_scales].[ProductSeries] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT INSERT
    ON OBJECT::[db_scales].[ProductSeries] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT DELETE
    ON OBJECT::[db_scales].[ProductSeries] TO [db_scales_users]
    AS [scales_owner];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Серии взвешиваний продукциию. Недоступна пользователю на редактирование. Заполняется в процессе операции взвешивания.', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'ProductSeries';

