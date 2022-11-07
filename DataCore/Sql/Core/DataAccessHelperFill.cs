// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableDwhModels;
using DataCore.Sql.Tables;
using DataCore.Sql.Xml;

namespace DataCore.Sql.Core;

public partial class DataAccessHelper
{
	#region Public and private methods

	public void FillReferences<T>(T? item) where T : SqlTableBase, new()
	{
		switch (item)
		{
			case XmlDeviceModel xmlDevice:
                xmlDevice.Scale = GetItemNotNull<ScaleModel>(xmlDevice.Scale.IdentityValueId);
				break;
			case LogModel log:
				log.App = GetItemNotNull<AppModel>(log.App?.IdentityValueUid);
				log.Device = GetItemNullable<DeviceModel>(log.Device?.IdentityValueUid);
				log.LogType = GetItemNotNull<LogTypeModel>(log.LogType?.IdentityValueUid);
				break;
			// Scales.
			case BarCodeModel barcode:
				barcode.PluLabel = GetItemNotNull<PluLabelModel>(barcode.PluLabel.IdentityValueUid);
				break;
			case DeviceTypeFkModel deviceTypeFk:
				deviceTypeFk.Device = GetItemNotNull<DeviceModel>(deviceTypeFk.Device.IdentityValueUid);
				deviceTypeFk.DeviceType = GetItemNotNull<DeviceTypeModel>(deviceTypeFk.DeviceType.IdentityValueUid);
				break;
			case DeviceScaleFkModel deviceScaleFk:
				deviceScaleFk.Device = GetItemNotNull<DeviceModel>(deviceScaleFk.Device.IdentityValueUid);
				deviceScaleFk.Scale = GetItemNotNull<ScaleModel>(deviceScaleFk.Scale.IdentityValueId);
				break;
			case OrderWeighingModel orderWeighing:
                orderWeighing.Order = GetItemNotNull<OrderModel>(orderWeighing.Order.IdentityValueUid);
                orderWeighing.PluWeighing = GetItemNotNull<PluWeighingModel>(orderWeighing.PluWeighing.IdentityValueUid);
				break;
			case PluModel plu:
                plu.Template = GetItemNotNull<TemplateModel>(plu.Template.IdentityValueId);
                plu.Nomenclature = GetItemNotNull<TableScaleModels.NomenclatureModel>(plu.Nomenclature.IdentityValueId);
				break;
			case PluLabelModel pluLabel:
				pluLabel.PluWeighing = GetItemNullable<PluWeighingModel>(pluLabel.PluWeighing?.IdentityValueUid);
                pluLabel.PluScale = GetItemNotNull<PluScaleModel>(pluLabel.PluScale.IdentityValueUid);
                break;
			case PluPackageModel pluPackage:
				pluPackage.Plu = GetItemNotNull<PluModel>(pluPackage.Plu.IdentityValueUid);
                pluPackage.Package = GetItemNotNull<PackageModel>(pluPackage.Package.IdentityValueUid);
				break;
			case PluScaleModel pluScale:
                pluScale.Plu = GetItemNotNull<PluModel>(pluScale.Plu.IdentityValueUid);
                pluScale.Scale = GetItemNotNull<ScaleModel>(pluScale.Scale.IdentityValueId);
				break;
			case PluWeighingModel pluWeighing:
                pluWeighing.PluScale = GetItemNotNull<PluScaleModel>(pluWeighing.PluScale.IdentityValueUid);
				pluWeighing.Series = GetItemNullable<ProductSeriesModel>(pluWeighing.Series?.IdentityValueId);
				break;
			case PrinterModel printer:
                printer.PrinterType = GetItemNotNull<PrinterTypeModel>(printer.PrinterType.IdentityValueId);
				break;
			case PrinterResourceModel printerResource:
                printerResource.Printer = GetItemNotNull<PrinterModel>(printerResource.Printer.IdentityValueId);
                printerResource.TemplateResource = GetItemNotNull<TemplateResourceModel>(printerResource.TemplateResource.IdentityValueId);
				if (string.IsNullOrEmpty(printerResource.TemplateResource.Description))
					printerResource.TemplateResource.Description = printerResource.TemplateResource.Name;
				break;
			case ProductSeriesModel product:
                product.Scale = GetItemNotNull<ScaleModel>(product.Scale.IdentityValueId);
				break;
			case ScaleModel scale:
				scale.TemplateDefault = GetItemNullable<TemplateModel>(scale.TemplateDefault?.IdentityValueId);
				scale.TemplateSeries = GetItemNullable<TemplateModel>(scale.TemplateSeries?.IdentityValueId);
				scale.PrinterMain = GetItemNullable<PrinterModel>(scale.PrinterMain?.IdentityValueId);
				scale.PrinterShipping = GetItemNullable<PrinterModel>(scale.PrinterShipping?.IdentityValueId);
				scale.WorkShop = GetItemNullable<WorkShopModel>(scale.WorkShop?.IdentityValueId);
				break;
			case ScaleScreenShotModel scaleScreenShot:
				scaleScreenShot.Scale = GetItemNotNull<ScaleModel>(scaleScreenShot.Scale.IdentityValueId);
				break;
			case TaskModel task:
                task.TaskType = GetItemNotNull<TaskTypeModel>(task.TaskType.IdentityValueUid);
                task.Scale = GetItemNotNull<ScaleModel>(task.Scale.IdentityValueId);
				break;
			case WorkShopModel workshop:
                workshop.ProductionFacility = GetItemNotNull<ProductionFacilityModel>(workshop.ProductionFacility.IdentityValueId);
				break;
			// Dwh.
			case BrandModel brand:
                brand.InformationSystem = GetItemNotNull<InformationSystemModel>(brand.InformationSystem.IdentityValueId);
				break;
			case TableDwhModels.NomenclatureModel nomenclatureDwh:
                nomenclatureDwh.Status = GetItemNotNull<StatusModel>(nomenclatureDwh.Status.IdentityValueId);
				break;
			case NomenclatureGroupModel nomenclatureGroup:
                nomenclatureGroup.InformationSystem = GetItemNotNull<InformationSystemModel>(nomenclatureGroup.InformationSystem.IdentityValueId);
				break;
			case NomenclatureLightModel nomenclatureLight:
                nomenclatureLight.InformationSystem = GetItemNotNull<InformationSystemModel>(nomenclatureLight.InformationSystem.IdentityValueId);
				break;
			case NomenclatureTypeModel nomenclatureType:
                nomenclatureType.InformationSystem = GetItemNotNull<InformationSystemModel>(nomenclatureType.InformationSystem.IdentityValueId);
				break;
		}
	}

	#endregion
}
