// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.BundlesFks;
using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;
using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;
using DataCore.Sql.TableScaleFkModels.NomenclaturesCharacteristicsFks;
using DataCore.Sql.TableScaleFkModels.NomenclaturesGroupsFks;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleFkModels.PlusTemplatesFks;
using DataCore.Sql.TableScaleModels.Apps;
using DataCore.Sql.TableScaleModels.BarCodes;
using DataCore.Sql.TableScaleModels.Boxes;
using DataCore.Sql.TableScaleModels.Bundles;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.DeviceTypes;
using DataCore.Sql.TableScaleModels.Logs;
using DataCore.Sql.TableScaleModels.LogsTypes;
using DataCore.Sql.TableScaleModels.Nomenclatures;
using DataCore.Sql.TableScaleModels.NomenclaturesCharacteristics;
using DataCore.Sql.TableScaleModels.NomenclaturesGroups;
using DataCore.Sql.TableScaleModels.Orders;
using DataCore.Sql.TableScaleModels.OrdersWeighings;
using DataCore.Sql.TableScaleModels.Packages;
using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.PlusLabels;
using DataCore.Sql.TableScaleModels.PlusPackages;
using DataCore.Sql.TableScaleModels.PlusScales;
using DataCore.Sql.TableScaleModels.PlusWeighings;
using DataCore.Sql.TableScaleModels.Printers;
using DataCore.Sql.TableScaleModels.PrintersResources;
using DataCore.Sql.TableScaleModels.PrintersTypes;
using DataCore.Sql.TableScaleModels.ProductionFacilities;
using DataCore.Sql.TableScaleModels.ProductSeries;
using DataCore.Sql.TableScaleModels.Scales;
using DataCore.Sql.TableScaleModels.ScalesScreenshots;
using DataCore.Sql.TableScaleModels.Tasks;
using DataCore.Sql.TableScaleModels.TasksTypes;
using DataCore.Sql.TableScaleModels.Templates;
using DataCore.Sql.TableScaleModels.TemplatesResources;
using DataCore.Sql.TableScaleModels.WorkShops;
using DataCore.Sql.Xml;

namespace DataCore.Sql.Core;

public partial class DataAccessHelper
{
	#region Public and private methods

	public void FillReferences<T>(T? item) where T : class, new()
	{
		switch (item)
		{
			case XmlDeviceModel xmlDevice:
                xmlDevice.Scale = GetItemNotNullable<ScaleModel>(xmlDevice.Scale.IdentityValueId);
				break;
			case LogModel log:
				log.App = GetItemNotNullable<AppModel>(log.App?.IdentityValueUid);
				log.Device = GetItemNullable<DeviceModel>(log.Device?.IdentityValueUid);
				log.LogType = GetItemNotNullable<LogTypeModel>(log.LogType?.IdentityValueUid);
				break;
			// Scales.
			case BarCodeModel barcode:
				barcode.PluLabel = GetItemNotNullable<PluLabelModel>(barcode.PluLabel.IdentityValueUid);
				break;
            case BundleFkModel bundleFk:
                bundleFk.Bundle = GetItemNotNullable<BundleModel>(bundleFk.Bundle.IdentityValueUid);
                bundleFk.Box = GetItemNotNullable<BoxModel>(bundleFk.Box.IdentityValueUid);
                break;
            case DeviceTypeFkModel deviceTypeFk:
				deviceTypeFk.Device = GetItemNotNullable<DeviceModel>(deviceTypeFk.Device.IdentityValueUid);
				deviceTypeFk.Type = GetItemNotNullable<DeviceTypeModel>(deviceTypeFk.Type.IdentityValueUid);
				break;
			case DeviceScaleFkModel deviceScaleFk:
				deviceScaleFk.Device = GetItemNotNullable<DeviceModel>(deviceScaleFk.Device.IdentityValueUid);
				deviceScaleFk.Scale = GetItemNotNullable<ScaleModel>(deviceScaleFk.Scale.IdentityValueId);
				break;
			case NomenclaturesGroupFkModel nomenclatureGroupFk:
                nomenclatureGroupFk.NomenclatureGroup = GetItemNotNullable<NomenclatureGroupModel>(nomenclatureGroupFk.NomenclatureGroup.IdentityValueUid);
                nomenclatureGroupFk.NomenclatureGroupParent = GetItemNotNullable<NomenclatureGroupModel>(nomenclatureGroupFk.NomenclatureGroupParent.IdentityValueUid);
				break;
            case NomenclaturesCharacteristicsFkModel nomenclatureCharacteristicsFk:
                nomenclatureCharacteristicsFk.Nomenclature = GetItemNotNullable<NomenclatureV2Model>(nomenclatureCharacteristicsFk.Nomenclature.IdentityValueUid);
                nomenclatureCharacteristicsFk.NomenclaturesCharacteristics = GetItemNotNullable<NomenclaturesCharacteristicsModel>(nomenclatureCharacteristicsFk.NomenclaturesCharacteristics.IdentityValueUid);
                break;
            case OrderWeighingModel orderWeighing:
                orderWeighing.Order = GetItemNotNullable<OrderModel>(orderWeighing.Order.IdentityValueUid);
                orderWeighing.PluWeighing = GetItemNotNullable<PluWeighingModel>(orderWeighing.PluWeighing.IdentityValueUid);
				break;
			case PluModel plu:
                plu.Nomenclature = GetItemNotNullable<NomenclatureModel>(plu.Nomenclature.IdentityValueId);
				break;
            case PluBundleFkModel pluBundle:
                pluBundle.BundleFk = GetItemNotNullable<BundleFkModel>(pluBundle.BundleFk.IdentityValueUid);
                pluBundle.Plu = GetItemNotNullable<PluModel>(pluBundle.Plu.IdentityValueUid);
                break;
            case PluLabelModel pluLabel:
				pluLabel.PluWeighing = GetItemNullable<PluWeighingModel>(pluLabel.PluWeighing?.IdentityValueUid);
                pluLabel.PluScale = GetItemNotNullable<PluScaleModel>(pluLabel.PluScale.IdentityValueUid);
                break;
			case PluPackageModel pluPackage:
				pluPackage.Plu = GetItemNotNullable<PluModel>(pluPackage.Plu.IdentityValueUid);
                pluPackage.Package = GetItemNotNullable<PackageModel>(pluPackage.Package.IdentityValueUid);
				break;
			case PluScaleModel pluScale:
                pluScale.Plu = GetItemNotNullable<PluModel>(pluScale.Plu.IdentityValueUid);
                pluScale.Scale = GetItemNotNullable<ScaleModel>(pluScale.Scale.IdentityValueId);
				break;
			case PluTemplateFkModel pluTemplateFk:
                pluTemplateFk.Plu = GetItemNotNullable<PluModel>(pluTemplateFk.Plu.IdentityValueUid);
                pluTemplateFk.Template = GetItemNotNullable<TemplateModel>(pluTemplateFk.Template.IdentityValueId);
				break;
			case PluWeighingModel pluWeighing:
                pluWeighing.PluScale = GetItemNotNullable<PluScaleModel>(pluWeighing.PluScale.IdentityValueUid);
				pluWeighing.Series = GetItemNullable<ProductSeriesModel>(pluWeighing.Series?.IdentityValueId);
				break;
			case PrinterModel printer:
                printer.PrinterType = GetItemNotNullable<PrinterTypeModel>(printer.PrinterType.IdentityValueId);
				break;
			case PrinterResourceModel printerResource:
                printerResource.Printer = GetItemNotNullable<PrinterModel>(printerResource.Printer.IdentityValueId);
                printerResource.TemplateResource = GetItemNotNullable<TemplateResourceModel>(printerResource.TemplateResource.IdentityValueId);
				if (string.IsNullOrEmpty(printerResource.TemplateResource.Description))
					printerResource.TemplateResource.Description = printerResource.TemplateResource.Name;
				break;
			case ProductSeriesModel product:
                product.Scale = GetItemNotNullable<ScaleModel>(product.Scale.IdentityValueId);
				break;
			case ScaleModel scale:
				scale.TemplateDefault = GetItemNullable<TemplateModel>(scale.TemplateDefault?.IdentityValueId);
				scale.TemplateSeries = GetItemNullable<TemplateModel>(scale.TemplateSeries?.IdentityValueId);
				scale.PrinterMain = GetItemNullable<PrinterModel>(scale.PrinterMain?.IdentityValueId);
				scale.PrinterShipping = GetItemNullable<PrinterModel>(scale.PrinterShipping?.IdentityValueId);
				scale.WorkShop = GetItemNullable<WorkShopModel>(scale.WorkShop?.IdentityValueId);
				break;
			case ScaleScreenShotModel scaleScreenShot:
				scaleScreenShot.Scale = GetItemNotNullable<ScaleModel>(scaleScreenShot.Scale.IdentityValueId);
				break;
			case TaskModel task:
                task.TaskType = GetItemNotNullable<TaskTypeModel>(task.TaskType.IdentityValueUid);
                task.Scale = GetItemNotNullable<ScaleModel>(task.Scale.IdentityValueId);
				break;
			case WorkShopModel workshop:
                workshop.ProductionFacility = GetItemNotNullable<ProductionFacilityModel>(workshop.ProductionFacility.IdentityValueId);
				break;
		}
	}

	#endregion
}
