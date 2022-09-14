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
		object? value = GetPropertyValue(item, propertyName);
		switch (value)
		{
			case string strValue:
				return strValue;
			case int intValue:
				return intValue.ToString(CultureInfo.InvariantCulture);
			case short shortValue:
				return shortValue.ToString(CultureInfo.InvariantCulture);
			case decimal decValue:
				return decValue.ToString(CultureInfo.InvariantCulture);
			case DateTime dtValue:
				if (item is VersionModel version && string.Equals(propertyName, nameof(version.ReleaseDt)))
				{
					return version.ReleaseDt.ToString("yyyy-MM-dd");
				}
				else
				{
					return dtValue.ToString("yyyy-MM-dd HH:mm:ss");
				}
			case byte byteValue:
				if (item is AccessModel access && string.Equals(propertyName, nameof(access.Rights)))
				{
					return DataAccessHelper.Instance.GetAccessRightsDescription(access.Rights);
				}
				else
				{
					return byteValue.ToString(CultureInfo.InvariantCulture);
				}
			case SqlFieldMacAddressModel macAddress:
				if (item is HostModel host1 && string.Equals(propertyName, nameof(host1.MacAddress)))
				{
					return host1.MacAddress.ValuePrettyLookMinus;
				}
				else
				{
					return macAddress.ValuePrettyLookMinus;
				}
			case HostModel host:
				if (item is ScaleModel scale && string.Equals(propertyName, nameof(scale.Host)))
				{
					return scale.Host is not null ? scale.Host.Name : LocaleCore.Table.FieldNull;
				}
				else
				{
					return host.Name;
				}
			case PrinterModel printer1:
				if (item is ScaleModel scale2)
				{
					if (string.Equals(propertyName, nameof(scale.PrinterMain)))
						return scale2.PrinterMain is not null ? scale2.PrinterMain.Name : LocaleCore.Table.FieldNull;
					if (string.Equals(propertyName, nameof(scale.PrinterShipping)))
						return scale2.PrinterShipping is not null ? scale2.PrinterShipping.Name : LocaleCore.Table.FieldNull;
				}
				if (item is PrinterResourceModel printerResource)
				{
					if (string.Equals(propertyName, nameof(printerResource.Printer)))
						return printerResource.Printer.Name;
				}
				return printer1.Name;
			case PrinterTypeModel printerType:
				if (item is PrinterModel printer)
				{
					if (string.Equals(propertyName, nameof(printer.PrinterType)))
						return printer.PrinterType.Name;
				}
				return printerType.Name;
			case WorkShopModel workShop:
				if (item is ScaleModel scale3)
				{
					if (string.Equals(propertyName, nameof(scale.WorkShop)))
						return scale3.WorkShop is not null ? scale3.WorkShop.Name : LocaleCore.Table.FieldNull;
				}
				return workShop.Name;
		}
		return string.Empty;
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
			PropertyInfo[] properties = typeof(T).GetProperties();
			foreach (PropertyInfo property in properties)
			{
				if (string.Equals(property.Name, propertyName))
					return property.GetValue(item);
			}
		}
		return null;
	}

	#endregion
}
