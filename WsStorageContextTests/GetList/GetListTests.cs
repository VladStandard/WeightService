// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using DataCore.Sql.TableScaleFkModels.PlusBrandsFks;
using DataCore.Sql.TableScaleFkModels.PlusStorageMethodsFks;
using DataCore.Sql.TableScaleFkModels.PrintersResourcesFks;
using DataCore.Sql.TableScaleModels.LogsWebs;
using DataCore.Sql.TableScaleModels.PlusStorageMethods;

namespace WsStorageContextTests.GetList;

[TestFixture]
internal class GetListTests
{
    private readonly SqlCrudConfigModel _sqlCrudConfig = new(false, true, false, true);

    [Test]
    public void DataContext_AssertGetList_AccessModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<AccessModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_AppModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<AppModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_BarCodeModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<BarCodeModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_BoxModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<BoxModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_BrandModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<BrandModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_BundleModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<BundleModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_ClipModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<ClipModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_ContragentModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<ContragentModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_DeviceModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<DeviceModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_DeviceScaleFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<DeviceScaleFkModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_DeviceTypeModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<DeviceTypeModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_DeviceTypeFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<DeviceTypeFkModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_LogTypeModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<LogTypeModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_LogModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<LogModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_LogWebModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<LogWebModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_LogWebFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<LogWebFkModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_OrderModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<OrderModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_OrderWeighingModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<OrderWeighingModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_OrganizationModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<OrganizationModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_PluModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_PluBrandFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluBrandFkModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_PluBundleFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluBundleFkModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_PluCharacteristicModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluCharacteristicModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_PluCharacteristicsFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluCharacteristicsFkModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_PluClipFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluClipFkModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_PluFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluFkModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_PluGroupModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluGroupModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_PluGroupFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluGroupFkModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_PluLabelModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluLabelModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_PluNestingFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluNestingFkModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_PluScaleModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluScaleModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_PluStorageMethodModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluStorageMethodModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_PluStorageMethodFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluStorageMethodFkModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_PluTemplateFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluTemplateFkModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_PluWeighingModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluWeighingModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_ProductionFacilityModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<ProductionFacilityModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_ProductSeriesModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<ProductSeriesModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_ScaleModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<ScaleModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_ScaleScreenShotModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<ScaleScreenShotModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_TaskModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<TaskModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_TaskTypeModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<TaskTypeModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_TemplateModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<TemplateModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_TemplateResourceModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<TemplateResourceModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_WorkShopModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<WorkShopModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_PrinterModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PrinterModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_PrinterResourceFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PrinterResourceFkModel>(_sqlCrudConfig);
    }

    [Test]
    public void DataContext_AssertGetList_PrinterTypeModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PrinterTypeModel>(_sqlCrudConfig);
    }
}