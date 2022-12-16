// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;
using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;
using DataCore.Sql.TableScaleFkModels.NomenclaturesCharacteristicsFks;
using DataCore.Sql.TableScaleFkModels.NomenclaturesGroupsFks;
using DataCore.Sql.TableScaleFkModels.PlusTemplatesFks;
using DataCore.Sql.TableScaleModels.Access;
using DataCore.Sql.TableScaleModels.Apps;
using DataCore.Sql.TableScaleModels.BarCodes;
using DataCore.Sql.TableScaleModels.Brands;
using DataCore.Sql.TableScaleModels.Contragents;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.DeviceTypes;
using DataCore.Sql.TableScaleModels.Logs;
using DataCore.Sql.TableScaleModels.LogsTypes;
using DataCore.Sql.TableScaleModels.Nomenclatures;
using DataCore.Sql.TableScaleModels.NomenclaturesCharacteristics;
using DataCore.Sql.TableScaleModels.NomenclaturesGroups;
using DataCore.Sql.TableScaleModels.Orders;
using DataCore.Sql.TableScaleModels.OrdersWeighings;
using DataCore.Sql.TableScaleModels.Organizations;
using DataCore.Sql.TableScaleModels.Packages;
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
internal class TablesSerializeTests
{
	#region Public and private fields, properties, constructor

	private static DataCoreHelper DataCore => DataCoreHelper.Instance;

	#endregion

	#region Public and private methods

	[Test]
	public void Table_AssertSerialize_Model()
	{
		DataCore.AssertAction(() =>
		{
			List<Type> sqlTableTypes = DataCore.DataContext.GetTableTypes();
			foreach (Type sqlTableType in sqlTableTypes)
			{
				switch (sqlTableType)
				{
					case var cls when cls == typeof(AccessModel):
						DataCore.TableBaseModelAssertSerialize<AccessModel>();
						break;
					case var cls when cls == typeof(AppModel):
						DataCore.TableBaseModelAssertSerialize<AppModel>();
						break;
					case var cls when cls == typeof(BarCodeModel):
						DataCore.TableBaseModelAssertSerialize<BarCodeModel>();
						break;
					case var cls when cls == typeof(BrandModel):
						DataCore.TableBaseModelAssertSerialize<BrandModel>();
						break;
					case var cls when cls == typeof(ContragentModel):
						DataCore.TableBaseModelAssertSerialize<ContragentModel>();
						break;
					case var cls when cls == typeof(DeviceModel):
						DataCore.TableBaseModelAssertSerialize<DeviceModel>();
						break;
					case var cls when cls == typeof(DeviceTypeModel):
						DataCore.TableBaseModelAssertSerialize<DeviceTypeModel>();
						break;
					case var cls when cls == typeof(DeviceTypeFkModel):
						DataCore.TableBaseModelAssertSerialize<DeviceTypeFkModel>();
						break;
					case var cls when cls == typeof(DeviceScaleFkModel):
						DataCore.TableBaseModelAssertSerialize<DeviceScaleFkModel>();
						break;
					case var cls when cls == typeof(LogModel):
						DataCore.TableBaseModelAssertSerialize<LogModel>();
						break;
					case var cls when cls == typeof(LogTypeModel):
						DataCore.TableBaseModelAssertSerialize<LogTypeModel>();
						break;
					case var cls when cls == typeof(NomenclatureModel):
						DataCore.TableBaseModelAssertSerialize<NomenclatureModel>();
						break;
					case var cls when cls == typeof(NomenclatureV2Model):
						DataCore.TableBaseModelAssertSerialize<NomenclatureV2Model>();
						break;
                    case var cls when cls == typeof(NomenclaturesCharacteristicsModel):
                        DataCore.TableBaseModelAssertSerialize<NomenclaturesCharacteristicsModel>();
                        break;
                    case var cls when cls == typeof(NomenclaturesCharacteristicsFkModel):
                        DataCore.TableBaseModelAssertSerialize<NomenclaturesCharacteristicsFkModel>();
                        break;
                    case var cls when cls == typeof(NomenclatureGroupModel):
						DataCore.TableBaseModelAssertSerialize<NomenclatureGroupModel>();
						break;
					case var cls when cls == typeof(NomenclaturesGroupFkModel):
						DataCore.TableBaseModelAssertSerialize<NomenclaturesGroupFkModel>();
						break;
					case var cls when cls == typeof(OrderModel):
						DataCore.TableBaseModelAssertSerialize<OrderModel>();
						break;
					case var cls when cls == typeof(OrderWeighingModel):
						DataCore.TableBaseModelAssertSerialize<OrderWeighingModel>();
						break;
					case var cls when cls == typeof(OrganizationModel):
						DataCore.TableBaseModelAssertSerialize<OrganizationModel>();
						break;
					case var cls when cls == typeof(PackageModel):
						DataCore.TableBaseModelAssertSerialize<PackageModel>();
						break;
					case var cls when cls == typeof(PluLabelModel):
						DataCore.TableBaseModelAssertSerialize<PluLabelModel>();
						break;
					case var cls when cls == typeof(PluModel):
						DataCore.TableBaseModelAssertSerialize<PluModel>();
						break;
					case var cls when cls == typeof(PluPackageModel):
						DataCore.TableBaseModelAssertSerialize<PluPackageModel>();
						break;
					case var cls when cls == typeof(PluScaleModel):
						DataCore.TableBaseModelAssertSerialize<PluScaleModel>();
						break;
					case var cls when cls == typeof(PluTemplateFkModel):
						DataCore.TableBaseModelAssertSerialize<PluTemplateFkModel>();
						break;
					case var cls when cls == typeof(PluWeighingModel):
						DataCore.TableBaseModelAssertSerialize<PluWeighingModel>();
						break;
					case var cls when cls == typeof(PrinterModel):
						DataCore.TableBaseModelAssertSerialize<PrinterModel>();
						break;
					case var cls when cls == typeof(PrinterResourceModel):
						DataCore.TableBaseModelAssertSerialize<PrinterResourceModel>();
						break;
					case var cls when cls == typeof(PrinterTypeModel):
						DataCore.TableBaseModelAssertSerialize<PrinterTypeModel>();
						break;
					case var cls when cls == typeof(ProductionFacilityModel):
						DataCore.TableBaseModelAssertSerialize<ProductionFacilityModel>();
						break;
					case var cls when cls == typeof(ProductSeriesModel):
						DataCore.TableBaseModelAssertSerialize<ProductSeriesModel>();
						break;
					case var cls when cls == typeof(ScaleModel):
						DataCore.TableBaseModelAssertSerialize<ScaleModel>();
						break;
					case var cls when cls == typeof(ScaleScreenShotModel):
						DataCore.TableBaseModelAssertSerialize<ScaleScreenShotModel>();
						break;
					case var cls when cls == typeof(TaskModel):
						DataCore.TableBaseModelAssertSerialize<TaskModel>();
						break;
					case var cls when cls == typeof(TaskTypeModel):
						DataCore.TableBaseModelAssertSerialize<TaskTypeModel>();
						break;
					case var cls when cls == typeof(TemplateModel):
						DataCore.TableBaseModelAssertSerialize<TemplateModel>();
						break;
					case var cls when cls == typeof(TemplateResourceModel):
						DataCore.TableBaseModelAssertSerialize<TemplateResourceModel>();
						break;
					case var cls when cls == typeof(VersionModel):
						DataCore.TableBaseModelAssertSerialize<VersionModel>();
						break;
					case var cls when cls == typeof(WorkShopModel):
						DataCore.TableBaseModelAssertSerialize<WorkShopModel>();
						break;
				}
			}
		}, false);

	}

	#endregion
}
