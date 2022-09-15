// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableDwhModels;
using DataCore.Sql.Tables;
using DataCore.Sql.Xml;

namespace DataCore.Sql.Core;

public static class DataAccessHelperFill
{
	#region Public and private methods

	public static void FillReferences<T>(this DataAccessHelper dataAccess, T? item) where T : SqlTableBase, new()
	{
		InformationSystemModel? informationSystem;
		switch (item)
		{
			case DeviceModel device:
				ScaleModel? scaleDevice = dataAccess.GetItemById<ScaleModel>(device.Scale.IdentityValueId);
				if (scaleDevice is not null)
					device.Scale = scaleDevice;
				break;
			case LogModel log:
				log.App = log.App?.IdentityValueUid == null ? new() : dataAccess.GetItemByUid<AppModel>(log.App.IdentityValueUid);
				log.Host = log.Host?.IdentityValueId == null ? new() : dataAccess.GetItemById<HostModel>(log.Host.IdentityValueId);
				log.LogType = log.LogType?.IdentityValueUid == null ? new() : dataAccess.GetItemByUid<LogTypeModel>(log.LogType.IdentityValueUid);
				break;
			// Scales.
			case BarCodeModel barcode:
				barcode.BarcodeType = dataAccess.GetItemByUid<BarCodeTypeModel>(barcode.BarcodeType?.IdentityValueUid);
				barcode.Contragent = dataAccess.GetItemByUid<ContragentModel>(barcode.Contragent?.IdentityValueUid);
				barcode.Nomenclature = dataAccess.GetItemById<TableScaleModels.NomenclatureModel>(barcode.Nomenclature?.IdentityValueId);
				break;
			case OrderWeighingModel orderWeighing:
				OrderModel? order = dataAccess.GetItemByUid<OrderModel>(orderWeighing.Order.IdentityValueUid);
				if (order is not null)
					orderWeighing.Order = order;
				PluWeighingModel? pluWeighing1 = dataAccess.GetItemByUid<PluWeighingModel>(orderWeighing.PluWeighing.IdentityValueUid);
				if (pluWeighing1 is not null)
					orderWeighing.PluWeighing = pluWeighing1;
				break;
			case PluModel plu:
				TemplateModel? template = dataAccess.GetItemById<TemplateModel>(plu.Template.IdentityValueId);
				if (template is not null)
					plu.Template = template;
				TableScaleModels.NomenclatureModel? nomenclature = dataAccess.GetItemById<TableScaleModels.NomenclatureModel>(plu.Nomenclature.IdentityValueId);
				if (nomenclature is not null)
					plu.Nomenclature = nomenclature;
				break;
			case PluLabelModel pluLabel:
				PluWeighingModel? pluWeighing2 = dataAccess.GetItemByUid<PluWeighingModel>(pluLabel.PluWeighing?.IdentityValueUid);
				if (pluWeighing2 is not null)
					pluLabel.PluWeighing = pluWeighing2;
				break;
			//case PluObsoleteModel pluObsolete:
			//	TemplateModel? template1 = dataAccess.GetItemById<TemplateModel>(pluObsolete.Template.IdentityValueId);
			//	if (template1 is not null)
			//		pluObsolete.Template = template1;
			//	ScaleModel? scale1 = dataAccess.GetItemById<ScaleModel>(pluObsolete.Scale.IdentityValueId);
			//	if (scale1 is not null)
			//		pluObsolete.Scale = scale1;
			//	TableScaleModels.NomenclatureModel? nomenclature2 = dataAccess.GetItemById<TableScaleModels.NomenclatureModel>(pluObsolete.Nomenclature.IdentityValueId);
			//	if (nomenclature2 is not null)
			//		pluObsolete.Nomenclature = nomenclature2;
			//	break;
			case PluScaleModel pluScale:
				PluModel? plu2 = dataAccess.GetItemByUid<PluModel>(pluScale.Plu.IdentityValueUid);
				if (plu2 is not null)
					pluScale.Plu = plu2;
				ScaleModel? scale2 = dataAccess.GetItemById<ScaleModel>(pluScale.Scale.IdentityValueId);
				if (scale2 is not null)
					pluScale.Scale = scale2;
				break;
			case PluWeighingModel pluWeighing:
				PluScaleModel? pluScale2 = dataAccess.GetItemByUid<PluScaleModel>(pluWeighing.PluScale.IdentityValueUid);
				if (pluScale2 is not null)
					pluWeighing.PluScale = pluScale2;
				ProductSeriesModel? productSeries = dataAccess.GetItemById<ProductSeriesModel>(pluWeighing.Series.IdentityValueId);
				if (productSeries is not null)
					pluWeighing.Series = productSeries;
				break;
			case PrinterModel printer:
				PrinterTypeModel? printerType = dataAccess.GetItemById<PrinterTypeModel>(printer.PrinterType.IdentityValueId);
				if (printerType is not null)
					printer.PrinterType = printerType;
				break;
			case PrinterResourceModel printerResource:
				PrinterModel? printer2 = dataAccess.GetItemById<PrinterModel>(printerResource.Printer.IdentityValueId);
				if (printer2 is not null)
					printerResource.Printer = printer2;
				TemplateResourceModel? templateResource2 = dataAccess.GetItemById<TemplateResourceModel>(printerResource.TemplateResource.IdentityValueId);
				if (templateResource2 is not null)
					printerResource.TemplateResource = templateResource2;
				if (string.IsNullOrEmpty(printerResource.TemplateResource.Description))
					printerResource.TemplateResource.Description = printerResource.TemplateResource.Name;
				break;
			case ProductSeriesModel product:
				ScaleModel? scale3 = dataAccess.GetItemById<ScaleModel>(product.Scale.IdentityValueId);
				if (scale3 is not null)
					product.Scale = scale3;
				break;
			case ScaleModel scale:
				scale.TemplateDefault = dataAccess.GetItemById<TemplateModel>(scale.TemplateDefault?.IdentityValueId);
				scale.TemplateSeries = dataAccess.GetItemById<TemplateModel>(scale.TemplateSeries?.IdentityValueId);
				scale.PrinterMain = dataAccess.GetItemById<PrinterModel>(scale.PrinterMain?.IdentityValueId);
				scale.PrinterShipping = dataAccess.GetItemById<PrinterModel>(scale.PrinterShipping?.IdentityValueId);
				scale.Host = dataAccess.GetItemById<HostModel>(scale.Host?.IdentityValueId);
				scale.WorkShop = dataAccess.GetItemById<WorkShopModel>(scale.WorkShop?.IdentityValueId);
				break;
			case TaskModel task:
				TaskTypeModel? taskType = dataAccess.GetItemByUid<TaskTypeModel>(task.TaskType.IdentityValueUid);
				if (taskType is not null)
					task.TaskType = taskType;
				ScaleModel? scale4 = dataAccess.GetItemById<ScaleModel>(task.Scale.IdentityValueId);
				if (scale4 is not null)
					task.Scale = scale4;
				break;
			case WorkShopModel workshop:
				ProductionFacilityModel? productionFacility = dataAccess.GetItemById<ProductionFacilityModel>(workshop.ProductionFacility.IdentityValueId);
				if (productionFacility is not null)
					workshop.ProductionFacility = productionFacility;
				break;
			// Dwh.
			case BrandModel brand:
				informationSystem = dataAccess.GetItemById<InformationSystemModel>(brand.InformationSystem.IdentityValueId);
				if (informationSystem is not null)
					brand.InformationSystem = informationSystem;
				break;
			case TableDwhModels.NomenclatureModel nomenclatureDwh:
				StatusModel? status = dataAccess.GetItemById<StatusModel>(nomenclatureDwh.Status.IdentityValueId);
				if (status is not null)
					nomenclatureDwh.Status = status;
				break;
			case NomenclatureGroupModel nomenclatureGroup:
				informationSystem = dataAccess.GetItemById<InformationSystemModel>(nomenclatureGroup.InformationSystem.IdentityValueId);
				if (informationSystem is not null)
					nomenclatureGroup.InformationSystem = informationSystem;
				break;
			case NomenclatureLightModel nomenclatureLight:
				informationSystem = dataAccess.GetItemById<InformationSystemModel>(nomenclatureLight.InformationSystem.IdentityValueId);
				if (informationSystem is not null)
					nomenclatureLight.InformationSystem = informationSystem;
				break;
			case NomenclatureTypeModel nomenclatureType:
				informationSystem = dataAccess.GetItemById<InformationSystemModel>(nomenclatureType.InformationSystem.IdentityValueId);
				if (informationSystem is not null)
					nomenclatureType.InformationSystem = informationSystem;
				break;
		}
	}

	#endregion
}
