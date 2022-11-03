// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using NUnit.Framework.Interfaces;

namespace DataCoreTests.Sql.Core;

[TestFixture]
internal class DataContextTests
{
	#region Public and private fields, properties, constructor

	private DataCoreHelper DataCore { get; } = DataCoreHelper.Instance;

	#endregion

	#region Public and private methods

	[Test]
	public void GetListNotNull_Exec_DoesNotThrow()
	{
		DataCore.AssertAction(() =>
		{
			List<Type> sqlTableTypes = DataCore.DataContext.GetTableTypes();
			foreach (Type sqlTableType in sqlTableTypes)
			{
				switch (sqlTableType)
				{
					case var cls when cls == typeof(AccessModel):
						// Arrange & Act.
						GetListNotNull<AccessModel>();
						break;
					case var cls when cls == typeof(AppModel):
						// Arrange & Act.
						GetListNotNull<AppModel>();
						break;
					case var cls when cls == typeof(BarCodeModel):
						// Arrange & Act.
						GetListNotNull<BarCodeModel>();
						break;
					case var cls when cls == typeof(ContragentModel):
						// Arrange & Act.
						GetListNotNull<ContragentModel>();
						break;
					case var cls when cls == typeof(DeviceModel):
						// Arrange & Act.
						GetListNotNull<DeviceModel>();
						break;
					case var cls when cls == typeof(DeviceTypeModel):
						// Arrange & Act.
						GetListNotNull<DeviceTypeModel>();
						break;
					case var cls when cls == typeof(DeviceTypeFkModel):
						// Arrange & Act.
						GetListNotNull<DeviceTypeFkModel>();
						break;
					case var cls when cls == typeof(DeviceScaleFkModel):
						// Arrange & Act.
						GetListNotNull<DeviceScaleFkModel>();
						break;
					case var cls when cls == typeof(LogModel):
						// Arrange & Act.
						GetListNotNull<LogModel>();
						break;
					case var cls when cls == typeof(LogTypeModel):
						// Arrange & Act.
						GetListNotNull<LogTypeModel>();
						break;
					case var cls when cls == typeof(NomenclatureModel):
						// Arrange & Act.
						GetListNotNull<NomenclatureModel>();
						break;
					case var cls when cls == typeof(OrderModel):
						GetListNotNull<OrderModel>();
						break;
					case var cls when cls == typeof(OrganizationModel):
						GetListNotNull<OrganizationModel>();
						break;
					case var cls when cls == typeof(PackageModel):
						GetListNotNull<PackageModel>();
						break;
					case var cls when cls == typeof(PluLabelModel):
						GetListNotNull<PluLabelModel>();
						break;
					case var cls when cls == typeof(PluModel):
						GetListNotNull<PluModel>();
						break;
					case var cls when cls == typeof(PluPackageModel):
						GetListNotNull<PluPackageModel>();
						break;
					case var cls when cls == typeof(PluScaleModel):
						GetListNotNull<PluScaleModel>();
						break;
					case var cls when cls == typeof(PluWeighingModel):
						GetListNotNull<PluWeighingModel>();
						break;
					case var cls when cls == typeof(PrinterModel):
						GetListNotNull<PrinterModel>();
						break;
					case var cls when cls == typeof(PrinterResourceModel):
						GetListNotNull<PrinterResourceModel>();
						break;
					case var cls when cls == typeof(PrinterTypeModel):
						GetListNotNull<PrinterTypeModel>();
						break;
					case var cls when cls == typeof(ProductionFacilityModel):
						GetListNotNull<ProductionFacilityModel>();
						break;
					case var cls when cls == typeof(ProductSeriesModel):
						GetListNotNull<ProductSeriesModel>();
						break;
					case var cls when cls == typeof(ScaleModel):
						GetListNotNull<ScaleModel>();
						break;
					case var cls when cls == typeof(ScaleScreenShotModel):
						GetListNotNull<ScaleScreenShotModel>();
						break;
					case var cls when cls == typeof(TaskModel):
						GetListNotNull<TaskModel>();
						break;
					case var cls when cls == typeof(TaskTypeModel):
						GetListNotNull<TaskTypeModel>();
						break;
					case var cls when cls == typeof(TemplateModel):
						GetListNotNull<TemplateModel>();
						break;
					case var cls when cls == typeof(TemplateResourceModel):
						GetListNotNull<TemplateResourceModel>();
						break;
					case var cls when cls == typeof(VersionModel):
						GetListNotNull<VersionModel>();
						break;
					case var cls when cls == typeof(WorkShopModel):
						GetListNotNull<WorkShopModel>();
						break;
				}
			}
		});
	}

	private void GetListNotNull<T>() where T : SqlTableBase, new()
	{
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(true, true);
		// Arrange & Act.
		List<T> items = DataCore.DataContext.GetListNotNull<T>(sqlCrudConfig);
		TestContext.WriteLine($"Get {DataCore.DataContext.GetTableModelName<T>()} list: {items.Count}");
		foreach (T item in items)
		{
			// Assert.
			DataCore.AssertSqlValidate(item, true);
		}
	}

	#endregion
}
