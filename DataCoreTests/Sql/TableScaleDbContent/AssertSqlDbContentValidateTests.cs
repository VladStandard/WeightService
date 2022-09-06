// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql.TableScaleDbContent;

[TestFixture]
internal class AssertSqlDbContentValidateTests
{
    private DataCoreHelper DataCore { get; } = DataCoreHelper.Instance;

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
    public void DbContent_Validate_BarCodeTypeModel()
    {
        DataCore.AssertSqlDbContentValidate<BarCodeTypeModel>();
    }

    [Test]
    public void DbContent_Validate_ContragentModel()
    {
        DataCore.AssertSqlDbContentValidate<ContragentModel>();
    }

    [Test]
    public void DbContent_Validate_HostModel()
    {
        DataCore.AssertSqlDbContentValidate<HostModel>();
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
    public void DbContent_Validate_PluObsoleteModel()
    {
        DataCore.AssertSqlDbContentValidate<PluObsoleteModel>();
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
}
