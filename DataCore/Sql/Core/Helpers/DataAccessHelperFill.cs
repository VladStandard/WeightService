// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;
using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;
using DataCore.Sql.TableScaleFkModels.LogsWebsFks;
using DataCore.Sql.TableScaleFkModels.PlusBrandsFks;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleFkModels.PlusCharacteristicsFks;
using DataCore.Sql.TableScaleFkModels.PlusClipsFks;
using DataCore.Sql.TableScaleFkModels.PlusFks;
using DataCore.Sql.TableScaleFkModels.PlusGroupsFks;
using DataCore.Sql.TableScaleFkModels.PlusNestingFks;
using DataCore.Sql.TableScaleFkModels.PlusStorageMethodsFks;
using DataCore.Sql.TableScaleFkModels.PlusTemplatesFks;
using DataCore.Sql.TableScaleFkModels.PrintersResourcesFks;
using DataCore.Sql.TableScaleModels.Apps;
using DataCore.Sql.TableScaleModels.BarCodes;
using DataCore.Sql.TableScaleModels.Boxes;
using DataCore.Sql.TableScaleModels.Brands;
using DataCore.Sql.TableScaleModels.Bundles;
using DataCore.Sql.TableScaleModels.Clips;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.DeviceTypes;
using DataCore.Sql.TableScaleModels.Logs;
using DataCore.Sql.TableScaleModels.LogsTypes;
using DataCore.Sql.TableScaleModels.LogsWebs;
using DataCore.Sql.TableScaleModels.Orders;
using DataCore.Sql.TableScaleModels.OrdersWeighings;
using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.PlusCharacteristics;
using DataCore.Sql.TableScaleModels.PlusGroups;
using DataCore.Sql.TableScaleModels.PlusLabels;
using DataCore.Sql.TableScaleModels.PlusScales;
using DataCore.Sql.TableScaleModels.PlusStorageMethods;
using DataCore.Sql.TableScaleModels.PlusWeighings;
using DataCore.Sql.TableScaleModels.Printers;
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

namespace DataCore.Sql.Core.Helpers;

public partial class DataAccessHelper
{
	#region Public and private methods

    private void FillReferences<T>(T? item, bool isFillReferences) where T : SqlTableBase, new()
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
            case DeviceTypeFkModel deviceTypeFk:
				deviceTypeFk.Device = GetItemNotNullable<DeviceModel>(deviceTypeFk.Device.IdentityValueUid);
				deviceTypeFk.Type = GetItemNotNullable<DeviceTypeModel>(deviceTypeFk.Type.IdentityValueUid);
				break;
			case DeviceScaleFkModel deviceScaleFk:
				deviceScaleFk.Device = GetItemNotNullable<DeviceModel>(deviceScaleFk.Device.IdentityValueUid);
				deviceScaleFk.Scale = GetItemNotNullable<ScaleModel>(deviceScaleFk.Scale.IdentityValueId);
				break;
            case LogWebFkModel logWebFk:
                logWebFk.LogWebRequest = GetItemNotNullable<LogWebModel>(logWebFk.LogWebRequest.IdentityValueUid);
                logWebFk.LogWebResponse = GetItemNotNullable<LogWebModel>(logWebFk.LogWebResponse.IdentityValueUid);
                logWebFk.App = GetItemNotNullable<AppModel>(logWebFk.App.IdentityValueUid);
                logWebFk.LogType = GetItemNotNullable<LogTypeModel>(logWebFk.LogType.IdentityValueUid);
                logWebFk.Device = GetItemNotNullable<DeviceModel>(logWebFk.Device.IdentityValueUid);
                break;
            case PluFkModel pluFk:
                pluFk.Plu = GetItemNotNullable<PluModel>(pluFk.Plu.IdentityValueUid);
                pluFk.Parent = GetItemNotNullable<PluModel>(pluFk.Parent.IdentityValueUid);
                break;
            case PluBrandFkModel pluBrandFk:
                pluBrandFk.Plu = GetItemNotNullable<PluModel>(pluBrandFk.Plu.IdentityValueUid);
                pluBrandFk.Brand = GetItemNotNullable<BrandModel>(pluBrandFk.Brand.IdentityValueUid);
                break;
            case PluBundleFkModel pluBundle:
                pluBundle.Plu = GetItemNotNullable<PluModel>(pluBundle.Plu.IdentityValueUid);
                pluBundle.Bundle = GetItemNotNullable<BundleModel>(pluBundle.Bundle.IdentityValueUid);
                break;
            case PluClipFkModel pluClip:
                pluClip.Clip = GetItemNotNullable<ClipModel>(pluClip.Clip.IdentityValueUid);
                pluClip.Plu = GetItemNotNullable<PluModel>(pluClip.Plu.IdentityValueUid);
                break;
            case PluLabelModel pluLabel:
                pluLabel.PluWeighing = GetItemNullable<PluWeighingModel>(pluLabel.PluWeighing?.IdentityValueUid);
                pluLabel.PluScale = GetItemNotNullable<PluScaleModel>(pluLabel.PluScale.IdentityValueUid);
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
            case PluNestingFkModel pluNestingFk:
                pluNestingFk.PluBundle = GetItemNotNullable<PluBundleFkModel>(pluNestingFk.PluBundle.IdentityValueUid);
                pluNestingFk.Box = GetItemNotNullable<BoxModel>(pluNestingFk.Box.IdentityValueUid);
                break;
            case PluGroupFkModel pluGroupFk:
                pluGroupFk.PluGroup = GetItemNotNullable<PluGroupModel>(pluGroupFk.PluGroup.IdentityValueUid);
                pluGroupFk.Parent = GetItemNotNullable<PluGroupModel>(pluGroupFk.Parent.IdentityValueUid);
				break;
            case PluCharacteristicsFkModel pluCharacteristicsFk:
                pluCharacteristicsFk.Plu = GetItemNotNullable<PluModel>(pluCharacteristicsFk.Plu.IdentityValueUid);
                pluCharacteristicsFk.Characteristic = GetItemNotNullable<PluCharacteristicModel>(pluCharacteristicsFk.Characteristic.IdentityValueUid);
                break;
            case PluStorageMethodFkModel pluStorageMethod:
                if (isFillReferences)
                {
                    pluStorageMethod.Plu = GetItemNotNullable<PluModel>(pluStorageMethod.Plu.IdentityValueUid);
                    pluStorageMethod.Method = GetItemNotNullable<PluStorageMethodModel>(pluStorageMethod.Method.IdentityValueUid);
                    pluStorageMethod.Resource = GetItemNotNullable<TemplateResourceModel>(pluStorageMethod.Resource.IdentityValueUid);
                }
                else
                {
                    //pluStorageMethod.FillProperties();
                    pluStorageMethod.Plu = new();
                    pluStorageMethod.Method = new();
                    pluStorageMethod.Resource = new();
                }
                break;
            case OrderWeighingModel orderWeighing:
                orderWeighing.Order = GetItemNotNullable<OrderModel>(orderWeighing.Order.IdentityValueUid);
                orderWeighing.PluWeighing = GetItemNotNullable<PluWeighingModel>(orderWeighing.PluWeighing.IdentityValueUid);
				break;
            case PrinterModel printer:
                printer.PrinterType = GetItemNotNullable<PrinterTypeModel>(printer.PrinterType.IdentityValueId);
				break;
			case PrinterResourceFkModel printerResource:
                printerResource.Printer = GetItemNotNullable<PrinterModel>(printerResource.Printer.IdentityValueId);
                printerResource.TemplateResource = GetItemNotNullable<TemplateResourceModel>(printerResource.TemplateResource.IdentityValueUid);
				if (string.IsNullOrEmpty(printerResource.TemplateResource.Description))
					printerResource.TemplateResource.Description = printerResource.TemplateResource.Name;
				break;
			case ProductSeriesModel product:
                product.Scale = GetItemNotNullable<ScaleModel>(product.Scale.IdentityValueId);
				break;
			case ScaleModel scale:
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