// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;

namespace DataCoreTests.Sql;

[TestFixture]
internal class TablesToStringTests
{
    private DataCoreHelper Helper { get; } = DataCoreHelper.Instance;

    [Test]
    public void DbTable_Validate_AccessModel()
    {
        Helper.TableBaseModelAssertToString<AccessModel>();
    }

    [Test]
    public void DbTable_Validate_AppModel()
    {
        Helper.TableBaseModelAssertToString<AppModel>();
    }

    [Test]
    public void DbTable_Validate_BarCodeModel()
    {
        Helper.TableBaseModelAssertToString<BarCodeModel>();
    }

    [Test]
    public void DbTable_Validate_BarCodeTypeModel()
    {
        Helper.TableBaseModelAssertToString<BarCodeTypeModel>();
    }

    [Test]
    public void DbTable_Validate_ContragentModel()
    {
        Helper.TableBaseModelAssertToString<ContragentModel>();
    }

    [Test]
    public void DbTable_Validate_HostModel()
    {
        Helper.TableBaseModelAssertToString<HostModel>();
    }

    [Test]
    public void DbTable_Validate_LogModel()
    {
        Helper.TableBaseModelAssertToString<LogModel>();
    }

    [Test]
    public void DbTable_Validate_LogTypeModel()
    {
        Helper.TableBaseModelAssertToString<LogTypeModel>();
    }

    [Test]
    public void DbTable_Validate_NomenclatureModel()
    {
        Helper.TableBaseModelAssertToString<NomenclatureModel>();
    }

    [Test]
    public void DbTable_Validate_OrderModel()
    {
        Helper.TableBaseModelAssertToString<OrderModel>();
    }

    [Test]
    public void DbTable_Validate_OrderWeighingModel()
    {
        Helper.TableBaseModelAssertToString<OrderWeighingModel>();
    }

    [Test]
    public void DbTable_Validate_OrganizationModel()
    {
        Helper.TableBaseModelAssertToString<OrganizationModel>();
    }

    [Test]
    public void DbTable_Validate_PluModel()
    {
        Helper.TableBaseModelAssertToString<PluModel>();
    }

    [Test]
    public void DbTable_Validate_PluLabelModel()
    {
        Helper.TableBaseModelAssertToString<PluLabelModel>();
    }

    [Test]
    public void DbTable_Validate_PluObsoleteModel()
    {
        Helper.TableBaseModelAssertToString<PluObsoleteModel>();
    }

    [Test]
    public void DbTable_Validate_PluScaleModel()
    {
        Helper.TableBaseModelAssertToString<PluScaleModel>();
    }

    [Test]
    public void DbTable_Validate_PluWeighingModel()
    {
        Helper.TableBaseModelAssertToString<PluWeighingModel>();
    }

    [Test]
    public void DbTable_Validate_PrinterModel()
    {
        Helper.TableBaseModelAssertToString<PrinterModel>();
    }

    [Test]
    public void DbTable_Validate_PrinterResourceModel()
    {
        Helper.TableBaseModelAssertToString<PrinterResourceModel>();
    }

    [Test]
    public void DbTable_Validate_PrinterTypeModel()
    {
        Helper.TableBaseModelAssertToString<PrinterTypeModel>();
    }

    [Test]
    public void DbTable_Validate_ProductionFacilityModel()
    {
        Helper.TableBaseModelAssertToString<ProductionFacilityModel>();
    }

    [Test]
    public void DbTable_Validate_ProductSeriesModel()
    {
        Helper.TableBaseModelAssertToString<ProductSeriesModel>();
    }

    [Test]
    public void DbTable_Validate_ScaleModel()
    {
        Helper.TableBaseModelAssertToString<ScaleModel>();
    }

    [Test]
    public void DbTable_Validate_TaskModel()
    {
        Helper.TableBaseModelAssertToString<TaskModel>();
    }

    [Test]
    public void DbTable_Validate_TaskTypeModel()
    {
        Helper.TableBaseModelAssertToString<TaskTypeModel>();
    }

    [Test]
    public void DbTable_Validate_TemplateModel()
    {
        Helper.TableBaseModelAssertToString<TemplateModel>();
    }

    [Test]
    public void DbTable_Validate_TemplateResourceModel()
    {
        Helper.TableBaseModelAssertToString<TemplateResourceModel>();
    }

    [Test]
    public void DbTable_Validate_VersionModel()
    {
        Helper.TableBaseModelAssertToString<VersionModel>();
    }

    [Test]
    public void DbTable_Validate_WorkShopModel()
    {
        Helper.TableBaseModelAssertToString<WorkShopModel>();
    }
}
