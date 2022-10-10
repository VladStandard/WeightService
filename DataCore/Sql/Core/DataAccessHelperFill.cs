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
		switch (item)
		{
			case DeviceModel device:
                device.Scale = dataAccess.GetItemByIdNotNull<ScaleModel>(device.Scale.IdentityValueId);
				break;
			case LogModel log:
				log.App = dataAccess.GetItemByUidNotNull<AppModel>(log.App?.IdentityValueUid);
				log.Host = dataAccess.GetItemByIdNotNull<HostModel>(log.Host?.IdentityValueId);
				log.LogType = dataAccess.GetItemByUidNotNull<LogTypeModel>(log.LogType?.IdentityValueUid);
				break;
			// Scales.
			case BarCodeModel barcode:
				barcode.BarcodeType = dataAccess.GetItemByUid<BarCodeTypeModel>(barcode.BarcodeType?.IdentityValueUid);
				barcode.Contragent = dataAccess.GetItemByUid<ContragentModel>(barcode.Contragent?.IdentityValueUid);
				barcode.Nomenclature = dataAccess.GetItemById<TableScaleModels.NomenclatureModel>(barcode.Nomenclature?.IdentityValueId);
				break;
			case OrderWeighingModel orderWeighing:
                orderWeighing.Order = dataAccess.GetItemByUidNotNull<OrderModel>(orderWeighing.Order.IdentityValueUid);
                orderWeighing.PluWeighing = dataAccess.GetItemByUidNotNull<PluWeighingModel>(orderWeighing.PluWeighing.IdentityValueUid);
				break;
			case PluModel plu:
                plu.Template = dataAccess.GetItemByIdNotNull<TemplateModel>(plu.Template.IdentityValueId);
                plu.Nomenclature = dataAccess.GetItemByIdNotNull<TableScaleModels.NomenclatureModel>(plu.Nomenclature.IdentityValueId);
				break;
			case PluLabelModel pluLabel:
				pluLabel.PluWeighing = dataAccess.GetItemByUid<PluWeighingModel>(pluLabel.PluWeighing?.IdentityValueUid);
				break;
			case PluPackageModel pluPackage:
				pluPackage.Plu = dataAccess.GetItemByUidNotNull<PluModel>(pluPackage.Plu.IdentityValueUid);
                pluPackage.Package = dataAccess.GetItemByUidNotNull<PackageModel>(pluPackage.Package.IdentityValueUid);
				break;
			case PluScaleModel pluScale:
                pluScale.Plu = dataAccess.GetItemByUidNotNull<PluModel>(pluScale.Plu.IdentityValueUid);
                pluScale.Scale = dataAccess.GetItemByIdNotNull<ScaleModel>(pluScale.Scale.IdentityValueId);
				break;
			case PluWeighingModel pluWeighing:
                pluWeighing.PluScale = dataAccess.GetItemByUidNotNull<PluScaleModel>(pluWeighing.PluScale.IdentityValueUid);
				pluWeighing.Series = dataAccess.GetItemById<ProductSeriesModel>(pluWeighing.Series?.IdentityValueId);
				break;
			case PrinterModel printer:
                printer.PrinterType = dataAccess.GetItemByIdNotNull<PrinterTypeModel>(printer.PrinterType.IdentityValueId);
				break;
			case PrinterResourceModel printerResource:
                printerResource.Printer = dataAccess.GetItemByIdNotNull<PrinterModel>(printerResource.Printer.IdentityValueId);
                printerResource.TemplateResource = dataAccess.GetItemByIdNotNull<TemplateResourceModel>(printerResource.TemplateResource.IdentityValueId);
				if (string.IsNullOrEmpty(printerResource.TemplateResource.Description))
					printerResource.TemplateResource.Description = printerResource.TemplateResource.Name;
				break;
			case ProductSeriesModel product:
                product.Scale = dataAccess.GetItemByIdNotNull<ScaleModel>(product.Scale.IdentityValueId);
				break;
			case ScaleModel scale:
				scale.TemplateDefault = dataAccess.GetItemById<TemplateModel>(scale.TemplateDefault?.IdentityValueId);
				scale.TemplateSeries = dataAccess.GetItemById<TemplateModel>(scale.TemplateSeries?.IdentityValueId);
				scale.PrinterMain = dataAccess.GetItemById<PrinterModel>(scale.PrinterMain?.IdentityValueId);
				scale.PrinterShipping = dataAccess.GetItemById<PrinterModel>(scale.PrinterShipping?.IdentityValueId);
				scale.Host = dataAccess.GetItemById<HostModel>(scale.Host?.IdentityValueId);
				scale.WorkShop = dataAccess.GetItemById<WorkShopModel>(scale.WorkShop?.IdentityValueId);
				break;
			case ScaleScreenShotModel scaleScreenShot:
				scaleScreenShot.Scale = dataAccess.GetItemByIdNotNull<ScaleModel>(scaleScreenShot.Scale.IdentityValueId);
				break;
			case TaskModel task:
                task.TaskType = dataAccess.GetItemByUidNotNull<TaskTypeModel>(task.TaskType.IdentityValueUid);
                task.Scale = dataAccess.GetItemByIdNotNull<ScaleModel>(task.Scale.IdentityValueId);
				break;
			case WorkShopModel workshop:
                workshop.ProductionFacility = dataAccess.GetItemByIdNotNull<ProductionFacilityModel>(workshop.ProductionFacility.IdentityValueId);
				break;
			// Dwh.
			case BrandModel brand:
                brand.InformationSystem = dataAccess.GetItemByIdNotNull<InformationSystemModel>(brand.InformationSystem.IdentityValueId);
				break;
			case TableDwhModels.NomenclatureModel nomenclatureDwh:
                nomenclatureDwh.Status = dataAccess.GetItemByIdNotNull<StatusModel>(nomenclatureDwh.Status.IdentityValueId);
				break;
			case NomenclatureGroupModel nomenclatureGroup:
                nomenclatureGroup.InformationSystem = dataAccess.GetItemByIdNotNull<InformationSystemModel>(nomenclatureGroup.InformationSystem.IdentityValueId);
				break;
			case NomenclatureLightModel nomenclatureLight:
                nomenclatureLight.InformationSystem = dataAccess.GetItemByIdNotNull<InformationSystemModel>(nomenclatureLight.InformationSystem.IdentityValueId);
				break;
			case NomenclatureTypeModel nomenclatureType:
                nomenclatureType.InformationSystem = dataAccess.GetItemByIdNotNull<InformationSystemModel>(nomenclatureType.InformationSystem.IdentityValueId);
				break;
		}
	}

	#endregion
}
