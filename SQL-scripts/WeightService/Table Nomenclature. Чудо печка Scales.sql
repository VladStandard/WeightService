-- Таблица Nomenclature. Чудо печка Scales.sql
USE [ScalesDB]

INSERT INTO [db_scales].[Nomenclature]([Id],[Code],[Name],[IdRRef],[SerializedRepresentationObject],[CreateDate],[ModifiedDate])
VALUES (-2147422336,'','Чудо печка куриная голень',null,'{"parents":["Мясные продукты","Полуфабрикаты рубленые","яКотлеты Из мраморной говядины 400 г"]}',getdate(),getdate())
SELECT * FROM [ScalesDB].[db_scales].[Nomenclature] where [Name] like 'Чудо печка%'
