// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Core;

public enum SqlDbTypeEnum
{
	Debug,
	Release,
}

public enum SqlTableActionEnum
{
	Default,
	Cancel,
	Copy,
	Delete,
	Edit,
	Mark,
	New,
	Reload,
	Save,
}

public enum SqlTableDwhEnum
{
	Default,
	InformationSystem,
	Nomenclature,
	NomenclatureMaster,
	NomenclatureNonNormalize,
}

public enum SqlTableSystemEnum
{
	Default,
	Accesses,
	Logs,
	LogTypes,
	Versions,
	Tasks,
	TasksTypes,
}

public enum SqlTableScaleEnum
{
	Default,
	BarCodes,
	BarCodeTypes,
	Contragents,
	Hosts,
	Nomenclatures,
	Orders,
	OrdersWeighings,
	Organizations,
	Plus,
	PlusLabels,
	PlusObsolete,
	PlusScales,
	PlusWeighings,
	Printers,
	PrintersResources,
	PrintersTypes,
	ProductionFacilities,
	ProductSeries,
	Scales,
	Templates,
	TemplatesResources,
	Workshops,
}

public enum SqlFieldEnum
{
	Default,
	CategoryId,
	ChangeDt,
	CodeInIs,
	CreateDt,
	Description,
	GoodsName,
	HostName,
	IdentityValueId,
	IdentityValueUid,
	IsMarked,
	Name,
	Number,
	Plu,
	PluNumber,
	PrinterId,
	ProductDt,
	ReleaseDt,
	Scale,
	Scale_Id,
	ScaleId,
	Task_Uid,
	Title,
	Type,
	User,
	Value,
	Version,
	WeithingDate,
}

public enum SqlFieldIdentityEnum
{
    Default,
    Id,
    Uid,
}

public enum SqlFieldComparerEnum
{
	Equal,
	NotEqual,
	More,
	Less,
}

public enum SqlFieldOrderDirectionEnum
{
	Asc,
	Desc
}
