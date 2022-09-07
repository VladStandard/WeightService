// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Core;

public enum SqlDbTypeEnum
{
	Empty,
	Debug,
	Release,
}

public enum SqlTableActionEnum
{
	Empty,
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
	Empty,
	InformationSystem,
	Nomenclature,
	NomenclatureMaster,
	NomenclatureNonNormalize,
}

public enum SqlTableScaleEnum
{
	Empty,
	Accesses,
	BarCodes,
	BarCodesTypes,
	Contragents,
	Hosts,
	Logs,
	LogTypes,
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
	Tasks,
	TasksTypes,
	Templates,
	TemplatesResources,
	Versions,
	WorkShops,
}

public enum SqlFieldEnum
{
	Empty,
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
	Empty,
    Id,
    Uid,
}

public enum SqlFieldComparerEnum
{
	Empty,
	Equal,
	NotEqual,
	More,
	Less,
}

public enum SqlFieldOrderDirectionEnum
{
	Empty,
	Asc,
	Desc
}
