// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;
using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;
using DataCore.Sql.TableScaleModels.Access;
using DataCore.Sql.TableScaleModels.Apps;
using DataCore.Sql.TableScaleModels.BarCodes;
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
using DataCore.Sql.TableScaleModels.Packages;
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
		DataCore.AssertSqlPropertyCheckDt<AccessModel>(nameof(AccessModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<AccessModel>(nameof(AccessModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<AccessModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void AppModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<AppModel>(nameof(AppModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<AppModel>(nameof(AppModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<AppModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void BarCodeModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<BarCodeModel>(nameof(BarCodeModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<BarCodeModel>(nameof(BarCodeModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<BarCodeModel>(nameof(SqlTableBase.IsMarked));
	}

	//[Test]
	//public void BarCodeTypeModel_AssertSqlFields_Check()
	//{
	//	// Arrange & Act & Assert.
	//	DataCore.AssertSqlPropertyCheckDt<BarCodeTypeModel>(nameof(BarCodeTypeModel.CreateDt));
	//	DataCore.AssertSqlPropertyCheckDt<BarCodeTypeModel>(nameof(BarCodeTypeModel.ChangeDt));
	//	DataCore.AssertSqlPropertyCheckBool<BarCodeTypeModel>(nameof(SqlTableBase.IsMarked));
	//}

	[Test]
	public void ContragentModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<ContragentModel>(nameof(ContragentModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<ContragentModel>(nameof(ContragentModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<ContragentModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void DeviceModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<DeviceModel>(nameof(DeviceModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<DeviceModel>(nameof(DeviceModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<DeviceModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void DeviceTypeModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<DeviceTypeModel>(nameof(DeviceTypeModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<DeviceTypeModel>(nameof(DeviceTypeModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<DeviceTypeModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void DeviceTypeFkModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<DeviceTypeFkModel>(nameof(DeviceTypeFkModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<DeviceTypeFkModel>(nameof(DeviceTypeFkModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<DeviceTypeFkModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void DeviceScaleFkModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<DeviceScaleFkModel>(nameof(DeviceScaleFkModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<DeviceScaleFkModel>(nameof(DeviceScaleFkModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<DeviceScaleFkModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void LogModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<LogModel>(nameof(LogModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<LogModel>(nameof(LogModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<LogModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void LogTypeModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<LogTypeModel>(nameof(LogTypeModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<LogTypeModel>(nameof(LogTypeModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<LogTypeModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void NomenclatureModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<NomenclatureModel>(nameof(NomenclatureModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<NomenclatureModel>(nameof(NomenclatureModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<NomenclatureModel>(nameof(SqlTableBase.IsMarked));
	}

    [Test]
    public void NomenclaturesCharacteristics_AssertSqlFields_Check()
    {
        DataCore.AssertSqlPropertyCheckDt<NomenclaturesCharacteristicsModel>(nameof(NomenclaturesCharacteristicsModel.CreateDt));
        DataCore.AssertSqlPropertyCheckDt<NomenclaturesCharacteristicsModel>(nameof(NomenclaturesCharacteristicsModel.ChangeDt));
        DataCore.AssertSqlPropertyCheckBool<NomenclaturesCharacteristicsModel>(nameof(NomenclaturesCharacteristicsModel.IsMarked));
    }

    [Test]
	public void OrderModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<OrderModel>(nameof(OrderModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<OrderModel>(nameof(OrderModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<OrderModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void OrderWeighingModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<OrderWeighingModel>(nameof(OrderWeighingModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<OrderWeighingModel>(nameof(OrderWeighingModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<OrderWeighingModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void OrganizationModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<OrganizationModel>(nameof(OrganizationModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<OrganizationModel>(nameof(OrganizationModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<OrganizationModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void PackageModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<PackageModel>(nameof(PackageModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<PackageModel>(nameof(PackageModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<PackageModel>(nameof(SqlTableBase.IsMarked));
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
		DataCore.AssertSqlPropertyCheckDt<PluModel>(nameof(PluModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<PluModel>(nameof(PluModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<PluModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void PluScaleModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<PluScaleModel>(nameof(PluScaleModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<PluScaleModel>(nameof(PluScaleModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<PluScaleModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void PluWeighingModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<PluWeighingModel>(nameof(PluWeighingModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<PluWeighingModel>(nameof(PluWeighingModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<PluWeighingModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void PrinterModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<PrinterModel>(nameof(PrinterModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<PrinterModel>(nameof(PrinterModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<PrinterModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void PrinterResourceModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<PrinterResourceModel>(nameof(PrinterResourceModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<PrinterResourceModel>(nameof(PrinterResourceModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<PrinterResourceModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void PrinterTypeModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<PrinterTypeModel>(nameof(PrinterTypeModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<PrinterTypeModel>(nameof(PrinterTypeModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<PrinterTypeModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void ProductionFacilityModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<ProductionFacilityModel>(nameof(ProductionFacilityModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<ProductionFacilityModel>(nameof(ProductionFacilityModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<ProductionFacilityModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void ProductSeriesModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<ProductSeriesModel>(nameof(ProductSeriesModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<ProductSeriesModel>(nameof(ProductSeriesModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<ProductSeriesModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void ScaleModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<ScaleModel>(nameof(ScaleModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<ScaleModel>(nameof(ScaleModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<ScaleModel>(nameof(SqlTableBase.IsMarked));
		//DataCore.AssertSqlFieldStringCheck<ScaleModel>(nameof(ScaleModel.Host.Name));
		DataCore.AssertSqlPropertyCheckString<ScaleModel>(nameof(ScaleModel.Description));
	}

	[Test]
	public void ScaleScreenShotModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<ScaleScreenShotModel>(nameof(ScaleScreenShotModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<ScaleScreenShotModel>(nameof(ScaleScreenShotModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<ScaleScreenShotModel>(nameof(ScaleScreenShotModel.IsMarked));
	}
	
	[Test]
	public void TaskModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<TaskModel>(nameof(TaskModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<TaskModel>(nameof(TaskModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<TaskModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void TaskTypeModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<TaskTypeModel>(nameof(TaskTypeModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<TaskTypeModel>(nameof(TaskTypeModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<TaskTypeModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void TemplateModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<TemplateModel>(nameof(TemplateModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<TemplateModel>(nameof(TemplateModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<TemplateModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void TemplateResourceModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<TemplateResourceModel>(nameof(TemplateResourceModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<TemplateResourceModel>(nameof(TemplateResourceModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<TemplateResourceModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void VersionModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<VersionModel>(nameof(VersionModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<VersionModel>(nameof(VersionModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckDt<VersionModel>(nameof(VersionModel.ReleaseDt));
		DataCore.AssertSqlPropertyCheckString<VersionModel>(nameof(VersionModel.Description));
		DataCore.AssertSqlPropertyCheckString<VersionModel>(nameof(VersionModel.Version));
		DataCore.AssertSqlPropertyCheckBool<VersionModel>(nameof(SqlTableBase.IsMarked));
	}

	[Test]
	public void WorkShopModel_AssertSqlFields_Check()
	{
		// Arrange & Act & Assert.
		DataCore.AssertSqlPropertyCheckDt<WorkShopModel>(nameof(WorkShopModel.CreateDt));
		DataCore.AssertSqlPropertyCheckDt<WorkShopModel>(nameof(WorkShopModel.ChangeDt));
		DataCore.AssertSqlPropertyCheckBool<WorkShopModel>(nameof(WorkShopModel.IsMarked));
	}

	#endregion
}
