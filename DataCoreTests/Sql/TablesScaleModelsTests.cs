// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;
using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;
using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;
using DataCore.Sql.TableScaleFkModels.NomenclaturesGroupsFks;
using DataCore.Sql.TableScaleModels.Access;
using DataCore.Sql.TableScaleModels.Apps;
using DataCore.Sql.TableScaleModels.BarCodes;
using DataCore.Sql.TableScaleModels.Contragents;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.DeviceTypes;
using DataCore.Sql.TableScaleModels.LogsTypes;
using DataCore.Sql.TableScaleModels.Nomenclatures;
using DataCore.Sql.TableScaleModels.NomenclaturesCharacteristics;
using DataCore.Sql.TableScaleModels.NomenclaturesGroups;
using DataCore.Sql.TableScaleModels.Orders;
using DataCore.Sql.TableScaleModels.Organizations;
using DataCore.Sql.TableScaleModels.Plus;
using DataCore.Sql.TableScaleModels.PlusLabels;
using DataCore.Sql.TableScaleModels.PlusPackages;
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

namespace DataCoreTests.Sql;

[TestFixture]
internal class TablesScaleModelsTests
{
	#region Public and private fields, properties, constructor

	private static DataCoreHelper DataCore => DataCoreHelper.Instance;

	#endregion

	#region Public and private methods

	[Test]
	public void SqlTableModel_New_EqualsDefault()
	{
		DataCore.AssertAction(() =>
		{
			List<SqlTableBase> sqlTables = DataCore.DataContext.GetTableModels();
			foreach (SqlTableBase sqlTable in sqlTables)
			{
				TestContext.WriteLine(sqlTable.GetType());
				Assert.AreEqual(true, sqlTable.EqualsNew());
				Assert.AreEqual(true, sqlTable.EqualsDefault());
			}
		}, false);
	}

	[Test]
	public void SqlTableType_Validate_IsFalse()
	{
		DataCore.AssertAction(() =>
		{
			List<Type> sqlTableTypes = DataCore.DataContext.GetTableTypes();
			foreach (Type sqlTableType in sqlTableTypes)
			{
				switch (sqlTableType)
				{
					case var cls when cls == typeof(AccessModel):
						CreateNewSubstitute<AccessModel>();
						break;
					case var cls when cls == typeof(AppModel):
						CreateNewSubstitute<AppModel>();
						break;
					case var cls when cls == typeof(BarCodeModel):
						CreateNewSubstitute<BarCodeModel>();
						break;
					case var cls when cls == typeof(ContragentModel):
						CreateNewSubstitute<ContragentModel>();
						break;
					case var cls when cls == typeof(DeviceModel):
						CreateNewSubstitute<DeviceModel>();
						break;
					case var cls when cls == typeof(DeviceTypeModel):
						CreateNewSubstitute<DeviceTypeModel>();
						break;
					case var cls when cls == typeof(DeviceTypeFkModel):
						CreateNewSubstitute<DeviceTypeFkModel>();
						break;
					case var cls when cls == typeof(DeviceScaleFkModel):
						CreateNewSubstitute<DeviceScaleFkModel>();
						break;
					case var cls when cls == typeof(LogModel):
						CreateNewSubstitute<LogModel>();
						break;
					case var cls when cls == typeof(LogTypeModel):
						CreateNewSubstitute<LogTypeModel>();
						break;
					case var cls when cls == typeof(NomenclatureModel):
						CreateNewSubstitute<NomenclatureModel>();
						break;
                    case var cls when cls == typeof(NomenclatureV2Model):
                        CreateNewSubstitute<NomenclatureV2Model>();
                        break;
                    case var cls when cls == typeof(NomenclatureGroupModel):
                        CreateNewSubstitute<NomenclatureGroupModel>();
                        break;
                    case var cls when cls == typeof(NomenclatureGroupFkModel):
                        CreateNewSubstitute<NomenclatureGroupFkModel>();
                        break;
                    case var cls when cls == typeof(NomenclaturesCharacteristicsModel):
                        CreateNewSubstitute<NomenclaturesCharacteristicsModel>();
                        break;
                    case var cls when cls == typeof(OrderModel):
						CreateNewSubstitute<OrderModel>();
						break;
					case var cls when cls == typeof(OrganizationModel):
						CreateNewSubstitute<OrganizationModel>();
						break;
					case var cls when cls == typeof(PackageModel):
						CreateNewSubstitute<PackageModel>();
						break;
					case var cls when cls == typeof(PluLabelModel):
						CreateNewSubstitute<PluLabelModel>();
						break;
					case var cls when cls == typeof(PluModel):
						CreateNewSubstitute<PluModel>();
						break;
					case var cls when cls == typeof(PluPackageModel):
						CreateNewSubstitute<PluPackageModel>();
						break;
					case var cls when cls == typeof(PluScaleModel):
						CreateNewSubstitute<PluScaleModel>();
						break;
					case var cls when cls == typeof(PluWeighingModel):
						CreateNewSubstitute<PluWeighingModel>();
						break;
					case var cls when cls == typeof(PrinterModel):
						CreateNewSubstitute<PrinterModel>();
						break;
					case var cls when cls == typeof(PrinterResourceModel):
						CreateNewSubstitute<PrinterResourceModel>();
						break;
					case var cls when cls == typeof(PrinterTypeModel):
						CreateNewSubstitute<PrinterTypeModel>();
						break;
					case var cls when cls == typeof(ProductionFacilityModel):
						CreateNewSubstitute<ProductionFacilityModel>();
						break;
					case var cls when cls == typeof(ProductSeriesModel):
						CreateNewSubstitute<ProductSeriesModel>();
						break;
					case var cls when cls == typeof(ScaleModel):
						CreateNewSubstitute<ScaleModel>();
						break;
					case var cls when cls == typeof(ScaleScreenShotModel):
						CreateNewSubstitute<ScaleScreenShotModel>();
						break;
					case var cls when cls == typeof(TaskModel):
						CreateNewSubstitute<TaskModel>();
						break;
					case var cls when cls == typeof(TaskTypeModel):
						CreateNewSubstitute<TaskTypeModel>();
						break;
					case var cls when cls == typeof(TemplateModel):
						CreateNewSubstitute<TemplateModel>();
						break;
					case var cls when cls == typeof(TemplateResourceModel):
						CreateNewSubstitute<TemplateResourceModel>();
						break;
					case var cls when cls == typeof(VersionModel):
						CreateNewSubstitute<VersionModel>();
						break;
					case var cls when cls == typeof(WorkShopModel):
						CreateNewSubstitute<WorkShopModel>();
						break;
				}
			}
		}, false);
	}

	private void CreateNewSubstitute<T>() where T : SqlTableBase, new()
	{
		TestContext.WriteLine($"Get {DataCore.DataContext.GetTableModelName<T>()}");
		// Arrange & Act.
		T items = DataCore.CreateNewSubstitute<T>(false);
		// Assert.
		DataCore.AssertSqlValidate(items, false);
	}

	[Test]
	public void SqlTableType_Validate_IsTrue()
	{
		DataCore.AssertAction(() =>
		{
			List<Type> sqlTableTypes = DataCore.DataContext.GetTableTypes();
			foreach (Type sqlTableType in sqlTableTypes)
			{
				switch (sqlTableType)
				{
					case var cls when cls == typeof(AccessModel):
						// Arrange.
						AccessModel access = DataCore.CreateNewSubstitute<AccessModel>(true);
						// Act.
						foreach (AccessRightsEnum rights in Enum.GetValues(typeof(AccessRightsEnum)))
						{
							access.Rights = (byte)rights;
							// Assert.
							DataCore.AssertSqlValidate(access, true);
						}
						break;
					case var cls when cls == typeof(AppModel):
						// Arrange & Act.
						AppModel app = DataCore.CreateNewSubstitute<AppModel>(true);
						// Assert.
						DataCore.AssertSqlValidate(app, true);
						break;
				}
			}
		}, false);
	}

	#endregion
}
