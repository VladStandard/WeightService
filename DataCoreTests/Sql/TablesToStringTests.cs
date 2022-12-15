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
using DataCore.Sql.TableScaleModels.Tasks;
using DataCore.Sql.TableScaleModels.TasksTypes;
using DataCore.Sql.TableScaleModels.Templates;
using DataCore.Sql.TableScaleModels.TemplatesResources;
using DataCore.Sql.TableScaleModels.Versions;
using DataCore.Sql.TableScaleModels.WorkShops;

namespace DataCoreTests.Sql;

[TestFixture]
internal class TablesToStringTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void DbTable_Validate_AccessModel()
    {
        DataCore.TableBaseModelAssertToString<AccessModel>();
    }

    [Test]
    public void DbTable_Validate_AppModel()
    {
        DataCore.TableBaseModelAssertToString<AppModel>();
    }

    [Test]
    public void DbTable_Validate_BarCodeModel()
    {
        DataCore.TableBaseModelAssertToString<BarCodeModel>();
    }

    //[Test]
    //public void DbTable_Validate_BarCodeTypeModel()
    //{
    //    DataCore.TableBaseModelAssertToString<BarCodeTypeModel>();
    //}

    [Test]
    public void DbTable_Validate_ContragentModel()
    {
        DataCore.TableBaseModelAssertToString<ContragentModel>();
    }

    [Test]
    public void DbTable_Validate_DeviceModel()
    {
	    DataCore.TableBaseModelAssertToString<DeviceModel>();
    }

	[Test]
	public void DbTable_Validate_DeviceTypeModel()
	{
		DataCore.TableBaseModelAssertToString<DeviceTypeModel>();
	}

	[Test]
	public void DbTable_Validate_DeviceTypeFkModel()
	{
		DataCore.TableBaseModelAssertToString<DeviceTypeFkModel>();
	}

	[Test]
	public void DbTable_Validate_DeviceScaleFkModel()
	{
		DataCore.TableBaseModelAssertToString<DeviceScaleFkModel>();
	}

    [Test]
    public void DbTable_Validate_LogModel()
    {
        DataCore.TableBaseModelAssertToString<LogModel>();
    }

    [Test]
    public void DbTable_Validate_LogTypeModel()
    {
        DataCore.TableBaseModelAssertToString<LogTypeModel>();
    }
    
    [Test]
    public void DbTable_Validate_NomenclatureModel()
    {
        DataCore.TableBaseModelAssertToString<NomenclatureModel>();
    }
    
    [Test]
    public void DbTable_Validate_NomenclatureCharacteristicsModel()
    {
        DataCore.TableBaseModelAssertToString<NomenclaturesCharacteristicsModel>();
    }

    [Test]
    public void DbTable_Validate_OrderModel()
    {
        DataCore.TableBaseModelAssertToString<OrderModel>();
    }

    [Test]
    public void DbTable_Validate_OrderWeighingModel()
    {
        DataCore.TableBaseModelAssertToString<OrderWeighingModel>();
    }

    [Test]
    public void DbTable_Validate_OrganizationModel()
    {
        DataCore.TableBaseModelAssertToString<OrganizationModel>();
    }

    [Test]
    public void DbTable_Validate_PackageModel()
    {
        DataCore.TableBaseModelAssertToString<PackageModel>();
    }

    [Test]
    public void DbTable_Validate_PluModel()
    {
        DataCore.TableBaseModelAssertToString<PluModel>();
    }

    [Test]
    public void DbTable_Validate_PluLabelModel()
    {
        DataCore.TableBaseModelAssertToString<PluLabelModel>();
    }

    [Test]
    public void DbTable_Validate_PluScaleModel()
    {
        DataCore.TableBaseModelAssertToString<PluScaleModel>();
    }

    [Test]
    public void DbTable_Validate_PluWeighingModel()
    {
        DataCore.TableBaseModelAssertToString<PluWeighingModel>();
    }

    [Test]
    public void DbTable_Validate_PrinterModel()
    {
        DataCore.TableBaseModelAssertToString<PrinterModel>();
    }

    [Test]
    public void DbTable_Validate_PrinterResourceModel()
    {
        DataCore.TableBaseModelAssertToString<PrinterResourceModel>();
    }

    [Test]
    public void DbTable_Validate_PrinterTypeModel()
    {
        DataCore.TableBaseModelAssertToString<PrinterTypeModel>();
    }

    [Test]
    public void DbTable_Validate_ProductionFacilityModel()
    {
        DataCore.TableBaseModelAssertToString<ProductionFacilityModel>();
    }

    [Test]
    public void DbTable_Validate_ProductSeriesModel()
    {
        DataCore.TableBaseModelAssertToString<ProductSeriesModel>();
    }

    [Test]
    public void DbTable_Validate_ScaleModel()
    {
        DataCore.TableBaseModelAssertToString<ScaleModel>();
    }

    [Test]
    public void DbTable_Validate_TaskModel()
    {
        DataCore.TableBaseModelAssertToString<TaskModel>();
    }

    [Test]
    public void DbTable_Validate_TaskTypeModel()
    {
        DataCore.TableBaseModelAssertToString<TaskTypeModel>();
    }

    [Test]
    public void DbTable_Validate_TemplateModel()
    {
        DataCore.TableBaseModelAssertToString<TemplateModel>();
    }

    [Test]
    public void DbTable_Validate_TemplateResourceModel()
    {
        DataCore.TableBaseModelAssertToString<TemplateResourceModel>();
    }

    [Test]
    public void DbTable_Validate_VersionModel()
    {
        DataCore.TableBaseModelAssertToString<VersionModel>();
    }

    [Test]
    public void DbTable_Validate_WorkShopModel()
    {
        DataCore.TableBaseModelAssertToString<WorkShopModel>();
    }
}
