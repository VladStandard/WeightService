// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using DataCore.Sql.Core;
using DataCore.Sql.Tables;

namespace BlazorCore.Models;

public class ItemRazorPageBase<T> : RazorPageBase where T : TableBaseModel, new()
{
	#region Public and private fields, properties, constructor

	public T ItemCast { get => Item is null ? new() : (T)Item; set => Item = value; }

	public ItemRazorPageBase()
	{
		ItemCast = new();
		Table = ItemCast switch
		{
			AccessModel => new TableScaleModel(SqlTableScaleEnum.Accesses),
			BarCodeModel => new TableScaleModel(SqlTableScaleEnum.BarCodes),
			BarCodeTypeModel => new TableScaleModel(SqlTableScaleEnum.BarCodesTypes),
			ContragentModel => new TableScaleModel(SqlTableScaleEnum.Contragents),
			HostModel => new TableScaleModel(SqlTableScaleEnum.Hosts),
			LogModel => new TableScaleModel(SqlTableScaleEnum.Logs),
			NomenclatureModel => new TableScaleModel(SqlTableScaleEnum.Nomenclatures),
			PluLabelModel => new TableScaleModel(SqlTableScaleEnum.PlusLabels),
			PluModel => new TableScaleModel(SqlTableScaleEnum.Plus),
			PluObsoleteModel => new TableScaleModel(SqlTableScaleEnum.PlusObsolete),
			PluScaleModel => new TableScaleModel(SqlTableScaleEnum.PlusScales),
			PluWeighingModel => new TableScaleModel(SqlTableScaleEnum.PlusWeighings),
			PrinterModel => new TableScaleModel(SqlTableScaleEnum.Printers),
			PrinterResourceModel => new TableScaleModel(SqlTableScaleEnum.PrintersResources),
			PrinterTypeModel => new TableScaleModel(SqlTableScaleEnum.PrintersTypes),
			ProductionFacilityModel => new TableScaleModel(SqlTableScaleEnum.ProductionFacilities),
			ScaleModel => new TableScaleModel(SqlTableScaleEnum.Scales),
			TaskModel => new TableScaleModel(SqlTableScaleEnum.Tasks),
			TaskTypeModel => new TableScaleModel(SqlTableScaleEnum.TasksTypes),
			TemplateModel => new TableScaleModel(SqlTableScaleEnum.Templates),
			TemplateResourceModel => new TableScaleModel(SqlTableScaleEnum.TemplatesResources),
			VersionModel => new TableScaleModel(SqlTableScaleEnum.Versions),
			WorkShopModel => new TableScaleModel(SqlTableScaleEnum.WorkShops),
			_ => Table
		};
	}

	#endregion
}
