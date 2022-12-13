// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;
using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;
using DataCore.Sql.TableScaleFkModels.NomenclaturesGroupsFks;
using DataCore.Sql.TableScaleModels.Access;
using DataCore.Sql.TableScaleModels.Apps;
using DataCore.Sql.TableScaleModels.BarCodes;
using DataCore.Sql.TableScaleModels.Contragents;
using DataCore.Sql.TableScaleModels.Devices;
using DataCore.Sql.TableScaleModels.DeviceTypes;
using DataCore.Sql.TableScaleModels.Logs;
using DataCore.Sql.TableScaleModels.LogsTypes;
using DataCore.Sql.TableScaleModels.Nomenclatures;
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
using DataCore.Sql.TableScaleModels.Tasks;
using DataCore.Sql.TableScaleModels.TasksTypes;
using DataCore.Sql.TableScaleModels.Templates;
using DataCore.Sql.TableScaleModels.TemplatesResources;
using DataCore.Sql.TableScaleModels.Versions;
using DataCore.Sql.TableScaleModels.WorkShops;

namespace DataCoreTests.Sql;

[TestFixture]
internal class TablesEqualsNewTests
{
    #region Public and private fields, properties, constructor

    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    #endregion

    #region Public and private methods

    [Test]
    public void DbTable_Validate_AccessModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<AccessModel>();
    }

    [Test]
    public void DbTable_Validate_AppModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<AppModel>();
    }

    [Test]
    public void DbTable_Validate_BarCodeModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<BarCodeModel>();
    }

    [Test]
    public void DbTable_Validate_ContragentModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<ContragentModel>();
    }

    [Test]
    public void DbTable_Validate_DeviceModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<DeviceModel>();
    }

    [Test]
    public void DbTable_Validate_DeviceTypeModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<DeviceTypeModel>();
    }

    [Test]
    public void DbTable_Validate_DeviceTypeFkModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<DeviceTypeFkModel>();
    }

    [Test]
    public void DbTable_Validate_DeviceScaleFkModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<DeviceScaleFkModel>();
    }

    [Test]
    public void DbTable_Validate_LogModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<LogModel>();
    }

    [Test]
    public void DbTable_Validate_LogTypeModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<LogTypeModel>();
    }

    [Test]
    public void DbTable_Validate_NomenclatureModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<NomenclatureModel>();
    }

    [Test]
    public void DbTable_Validate_NomenclatureGroupModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<NomenclatureGroupModel>();
    }

    [Test]
    public void DbTable_Validate_NomenclatureGroupFkModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<NomenclatureGroupFkModel>();
    }

    [Test]
    public void DbTable_Validate_OrderModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<OrderModel>();
    }

    [Test]
    public void DbTable_Validate_OrderWeighingModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<OrderWeighingModel>();
    }

    [Test]
    public void DbTable_Validate_OrganizationModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<OrganizationModel>();
    }

    [Test]
    public void DbTable_Validate_PackageModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<PackageModel>();
    }

    [Test]
    public void DbTable_Validate_PluModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<PluModel>();
    }

    [Test]
    public void DbTable_Validate_PluLabelModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<PluLabelModel>();
    }

    [Test]
    public void DbTable_Validate_PluScaleModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<PluScaleModel>();
    }

    [Test]
    public void DbTable_Validate_PluWeighingModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<PluWeighingModel>();
    }

    [Test]
    public void DbTable_Validate_PrinterModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<PrinterModel>();
    }

    [Test]
    public void DbTable_Validate_PrinterResourceModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<PrinterResourceModel>();
    }

    [Test]
    public void DbTable_Validate_PrinterTypeModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<PrinterTypeModel>();
    }

    [Test]
    public void DbTable_Validate_ProductionFacilityModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<ProductionFacilityModel>();
    }

    [Test]
    public void DbTable_Validate_ProductSeriesModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<ProductSeriesModel>();
    }

    [Test]
    public void DbTable_Validate_ScaleModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<ScaleModel>();
    }

    [Test]
    public void DbTable_Validate_TaskModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<TaskModel>();
    }

    [Test]
    public void DbTable_Validate_TaskTypeModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<TaskTypeModel>();
    }

    [Test]
    public void DbTable_Validate_TemplateModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<TemplateModel>();
    }

    [Test]
    public void DbTable_Validate_TemplateResourceModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<TemplateResourceModel>();
    }

    [Test]
    public void DbTable_Validate_VersionModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<VersionModel>();
    }

    [Test]
    public void DbTable_Validate_WorkShopModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<WorkShopModel>();
    }

    #endregion
}
