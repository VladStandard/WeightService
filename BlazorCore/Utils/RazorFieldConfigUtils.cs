// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;
using DataCore.Localizations;
using DataCore.Sql.Tables;
using DataCore.Sql.Xml;
using Radzen;

namespace BlazorCore.Utils;

public static class RazorFieldConfigUtils
{
    public static class Access
    {
		public static RazorFieldConfigModel GetRights() => new(nameof(AccessModel.Rights), TextAlign.Left, LocaleCore.Table.AccessLevel);
		public static RazorFieldConfigModel GetUser() => new(nameof(AccessModel.User), TextAlign.Left, LocaleCore.Table.User);
	}

    public static class BarCode
	{
		public static RazorFieldConfigModel GetValue() => new(nameof(BarCodeModel.Value), TextAlign.Left, LocaleCore.Table.Value);
	}

    public static class Base
    {
		public static RazorFieldConfigModel GetChangeDt() => new(nameof(SqlTableBase.ChangeDt), TextAlign.Center, LocaleCore.Table.ChangeDt);
		public static RazorFieldConfigModel GetCreateDt() => new(nameof(SqlTableBase.CreateDt), TextAlign.Center, LocaleCore.Table.CreateDt);
		public static RazorFieldConfigModel GetDescription() => new(nameof(SqlTableBase.Description), TextAlign.Left, LocaleCore.Table.Description);
	}

	public static class Host
	{
		public static RazorFieldConfigModel GetDeviceIp() => new(nameof(HostModel.Ip), TextAlign.Left, LocaleCore.Table.DeviceIp);
		public static RazorFieldConfigModel GetHostName() => new(nameof(HostModel.HostName), TextAlign.Left, LocaleCore.Table.Host);
		public static RazorFieldConfigModel GetMacAddressPretty() => new(nameof(HostModel.MacAddress), TextAlign.Left, LocaleCore.Table.DeviceMac);
		public static RazorFieldConfigModel GetName() => new(nameof(HostModel.Name), TextAlign.Left, LocaleCore.Table.NamePretty);
	}

	public static class LogQuick
	{
	    public static RazorFieldConfigModel GetApp() => new(nameof(LogQuickModel.App), TextAlign.Left, LocaleCore.Table.App);
	    public static RazorFieldConfigModel GetHost(string link) => new(link, new HostModel(), nameof(LogQuickModel.Host), TextAlign.Left, LocaleCore.Table.Host, "string");
	    public static RazorFieldConfigModel GetIcon() => new(nameof(LogQuickModel.Icon), TextAlign.Left, LocaleCore.Table.Icon);
	    public static RazorFieldConfigModel GetMessage() => new(nameof(LogQuickModel.Message), TextAlign.Left, LocaleCore.Table.Message);
	    public static RazorFieldConfigModel GetScale(string link) => new(link, new ScaleModel(), nameof(LogQuickModel.Scale), TextAlign.Left, LocaleCore.Table.Scale, "string");
	    public static RazorFieldConfigModel GetVersion() => new(nameof(LogQuickModel.Version), TextAlign.Left, LocaleCore.Table.Version);
	}

	public static class Plu
    {
        public static RazorFieldConfigModel GetName() => new(nameof(PluModel.Name), TextAlign.Left, LocaleCore.Table.Name);
	    public static RazorFieldConfigModel GetShelfLifeDays() => new(nameof(PluModel.ShelfLifeDays), TextAlign.Center, LocaleCore.Table.ShelfLifeDaysShort);
		public static RazorFieldConfigModel GetBoxQuantly() => new(nameof(PluModel.BoxQuantly), TextAlign.Center, LocaleCore.Table.GoodsBoxQuantlyShort);
		public static RazorFieldConfigModel GetTareWeight() => new(nameof(PluModel.TareWeight), TextAlign.Center, LocaleCore.Table.TareWeightShort);
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
		public static RazorFieldConfigModel GetPrinterIp() => new(nameof(PrinterModel.Ip), TextAlign.Left, LocaleCore.Print.Ip);
		public static RazorFieldConfigModel GetPrinterType(string link) => new(link, new PrinterTypeModel(), nameof(PrinterModel.PrinterType), TextAlign.Left, LocaleCore.Print.Type, "string");
	}

	public static class PrinterResource
	{
		public static RazorFieldConfigModel GetDescription() => new(nameof(PrinterResourceModel.Description), TextAlign.Left, LocaleCore.Table.Description);
		public static RazorFieldConfigModel GetPrinter(string link) => new (link, new PrinterModel(), nameof(PrinterResourceModel.Printer), TextAlign.Left, LocaleCore.Print.Name, "string");
		public static RazorFieldConfigModel GetResource(string link) => new (link, new PrinterResourceModel(), nameof(PrinterResourceModel.TemplateResource), TextAlign.Left, LocaleCore.Print.TemplateResource, "string");
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
		public static RazorFieldConfigModel GetDescription() => new(nameof(ScaleModel.Description), TextAlign.Left, LocaleCore.Table.Description);
		public static RazorFieldConfigModel GetDeviceIp() => new(nameof(ScaleModel.DeviceIp), TextAlign.Left, LocaleCore.Table.DeviceIp);
		public static RazorFieldConfigModel GetHost(string link) => new(link, new HostModel(), nameof(ScaleModel.Host), TextAlign.Left, LocaleCore.Table.Host, "string");
		public static RazorFieldConfigModel GetNumber() => new(nameof(ScaleModel.Number), TextAlign.Left, LocaleCore.Table.Number);
		public static RazorFieldConfigModel GetPrinterMain(string link) => new(link, new PrinterModel(), nameof(ScaleModel.PrinterMain), TextAlign.Left, LocaleCore.Print.NameMain, "string");
		public static RazorFieldConfigModel GetPrinterShipping(string link) => new(link, new PrinterModel(), nameof(ScaleModel.PrinterShipping), TextAlign.Left, LocaleCore.Print.NameShipping, "string");
		public static RazorFieldConfigModel GetWorkShop(string link) => new(link, new WorkShopModel(), nameof(ScaleModel.WorkShop), TextAlign.Left, LocaleCore.Table.WorkShop, "string");
	}

	public static class Version
	{
        public static RazorFieldConfigModel GetDescription() => new(nameof(VersionModel.Description), TextAlign.Left, LocaleCore.Table.Description);
		public static RazorFieldConfigModel GetReleaseDt() => new(nameof(VersionModel.ReleaseDt), TextAlign.Center, LocaleCore.Table.ReleaseDt);
		public static RazorFieldConfigModel GetVersion() => new(nameof(VersionModel.Version), TextAlign.Center, LocaleCore.Table.Version);
    }

	public static class WorkShop
	{
        public static RazorFieldConfigModel GetName() => new(nameof(WorkShopModel.Name), TextAlign.Left, LocaleCore.Table.Name);
        public static RazorFieldConfigModel GetProductionFacility(string link) => new(link, new ProductionFacilityModel(), nameof(WorkShopModel.ProductionFacility), TextAlign.Left, LocaleCore.Table.ProductionFacility, "string");
	}
}
