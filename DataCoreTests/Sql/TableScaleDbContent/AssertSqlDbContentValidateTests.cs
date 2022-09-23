// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql.TableScaleDbContent;

[TestFixture]
internal class AssertSqlDbContentValidateTests
{
    #region Public and private fields, properties, constructor

    private DataCoreHelper Helper { get; } = DataCoreHelper.Instance;

    #endregion

    #region Public and private methods

    [Test]
    public void DbContent_Validate_AccessModel()
    {
        Helper.AssertSqlDbContentValidate<AccessModel>();
    }

    [Test]
    public void DbContent_Validate_AppModel()
    {
        Helper.AssertSqlDbContentValidate<AppModel>();
    }

    [Test]
    public void DbContent_Validate_BarCodeModel()
    {
        Helper.AssertSqlDbContentValidate<BarCodeModel>();
    }

    [Test]
    public void DbContent_Validate_BarCodeTypeModel()
    {
        Helper.AssertSqlDbContentValidate<BarCodeTypeModel>();
    }

    [Test]
    public void DbContent_Validate_ContragentModel()
    {
        Helper.AssertSqlDbContentValidate<ContragentModel>();
    }

    [Test]
    public void DbContent_Validate_HostModel()
    {
        Helper.AssertSqlDbContentValidate<HostModel>();
    }

    [Test]
    public void DbContent_Validate_LogModel()
    {
        Helper.AssertSqlDbContentValidate<LogModel>();
    }

    [Test]
    public void DbContent_Validate_LogTypeModel()
    {
        Helper.AssertSqlDbContentValidate<LogTypeModel>();
    }

    [Test]
    public void DbContent_Validate_NomenclatureModel()
    {
        Helper.AssertSqlDbContentValidate<NomenclatureModel>();
    }

    [Test]
    public void DbContent_Validate_OrderModel()
    {
        Helper.AssertSqlDbContentValidate<OrderModel>();
    }

    [Test]
    public void DbContent_Validate_OrderWeighingModel()
    {
        Helper.AssertSqlDbContentValidate<OrderWeighingModel>();
    }

    [Test]
    public void DbContent_Validate_OrganizationModel()
    {
        Helper.AssertSqlDbContentValidate<OrganizationModel>();
    }

    [Test]
    public void DbContent_Validate_PackageModel()
    {
        Helper.AssertSqlDbContentValidate<PackageModel>();
    }

    [Test]
    public void DbContent_Validate_PluModel()
    {
        Helper.AssertSqlDbContentValidate<PluModel>();
    }

    [Test]
    public void DbContent_Validate_PluLabelModel()
    {
        Helper.AssertSqlDbContentValidate<PluLabelModel>();
    }

    [Test]
    public void DbContent_Validate_PluScaleModel()
    {
        Helper.AssertSqlDbContentValidate<PluScaleModel>();
    }

    [Test]
    public void DbContent_Validate_PluWeighingModel()
    {
        Helper.AssertSqlDbContentValidate<PluWeighingModel>();
    }

    [Test]
    public void DbContent_Validate_PrinterModel()
    {
        Helper.AssertSqlDbContentValidate<PrinterModel>();
    }

    [Test]
    public void DbContent_Validate_PrinterResourceModel()
    {
        Helper.AssertSqlDbContentValidate<PrinterResourceModel>();
    }

    [Test]
    public void DbContent_Validate_PrinterTypeModel()
    {
        Helper.AssertSqlDbContentValidate<PrinterTypeModel>();
    }

    [Test]
    public void DbContent_Validate_ProductionFacilityModel()
    {
        Helper.AssertSqlDbContentValidate<ProductionFacilityModel>();
    }

    [Test]
    public void DbContent_Validate_ProductSeriesModel()
    {
        Helper.AssertSqlDbContentValidate<ProductSeriesModel>();
    }

    [Test]
    public void DbContent_Validate_ScaleModel()
    {
        Helper.AssertSqlDbContentValidate<ScaleModel>();
    }

    [Test]
    public void DbContent_Validate_TaskModel()
    {
        Helper.AssertSqlDbContentValidate<TaskModel>();
    }

    [Test]
    public void DbContent_Validate_TaskTypeModel()
    {
        Helper.AssertSqlDbContentValidate<TaskTypeModel>();
    }

    [Test]
    public void DbContent_Validate_TemplateModel()
    {
        Helper.AssertSqlDbContentValidate<TemplateModel>();
    }

    [Test]
    public void DbContent_Validate_TemplateResourceModel()
    {
        Helper.AssertSqlDbContentValidate<TemplateResourceModel>();
    }

    [Test]
    public void DbContent_Validate_VersionModel()
    {
        Helper.AssertSqlDbContentValidate<VersionModel>();
    }

    [Test]
    public void DbContent_Validate_WorkShopModel()
    {
        Helper.AssertSqlDbContentValidate<WorkShopModel>();
    }

    #endregion
}
