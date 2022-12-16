// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;
using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;
using DataCore.Sql.TableScaleFkModels.NomenclaturesCharacteristicsFks;
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
using DataCore.Sql.TableScaleModels.NomenclaturesGroups;
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

namespace DataCoreTests.Sql.TableScaleDbContent;

[TestFixture]
internal class AssertSqlDbContentValidateTests
{
    #region Public and private fields, properties, constructor

    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    #endregion

    #region Public and private methods

    [Test]
    public void DbContent_Validate_AccessModel()
    {
        DataCore.AssertSqlDbContentValidate<AccessModel>();
    }

    [Test]
    public void DbContent_Validate_AppModel()
    {
        DataCore.AssertSqlDbContentValidate<AppModel>();
    }

    [Test]
    public void DbContent_Validate_BarCodeModel()
    {
        DataCore.AssertSqlDbContentValidate<BarCodeModel>();
    }

    [Test]
    public void DbContent_Validate_ContragentModel()
    {
        DataCore.AssertSqlDbContentValidate<ContragentModel>();
    }

    [Test]
    public void DbContent_Validate_DeviceModel()
    {
	    DataCore.AssertSqlDbContentValidate<DeviceModel>();
    }

	[Test]
	public void DbContent_Validate_DeviceTypeModel()
	{
		DataCore.AssertSqlDbContentValidate<DeviceTypeModel>();
	}

	[Test]
	public void DbContent_Validate_DeviceTypeFkModel()
	{
		DataCore.AssertSqlDbContentValidate<DeviceTypeFkModel>();
	}

    [Test]
    public void DbContent_Validate_DeviceScaleFkModel()
    {
        DataCore.AssertSqlDbContentValidate<DeviceScaleFkModel>();
    }

    [Test]
    public void DbContent_Validate_LogModel()
    {
        DataCore.AssertSqlDbContentValidate<LogModel>();
    }

    [Test]
    public void DbContent_Validate_LogTypeModel()
    {
        DataCore.AssertSqlDbContentValidate<LogTypeModel>();
    }

    [Test]
    public void DbContent_Validate_NomenclatureModel()
    {
        DataCore.AssertSqlDbContentValidate<NomenclatureModel>();
    }

    [Test]
    public void DbContent_Validate_NomenclatureV2Model()
    {
        DataCore.AssertSqlDbContentValidate<NomenclatureV2Model>();
    }

    [Test]
    public void DbContent_Validate_NomenclatureGroupModel()
    {
        DataCore.AssertSqlDbContentValidate<NomenclatureGroupModel>();
    }

    [Test]
    public void DbContent_Validate_OrderModel()
    {
        DataCore.AssertSqlDbContentValidate<OrderModel>();
    }

    [Test]
    public void DbContent_Validate_OrderWeighingModel()
    {
        DataCore.AssertSqlDbContentValidate<OrderWeighingModel>();
    }

    [Test]
    public void DbContent_Validate_OrganizationModel()
    {
        DataCore.AssertSqlDbContentValidate<OrganizationModel>();
    }

    [Test]
    public void DbContent_Validate_PackageModel()
    {
        DataCore.AssertSqlDbContentValidate<PackageModel>();
    }

    [Test]
    public void DbContent_Validate_PluModel()
    {
        DataCore.AssertSqlDbContentValidate<PluModel>();
    }

    [Test]
    public void DbContent_Validate_PluLabelModel()
    {
        DataCore.AssertSqlDbContentValidate<PluLabelModel>();
    }

    [Test]
    public void DbContent_Validate_PluScaleModel()
    {
        DataCore.AssertSqlDbContentValidate<PluScaleModel>();
    }

    [Test]
    public void DbContent_Validate_PluWeighingModel()
    {
        DataCore.AssertSqlDbContentValidate<PluWeighingModel>();
    }

    [Test]
    public void DbContent_Validate_PrinterModel()
    {
        DataCore.AssertSqlDbContentValidate<PrinterModel>();
    }

    [Test]
    public void DbContent_Validate_PrinterResourceModel()
    {
        DataCore.AssertSqlDbContentValidate<PrinterResourceModel>();
    }

    [Test]
    public void DbContent_Validate_PrinterTypeModel()
    {
        DataCore.AssertSqlDbContentValidate<PrinterTypeModel>();
    }

    [Test]
    public void DbContent_Validate_ProductionFacilityModel()
    {
        DataCore.AssertSqlDbContentValidate<ProductionFacilityModel>();
    }

    [Test]
    public void DbContent_Validate_ProductSeriesModel()
    {
        DataCore.AssertSqlDbContentValidate<ProductSeriesModel>();
    }

    [Test]
    public void DbContent_Validate_ScaleModel()
    {
        DataCore.AssertSqlDbContentValidate<ScaleModel>();
    }

    [Test]
    public void DbContent_Validate_ScaleScreenShotModel()
    {
        DataCore.AssertSqlDbContentValidate<ScaleScreenShotModel>();
    }

    [Test]
    public void DbContent_Validate_TaskModel()
    {
        DataCore.AssertSqlDbContentValidate<TaskModel>();
    }

    [Test]
    public void DbContent_Validate_TaskTypeModel()
    {
        DataCore.AssertSqlDbContentValidate<TaskTypeModel>();
    }

    [Test]
    public void DbContent_Validate_TemplateModel()
    {
        DataCore.AssertSqlDbContentValidate<TemplateModel>();
    }

    [Test]
    public void DbContent_Validate_TemplateResourceModel()
    {
        DataCore.AssertSqlDbContentValidate<TemplateResourceModel>();
    }

    [Test]
    public void DbContent_Validate_VersionModel()
    {
        DataCore.AssertSqlDbContentValidate<VersionModel>();
    }

    [Test]
    public void DbContent_Validate_WorkShopModel()
    {
        DataCore.AssertSqlDbContentValidate<WorkShopModel>();
    }

    [Test]
    public void DbContent_Validate_NomenclaturesCharacteristicsModel()
    {
        DataCore.AssertSqlDbContentValidate<NomenclaturesCharacteristicsModel>();
    }

    [Test]
    public void DbContent_Validate_NomenclaturesCharacteristicsFkModel()
    {
        DataCore.AssertSqlDbContentValidate<NomenclaturesCharacteristicsFkModel>();
    }

    #endregion
}
