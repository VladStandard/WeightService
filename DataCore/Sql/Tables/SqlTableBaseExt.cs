// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable MissingXmlDoc

using System.Globalization;

namespace DataCore.Sql.Tables;

/// <summary>
/// DB table model.
/// </summary>
public static class SqlTableBaseExt
{
	#region Public and private methods

	public static string GetPropertyAsString<T>(this T? item, string propertyName) where T : SqlTableBase, new()
	{
		switch (item)
		{
			case AccessModel access:
				switch (propertyName)
				{
					case $"{nameof(AccessModel.LoginDt)}":
						return StringUtils.FormatDtRus(access.LoginDt, true);
					case $"{nameof(AccessModel.Name)}":
						return access.Name;
					case $"{nameof(AccessModel.Rights)}":
						return DataAccessHelper.Instance.GetAccessRightsDescription(access.Rights);
				}
				break;
			case DeviceModel device:
				switch (propertyName)
				{
					case $"{nameof(DeviceModel.LoginDt)}":
						return StringUtils.FormatDtRus(device.LoginDt, true);
					case $"{nameof(DeviceModel.LogoutDt)}":
						return StringUtils.FormatDtRus(device.LogoutDt, true);
					case $"{nameof(DeviceModel.Name)}":
						return device.Name;
					case $"{nameof(DeviceModel.PrettyName)}":
						return device.PrettyName;
					case $"{nameof(DeviceModel.Ipv4)}":
						return device.Ipv4;
					case $"{nameof(DeviceModel.MacAddress)}":
						return device.MacAddress.ValuePrettyLookMinus;
					case $"{nameof(DeviceTypeModel)}.{nameof(DeviceTypeModel.PrettyName)}":
						DeviceTypeFkModel deviceTypeFk = DataAccessHelper.Instance.GetItemDeviceTypeFkNotNull(device);
						return deviceTypeFk.DeviceType.PrettyName;
				};
				break;
			case DeviceTypeModel deviceType:
				switch (propertyName)
				{
					case nameof(DeviceTypeModel.Name):
						return deviceType.Name;
					case nameof(DeviceTypeModel.PrettyName):
						return deviceType.PrettyName;
				};
				break;
			case DeviceTypeFkModel deviceTypeFk:
				switch (propertyName)
				{
					case $"{nameof(DeviceModel)}.{nameof(DeviceModel.LoginDt)}":
						return StringUtils.FormatDtRus(deviceTypeFk.Device.LoginDt, true, true);
					case $"{nameof(DeviceModel)}.{nameof(DeviceModel.LogoutDt)}":
						return StringUtils.FormatDtRus(deviceTypeFk.Device.LogoutDt, true, true);
					case $"{nameof(DeviceModel)}.{nameof(DeviceModel.Name)}":
						return deviceTypeFk.Device.Name;
					case $"{nameof(DeviceModel)}.{nameof(DeviceModel.PrettyName)}":
						return deviceTypeFk.Device.PrettyName;
					case $"{nameof(DeviceModel)}.{nameof(DeviceModel.Ipv4)}":
						return deviceTypeFk.Device.Ipv4;
					case $"{nameof(DeviceModel)}.{nameof(DeviceModel.MacAddress)}":
						return deviceTypeFk.Device.MacAddress.ValuePrettyLookMinus;
					case $"{nameof(DeviceTypeModel)}.{nameof(DeviceTypeModel.Name)}":
						return deviceTypeFk.DeviceType.Name;
					case $"{nameof(DeviceTypeModel)}.{nameof(DeviceTypeModel.PrettyName)}":
						return deviceTypeFk.DeviceType.PrettyName;
				};
				break;
			case Xml.LogQuickModel logQuick:
				switch (propertyName)
				{
					case $"{nameof(Xml.LogQuickModel.App)}":
						return logQuick.App;
					case $"{nameof(Xml.LogQuickModel.Host)}":
						return logQuick.Host;
					case $"{nameof(Xml.LogQuickModel.Icon)}":
						return logQuick.Icon;
					case $"{nameof(Xml.LogQuickModel.Message)}":
						return logQuick.Message;
					case $"{nameof(Xml.LogQuickModel.Scale)}":
						return logQuick.Scale;
					case $"{nameof(Xml.LogQuickModel.Version)}":
						return logQuick.Version;
				}
				break;
			case OrganizationModel organization:
				switch (propertyName)
				{
					case $"{nameof(OrganizationModel.Name)}":
						return organization.Name;
					case $"{nameof(OrganizationModel.Gln)}":
						return organization.Gln.ToString();
				}
				break;
			case PackageModel package:
				switch (propertyName)
				{
					case $"{nameof(PackageModel.Name)}":
						return package.Name;
					case $"{nameof(PackageModel.Weight)}":
						return package.Weight.ToString(CultureInfo.InvariantCulture);
				}
				break;
			case PluModel plu:
				switch (propertyName)
				{
					case $"{nameof(PluModel.Name)}":
						return plu.Name;
					case $"{nameof(PluModel.Number)}":
						return plu.Number.ToString();
					case $"{nameof(PluModel.ShelfLifeDays)}":
						return plu.ShelfLifeDays.ToString();
					case $"{nameof(PluModel.BoxQuantly)}":
						return plu.BoxQuantly.ToString();
				}
				break;
			case PluScaleModel pluScale:
				switch (propertyName)
				{
					case $"{nameof(PluModel)}.{nameof(PluModel.Name)}":
						return pluScale.Plu.Name;
					case $"{nameof(PluModel)}.{nameof(PluModel.FullName)}":
						return pluScale.Plu.FullName;
				}
				break;
			case PrinterModel printer:
				switch (propertyName)
				{
					case $"{nameof(PrinterModel.Ip)}":
						return printer.Ip;
					case $"{nameof(PrinterModel.Name)}":
						return printer.Name;
					case $"{nameof(PrinterModel.MacAddress)}":
						return printer.MacAddress.ValuePrettyLookMinus;
					case $"{nameof(PrinterTypeModel)}.{nameof(PrinterTypeModel.Name)}":
						return printer.PrinterType.Name;
				};
				break;
			case PrinterResourceModel printerResource:
				switch (propertyName)
				{
					case $"{nameof(PrinterModel)}.{nameof(PrinterModel.Name)}":
						return printerResource.Printer.Name;
					case $"{nameof(TemplateResourceModel)}.{nameof(TemplateResourceModel.Name)}":
						return printerResource.TemplateResource.Name;
				};
				break;
			case PrinterTypeModel printerType:
				switch (propertyName)
				{
					case $"{nameof(PrinterTypeModel.Name)}":
						return printerType.Name;
				};
				break;
			case ProductionFacilityModel productionFacility:
				switch (propertyName)
				{
					case $"{nameof(ProductionFacilityModel.Address)}":
						return productionFacility.Address;
					case $"{nameof(ProductionFacilityModel.Name)}":
						return productionFacility.Name;
				};
				break;
			case ScaleModel scale:
				switch (propertyName)
				{
					case $"{nameof(ScaleModel.PrinterMain)}.{nameof(ScaleModel.PrinterMain.Name)}":
						return scale.PrinterMain is not null ? scale.PrinterMain.Name : LocaleCore.Table.FieldNull;
					case $"{nameof(ScaleModel.PrinterShipping)}.{nameof(ScaleModel.PrinterShipping.Name)}":
						return scale.PrinterShipping is not null ? scale.PrinterShipping.Name : LocaleCore.Table.FieldNull;
					case $"{nameof(WorkShopModel)}.{nameof(WorkShopModel.Name)}":
						return scale.WorkShop is not null ? scale.WorkShop.Name : LocaleCore.Table.FieldNull;
					case $"{nameof(ScaleModel.Number)}":
						return scale.Number.ToString();
					case $"{nameof(DeviceModel)}.{nameof(DeviceModel.Name)}":
						return DataAccessHelper.Instance.GetItemDeviceNotNull(scale).Name;
					case $"{nameof(DeviceModel)}.{nameof(DeviceModel.Ipv4)}":
						return DataAccessHelper.Instance.GetItemDeviceNotNull(scale).Ipv4;
				}
				break;
			case VersionModel version:
				switch (propertyName)
				{
					case nameof(version.ReleaseDt):
						return StringUtils.FormatDtRus(version.ReleaseDt, false, false);
					case nameof(version.Version):
						return version.Version.ToString();
				};
				break;
			case WorkShopModel workShop:
				switch (propertyName)
				{
					case $"{nameof(WorkShopModel.Name)}":
						return workShop.Name;
					case $"{nameof(ProductionFacilityModel)}.{nameof(ProductionFacilityModel.Name)}":
						return workShop.ProductionFacility.Name;
				};
				break;
		}
		if (item is SqlTableBase sqlTable)
		{
			switch (propertyName)
			{
				case nameof(SqlTableBase.CreateDt):
					return StringUtils.FormatDtRus(sqlTable.CreateDt, true, true);
				case nameof(SqlTableBase.ChangeDt):
					return StringUtils.FormatDtRus(sqlTable.ChangeDt, true, true);
				case nameof(SqlTableBase.Description):
					return sqlTable.Description;
			};
		}
		return LocaleCore.Table.FieldNotFound;
	}

	public static bool GetPropertyAsBool<T>(this T? item, string propertyName) where T : SqlTableBase, new()
	{
		object? value = GetPropertyValue(item, propertyName);
		if (value is bool isValue)
			return isValue;
		return false;
	}

	public static object? GetPropertyValue<T>(this T? item, string propertyName) where T : SqlTableBase, new()
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
