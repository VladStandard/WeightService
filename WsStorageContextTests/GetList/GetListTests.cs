// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Enums;
using DataCore.Sql.Core.Utils;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleFkModels.PlusBrandsFks;
using DataCore.Sql.TableScaleFkModels.PlusStorageMethodsFks;
using DataCore.Sql.TableScaleFkModels.PrintersResourcesFks;
using DataCore.Sql.TableScaleModels.LogsWebs;
using DataCore.Sql.TableScaleModels.PlusStorageMethods;
using NUnit.Framework.Internal;

namespace WsStorageContextTests.GetList;

[TestFixture]
internal class GetListTests
{
    private static SqlCrudConfigModel SqlCrudConfig => new(false, true, false, true);
    private static SqlCrudConfigModel SqlCrudConfigFk => new(true, true, false, true);
    private static List<PublishType> PublishTypes => new() { PublishType.ReleaseVs, PublishType.DevelopVs };
    private static List<PublishType> PublishTypesDev = new() { PublishType.DevelopVs };

    [Test]
    public void DataContext_AssertGetList_AccessModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<AccessModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_AppModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<AppModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_BarCodeModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<BarCodeModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_BoxModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<BoxModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_BrandModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<BrandModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_BundleModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<BundleModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_ClipModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<ClipModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_ContragentModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<ContragentModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_DeviceModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<DeviceModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_DeviceScaleFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<DeviceScaleFkModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_DeviceTypeModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<DeviceTypeModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_DeviceTypeFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<DeviceTypeFkModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_LogTypeModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<LogTypeModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_LogModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<LogModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_LogWebModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<LogWebModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_LogWebFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<LogWebFkModel>(SqlCrudConfigFk, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_OrderModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<OrderModel>(SqlCrudConfig, PublishTypes, false);
    }

    [Test]
    public void DataContext_AssertGetList_OrderWeighingModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<OrderWeighingModel>(SqlCrudConfig, PublishTypes, false);
    }

    [Test]
    public void DataContext_AssertGetList_OrganizationModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<OrganizationModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_PluModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_PluBrandFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluBrandFkModel>(SqlCrudConfig, PublishTypesDev);
    }

    [Test]
    public void DataContext_AssertGetList_PluBundleFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluBundleFkModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_PluCharacteristicModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluCharacteristicModel>(SqlCrudConfig, PublishTypesDev);
    }

    [Test]
    public void DataContext_AssertGetList_PluCharacteristicsFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluCharacteristicsFkModel>(SqlCrudConfig, PublishTypesDev);
    }

    [Test]
    public void DataContext_AssertGetList_PluClipFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluClipFkModel>(SqlCrudConfig, PublishTypes, false);
    }

    [Test]
    public void DataContext_AssertGetList_PluFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluFkModel>(SqlCrudConfig, PublishTypes, false);
    }

    [Test]
    public void DataContext_AssertGetList_PluGroupModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluGroupModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_PluGroupFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluGroupFkModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_PluLabelModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluLabelModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_PluNestingFkModel()
    {
        SqlCrudConfigModel sqlCrudConfig = SqlCrudConfig;
        sqlCrudConfig.NativeQuery = SqlQueries.DbScales.Tables.PluNestingFks.GetList(true);
        sqlCrudConfig.NativeParameters = new() { new("P_UID", new Guid("5B24E604-C550-43C9-91DD-74989A5E9D6C")), };
        DataCoreTestsUtils.DataCore.AssertGetList<PluNestingFkModel>(sqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_PluScaleModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluScaleModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_PluStorageMethodModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluStorageMethodModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_PluStorageMethodFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluStorageMethodFkModel>(SqlCrudConfigFk, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_PluTemplateFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluTemplateFkModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_PluWeighingModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PluWeighingModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_ProductionFacilityModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<ProductionFacilityModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_ProductSeriesModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<ProductSeriesModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_ScaleModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<ScaleModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_ScaleScreenShotModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<ScaleScreenShotModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_TaskModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<TaskModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_TaskTypeModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<TaskTypeModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_TemplateModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<TemplateModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_TemplateResourceModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<TemplateResourceModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_WorkShopModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<WorkShopModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_PrinterModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PrinterModel>(SqlCrudConfig, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_PrinterResourceFkModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PrinterResourceFkModel>(SqlCrudConfigFk, PublishTypes);
    }

    [Test]
    public void DataContext_AssertGetList_PrinterTypeModel()
    {
        DataCoreTestsUtils.DataCore.AssertGetList<PrinterTypeModel>(SqlCrudConfig, PublishTypes);
    }
}