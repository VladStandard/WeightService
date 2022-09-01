// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableDwhModels;
using DataCore.Sql.Tables;
using DataCore.Sql.Xml;

namespace DataCore.Sql.Controllers;

public partial class CrudController
{
	#region Public and private methods

	private void FillReferences<T>(T? item) where T : TableModel, new()
	{
		FillReferencesSystem(item);
		FillReferencesDatas(item);
		FillReferencesScales(item);
		FillReferencesDwh(item);
	}

	private void FillReferencesSystem<T>(T? item) where T : TableModel, new()
	{
		if (item == null) return;
		switch (item)
		{
			case LogModel log:
				log.App = log.App?.Identity.Uid == null ? new() : GetItemByUid<AppModel>(log.App.Identity.Uid);
				log.Host = log.Host?.Identity.Id == null ? new() : GetItemById<HostModel>(log.Host.Identity.Id);
				log.LogType = log.LogType?.Identity.Uid == null ? new() : GetItemByUid<LogTypeModel>(log.LogType.Identity.Uid);
				break;
		}
	}

	private void FillReferencesDatas<T>(T? item) where T : TableModel, new()
	{
		if (item == null) return;
		switch (item)
		{
			case DeviceModel device:
				ScaleModel? scale = GetItemById<ScaleModel>(device.Scale.Identity.Id);
				if (scale is not null)
					device.Scale = scale;
				break;
		}
	}

	private void FillReferencesScales<T>(T? item) where T : TableModel, new()
	{
		if (item == null) return;
		switch (item)
		{
			case BarCodeModel barcode:
				barcode.BarcodeType = GetItemByUid<BarCodeTypeModel>(barcode.BarcodeType?.Identity.Uid);
				barcode.Contragent = GetItemByUid<ContragentModel>(barcode.Contragent?.Identity.Uid);
				barcode.Nomenclature = GetItemById<TableScaleModels.NomenclatureModel>(barcode.Nomenclature?.Identity.Id);
				break;
			case OrderWeighingModel orderWeighing:
				OrderModel? order = GetItemByUid<OrderModel>(orderWeighing.Order.Identity.Uid);
				if (order is not null)
					orderWeighing.Order = order;
				PluWeighingModel? pluWeighing1 = GetItemByUid<PluWeighingModel>(orderWeighing.PluWeighing.Identity.Uid);
				if (pluWeighing1 is not null)
					orderWeighing.PluWeighing = pluWeighing1;
				break;
			case PluModel plu:
				TemplateModel? template = GetItemById<TemplateModel>(plu.Template.Identity.Id);
				if (template is not null)
					plu.Template = template;
				TableScaleModels.NomenclatureModel? nomenclature = GetItemById<TableScaleModels.NomenclatureModel>(plu.Nomenclature.Identity.Id);
				if (nomenclature is not null)
					plu.Nomenclature = nomenclature;
				break;
			case PluLabelModel pluLabel:
				PluWeighingModel? pluWeighing2 = GetItemByUid<PluWeighingModel>(pluLabel.PluWeighing?.Identity.Uid);
				if (pluWeighing2 is not null)
					pluLabel.PluWeighing = pluWeighing2;
				break;
			case PluObsoleteModel pluObsolete:
				TemplateModel? template1 = GetItemById<TemplateModel>(pluObsolete.Template.Identity.Id);
				if (template1 is not null)
					pluObsolete.Template = template1;
				ScaleModel? scale1 = GetItemById<ScaleModel>(pluObsolete.Scale.Identity.Id);
				if (scale1 is not null)
					pluObsolete.Scale = scale1;
				TableScaleModels.NomenclatureModel? nomenclature2 = GetItemById<TableScaleModels.NomenclatureModel>(pluObsolete.Nomenclature.Identity.Id);
				if (nomenclature2 is not null)
					pluObsolete.Nomenclature = nomenclature2;
				break;
			case PluScaleModel pluScale:
				PluModel? plu2 = GetItemByUid<PluModel>(pluScale.Plu.Identity.Uid);
				if (plu2 is not null)
					pluScale.Plu = plu2;
				ScaleModel? scale2 = GetItemById<ScaleModel>(pluScale.Scale.Identity.Id);
				if (scale2 is not null)
					pluScale.Scale = scale2;
				break;
			case PluWeighingModel pluWeighing:
				PluScaleModel? pluScale2 = GetItemByUid<PluScaleModel>(pluWeighing.PluScale.Identity.Uid);
				if (pluScale2 is not null)
					pluWeighing.PluScale = pluScale2;
				ProductSeriesModel? productSeries = GetItemById<ProductSeriesModel>(pluWeighing.Series.Identity.Id);
				if (productSeries is not null)
					pluWeighing.Series = productSeries;
				break;
			case PrinterModel printer:
				PrinterTypeModel? printerType = GetItemById<PrinterTypeModel>(printer.PrinterType.Identity.Id);
				if (printerType is not null)
					printer.PrinterType = printerType;
				break;
			case PrinterResourceModel printerResource:
				PrinterModel? printer2 = GetItemById<PrinterModel>(printerResource.Printer.Identity.Id);
				if (printer2 is not null)
					printerResource.Printer = printer2;
				TemplateResourceModel? templateResource2 = GetItemById<TemplateResourceModel>(printerResource.Resource.Identity.Id);
				if (templateResource2 is not null)
					printerResource.Resource = templateResource2;
				if (string.IsNullOrEmpty(printerResource.Resource.Description))
					printerResource.Resource.Description = printerResource.Resource.Name;
				break;
			case ProductSeriesModel product:
				ScaleModel? scale3 = GetItemById<ScaleModel>(product.Scale.Identity.Id);
				if (scale3 is not null)
					product.Scale = scale3;
				break;
			case ScaleModel scale:
				scale.TemplateDefault = GetItemById<TemplateModel>(scale.TemplateDefault?.Identity.Id);
				scale.TemplateSeries = GetItemById<TemplateModel>(scale.TemplateSeries?.Identity.Id);
				scale.PrinterMain = GetItemById<PrinterModel>(scale.PrinterMain?.Identity.Id);
				scale.PrinterShipping = GetItemById<PrinterModel>(scale.PrinterShipping?.Identity.Id);
				scale.Host = GetItemById<HostModel>(scale.Host?.Identity.Id);
				scale.WorkShop = GetItemById<WorkShopModel>(scale.WorkShop?.Identity.Id);
				break;
			case TaskModel task:
				TaskTypeModel? taskType = GetItemByUid<TaskTypeModel>(task.TaskType.Identity.Uid);
				if (taskType is not null)
					task.TaskType = taskType;
				ScaleModel? scale4 = GetItemById<ScaleModel>(task.Scale.Identity.Id);
				if (scale4 is not null)
					task.Scale = scale4;
				break;
			case WorkShopModel workshop:
				ProductionFacilityModel? productionFacility = GetItemById<ProductionFacilityModel>(workshop.ProductionFacility.Identity.Id);
				if (productionFacility is not null)
					workshop.ProductionFacility = productionFacility;
				break;
		}
	}

	private void FillReferencesDwh<T>(T? item) where T : TableModel, new()
	{
		if (item == null) return;
		InformationSystemModel? informationSystem;
		switch (item)
		{
			case BrandModel brand:
				informationSystem = GetItemById<InformationSystemModel>(brand.InformationSystem.Identity.Id);
				if (informationSystem is not null)
					brand.InformationSystem = informationSystem;
				break;
			case TableDwhModels.NomenclatureModel nomenclature:
				StatusModel? status = GetItemById<StatusModel>(nomenclature.Status.Identity.Id);
				if (status is not null)
					nomenclature.Status = status;
				break;
			case NomenclatureGroupModel nomenclatureGroup:
				informationSystem = GetItemById<InformationSystemModel>(nomenclatureGroup.InformationSystem.Identity.Id);
				if (informationSystem is not null)
					nomenclatureGroup.InformationSystem = informationSystem;
				break;
			case NomenclatureLightModel nomenclatureLight:
				informationSystem = GetItemById<InformationSystemModel>(nomenclatureLight.InformationSystem.Identity.Id);
				if (informationSystem is not null)
					nomenclatureLight.InformationSystem = informationSystem;
				break;
			case NomenclatureTypeModel nomenclatureType:
				informationSystem = GetItemById<InformationSystemModel>(nomenclatureType.InformationSystem.Identity.Id);
				if (informationSystem is not null)
					nomenclatureType.InformationSystem = informationSystem;
				break;
		}
	}

	#endregion
}
