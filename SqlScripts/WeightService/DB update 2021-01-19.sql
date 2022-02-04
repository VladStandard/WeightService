-- Обновление БД ScalesDB 2021-01-19
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

USE [ScalesDB]
drop table [db_scales].[BarCodes]
drop table [db_scales].[BarCodeTypes]

CREATE TABLE [db_scales].[BarCodeTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NULL,
PRIMARY KEY CLUSTERED 
([Id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [ScalesFileGroup]
) ON [ScalesFileGroup]

CREATE TABLE [db_scales].[BarCodes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[BarCodeTypeId] [int] NOT NULL,
	[NomenclatureId] [int] NOT NULL,
	[NomenclatureUnitId] [int] NULL,
	[ContragentId] [int] NULL,
	[Value] [nvarchar](150) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [ScalesFileGroup]
) ON [ScalesFileGroup]
ALTER TABLE [db_scales].[BarCodes] ADD  DEFAULT (getdate()) FOR [CreateDate]
ALTER TABLE [db_scales].[BarCodes] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
ALTER TABLE [db_scales].[BarCodes]  WITH NOCHECK ADD FOREIGN KEY([BarCodeTypeId]) REFERENCES [db_scales].[BarCodeTypes] ([Id])
ALTER TABLE [db_scales].[BarCodes]  WITH NOCHECK ADD FOREIGN KEY([ContragentId]) REFERENCES [db_scales].[Contragents] ([Id])
ALTER TABLE [db_scales].[BarCodes]  WITH NOCHECK ADD FOREIGN KEY([NomenclatureId]) REFERENCES [db_scales].[Nomenclature] ([Id])
ALTER TABLE [db_scales].[BarCodes]  WITH NOCHECK ADD FOREIGN KEY([NomenclatureUnitId]) REFERENCES [db_scales].[NomenclatureUnits] ([Id])

USE [ScalesDB]
INSERT INTO [db_scales].[BarCodeTypes] ([Name]) VALUES ('Code128')
INSERT INTO [db_scales].[BarCodeTypes] ([Name]) VALUES ('Code39')
INSERT INTO [db_scales].[BarCodeTypes] ([Name]) VALUES ('EAN128')
INSERT INTO [db_scales].[BarCodeTypes] ([Name]) VALUES ('EAN13')
INSERT INTO [db_scales].[BarCodeTypes] ([Name]) VALUES ('EAN8')
INSERT INTO [db_scales].[BarCodeTypes] ([Name]) VALUES ('ITF14')
