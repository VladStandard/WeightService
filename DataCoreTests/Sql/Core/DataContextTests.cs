// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;
using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleModels.Access;
using DataCore.Sql.TableScaleModels.Apps;
using DataCore.Sql.TableScaleModels.BarCodes;
using DataCore.Sql.TableScaleModels.Boxes;
using DataCore.Sql.TableScaleModels.Brands;
using DataCore.Sql.TableScaleModels.Bundles;
using DataCore.Sql.TableScaleModels.Contragents;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.DeviceTypes;
using DataCore.Sql.TableScaleModels.Logs;
using DataCore.Sql.TableScaleModels.LogsTypes;
using DataCore.Sql.TableScaleModels.Nomenclatures;
using DataCore.Sql.TableScaleModels.Orders;
using DataCore.Sql.TableScaleModels.Organizations;
using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.PlusLabels;
using DataCore.Sql.TableScaleModels.PlusScales;
using DataCore.Sql.TableScaleModels.PlusWeighings;
using DataCore.Sql.TableScaleModels.Printers;
using DataCore.Sql.TableScaleModels.PrintersResources;
using DataCore.Sql.TableScaleModels.PrintersTypes;
using DataCore.Sql.TableScaleModels.ProductionFacilities;
using DataCore.Sql.TableScaleModels.ProductSeries;
using DataCore.Sql.TableScaleModels.Scales;
using DataCore.Sql.TableScaleModels.ScalesScreenshots;
using DataCore.Sql.TableScaleModels.Tasks;
using DataCore.Sql.TableScaleModels.TasksTypes;
using DataCore.Sql.TableScaleModels.Templates;
using DataCore.Sql.TableScaleModels.TemplatesResources;
using DataCore.Sql.TableScaleModels.Versions;
using DataCore.Sql.TableScaleModels.WorkShops;

namespace DataCoreTests.Sql.Core;

[TestFixture]
internal class DataContextTests
{
	#region Public and private fields, properties, constructor

	private static DataCoreHelper DataCore => DataCoreHelper.Instance;

	#endregion

	#region Public and private methods

	[Test]
	public void GetListNotNullable_Exec_DoesNotThrow()
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
						GetListNotNullable<AccessModel>();
						break;
					case var cls when cls == typeof(AppModel):
						// Arrange & Act.
						GetListNotNullable<AppModel>();
						break;
					case var cls when cls == typeof(BarCodeModel):
						// Arrange & Act.
						GetListNotNullable<BarCodeModel>();
						break;
					case var cls when cls == typeof(BrandModel):
						// Arrange & Act.
						GetListNotNullable<BrandModel>();
						break;
					case var cls when cls == typeof(BoxModel):
						// Arrange & Act.
						GetListNotNullable<BoxModel>();
						break;
					case var cls when cls == typeof(BundleModel):
						// Arrange & Act.
						GetListNotNullable<BundleModel>();
						break;
					case var cls when cls == typeof(ContragentModel):
						// Arrange & Act.
						GetListNotNullable<ContragentModel>();
						break;
					case var cls when cls == typeof(DeviceModel):
						// Arrange & Act.
						GetListNotNullable<DeviceModel>();
						break;
					case var cls when cls == typeof(DeviceTypeModel):
						// Arrange & Act.
						GetListNotNullable<DeviceTypeModel>();
						break;
					case var cls when cls == typeof(DeviceTypeFkModel):
						// Arrange & Act.
						GetListNotNullable<DeviceTypeFkModel>();
						break;
					case var cls when cls == typeof(DeviceScaleFkModel):
						// Arrange & Act.
						GetListNotNullable<DeviceScaleFkModel>();
						break;
					case var cls when cls == typeof(LogModel):
						// Arrange & Act.
						GetListNotNullable<LogModel>();
						break;
					case var cls when cls == typeof(LogTypeModel):
						// Arrange & Act.
						GetListNotNullable<LogTypeModel>();
						break;
					case var cls when cls == typeof(NomenclatureModel):
						// Arrange & Act.
						GetListNotNullable<NomenclatureModel>();
						break;
					case var cls when cls == typeof(OrderModel):
						GetListNotNullable<OrderModel>();
						break;
					case var cls when cls == typeof(OrganizationModel):
						GetListNotNullable<OrganizationModel>();
						break;
					case var cls when cls == typeof(PluLabelModel):
						GetListNotNullable<PluLabelModel>();
						break;
					case var cls when cls == typeof(PluModel):
						GetListNotNullable<PluModel>();
						break;
					case var cls when cls == typeof(PluBundleFkModel):
						GetListNotNullable<PluBundleFkModel>();
						break;
					case var cls when cls == typeof(PluScaleModel):
						GetListNotNullable<PluScaleModel>();
						break;
					case var cls when cls == typeof(PluWeighingModel):
						GetListNotNullable<PluWeighingModel>();
						break;
					case var cls when cls == typeof(PrinterModel):
						GetListNotNullable<PrinterModel>();
						break;
					case var cls when cls == typeof(PrinterResourceModel):
						GetListNotNullable<PrinterResourceModel>();
						break;
					case var cls when cls == typeof(PrinterTypeModel):
						GetListNotNullable<PrinterTypeModel>();
						break;
					case var cls when cls == typeof(ProductionFacilityModel):
						GetListNotNullable<ProductionFacilityModel>();
						break;
					case var cls when cls == typeof(ProductSeriesModel):
						GetListNotNullable<ProductSeriesModel>();
						break;
					case var cls when cls == typeof(ScaleModel):
						GetListNotNullable<ScaleModel>();
						break;
					case var cls when cls == typeof(ScaleScreenShotModel):
						GetListNotNullable<ScaleScreenShotModel>();
						break;
					case var cls when cls == typeof(TaskModel):
						GetListNotNullable<TaskModel>();
						break;
					case var cls when cls == typeof(TaskTypeModel):
						GetListNotNullable<TaskTypeModel>();
						break;
					case var cls when cls == typeof(TemplateModel):
						GetListNotNullable<TemplateModel>();
						break;
					case var cls when cls == typeof(TemplateResourceModel):
						GetListNotNullable<TemplateResourceModel>();
						break;
					case var cls when cls == typeof(VersionModel):
						GetListNotNullable<VersionModel>();
						break;
					case var cls when cls == typeof(WorkShopModel):
						GetListNotNullable<WorkShopModel>();
						break;
				}
			}
		}, false);
	}

	private void GetListNotNullable<T>() where T : SqlTableBase, new()
	{
		SqlCrudConfigModel sqlCrudConfig = SqlCrudConfigUtils.GetCrudConfig(true, true);
		// Arrange & Act.
		List<T> items = DataCore.DataContext.GetListNotNullable<T>(sqlCrudConfig);
		TestContext.WriteLine($"Get {DataCore.DataContext.GetTableModelName<T>()} list: {items.Count}");
		foreach (T item in items)
		{
			// Assert.
			DataCore.AssertSqlValidate(item, true);
		}
	}

    #endregion
}
