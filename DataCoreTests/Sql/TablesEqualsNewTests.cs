// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql;

[TestFixture]
internal class TablesEqualsNewTests
{
    #region Public and private fields, properties, constructor

    private DataCoreHelper DataCore { get; } = DataCoreHelper.Instance;

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
    public void DbTable_Validate_BarCodeTypeModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<BarCodeTypeModel>();
    }

    [Test]
    public void DbTable_Validate_ContragentModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<ContragentModel>();
    }

    [Test]
    public void DbTable_Validate_HostModel()
    {
        DataCore.TableBaseModelAssertEqualsNew<HostModel>();
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
