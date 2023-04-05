// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Enums;
using DataCore.Sql.Core.Helpers;
using DataCore.Sql.TableDiagModels.Logs;
using DataCore.Sql.TableDiagModels.LogsMemories;
using DataCore.Sql.TableDiagModels.LogsTypes;
using DataCore.Sql.TableDiagModels.LogsWebs;
using DataCore.Sql.TableDiagModels.LogsWebsFks;
using DataCore.Sql.TableDiagModels.ScalesScreenshots;
using DataCore.Sql.TableScaleFkModels.PlusBrandsFks;
using DataCore.Sql.TableScaleFkModels.PlusLabels;
using DataCore.Sql.TableScaleFkModels.PlusWeighingsFks;
using DataCore.Sql.TableScaleFkModels.PrintersResourcesFks;

namespace WsStorageCoreTests.Core.Helpers;

[TestFixture]
internal class DataAccessHelperMapTests
{
    #region Public and private methods

    [Test]
    public void DataAccess_SetFluentConfigurationForTest()
    {
        DataCoreTestsUtils.DataCore.AssertAction(() =>
        {
            if (DataCoreTestsUtils.DataAccess.SqlConfiguration is null)
                throw new ArgumentNullException(nameof(DataCoreTestsUtils.DataAccess.SqlConfiguration));

            FluentConfiguration fluentConfiguration = Fluently.Configure().Database(DataCoreTestsUtils.DataAccess.SqlConfiguration);
            DataAccessHelper.AddConfigurationMappings(fluentConfiguration);
            fluentConfiguration.ExposeConfiguration(cfg => cfg.SetProperty("hbm2ddl.keywords", "auto-quote"));
            ISessionFactory sessionFactory = fluentConfiguration.BuildSessionFactory();
            sessionFactory.OpenSession();
            sessionFactory.Close();
            sessionFactory.Dispose();

        }, false, new() { Configuration.DevelopVS, Configuration.ReleaseVS });
    }

    [Test]
    public void DataAccess_Map_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            List<Type> sqlTableMaps = DataCoreTestsUtils.DataCore.DataContext.GetTableMaps();
            foreach (Type sqlTableMap in sqlTableMaps)
            {
                TestContext.WriteLine(sqlTableMap);
            }
        });
    }

    [Test]
    public void DataAccess_AccessMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            AccessMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_AppMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            AppMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_BarCodeMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            BarCodeMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_BoxMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            BoxMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_BrandMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            BrandMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_BundleMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            BundleMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_ClipMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            ClipMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_ContragentMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            ContragentMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_DeviceMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            DeviceMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_DeviceScaleFkMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            DeviceScaleFkMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_DeviceTypeFkMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            DeviceTypeFkMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_DeviceTypeMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            DeviceTypeMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_LogMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            LogMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_LogMemoryMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            LogMemoryMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_LogTypeMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            LogTypeMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_LogWebMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            LogWebMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_LogWebFkMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            LogWebFkMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_PluGroupMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PluGroupMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_NomenclaturesCharacteristicsFkMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PluCharacteristicsFkMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_NomenclaturesCharacteristicsMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PluCharacteristicMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_NomenclaturesGroupFkMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PluGroupFkMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_OrderMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            OrderMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_OrderWeighingMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            OrderWeighingMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_OrganizationMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            OrganizationMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_PluBrandFkMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PluBrandFkMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_PluBundleFkMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PluBundleFkMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_PluLabelMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PluLabelMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_PluMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PluMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_PluScaleMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PluScaleMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_PluTemplateFkMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PluTemplateFkMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_PluWeighingMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PluWeighingMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_PrinterMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PrinterMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_PrinterResourceMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PrinterResourceFkMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_PrinterTypeMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PrinterTypeMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_ProductionFacilityMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            ProductionFacilityMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_ProductSeriesMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            ProductSeriesMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_ScaleMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            ScaleMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_ScaleScreenShotMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            ScaleScreenShotMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_TaskMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            TaskMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_TaskTypeMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            TaskTypeMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_TemplateMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            TemplateMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_TemplateResourceMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            TemplateResourceMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_VersionMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            VersionMap item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_WorkShopMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            WorkShopMap item = new();
            TestContext.WriteLine(item);
        });
    }

    #endregion
}
