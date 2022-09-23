// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql;

[TestFixture]
internal class TablesEqualsNewTests
{
    #region Public and private fields, properties, constructor

    private DataCoreHelper Helper { get; } = DataCoreHelper.Instance;

    #endregion

    #region Public and private methods

    [Test]
    public void DbTable_Validate_AccessModel()
    {
        Helper.TableBaseModelAssertEqualsNew<AccessModel>();
    }

    [Test]
    public void DbTable_Validate_AppModel()
    {
        Helper.TableBaseModelAssertEqualsNew<AppModel>();
    }

    [Test]
    public void DbTable_Validate_BarCodeModel()
    {
        Helper.TableBaseModelAssertEqualsNew<BarCodeModel>();
    }

    [Test]
    public void DbTable_Validate_BarCodeTypeModel()
    {
        Helper.TableBaseModelAssertEqualsNew<BarCodeTypeModel>();
    }

    [Test]
    public void DbTable_Validate_ContragentModel()
    {
        Helper.TableBaseModelAssertEqualsNew<ContragentModel>();
    }

    [Test]
    public void DbTable_Validate_HostModel()
    {
        Helper.TableBaseModelAssertEqualsNew<HostModel>();
    }

    [Test]
    public void DbTable_Validate_LogModel()
    {
        Helper.TableBaseModelAssertEqualsNew<LogModel>();
    }

    [Test]
    public void DbTable_Validate_LogTypeModel()
    {
        Helper.TableBaseModelAssertEqualsNew<LogTypeModel>();
    }

    [Test]
    public void DbTable_Validate_NomenclatureModel()
    {
        Helper.TableBaseModelAssertEqualsNew<NomenclatureModel>();
    }

    [Test]
    public void DbTable_Validate_OrderModel()
    {
        Helper.TableBaseModelAssertEqualsNew<OrderModel>();
    }

    [Test]
    public void DbTable_Validate_OrderWeighingModel()
    {
        Helper.TableBaseModelAssertEqualsNew<OrderWeighingModel>();
    }

    [Test]
    public void DbTable_Validate_OrganizationModel()
    {
        Helper.TableBaseModelAssertEqualsNew<OrganizationModel>();
    }

    [Test]
    public void DbTable_Validate_PackageModel()
    {
        Helper.TableBaseModelAssertEqualsNew<PackageModel>();
    }

    [Test]
    public void DbTable_Validate_PluModel()
    {
        Helper.TableBaseModelAssertEqualsNew<PluModel>();
    }

    [Test]
    public void DbTable_Validate_PluLabelModel()
    {
        Helper.TableBaseModelAssertEqualsNew<PluLabelModel>();
    }

    [Test]
    public void DbTable_Validate_PluScaleModel()
    {
        Helper.TableBaseModelAssertEqualsNew<PluScaleModel>();
    }

    [Test]
    public void DbTable_Validate_PluWeighingModel()
    {
        Helper.TableBaseModelAssertEqualsNew<PluWeighingModel>();
    }

    [Test]
    public void DbTable_Validate_PrinterModel()
    {
        Helper.TableBaseModelAssertEqualsNew<PrinterModel>();
    }

    [Test]
    public void DbTable_Validate_PrinterResourceModel()
    {
        Helper.TableBaseModelAssertEqualsNew<PrinterResourceModel>();
    }

    [Test]
    public void DbTable_Validate_PrinterTypeModel()
    {
        Helper.TableBaseModelAssertEqualsNew<PrinterTypeModel>();
    }

    [Test]
    public void DbTable_Validate_ProductionFacilityModel()
    {
        Helper.TableBaseModelAssertEqualsNew<ProductionFacilityModel>();
    }

    [Test]
    public void DbTable_Validate_ProductSeriesModel()
    {
        Helper.TableBaseModelAssertEqualsNew<ProductSeriesModel>();
    }

    [Test]
    public void DbTable_Validate_ScaleModel()
    {
        Helper.TableBaseModelAssertEqualsNew<ScaleModel>();
    }

    [Test]
    public void DbTable_Validate_TaskModel()
    {
        Helper.TableBaseModelAssertEqualsNew<TaskModel>();
    }

    [Test]
    public void DbTable_Validate_TaskTypeModel()
    {
        Helper.TableBaseModelAssertEqualsNew<TaskTypeModel>();
    }

    [Test]
    public void DbTable_Validate_TemplateModel()
    {
        Helper.TableBaseModelAssertEqualsNew<TemplateModel>();
    }

    [Test]
    public void DbTable_Validate_TemplateResourceModel()
    {
        Helper.TableBaseModelAssertEqualsNew<TemplateResourceModel>();
    }

    [Test]
    public void DbTable_Validate_VersionModel()
    {
        Helper.TableBaseModelAssertEqualsNew<VersionModel>();
    }

    [Test]
    public void DbTable_Validate_WorkShopModel()
    {
        Helper.TableBaseModelAssertEqualsNew<WorkShopModel>();
    }

    #endregion
}
