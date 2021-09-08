CREATE TABLE [DW].[MineBaseSalesOfGoods]
(
	[Id]					BIGINT NOT NULL  IDENTITY(-9223372036854775808,1),
	[CreateDate]			datetime NOT NULL DEFAULT GETDATE(),
	[DLM]					datetime  NOT NULL DEFAULT GETDATE(),
	[InformationSystemID]	int NOT NULL,

	NomenclatureID			varbinary(16),
	ContragentID			varbinary(16),
	DeliveryPlaceID			varbinary(16),
	RegionID				varbinary(16),
	[DateId]				int NOT NULL,

	_NomenclatureID			bigint,
	_ContragentID			bigint,
	_DeliveryPlaceID		bigint,
	_RegionID				bigint,
	_Date					date,

	[Method]	nvarchar(20),
	[Qty]		decimal(15,3),
    [Price]		decimal(15,2),
    [Cost]		decimal(15,2),
    PRIMARY KEY CLUSTERED ([NomenclatureID] ASC, [ContragentID] ASC, [DeliveryPlaceID] ASC, [DateId] ASC, [ID] ASC),

) on [FACTFileGrooup]
GO
