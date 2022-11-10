// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Localizations;
using Radzen;

namespace BlazorCore.Utils;

public static class RazorFieldConfigUtils
{
	public static class Access
	{
		public static RazorFieldConfigModel GetRights() => new(nameof(AccessModel.Rights), TextAlign.Left, LocaleCore.Table.AccessLevel);
		public static RazorFieldConfigModel GetName() => new(nameof(AccessModel.Name), TextAlign.Left, LocaleCore.Table.User);
		public static RazorFieldConfigModel GetLoginDt() => new(nameof(AccessModel.LoginDt), TextAlign.Center, LocaleCore.Table.LoginDt);
	}

	public static class BarCode
	{
		public static RazorFieldConfigModel GetTypeTop() => new(nameof(BarCodeModel.TypeTop), TextAlign.Left, LocaleCore.Table.Type);
		public static RazorFieldConfigModel GetValueTop() => new(nameof(BarCodeModel.ValueTop), TextAlign.Left, LocaleCore.Table.Value);
		public static RazorFieldConfigModel GetValueRight() => new(nameof(BarCodeModel.ValueRight), TextAlign.Left, LocaleCore.Table.Value);
		public static RazorFieldConfigModel GetValueBottom() => new(nameof(BarCodeModel.ValueBottom), TextAlign.Left, LocaleCore.Table.Value);
		public static RazorFieldConfigModel GetPluLabelChangeDt() => new(nameof(BarCodeModel.PluLabel.ChangeDt), TextAlign.Center, LocaleCore.Table.Label);
	}

	public static class Base
	{
		public static RazorFieldConfigModel GetChangeDt() => new(nameof(SqlTableBase.ChangeDt), TextAlign.Center, LocaleCore.Table.ChangeDt);
		public static RazorFieldConfigModel GetCreateDt() => new(nameof(SqlTableBase.CreateDt), TextAlign.Center, LocaleCore.Table.CreateDt);
		public static RazorFieldConfigModel GetDescription() => new(nameof(SqlTableBase.Description), TextAlign.Left, LocaleCore.Table.Description);
	}

	public static class Device
	{
		public static RazorFieldConfigModel GetLoginDt() => new(nameof(DeviceModel.LoginDt), TextAlign.Left, LocaleCore.Table.LoginDt);
		public static RazorFieldConfigModel GetLogoutDt() => new(nameof(DeviceModel.LogoutDt), TextAlign.Left, LocaleCore.Table.LogoutDt);
		public static RazorFieldConfigModel GetName() => new(nameof(DeviceModel.Name), TextAlign.Left, LocaleCore.Table.Name);
		public static RazorFieldConfigModel GetPrettyName() => new(nameof(DeviceModel.PrettyName), TextAlign.Left, LocaleCore.Table.PrettyName);
		public static RazorFieldConfigModel GetIpv4() => new(nameof(DeviceModel.Ipv4), TextAlign.Left, LocaleCore.Table.DeviceIp);
		public static RazorFieldConfigModel GetMacAddress() => new(nameof(DeviceModel.MacAddress), TextAlign.Center, LocaleCore.Table.DeviceMac);
		public static RazorFieldConfigModel GetTypePrettyName() => new(
			$"{nameof(DeviceTypeModel)}.{nameof(DeviceTypeModel.PrettyName)}", TextAlign.Left, LocaleCore.Table.Type);
	}

	public static class DeviceScaleFk
	{
		public static RazorFieldConfigModel GetDeviceIp() => new($"{nameof(DeviceScaleFkModel.Device)}.{nameof(DeviceModel.Ipv4)}", TextAlign.Left, LocaleCore.Table.DeviceIp);
		public static RazorFieldConfigModel GetDeviceLoginDt() => new($"{nameof(DeviceScaleFkModel.Device)}.{nameof(DeviceModel.LoginDt)}", TextAlign.Center, LocaleCore.Table.LoginDt);
		public static RazorFieldConfigModel GetDeviceLogoutDt() => new($"{nameof(DeviceScaleFkModel.Device)}.{nameof(DeviceModel.LogoutDt)}", TextAlign.Center, LocaleCore.Table.LogoutDt);
		public static RazorFieldConfigModel GetDeviceMacAddress() => new($"{nameof(DeviceScaleFkModel.Device)}.{nameof(DeviceModel.MacAddress)}", TextAlign.Center, LocaleCore.Table.DeviceMac);
		public static RazorFieldConfigModel GetDeviceName() => new($"{nameof(DeviceScaleFkModel.Device)}.{nameof(DeviceModel.Name)}", TextAlign.Left, LocaleCore.Table.Name);
		public static RazorFieldConfigModel GetDevicePrettyName() => new($"{nameof(DeviceScaleFkModel.Device)}.{nameof(DeviceModel.PrettyName)}", TextAlign.Left, LocaleCore.Table.PrettyName);
	}

	public static class DeviceType
	{
		public static RazorFieldConfigModel GetName() => new(nameof(DeviceTypeModel.Name), TextAlign.Left, LocaleCore.Table.Name);
		public static RazorFieldConfigModel GetPrettyName() => new(nameof(DeviceTypeModel.PrettyName), TextAlign.Left, LocaleCore.Table.PrettyName);
	}

	public static class DeviceTypeFk
	{
		public static RazorFieldConfigModel GetDeviceLoginDt() => new(
			$"{nameof(DeviceTypeFkModel.Device)}.{nameof(DeviceModel.LoginDt)}",
			TextAlign.Center, LocaleCore.Table.LoginDt, nameof(DateTime));
		public static RazorFieldConfigModel GetDeviceLogoutDt() => new(
			$"{nameof(DeviceTypeFkModel.Device)}.{nameof(DeviceModel.LogoutDt)}", TextAlign.Center, LocaleCore.Table.LogoutDt);
		public static RazorFieldConfigModel GetDeviceName() => new(
			$"{nameof(DeviceTypeFkModel.Device)}.{nameof(DeviceModel.Name)}", TextAlign.Left, LocaleCore.Table.Name);
		public static RazorFieldConfigModel GetDevicePrettyName() => new(
			$"{nameof(DeviceTypeFkModel.Device)}.{nameof(DeviceModel.PrettyName)}", TextAlign.Left, LocaleCore.Table.PrettyName);
		public static RazorFieldConfigModel GetDeviceIpv4() => new(
			$"{nameof(DeviceTypeFkModel.Device)}.{nameof(DeviceModel.Ipv4)}", TextAlign.Left, LocaleCore.Table.DeviceIp);
		public static RazorFieldConfigModel GetDeviceMacAddress() => new(
			$"{nameof(DeviceTypeFkModel.Device)}.{nameof(DeviceModel.MacAddress)}", TextAlign.Center, LocaleCore.Table.DeviceMac);
		public static RazorFieldConfigModel GetDeviceTypeName() => new(
			$"{nameof(DeviceTypeFkModel.DeviceType)}.{nameof(DeviceTypeModel.Name)}", TextAlign.Left, LocaleCore.Table.Type);
		public static RazorFieldConfigModel GetDeviceTypePrettyName() => new(
			$"{nameof(DeviceTypeFkModel.DeviceType)}.{nameof(DeviceTypeModel.PrettyName)}", TextAlign.Left, LocaleCore.Table.Type);
	}

	public static class LogQuick
	{
		public static RazorFieldConfigModel GetApp() => new(nameof(DataCore.Sql.Xml.LogQuickModel.App), TextAlign.Left, LocaleCore.Table.App);
		public static RazorFieldConfigModel GetDevice(string link) => new(link, new DeviceModel(),
			$"{nameof(DataCore.Sql.Xml.LogQuickModel.Host)}", TextAlign.Left, LocaleCore.Table.Host, "string");
		public static RazorFieldConfigModel GetIcon() => new(nameof(DataCore.Sql.Xml.LogQuickModel.Icon), TextAlign.Left, LocaleCore.Table.Icon);
		public static RazorFieldConfigModel GetMessage() => new(nameof(DataCore.Sql.Xml.LogQuickModel.Message), TextAlign.Left, LocaleCore.Table.Message);
		public static RazorFieldConfigModel GetScale(string link) => new(link, new ScaleModel(), nameof(DataCore.Sql.Xml.LogQuickModel.Scale), TextAlign.Left, LocaleCore.Table.Arm, "string");
		public static RazorFieldConfigModel GetVersion() => new(nameof(DataCore.Sql.Xml.LogQuickModel.Version), TextAlign.Left, LocaleCore.Table.Version);
	}

	public static class Organization
	{
		public static RazorFieldConfigModel GetName() => new(nameof(OrganizationModel.Name), TextAlign.Left, LocaleCore.Table.Name);
		public static RazorFieldConfigModel GetGln() => new(nameof(OrganizationModel.Gln), TextAlign.Center, LocaleCore.Table.Gln);
	}

	public static class Package
	{
		public static RazorFieldConfigModel GetName() => new(nameof(PackageModel.Name), TextAlign.Left, LocaleCore.Table.Name);
		public static RazorFieldConfigModel GetWeight() => new(nameof(PackageModel.Weight), TextAlign.Center, LocaleCore.Table.WeightShort);
	}

	public static class Plu
	{
		public static RazorFieldConfigModel GetName() => new(nameof(PluModel.Name), TextAlign.Left, LocaleCore.Table.Name);
		public static RazorFieldConfigModel GetNumber() => new(nameof(PluModel.Number), TextAlign.Center, LocaleCore.Table.Number);
		public static RazorFieldConfigModel GetShelfLifeDays() => new(nameof(PluModel.ShelfLifeDays), TextAlign.Center, LocaleCore.Table.ShelfLifeDaysShort);
		public static RazorFieldConfigModel GetBoxQuantly() => new(nameof(PluModel.BoxQuantly), TextAlign.Center, LocaleCore.Table.GoodsBoxQuantlyShort);
	}

	public static class PluScale
	{
		public static RazorFieldConfigModel GetScaleNumber() => new(nameof(PluScaleModel.Scale.Number), TextAlign.Left, LocaleCore.Table.NumberShort);
		public static RazorFieldConfigModel GetPluNumber() => new(nameof(PluScaleModel.Plu.Number), TextAlign.Left, LocaleCore.Table.NumberShort);
		public static RazorFieldConfigModel GetActive() => new(nameof(PluScaleModel.IsActive), TextAlign.Left, LocaleCore.Table.ActiveShort, "bool");
	}

	public static class Printer
	{
		public static RazorFieldConfigModel GetIp() => new(nameof(PrinterModel.Ip), TextAlign.Left, LocaleCore.Print.Ip);
		public static RazorFieldConfigModel GetMacAddress() => new(nameof(PrinterModel.MacAddress), TextAlign.Left, LocaleCore.Print.Mac);
		public static RazorFieldConfigModel GetName() => new(nameof(PrinterModel.Name), TextAlign.Left, LocaleCore.Table.Name);
		public static RazorFieldConfigModel GetPrinterType(string link) => new(link, new PrinterTypeModel(), $"{nameof(PrinterModel.PrinterType)}.{nameof(PrinterTypeModel.Name)}", TextAlign.Left, LocaleCore.Print.Type, "string");
	}

	public static class PrinterResource
	{
		public static RazorFieldConfigModel GetName(string link) => new(link, new PrinterResourceModel(), $"{nameof(PrinterResourceModel.TemplateResource)}.{nameof(TemplateResourceModel.Name)}", TextAlign.Left, LocaleCore.Print.TemplateResource, "string");
		public static RazorFieldConfigModel GetPrinter(string link) => new(link, new PrinterModel(), $"{nameof(PrinterResourceModel.Printer)}.{nameof(PrinterModel.Name)}", TextAlign.Left, LocaleCore.Print.Name, "string");
	}

	public static class PrinterType
	{
		public static RazorFieldConfigModel GetName() => new(nameof(PrinterTypeModel.Name), TextAlign.Left, LocaleCore.Table.Name);
	}

	public static class ProductionFacility
	{
		public static RazorFieldConfigModel GetAddress() => new(nameof(ProductionFacilityModel.Address), TextAlign.Left, LocaleCore.Table.Address);
		public static RazorFieldConfigModel GetName() => new(nameof(ProductionFacilityModel.Name), TextAlign.Left, LocaleCore.Table.Name);
	}

	public static class Scale
	{
		public static RazorFieldConfigModel GetName() => new(nameof(ScaleModel.Description), TextAlign.Left, LocaleCore.Table.Name);
		public static RazorFieldConfigModel GetNumber() => new($"{nameof(ScaleModel.Number)}", TextAlign.Left, LocaleCore.Table.Number);
		public static RazorFieldConfigModel GetPrinterMain(string link) => new(link, new PrinterModel(), $"{nameof(ScaleModel.PrinterMain)}.{nameof(ScaleModel.PrinterMain.Name)}",
			TextAlign.Left, LocaleCore.Print.NameMain, "string");
		public static RazorFieldConfigModel GetPrinterShipping(string link) => new(link, new PrinterModel(), $"{nameof(ScaleModel.PrinterShipping)}.{nameof(ScaleModel.PrinterShipping.Name)}",
			TextAlign.Left, LocaleCore.Print.NameShipping, "string");
		public static RazorFieldConfigModel GetWorkShop(string link) => new(link, new WorkShopModel(), $"{nameof(ScaleModel.WorkShop)}.{nameof(WorkShopModel.Name)}",
			TextAlign.Left, LocaleCore.Table.WorkShop, "string");
		public static RazorFieldConfigModel GetDeviceName(string link) => new(link, new DeviceModel(), $"{nameof(ScaleModel.Device)}.{nameof(DeviceModel.Name)}", TextAlign.Left, LocaleCore.Table.Host, "string");
	}

	public static class Version
	{
		public static RazorFieldConfigModel GetReleaseDt() => new(nameof(VersionModel.ReleaseDt), TextAlign.Center, LocaleCore.Table.ReleaseDt);
		public static RazorFieldConfigModel GetVersion() => new(nameof(VersionModel.Version), TextAlign.Center, LocaleCore.Table.Version);
	}

	public static class WorkShop
	{
		public static RazorFieldConfigModel GetName() => new($"{nameof(WorkShopModel.Name)}", TextAlign.Left, LocaleCore.Table.Name);
		public static RazorFieldConfigModel GetProductionFacility(string link) => new(link, new ProductionFacilityModel(), $"{nameof(WorkShopModel.ProductionFacility)}.{nameof(ProductionFacilityModel.Name)}", TextAlign.Left, LocaleCore.Table.ProductionFacility, "string");
	}
}
