// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Radzen;
using WsBlazorCore.Razors;
using WsStorageCore.Models;
using WsStorageCore.TableScaleFkModels.DeviceScalesFks;
using WsStorageCore.TableScaleFkModels.DeviceTypesFks;
using WsStorageCore.TableScaleFkModels.PlusNestingFks;
using WsStorageCore.TableScaleFkModels.PrintersResourcesFks;
using WsStorageCore.TableScaleModels.Access;
using WsStorageCore.TableScaleModels.BarCodes;
using WsStorageCore.TableScaleModels.Boxes;
using WsStorageCore.TableScaleModels.Bundles;
using WsStorageCore.TableScaleModels.Devices;
using WsStorageCore.TableScaleModels.DeviceTypes;
using WsStorageCore.TableScaleModels.Organizations;
using WsStorageCore.TableScaleModels.Plus;
using WsStorageCore.TableScaleModels.PlusScales;
using WsStorageCore.TableScaleModels.Printers;
using WsStorageCore.TableScaleModels.PrintersTypes;
using WsStorageCore.TableScaleModels.ProductionFacilities;
using WsStorageCore.TableScaleModels.Scales;
using WsStorageCore.TableScaleModels.TemplatesResources;
using WsStorageCore.TableScaleModels.Versions;
using WsStorageCore.TableScaleModels.WorkShops;
using WsStorageCore.ViewScaleModels;

namespace WsBlazorCore.Utils;

public static class RazorFieldConfigUtils
{
    public static class Base
    {
        public static RazorFieldConfigModel GetChangeDt() => new(nameof(WsSqlTableBase.ChangeDt), TextAlign.Center, LocaleCore.Table.ChangeDt);
        public static RazorFieldConfigModel GetCreateDt() => new(nameof(WsSqlTableBase.CreateDt), TextAlign.Center, LocaleCore.Table.CreateDt);
        public static RazorFieldConfigModel GetDescription(string description = "") => new(nameof(WsSqlTableBase.Description), TextAlign.Left, !string.IsNullOrEmpty(description) ? description : LocaleCore.Table.Description);
        public static RazorFieldConfigModel GetName(string name = "") => new(nameof(WsSqlTableBase.Name), TextAlign.Left, !string.IsNullOrEmpty(name) ? name : LocaleCore.Table.Name);
    }
    
    public static class Access
	{
		public static RazorFieldConfigModel GetRights() => new(nameof(WsSqlAccessModel.Rights), TextAlign.Left, LocaleCore.Table.AccessLevel);
		public static RazorFieldConfigModel GetLoginDt() => new(nameof(WsSqlAccessModel.LoginDt), TextAlign.Center, LocaleCore.Table.LoginDt);
	}

	public static class BarCode
	{
		public static RazorFieldConfigModel GetTypeTop() => new(nameof(WsSqlBarCodeModel.TypeTop), TextAlign.Left, LocaleCore.Table.Type);
		public static RazorFieldConfigModel GetValueTop() => new(nameof(WsSqlBarCodeModel.ValueTop), TextAlign.Left, LocaleCore.Table.Value);
		public static RazorFieldConfigModel GetValueRight() => new(nameof(WsSqlBarCodeModel.ValueRight), TextAlign.Left, LocaleCore.Table.Value);
		public static RazorFieldConfigModel GetValueBottom() => new(nameof(WsSqlBarCodeModel.ValueBottom), TextAlign.Left, LocaleCore.Table.Value);
		public static RazorFieldConfigModel GetPluLabelChangeDt() => new(nameof(WsSqlBarCodeModel.PluLabel.ChangeDt), TextAlign.Center, LocaleCore.Table.Label);
	}

    public static class Device
    {
        public static RazorFieldConfigModel GetLoginDt() => new(nameof(WsSqlDeviceModel.LoginDt), TextAlign.Left, LocaleCore.Table.LoginDt);
        public static RazorFieldConfigModel GetLogoutDt() => new(nameof(WsSqlDeviceModel.LogoutDt), TextAlign.Left, LocaleCore.Table.LogoutDt);
        public static RazorFieldConfigModel GetIpv4() => new(nameof(WsSqlDeviceModel.Ipv4), TextAlign.Left, LocaleCore.Table.DeviceIp);
        public static RazorFieldConfigModel GetMacAddress() => new(nameof(WsSqlDeviceModel.MacAddress), TextAlign.Center, LocaleCore.Table.DeviceMac);
        public static RazorFieldConfigModel GetPrettyName() => new(nameof(WsSqlDeviceModel.PrettyName), TextAlign.Left, LocaleCore.Table.PrettyName);
        public static RazorFieldConfigModel GetTypePrettyName() => new(
            $"{nameof(WsSqlDeviceTypeModel)}.{nameof(WsSqlDeviceTypeModel.PrettyName)}", TextAlign.Left, LocaleCore.Table.Type);
    }
    
    public static class DeviceScaleFk
	{
		public static RazorFieldConfigModel GetDeviceIp() => new($"{nameof(WsSqlDeviceScaleFkModel.Device)}.{nameof(WsSqlDeviceModel.Ipv4)}", TextAlign.Left, LocaleCore.Table.DeviceIp);
		public static RazorFieldConfigModel GetDeviceLoginDt() => new($"{nameof(WsSqlDeviceScaleFkModel.Device)}.{nameof(WsSqlDeviceModel.LoginDt)}", TextAlign.Center, LocaleCore.Table.LoginDt);
		public static RazorFieldConfigModel GetDeviceLogoutDt() => new($"{nameof(WsSqlDeviceScaleFkModel.Device)}.{nameof(WsSqlDeviceModel.LogoutDt)}", TextAlign.Center, LocaleCore.Table.LogoutDt);
		public static RazorFieldConfigModel GetDeviceMacAddress() => new($"{nameof(WsSqlDeviceScaleFkModel.Device)}.{nameof(WsSqlDeviceModel.MacAddress)}", TextAlign.Center, LocaleCore.Table.DeviceMac);
		public static RazorFieldConfigModel GetDeviceName() => new(new WsSqlDeviceModel(), $"{nameof(WsSqlDeviceScaleFkModel.Device)}.{nameof(WsSqlDeviceModel.Name)}", TextAlign.Left, LocaleCore.Table.Device, LocaleCore.Table.Name);
		public static RazorFieldConfigModel GetDevicePrettyName() => new($"{nameof(WsSqlDeviceScaleFkModel.Device)}.{nameof(WsSqlDeviceModel.PrettyName)}", TextAlign.Left, LocaleCore.Table.PrettyName);
	}

	public static class DeviceType
	{
		public static RazorFieldConfigModel GetPrettyName() => new(nameof(WsSqlDeviceTypeModel.PrettyName), TextAlign.Left, LocaleCore.Table.PrettyName);
	}

	public static class DeviceTypeFk
	{
		public static RazorFieldConfigModel GetDeviceLoginDt() => new(
			$"{nameof(WsSqlDeviceTypeFkModel.Device)}.{nameof(WsSqlDeviceModel.LoginDt)}",
			TextAlign.Center, LocaleCore.Table.LoginDt, nameof(DateTime));
		public static RazorFieldConfigModel GetDeviceLogoutDt() => new(
			$"{nameof(WsSqlDeviceTypeFkModel.Device)}.{nameof(WsSqlDeviceModel.LogoutDt)}", TextAlign.Center, LocaleCore.Table.LogoutDt);
		public static RazorFieldConfigModel GetDeviceName() => new(
			$"{nameof(WsSqlDeviceTypeFkModel.Device)}.{nameof(WsSqlDeviceModel.Name)}", TextAlign.Left, LocaleCore.Table.Name);
		public static RazorFieldConfigModel GetDevicePrettyName() => new(
			$"{nameof(WsSqlDeviceTypeFkModel.Device)}.{nameof(WsSqlDeviceModel.PrettyName)}", TextAlign.Left, LocaleCore.Table.PrettyName);
		public static RazorFieldConfigModel GetDeviceIpv4() => new(
			$"{nameof(WsSqlDeviceTypeFkModel.Device)}.{nameof(WsSqlDeviceModel.Ipv4)}", TextAlign.Left, LocaleCore.Table.DeviceIp);
		public static RazorFieldConfigModel GetDeviceMacAddress() => new(
			$"{nameof(WsSqlDeviceTypeFkModel.Device)}.{nameof(WsSqlDeviceModel.MacAddress)}", TextAlign.Center, LocaleCore.Table.DeviceMac);
		public static RazorFieldConfigModel GetDeviceTypeName() => new(
			$"{nameof(WsSqlDeviceTypeFkModel.Type)}.{nameof(WsSqlDeviceTypeModel.Name)}", TextAlign.Left, LocaleCore.Table.Type);
		public static RazorFieldConfigModel GetDeviceTypePrettyName() => new(
			$"{nameof(WsSqlDeviceTypeFkModel.Type)}.{nameof(WsSqlDeviceTypeModel.PrettyName)}", TextAlign.Left, LocaleCore.Table.Type);
	}
    
    public static class LogQuick
	{
		public static RazorFieldConfigModel GetApp() => new(nameof(LogView.App), TextAlign.Left, LocaleCore.Table.App);
		public static RazorFieldConfigModel GetDevice() => new(nameof(LogView.Host), TextAlign.Left, LocaleCore.Table.Host, "string");
		public static RazorFieldConfigModel GetIcon() => new(nameof(LogView.LogType), TextAlign.Left, LocaleCore.Table.Icon);
		public static RazorFieldConfigModel GetMessage() => new(nameof(LogView.Message), TextAlign.Left, LocaleCore.Table.Message);
		public static RazorFieldConfigModel GetScale() => new(nameof(LogView.Line), TextAlign.Left, LocaleCore.Table.Line, "string");
		public static RazorFieldConfigModel GetVersion() => new(nameof(LogView.Version), TextAlign.Left, LocaleCore.Table.Version);
	}

	public static class Organization
	{
		public static RazorFieldConfigModel GetGln() => new(nameof(WsSqlOrganizationModel.Gln), TextAlign.Center, LocaleCore.Table.Gln);
	}

    public static class Box
    {
        public static RazorFieldConfigModel GetName() => new(nameof(WsSqlBoxModel.Name), TextAlign.Left, LocaleCore.Table.Name);
        public static RazorFieldConfigModel GetWeight() => new(nameof(WsSqlBoxModel.Weight), TextAlign.Center, LocaleCore.Table.BoxWeight);
    }

    public static class Bundle
    {
        public static RazorFieldConfigModel GetName() => new(nameof(WsSqlBundleModel.Name), TextAlign.Left, LocaleCore.Table.Name);
        public static RazorFieldConfigModel GetWeight() => new(nameof(WsSqlBundleModel.Weight), TextAlign.Center, LocaleCore.Table.BundleWeight);
    }

    public static class BundleFk
	{
        public static RazorFieldConfigModel GetBoxName() => new(nameof(WsSqlPluNestingFkModel.Box.Name), TextAlign.Center, LocaleCore.Table.BundleCount);
        public static RazorFieldConfigModel GetBundleCount() => new(nameof(WsSqlPluNestingFkModel.BundleCount), TextAlign.Center, LocaleCore.Table.BundleCount);
    }

    public static class PluBundleFk
	{
		public static RazorFieldConfigModel GetWeightTare() => new("0", TextAlign.Center, LocaleCore.Table.WeightShort);
	}

	public static class Plu
	{
		public static RazorFieldConfigModel GetNumber() => new(nameof(WsSqlPluModel.Number), TextAlign.Center, LocaleCore.Table.Number);
		public static RazorFieldConfigModel GetShelfLifeDays() => new(nameof(WsSqlPluModel.ShelfLifeDays), TextAlign.Center, LocaleCore.Table.ShelfLifeDaysShort);
        public static RazorFieldConfigModel GetCode() => new(nameof(WsSqlPluModel.Code), TextAlign.Center, LocaleCore.Table.Code);
    }

    public static class PluScale
	{
		public static RazorFieldConfigModel GetScaleNumber() => new(nameof(WsSqlPluScaleModel.Scale.Number), TextAlign.Left, LocaleCore.Table.NumberShort);
		public static RazorFieldConfigModel GetPluNumber() => new(nameof(WsSqlPluScaleModel.Plu.Number), TextAlign.Left, LocaleCore.Table.NumberShort);
		public static RazorFieldConfigModel GetActive() => new(nameof(WsSqlPluScaleModel.IsActive), TextAlign.Left, LocaleCore.Table.ActiveShort, "bool");
	}

	public static class Printer
	{
		public static RazorFieldConfigModel GetIp() => new(nameof(WsSqlPrinterModel.Ip), TextAlign.Left, LocaleCore.Print.Ip);
		public static RazorFieldConfigModel GetMacAddress() => new(nameof(WsSqlPrinterModel.MacAddress), TextAlign.Left, LocaleCore.Print.Mac);
		public static RazorFieldConfigModel GetPrinterType() => new(new WsSqlPrinterTypeModel(), $"{nameof(WsSqlPrinterModel.PrinterType)}.{nameof(WsSqlPrinterTypeModel.Name)}", TextAlign.Left, LocaleCore.Print.Type, "string");
	}

	public static class PrinterResource
	{
		public static RazorFieldConfigModel GetName() => new(new WsSqlPrinterResourceFkModel(), $"{nameof(WsSqlPrinterResourceFkModel.TemplateResource)}.{nameof(WsSqlTemplateResourceModel.Name)}", TextAlign.Left, LocaleCore.Print.TemplateResource, "string");
		public static RazorFieldConfigModel GetPrinter() => new(new WsSqlPrinterModel(), $"{nameof(WsSqlPrinterResourceFkModel.Printer)}.{nameof(WsSqlPrinterModel.Name)}", TextAlign.Left, LocaleCore.Print.Name, "string");
	}

	public static class ProductionFacility
	{
		public static RazorFieldConfigModel GetAddress() => new(nameof(WsSqlProductionFacilityModel.Address), TextAlign.Left, LocaleCore.Table.Address);
	}

	public static class Scale
	{
		public static RazorFieldConfigModel GetNumber() => new($"{nameof(WsSqlScaleModel.Number)}", 
            TextAlign.Center, LocaleCore.Table.Number);
		public static RazorFieldConfigModel GetPrinterMain() => new(new WsSqlPrinterModel(), 
            $"{nameof(WsSqlScaleModel.PrinterMain)}.{nameof(WsSqlScaleModel.PrinterMain.Name)}",
			TextAlign.Left, LocaleCore.Print.NameMain, "string");
		public static RazorFieldConfigModel GetPrinterShipping() => new(new WsSqlPrinterModel(), 
            $"{nameof(WsSqlScaleModel.PrinterShipping)}.{nameof(WsSqlScaleModel.PrinterShipping.Name)}",
			TextAlign.Left, LocaleCore.Print.NameShipping, "string");
		public static RazorFieldConfigModel GetWorkShop() => new(new WsSqlWorkShopModel(), 
            $"{nameof(WsSqlScaleModel.WorkShop)}.{nameof(WsSqlWorkShopModel.Name)}",
			TextAlign.Left, LocaleCore.Table.WorkShop, "string");
    }

    public static class TemplateResource
    {
        public static RazorFieldConfigModel GetDataType() => new($"{nameof(WsSqlTemplateResourceModel.Type)}", 
            TextAlign.Left, LocaleCore.Table.Type, "string");
        public static RazorFieldConfigModel GetDataSize() => new($"{nameof(LocaleCore.Table.Size)}",
            TextAlign.Left, LocaleCore.Table.Size, "string");
    }

    public static class Version
	{
		public static RazorFieldConfigModel GetReleaseDt() => new(nameof(WsSqlVersionModel.ReleaseDt), TextAlign.Center, LocaleCore.Table.ReleaseDt);
		public static RazorFieldConfigModel GetVersion() => new(nameof(WsSqlVersionModel.Version), TextAlign.Center, LocaleCore.Table.Version);
	}

	public static class WorkShop
	{
		public static RazorFieldConfigModel GetProductionFacility() => new(new WsSqlProductionFacilityModel(), $"{nameof(WsSqlWorkShopModel.ProductionFacility)}.{nameof(WsSqlProductionFacilityModel.Name)}", TextAlign.Left, LocaleCore.Table.ProductionFacility, "string");
	}
}