// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.BundlesFks;
using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;
using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;
using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleModels.Access;
using DataCore.Sql.TableScaleModels.Apps;
using DataCore.Sql.TableScaleModels.BarCodes;
using DataCore.Sql.TableScaleModels.Boxes;
using DataCore.Sql.TableScaleModels.Bundles;
using DataCore.Sql.TableScaleModels.Contragents;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.DeviceTypes;
using DataCore.Sql.TableScaleModels.Logs;
using DataCore.Sql.TableScaleModels.LogsTypes;
using DataCore.Sql.TableScaleModels.Nomenclatures;
using DataCore.Sql.TableScaleModels.NomenclaturesCharacteristics;
using DataCore.Sql.TableScaleModels.Orders;
using DataCore.Sql.TableScaleModels.OrdersWeighings;
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

namespace DataCoreTests.Sql.Tables;

[TestFixture]
internal class SqlTableBaseTests
{
	#region Public and private fields, properties, constructor

	private static DataCoreHelper DataCore => DataCoreHelper.Instance;

	#endregion

	#region Public and private methods

	[Test]
	public void AccessModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<AccessModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<AccessModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<AccessModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void AppModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<AppModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<AppModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<AppModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void BarCodeModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<BarCodeModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<BarCodeModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<BarCodeModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void BoxModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<BoxModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<BoxModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<BoxModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void BundleModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<BundleModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<BundleModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<BundleModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void BundleFkModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<BundleFkModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<BundleFkModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<BundleModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void PluBundleFkModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<PluBundleFkModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<PluBundleFkModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<PluBundleFkModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void ContragentModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<ContragentModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<ContragentModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<ContragentModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void DeviceModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<DeviceModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<DeviceModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<DeviceModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void DeviceTypeModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<DeviceTypeModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<DeviceTypeModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<DeviceTypeModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void DeviceTypeFkModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<DeviceTypeFkModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<DeviceTypeFkModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<DeviceTypeFkModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void DeviceScaleFkModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<DeviceScaleFkModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<DeviceScaleFkModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<DeviceScaleFkModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void LogModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<LogModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<LogModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<LogModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void LogTypeModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<LogTypeModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<LogTypeModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<LogTypeModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void NomenclatureModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<NomenclatureModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<NomenclatureModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<NomenclatureModel>(nameof(SqlTableBase.IsMarked));
	}

    [Test]
    public void NomenclaturesCharacteristics_AssertSqlFields_Check()
    {
        DataCore.AssertSqlPropertyCheckDt<NomenclaturesCharacteristicsModel>(nameof(SqlTableBase.CreateDt));
        DataCore.AssertSqlPropertyCheckDt<NomenclaturesCharacteristicsModel>(nameof(SqlTableBase.ChangeDt));
        DataCore.AssertSqlPropertyCheckBool<NomenclaturesCharacteristicsModel>(nameof(SqlTableBase.IsMarked));
    }

    [Test]
	public void OrderModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<OrderModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<OrderModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<OrderModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void OrderWeighingModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<OrderWeighingModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<OrderWeighingModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<OrderWeighingModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void OrganizationModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<OrganizationModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<OrganizationModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<OrganizationModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void PluLabelModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<PluLabelModel>(nameof(PluLabelModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<PluLabelModel>(nameof(PluLabelModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<PluLabelModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void PluModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<PluModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<PluModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<PluModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void PluScaleModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<PluScaleModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<PluScaleModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<PluScaleModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void PluWeighingModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<PluWeighingModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<PluWeighingModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<PluWeighingModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void PrinterModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<PrinterModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<PrinterModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<PrinterModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void PrinterResourceModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<PrinterResourceModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<PrinterResourceModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<PrinterResourceModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void PrinterTypeModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<PrinterTypeModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<PrinterTypeModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<PrinterTypeModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void ProductionFacilityModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<ProductionFacilityModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<ProductionFacilityModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<ProductionFacilityModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void ProductSeriesModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<ProductSeriesModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<ProductSeriesModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<ProductSeriesModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void ScaleModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<ScaleModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<ScaleModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<ScaleModel>(nameof(SqlTableBase.IsMarked));
		DataCore.AssertSqlPropertyCheckString<ScaleModel>(nameof(SqlTableBase.Description));
	}

	[Test]
	public void ScaleScreenShotModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<ScaleScreenShotModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<ScaleScreenShotModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<ScaleScreenShotModel>(nameof(SqlTableBase.IsMarked));
	}
	
	[Test]
	public void TaskModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<TaskModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<TaskModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<TaskModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void TaskTypeModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<TaskTypeModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<TaskTypeModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<TaskTypeModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void TemplateModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<TemplateModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<TemplateModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<TemplateModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void TemplateResourceModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<TemplateResourceModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<TemplateResourceModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<TemplateResourceModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void VersionModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<VersionModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<VersionModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckDt<VersionModel>(nameof(VersionModel.ReleaseDt));
		DataCore.AssertSqlPropertyCheckString<VersionModel>(nameof(SqlTableBase.Description));
		DataCore.AssertSqlPropertyCheckString<VersionModel>(nameof(VersionModel.Version));
		DataCore.AssertSqlPropertyCheckBool<VersionModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void WorkShopModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<WorkShopModel>(nameof(SqlTableBase.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<WorkShopModel>(nameof(SqlTableBase.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<WorkShopModel>(nameof(WorkShopModel.IsMarked));
	}

	#endregion
}
