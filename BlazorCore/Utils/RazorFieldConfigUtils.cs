// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models;
using DataCore.Localizations;
using DataCore.Sql.Tables;
using Radzen;

namespace BlazorCore.Utils;

public static class RazorFieldConfigUtils
{
    public static class Base
    {
		public static RazorFieldConfigModel GetCreateDt() =>
			(new(nameof(SqlTableBase.CreateDt), TextAlign.Center, LocaleCore.Table.CreateDt));
		public static RazorFieldConfigModel GetChangeDt() =>
			(new(nameof(SqlTableBase.ChangeDt), TextAlign.Center, LocaleCore.Table.ChangeDt));
		public static RazorFieldConfigModel GetDescription() =>
			(new(nameof(SqlTableBase.Description), TextAlign.Left, LocaleCore.Table.Description));
	}

    public static class BarCode
	{
		public static RazorFieldConfigModel GetValue() =>
			(new(nameof(BarCodeModel.Value), TextAlign.Left, LocaleCore.Table.Value));
	}

    public static class Access
    {
		public static RazorFieldConfigModel GetUser() =>
			(new(nameof(AccessModel.User), TextAlign.Left, LocaleCore.Table.User));
		public static RazorFieldConfigModel GetRights() =>
			(new(nameof(AccessModel.Rights), TextAlign.Left, LocaleCore.Table.AccessLevel));
	}

	public static class Plu
    {
        public static RazorFieldConfigModel GetName() => 
            (new(nameof(PluModel.Name), TextAlign.Left, LocaleCore.Table.Name));
	    public static RazorFieldConfigModel GetShelfLifeDays() => 
            (new(nameof(PluModel.ShelfLifeDays), TextAlign.Center, LocaleCore.Table.ShelfLifeDaysShort));
		public static RazorFieldConfigModel GetTareWeight() => 
            (new(nameof(PluModel.TareWeight), TextAlign.Center, LocaleCore.Table.TareWeightShort));
		public static RazorFieldConfigModel GetBoxQuantly() => 
            (new(nameof(PluModel.BoxQuantly), TextAlign.Center, LocaleCore.Table.GoodsBoxQuantlyShort));
    }
	
	public static class Scale
    {
		public static RazorFieldConfigModel GetNumber() => 
            (new(nameof(ScaleModel.Number), TextAlign.Left, LocaleCore.Table.Number));
		public static RazorFieldConfigModel GetDescription() => 
            (new(nameof(ScaleModel.Description), TextAlign.Left, LocaleCore.Table.Description));
		public static RazorFieldConfigModel GetDeviceIp() => 
            (new(nameof(ScaleModel.DeviceIp), TextAlign.Left, LocaleCore.Table.DeviceIp));
		public static RazorFieldConfigModel GetHost(string link) => 
            (new(link, nameof(ScaleModel.Host), TextAlign.Left, LocaleCore.Table.Host, "string"));
		public static RazorFieldConfigModel GetPrinterMain(string link) => 
            (new(link, nameof(ScaleModel.PrinterMain), TextAlign.Left, LocaleCore.Print.NameMain, "string"));
	}

	public static class Version
	{
        public static RazorFieldConfigModel GetDescription() => 
            (new(nameof(VersionModel.Description), TextAlign.Left, LocaleCore.Table.Version));
		public static RazorFieldConfigModel GetReleaseDt() => 
            (new(nameof(VersionModel.ReleaseDt), TextAlign.Center, LocaleCore.Table.ReleaseDt));
		public static RazorFieldConfigModel GetVersion() => 
            (new(nameof(VersionModel.Version), TextAlign.Center, LocaleCore.Table.Version));
    }
}
