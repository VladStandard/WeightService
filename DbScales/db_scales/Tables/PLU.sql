CREATE TABLE [db_scales].[PLU] (
    [Id]                   INT             IDENTITY (1, 1) NOT NULL,
    [GoodsName]            NVARCHAR (150)  NULL,
    [GoodsFullName]        NVARCHAR (MAX)  NULL,
    [GoodsDescription]     NVARCHAR (MAX)  NULL,
    [TemplateID]           INT             NULL,
    [GTIN]                 VARCHAR (150)   NULL,
    [EAN13]                VARCHAR (150)   NULL,
    [ITF14]                VARCHAR (150)   NULL,
    [GoodsShelfLifeDays]   TINYINT         NULL,
    [GoodsTareWeight]      DECIMAL (10, 3) NULL,
    [GoodsBoxQuantly]      INT             NULL,
    [ScaleId]              INT             NOT NULL,
    [NomenclatureId]       INT             NOT NULL,
    [Plu]                  INT             NOT NULL,
    [CreateDate]           DATETIME        DEFAULT (getdate()) NULL,
    [ModifiedDate]         DATETIME        DEFAULT (getdate()) NULL,
    [Active]               BIT             DEFAULT ((1)) NULL,
    [UpperWeightThreshold] DECIMAL (10, 3) NULL,
    [NominalWeight]        DECIMAL (10, 3) NULL,
    [LowerWeightThreshold] DECIMAL (10, 3) NULL,
    [CheckWeight]          BIT             DEFAULT ((1)) NULL,
    [Marked]               BIT             DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC) ON [ScalesFileGroup],
    FOREIGN KEY ([NomenclatureId]) REFERENCES [db_scales].[Nomenclature] ([Id]),
    FOREIGN KEY ([ScaleId]) REFERENCES [db_scales].[Scales] ([Id]),
    FOREIGN KEY ([TemplateID]) REFERENCES [db_scales].[Templates] ([Id])
) ON [ScalesFileGroup] TEXTIMAGE_ON [ScalesFileGroup];


GO
GRANT UPDATE
    ON OBJECT::[db_scales].[PLU] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT SELECT
    ON OBJECT::[db_scales].[PLU] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT INSERT
    ON OBJECT::[db_scales].[PLU] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT DELETE
    ON OBJECT::[db_scales].[PLU] TO [db_scales_users]
    AS [scales_owner];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Справочник единиц взвешивания на устройстве (весах). Часто изменяются пользователями.', @level0type = N'SCHEMA', @level0name = N'db_scales', @level1type = N'TABLE', @level1name = N'PLU';

