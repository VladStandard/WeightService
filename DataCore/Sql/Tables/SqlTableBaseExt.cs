// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MissingXmlDoc

using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;
using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;
using DataCore.Sql.TableScaleModels.Access;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.DeviceTypes;
using DataCore.Sql.TableScaleModels.Organizations;
using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.PlusScales;
using DataCore.Sql.TableScaleModels.Printers;
using DataCore.Sql.TableScaleModels.PrintersResources;
using DataCore.Sql.TableScaleModels.PrintersTypes;
using DataCore.Sql.TableScaleModels.ProductionFacilities;
using DataCore.Sql.TableScaleModels.Scales;
using DataCore.Sql.TableScaleModels.TemplatesResources;
using DataCore.Sql.TableScaleModels.Versions;
using DataCore.Sql.TableScaleModels.WorkShops;
using DataCore.Sql.Xml;
using System.Globalization;
using DataCore.Sql.Core.Helpers;
using DataCore.Sql.Core.Interfaces;
using DataCore.Sql.TableScaleModels.Boxes;
using DataCore.Sql.TableScaleModels.Bundles;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;

namespace DataCore.Sql.Tables;

/// <summary>
/// DB table model.
/// </summary>
public static class SqlTableBaseExt
{
	#region Public and private methods

	public static string GetPropertyAsString<T>(this T? item, string propertyName) where T : ISqlTable
	{
		if (IsPropertyBase(propertyName))
		{
			return GetPropertySqlTable<T>(propertyName, item);
		}
		return item switch
		{
			AccessModel access => GetPropertyAccess(propertyName, access),
			DeviceModel device => GetPropertyDevice(propertyName, device),
			DeviceTypeModel deviceType => GetPropertyDeviceType(propertyName, deviceType),
			DeviceTypeFkModel deviceTypeFk => GetPropertyDeviceTypeFk(propertyName, deviceTypeFk),
			DeviceScaleFkModel deviceScaleFk => GetPropertyDeviceScaleFk(propertyName, deviceScaleFk),
			LogQuickModel logQuick => GetPropertyLogQuick(propertyName, logQuick),
			OrganizationModel organization => GetPropertyOrganization(propertyName, organization),
			BoxModel box => GetPropertyBox(propertyName, box),
			BundleModel bundle => GetPropertyBundle(propertyName, bundle),
            PluBundleFkModel pluBundleFk => GetPropertyPluBundleFk(propertyName, pluBundleFk),
			PluModel plu => GetPropertyPlu(propertyName, plu),
			PluScaleModel pluScale => GetPropertyPluScale(propertyName, pluScale),
			PrinterModel printer => GetPropertyPrinter(propertyName, printer),
			PrinterResourceModel printerResource => GetPropertyPrinterResource(propertyName, printerResource),
			PrinterTypeModel printerType => GetPropertyPrinterType(propertyName, printerType),
			ProductionFacilityModel productionFacility => GetPropertyProductionFacility(propertyName, productionFacility),
			ScaleModel scale => GetPropertyScale(propertyName, scale),
			VersionModel version => GetPropertyVersion(propertyName, version),
			WorkShopModel workShop => GetPropertyWorkShop(propertyName, workShop),
			// Must be at the end.
			_ => GetPropertySqlTable<T>(propertyName, item)
        };
	}

	private static bool IsPropertyBase(string propertyName)
	{
		if (string.IsNullOrEmpty(propertyName)) return false;
		return propertyName switch
		{
			nameof(SqlTableBase.IdentityValueId) => true,
			nameof(SqlTableBase.IdentityValueUid) => true,
			nameof(SqlTableBase.IsNew) => true,
			nameof(SqlTableBase.IsNotNew) => true,
			nameof(SqlTableBase.CreateDt) => true,
			nameof(SqlTableBase.ChangeDt) => true,
			nameof(SqlTableBase.IsMarked) => true,
			nameof(SqlTableBase.Name) => true,
			nameof(SqlTableBase.Description) => true,
			_ => false
        };
	}

	private static string GetPropertySqlTable<T>(string propertyName, T? item) where T : ISqlTable
	{
		if (item is null) return LocaleCore.Table.FieldNotFound;
		return propertyName switch
		{
			nameof(SqlTableBase.IdentityValueId) => $"{item.IdentityValueId}",
			nameof(SqlTableBase.IdentityValueUid) => $"{item.IdentityValueUid}",
			nameof(SqlTableBase.IsNew) => $"{item.IsNew}",
			nameof(SqlTableBase.IsNotNew) => $"{item.IsNotNew}",
			nameof(SqlTableBase.CreateDt) => StringUtils.FormatDtRus(item.CreateDt, true, true),
			nameof(SqlTableBase.ChangeDt) => StringUtils.FormatDtRus(item.ChangeDt, true, true),
			nameof(SqlTableBase.IsMarked) => $"{item.IsMarked}",
			nameof(SqlTableBase.Name) => item.Name,
			nameof(SqlTableBase.Description) => item.Description,
			_ => LocaleCore.Table.FieldNotFound
		};
	}

	private static string GetPropertyAccess(string propertyName, AccessModel access)
	{
		return propertyName switch
		{
			nameof(AccessModel.LoginDt) => StringUtils.FormatDtRus(access.LoginDt, true),
			nameof(AccessModel.Rights) => DataAccessHelper.Instance.GetAccessRightsDescription(access.Rights),
			_ => LocaleCore.Table.FieldNotFound
		};
	}

	private static string GetPropertyDevice(string propertyName, DeviceModel device)
	{
		return propertyName switch
		{
			nameof(DeviceModel.LoginDt) => StringUtils.FormatDtRus(device.LoginDt, true),
			nameof(DeviceModel.LogoutDt) => StringUtils.FormatDtRus(device.LogoutDt, true),
			nameof(DeviceModel.PrettyName) => device.PrettyName,
			nameof(DeviceModel.Ipv4) => device.Ipv4,
			nameof(DeviceModel.MacAddress) => device.MacAddress.ValuePrettyLookMinus,
			$"{nameof(DeviceTypeModel)}.{nameof(DeviceTypeModel.PrettyName)}" => DataAccessHelper.Instance.GetItemDeviceTypeFkNotNullable(device).Type.PrettyName,
			_ => LocaleCore.Table.FieldNotFound
		};
	}

	private static string GetPropertyDeviceType(string propertyName, DeviceTypeModel deviceType)
	{
		return propertyName switch
		{
			nameof(DeviceTypeModel.PrettyName) => deviceType.PrettyName,
			_ => LocaleCore.Table.FieldNotFound
		};
	}

	private static string GetPropertyDeviceTypeFk(string propertyName, DeviceTypeFkModel deviceTypeFk)
	{
		return propertyName switch
		{
			$"{nameof(DeviceTypeFkModel.Device)}.{nameof(DeviceModel.LoginDt)}" => StringUtils.FormatDtRus(
				deviceTypeFk.Device.LoginDt, true, true),
			$"{nameof(DeviceTypeFkModel.Device)}.{nameof(DeviceModel.LogoutDt)}" => StringUtils.FormatDtRus(
				deviceTypeFk.Device.LogoutDt, true, true),
			$"{nameof(DeviceTypeFkModel.Device)}.{nameof(DeviceModel.Name)}" => deviceTypeFk.Device.Name,
			$"{nameof(DeviceTypeFkModel.Device)}.{nameof(DeviceModel.PrettyName)}" => deviceTypeFk.Device.PrettyName,
			$"{nameof(DeviceTypeFkModel.Device)}.{nameof(DeviceModel.Ipv4)}" => deviceTypeFk.Device.Ipv4,
			$"{nameof(DeviceTypeFkModel.Device)}.{nameof(DeviceModel.MacAddress)}" => deviceTypeFk.Device.MacAddress
				.ValuePrettyLookMinus,
			$"{nameof(DeviceTypeFkModel.Type)}.{nameof(DeviceTypeModel.Name)}" => deviceTypeFk.Type.Name,
			$"{nameof(DeviceTypeFkModel.Type)}.{nameof(DeviceTypeModel.PrettyName)}" => deviceTypeFk.Type
				.PrettyName,
			_ => LocaleCore.Table.FieldNotFound
		};
	}

	private static string GetPropertyDeviceScaleFk(string propertyName, DeviceScaleFkModel deviceScaleFk)
	{
		return propertyName switch
		{
			$"{nameof(DeviceScaleFkModel.Device)}.{nameof(DeviceModel.Ipv4)}" => deviceScaleFk.Device.Ipv4,
			$"{nameof(DeviceScaleFkModel.Device)}.{nameof(DeviceModel.LoginDt)}" => StringUtils.FormatDtRus(deviceScaleFk.Device.LoginDt, true, true),
			$"{nameof(DeviceScaleFkModel.Device)}.{nameof(DeviceModel.LogoutDt)}" => StringUtils.FormatDtRus(deviceScaleFk.Device.LogoutDt, true, true),
			$"{nameof(DeviceScaleFkModel.Device)}.{nameof(DeviceModel.MacAddress)}" => deviceScaleFk.Device.MacAddress.ValuePrettyLookMinus,
			$"{nameof(DeviceScaleFkModel.Device)}.{nameof(DeviceModel.Name)}" => deviceScaleFk.Device.Name,
			$"{nameof(DeviceScaleFkModel.Device)}.{nameof(DeviceModel.PrettyName)}" => deviceScaleFk.Device.PrettyName,
			$"{nameof(DeviceScaleFkModel.Scale)}.{nameof(ScaleModel.Description)}" => deviceScaleFk.Scale.Description,
			$"{nameof(DeviceScaleFkModel.Scale)}.{nameof(ScaleModel.Name)}" => deviceScaleFk.Scale.Name,
			_ => LocaleCore.Table.FieldNotFound
		};
	}

	private static string GetPropertyLogQuick(string propertyName, LogQuickModel logQuick)
	{
		return propertyName switch
		{
			nameof(Xml.LogQuickModel.App) => logQuick.App,
			nameof(Xml.LogQuickModel.Host) => logQuick.Host,
			nameof(Xml.LogQuickModel.Icon) => logQuick.Icon,
			nameof(Xml.LogQuickModel.Message) => logQuick.Message,
			nameof(Xml.LogQuickModel.Scale) => logQuick.Scale,
			nameof(Xml.LogQuickModel.Version) => logQuick.Version,
			_ => LocaleCore.Table.FieldNotFound
		};
	}

	private static string GetPropertyOrganization(string propertyName, OrganizationModel organization)
	{
		return propertyName switch
		{
			nameof(OrganizationModel.Gln) => organization.Gln.ToString(),
			_ => LocaleCore.Table.FieldNotFound
		};
	}

	private static string GetPropertyBox(string propertyName, BoxModel box)
	{
		return propertyName switch
		{
			nameof(BoxModel.Weight) => box.Weight.ToString(CultureInfo.InvariantCulture),
			_ => LocaleCore.Table.FieldNotFound
		};
	}

	private static string GetPropertyBundle(string propertyName, BundleModel bundle)
	{
		return propertyName switch
		{
			nameof(BundleModel.Weight) => bundle.Weight.ToString(CultureInfo.InvariantCulture),
			_ => LocaleCore.Table.FieldNotFound
		};
	}

    private static string GetPropertyPluBundleFk(string propertyName, PluBundleFkModel pluBundleFk)
    {
	    return propertyName switch
	    {
		    _ => LocaleCore.Table.FieldNotFound
	    };
    }

	private static string GetPropertyPlu(string propertyName, PluModel plu)
	{
		return propertyName switch
		{
			nameof(PluModel.Number) => plu.Number.ToString(),
			nameof(PluModel.ShelfLifeDays) => plu.ShelfLifeDays.ToString(),
			nameof(PluModel.Code) => plu.Code,
			_ => LocaleCore.Table.FieldNotFound
		}; ;
	}

	private static string GetPropertyPluScale(string propertyName, PluScaleModel pluScale)
	{
		return propertyName switch
		{
			$"{nameof(PluScaleModel.Plu)}.{nameof(PluModel.Name)}" => pluScale.Plu.Name,
			$"{nameof(PluScaleModel.Plu)}.{nameof(PluModel.FullName)}" => pluScale.Plu.FullName,
			_ => LocaleCore.Table.FieldNotFound
		};
	}

	private static string GetPropertyPrinter(string propertyName, PrinterModel printer)
	{
		return propertyName switch
		{
			nameof(PrinterModel.Ip) => printer.Ip,
			nameof(PrinterModel.MacAddress) => printer.MacAddress.ValuePrettyLookMinus,
			$"{nameof(PrinterModel.PrinterType)}.{nameof(PrinterTypeModel.Name)}" => printer.PrinterType.Name,
			_ => LocaleCore.Table.FieldNotFound
		};
	}

	private static string GetPropertyPrinterResource(string propertyName, PrinterResourceModel printerResource)
	{
		return propertyName switch
		{
			$"{nameof(PrinterResourceModel.Printer)}.{nameof(PrinterModel.Name)}" => printerResource.Printer.Name,
			$"{nameof(PrinterResourceModel.TemplateResource)}.{nameof(TemplateResourceDeprecatedModel.Name)}" => printerResource.TemplateResource
				.Name,
			_ => LocaleCore.Table.FieldNotFound
		};
	}

	private static string GetPropertyPrinterType(string propertyName, PrinterTypeModel printerType)
	{
		return propertyName switch
		{
			_ => LocaleCore.Table.FieldNotFound
		};
	}

	private static string GetPropertyProductionFacility(string propertyName, ProductionFacilityModel productionFacility)
	{
		return propertyName switch
		{
			nameof(ProductionFacilityModel.Address) => productionFacility.Address,
			_ => LocaleCore.Table.FieldNotFound
		};
	}

	private static string GetPropertyScale(string propertyName, ScaleModel scale)
	{
		return propertyName switch
		{
			nameof(ScaleModel.Description) => scale.Description,
			nameof(ScaleModel.Number) => scale.Number.ToString(),
			$"{nameof(ScaleModel.PrinterMain)}.{nameof(ScaleModel.PrinterMain.Name)}" => scale.PrinterMain is not null ? scale.PrinterMain.Name : LocaleCore.Table.FieldEmpty,
			$"{nameof(ScaleModel.PrinterShipping)}.{nameof(ScaleModel.PrinterShipping.Name)}" => scale.PrinterShipping is not null ? scale.PrinterShipping.Name : LocaleCore.Table.FieldEmpty,
			$"{nameof(ScaleModel.WorkShop)}.{nameof(WorkShopModel.Name)}" => scale.WorkShop is not null ? scale.WorkShop.Name : LocaleCore.Table.FieldEmpty,
			_ => LocaleCore.Table.FieldNotFound
		};
	}

	private static string GetPropertyVersion(string propertyName, VersionModel version)
	{
		return propertyName switch
		{
			nameof(version.ReleaseDt) => StringUtils.FormatDtRus(version.ReleaseDt, false, false),
			nameof(version.Version) => version.Version.ToString(),
			_ => LocaleCore.Table.FieldNotFound
		};
	}

	private static string GetPropertyWorkShop(string propertyName, WorkShopModel workShop)
	{
		return propertyName switch
		{
			$"{nameof(WorkShopModel.ProductionFacility)}.{nameof(ProductionFacilityModel.Name)}" => workShop.ProductionFacility.Name,
			_ => LocaleCore.Table.FieldNotFound
		};
	}

	public static bool GetPropertyAsBool<T>(this T? item, string propertyName) where T : ISqlTable
    {
		object? value = GetPropertyAsObject(item, propertyName);
		if (value is bool isValue)
			return isValue;
		return false;
	}

	public static object? GetPropertyAsObject<T>(this T? item, string propertyName) where T : ISqlTable
    {
		if (item is not null && !string.IsNullOrEmpty(propertyName))
		{
			//if (propertyName.Contains('.'))
			//{
			//	foreach (PropertyInfo property in typeof(T).GetProperties())
			//	{
			//		if (string.Equals(property.Name, propertyName.Substring(0, propertyName.IndexOf('.'))))
			//		{
			//			T prop = (T)property.GetValue(item);
			//			string subPropertyName = propertyName.Substring(propertyName.IndexOf('.'), propertyName.Length - propertyName.IndexOf('.') - 1);
			//			return GetPropertyValue(prop, subPropertyName);
			//		}
			//	}
			//}
			//else
			{
				foreach (PropertyInfo property in typeof(T).GetProperties())
				{
					if (string.Equals(property.Name, propertyName))
						return property.GetValue(item);
				}
			}
		}
		return null;
	}

	#endregion
}