CREATE TABLE [db_sscc].[SSCCStorage] (
    [GLN]     INT NOT NULL,
    [COUNTER] INT DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([GLN] ASC)
);


GO
GRANT UPDATE
    ON OBJECT::[db_sscc].[SSCCStorage] TO [db_scales_users]
    AS [scales_owner];


GO
GRANT SELECT
    ON OBJECT::[db_sscc].[SSCCStorage] TO [db_scales_users]
    AS [scales_owner];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'таблица обеспечивает хранение текушей упаковки продукции в рамках предприятия', @level0type = N'SCHEMA', @level0name = N'db_sscc', @level1type = N'TABLE', @level1name = N'SSCCStorage';

