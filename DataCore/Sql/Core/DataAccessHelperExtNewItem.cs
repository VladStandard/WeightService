// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.Core;

public static partial class DataAccessHelperExt
{
	#region Public and private methods

	public static AccessModel GetNewAccess(this DataAccessHelper dataAccess) =>
		new() { Name = LocaleCore.Table.FieldNull };

	public static DeviceModel GetNewDevice(this DataAccessHelper dataAccess) =>
		new() { Name = LocaleCore.Table.FieldNull };

	public static DeviceTypeModel GetNewDeviceType(this DataAccessHelper dataAccess) =>
		new() { Name = LocaleCore.Table.FieldNull };

	public static DeviceTypeFkModel GetNewDeviceTypeFk(this DataAccessHelper dataAccess) =>
		new() { Device = dataAccess.GetNewDevice(), DeviceType = dataAccess.GetNewDeviceType() };

	public static DeviceScaleFkModel GetNewDeviceScaleFk(this DataAccessHelper dataAccess) =>
		new() { Device = dataAccess.GetNewDevice(), Scale = dataAccess.GetNewScale() };

	public static NomenclatureModel GetNewNomenclature(this DataAccessHelper dataAccess) =>
		new() { Name = LocaleCore.Table.FieldNull };

	public static PluModel GetNewPlu(this DataAccessHelper dataAccess) =>
		new() { Name = LocaleCore.Table.FieldNull };

	public static PluPackageModel GetNewPluPackage(this DataAccessHelper dataAccess) =>
		new() { Name = LocaleCore.Table.FieldNull };

	public static PrinterModel GetNewPrinter(this DataAccessHelper dataAccess) =>
		new() { Name = LocaleCore.Table.FieldNull };

	public static ProductionFacilityModel GetNewProductionFacility(this DataAccessHelper dataAccess) =>
		new() { Name = LocaleCore.Table.FieldNull };

	public static ScaleModel GetNewScale(this DataAccessHelper dataAccess) =>
		new() { Description = LocaleCore.Table.FieldNull };

	public static T GetNewTable<T>(this DataAccessHelper dataAccess) where T : SqlTableBase, new() =>
		new() { Description = LocaleCore.Table.FieldNull };

	public static TemplateModel GetNewTemplate(this DataAccessHelper dataAccess) =>
		new() { Title = LocaleCore.Table.FieldNull };

	public static OrganizationModel GetNewOrganization(this DataAccessHelper dataAccess) =>
		new() { Name = LocaleCore.Table.FieldNull };

	public static PackageModel GetNewPackage(this DataAccessHelper dataAccess) =>
		new() { Name = LocaleCore.Table.FieldNull };

	public static WorkShopModel GetNewWorkShop(this DataAccessHelper dataAccess) =>
		new() { Name = LocaleCore.Table.FieldNull };

	#endregion
}
