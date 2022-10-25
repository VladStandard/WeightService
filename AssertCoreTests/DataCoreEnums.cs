﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace AssertCoreTests;

/// <summary>
/// Enumeration of values.
/// </summary>
public static class DataCoreEnums
{
	/// <summary>
	/// List of bool values.
	/// </summary>
	/// <returns></returns>
	public static List<bool> GetBool()
	{
		return new() { false, true };
	}

	/// <summary>
	/// List of bool values with null value.
	/// </summary>
	/// <returns></returns>
	public static List<bool?> GetBoolNullable()
	{
		return new() { null, false, true };
	}

	/// <summary>
	/// List of string values.
	/// </summary>
	/// <returns></returns>
	public static List<string?> GetString()
	{
		return new() { null, "", string.Empty };
	}

	/// <summary>
	/// List of Guid values.
	/// </summary>
	/// <returns></returns>
	public static List<Guid> GetGuid()
	{
		return new() { Guid.Empty, Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), };
	}

	/// <summary>
	/// List of decimal values.
	/// </summary>
	/// <returns></returns>
	public static List<decimal> GetDecimal()
	{
		return new() { decimal.MinValue, decimal.MinValue / 2, 0, 1, decimal.MaxValue / 2, decimal.MaxValue };
	}

	/// <summary>
	/// List of ushort values.
	/// </summary>
	/// <returns></returns>
	public static List<ushort> GetUshort()
	{
		return new() { ushort.MinValue, 1, ushort.MaxValue / 2, ushort.MaxValue };
	}

	/// <summary>
	/// List of progress values.
	/// </summary>
	/// <returns></returns>
	public static List<ushort> GetProgress()
	{
		return new() { 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100 };
	}

	/// <summary>
	/// List of short values.
	/// </summary>
	/// <returns></returns>
	public static List<short> GetShort()
	{
		return new() { short.MinValue, 1, short.MaxValue / 2, short.MaxValue };
	}

	/// <summary>
	/// List of uint values.
	/// </summary>
	/// <returns></returns>
	public static List<uint> GetUint()
	{
		return new() { uint.MinValue, 1, uint.MaxValue / 2, uint.MaxValue };
	}

	/// <summary>
	/// List of int values.
	/// </summary>
	/// <returns></returns>
	public static List<int> GetInt()
	{
		return new() { int.MinValue, 1, int.MaxValue / 2, int.MaxValue };
	}

	/// <summary>
	/// List of int values with null value.
	/// </summary>
	/// <returns></returns>
	public static List<int?> GetIntNullable()
	{
		return new() { null, int.MinValue, 1, int.MaxValue / 2, int.MaxValue };
	}

	/// <summary>
	/// List of int values with null value.
	/// </summary>
	/// <returns></returns>
	public static List<long?> GetLongNullable()
	{
		return new() { null, long.MinValue, 1, long.MaxValue / 2, long.MaxValue };
	}

	/// <summary>
	/// List of long values.
	/// </summary>
	/// <returns></returns>
	public static List<long> GetLong()
	{
		return new() { long.MinValue, 1, long.MaxValue / 2, long.MaxValue };
	}

	/// <summary>
	/// List of DateTime values.
	/// </summary>
	/// <returns></returns>
	public static List<DateTime> GetDateTime()
	{
		return new() { DateTime.MinValue, DateTime.MaxValue, DateTime.Now, DateTime.Today, DateTime.UtcNow };
	}

	/// <summary>
	/// String value.
	/// </summary>
	/// <param name="str"></param>
	/// <returns></returns>
	public static string AsString(this string str)
	{
		return str == null ? "<null>" : str == "" ? "<empty>" : str;
	}

	/// <summary>
	/// List of uri values.
	/// </summary>
	/// <returns></returns>
	public static List<Uri> GetUri()
	{
		return new() { new("http://google.com/"), new("http://microsoft.com/") };
	}

	/// <summary>
	/// List of timeout values in ms.
	/// </summary>
	/// <returns></returns>
	public static List<int> GetTimeoutMs()
	{
		return new() { 50, 500 };
	}

	/// <summary>
	/// List of bytes.
	/// </summary>
	/// <returns></returns>
	public static List<byte> GetBytes()
	{
		return new() { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, };
	}

	/// <summary>
	/// List of models SqlTableBase.
	/// </summary>
	/// <returns></returns>
	public static List<SqlTableBase> GetSqlTableModels() => new()
	{
		new AccessModel(),
		new AppModel(),
		new BarCodeModel(),
		new ContragentModel(),
		new DeviceModel(),
		new DeviceTypeModel(),
		new HostModel(),
		new LogModel(),
		new LogTypeModel(),
		new NomenclatureModel(),
		new OrderModel(),
		new OrganizationModel(),
		new PackageModel(),
		new PluLabelModel(),
		new PluModel(),
		new PluPackageModel(),
		new PluScaleModel(),
		new PluWeighingModel(),
		new PrinterModel(),
		new PrinterResourceModel(),
		new PrinterTypeModel(),
		new ProductionFacilityModel(),
		new ProductSeriesModel(),
		new ScaleModel(),
		new ScaleScreenShotModel(),
		new TaskModel(),
		new TaskTypeModel(),
		new TemplateModel(),
		new TemplateResourceModel(),
		new VersionModel(),
		new WorkShopModel(),
	};

	///// <summary>
	///// List of models SqlTableBase.
	///// </summary>
	///// <returns></returns>
	//public static List<SqlTableBase> GetSqlTableModels(bool flag) => new()
	//{
	//	new AccessModel(),
	//	new AppModel(),
	//	new BarCodeModel(),
	//	//new BarCodeTypeModel(),
	//	new ContragentModel(),
	//	new HostModel(),
	//	new LogModel(),
	//	new LogTypeModel(),
	//	new NomenclatureModel(),
	//	new OrderModel(),
	//	new OrganizationModel(),
	//	new PackageModel(),
	//	new PluPackageModel(),
	//	new PluModel(),
	//	new PluLabelModel(),
	//	new PluScaleModel(),
	//	new PluWeighingModel(),
	//	new PrinterModel(),
	//	new PrinterResourceModel(),
	//	new PrinterTypeModel(),
	//	new ProductionFacilityModel(),
	//	new ProductSeriesModel(),
	//	new ScaleModel(),
	//	new TaskModel(),
	//	new TaskTypeModel(),
	//	new TemplateModel(),
	//	new TemplateResourceModel(),
	//	new VersionModel(),
	//	new WorkShopModel(),
	//};

	/// <summary>
	/// List of types SqlTableBase.
	/// </summary>
	/// <returns></returns>
	public static List<Type> GetSqlTableTypes() => new()
	{
		typeof(AccessModel),
		typeof(AppModel),
		typeof(BarCodeModel),
		typeof(ContragentModel),
		typeof(DeviceModel),
		typeof(DeviceTypeModel),
		typeof(HostModel),
		typeof(LogModel),
		typeof(LogTypeModel),
		typeof(NomenclatureModel),
		typeof(OrderModel),
		typeof(OrganizationModel),
		typeof(PackageModel),
		typeof(PluLabelModel),
		typeof(PluModel),
		typeof(PluPackageModel),
		typeof(PluScaleModel),
		typeof(PluWeighingModel),
		typeof(PrinterModel),
		typeof(PrinterResourceModel),
		typeof(PrinterTypeModel),
		typeof(ProductionFacilityModel),
		typeof(ProductSeriesModel),
		typeof(ScaleModel),
		typeof(TaskModel),
		typeof(TaskTypeModel),
		typeof(TemplateModel),
		typeof(TemplateResourceModel),
		typeof(VersionModel),
		typeof(WorkShopModel),
	};
}