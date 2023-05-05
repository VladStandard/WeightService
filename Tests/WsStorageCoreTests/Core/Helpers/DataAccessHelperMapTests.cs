// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Helpers;
using WsStorageCore.TableRefFkModels.Plus1CFk;

namespace WsStorageCoreTests.Core.Helpers;

[TestFixture]
public sealed class DataAccessHelperMapTests
{
    #region Public and private methods

    [Test]
    public void Set_fluent_configuration_for_test()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            if (WsTestsUtils.ContextManager.SqlConfiguration is null)
                throw new ArgumentNullException(nameof(WsTestsUtils.ContextManager.SqlConfiguration));

            FluentConfiguration fluentConfiguration = Fluently.Configure().Database(WsTestsUtils.ContextManager.SqlConfiguration);
            WsSqlContextManagerHelper.Instance.AccessManager.AddConfigurationMappings(fluentConfiguration);
            fluentConfiguration.ExposeConfiguration(cfg => cfg.SetProperty("hbm2ddl.keywords", "auto-quote"));
            ISessionFactory sessionFactory = fluentConfiguration.BuildSessionFactory();
            sessionFactory.OpenSession();
            sessionFactory.Close();
            sessionFactory.Dispose();

        }, false, new() { WsConfiguration.DevelopVS, WsConfiguration.ReleaseVS });
    }

    [Test]
    public void Get_string_from_maps()
    {
        Assert.DoesNotThrow(() =>
        {
            List<Type> sqlTableMaps = WsTestsUtils.DataTests.ContextManager.GetTableMaps();
            foreach (Type sqlTableMap in sqlTableMaps)
            {
                TestContext.WriteLine(sqlTableMap);
            }
        });
    }

    [Test]
    public void Get_string_from_AccessMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlAccessMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_AppMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlAppMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_BarCodeMap()
    {
        Assert.DoesNotThrow(() =>
        {
            BarCodeMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_BoxMap()
    {
        Assert.DoesNotThrow(() =>
        {
            BoxMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_BrandMap()
    {
        Assert.DoesNotThrow(() =>
        {
            BrandMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_BundleMap()
    {
        Assert.DoesNotThrow(() =>
        {
            BundleMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_ClipMap()
    {
        Assert.DoesNotThrow(() =>
        {
            ClipMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_ContragentMap()
    {
        Assert.DoesNotThrow(() =>
        {
            ContragentMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_DeviceMap()
    {
        Assert.DoesNotThrow(() =>
        {
            DeviceMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_DeviceScaleFkMap()
    {
        Assert.DoesNotThrow(() =>
        {
            DeviceScaleFkMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_DeviceTypeFkMap()
    {
        Assert.DoesNotThrow(() =>
        {
            DeviceTypeFkMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_DeviceTypeMap()
    {
        Assert.DoesNotThrow(() =>
        {
            DeviceTypeMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_LogMap()
    {
        Assert.DoesNotThrow(() =>
        {
            LogMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_LogMemoryMap()
    {
        Assert.DoesNotThrow(() =>
        {
            LogMemoryMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_LogTypeMap()
    {
        Assert.DoesNotThrow(() =>
        {
            LogTypeMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_LogWebMap()
    {
        Assert.DoesNotThrow(() =>
        {
            LogWebMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_LogWebFkMap()
    {
        Assert.DoesNotThrow(() =>
        {
            LogWebFkMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_PluGroupMap()
    {
        Assert.DoesNotThrow(() =>
        {
            PluGroupMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_NomenclaturesCharacteristicsFkMap()
    {
        Assert.DoesNotThrow(() =>
        {
            PluCharacteristicsFkMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_NomenclaturesCharacteristicsMap()
    {
        Assert.DoesNotThrow(() =>
        {
            PluCharacteristicMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_NomenclaturesGroupFkMap()
    {
        Assert.DoesNotThrow(() =>
        {
            PluGroupFkMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_OrderMap()
    {
        Assert.DoesNotThrow(() =>
        {
            OrderMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_OrderWeighingMap()
    {
        Assert.DoesNotThrow(() =>
        {
            OrderWeighingMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_OrganizationMap()
    {
        Assert.DoesNotThrow(() =>
        {
            OrganizationMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_PluBrandFkMap()
    {
        Assert.DoesNotThrow(() =>
        {
            PluBrandFkMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_PluBundleFkMap()
    {
        Assert.DoesNotThrow(() =>
        {
            PluBundleFkMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_PluLabelMap()
    {
        Assert.DoesNotThrow(() =>
        {
            PluLabelMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_PluMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPluMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_PluScaleMap()
    {
        Assert.DoesNotThrow(() =>
        {
            PluScaleMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_PluTemplateFkMap()
    {
        Assert.DoesNotThrow(() =>
        {
            PluTemplateFkMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_PluWeighingMap()
    {
        Assert.DoesNotThrow(() =>
        {
            PluWeighingMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_PrinterMap()
    {
        Assert.DoesNotThrow(() =>
        {
            PrinterMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_PrinterResourceMap()
    {
        Assert.DoesNotThrow(() =>
        {
            PrinterResourceFkMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_PrinterTypeMap()
    {
        Assert.DoesNotThrow(() =>
        {
            PrinterTypeMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_ProductionFacilityMap()
    {
        Assert.DoesNotThrow(() =>
        {
            ProductionFacilityMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void DataAccess_ProductSeriesMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            ProductSeriesMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_ScaleMap()
    {
        Assert.DoesNotThrow(() =>
        {
            ScaleMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_ScaleScreenShotMap()
    {
        Assert.DoesNotThrow(() =>
        {
            ScaleScreenShotMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_TaskMap()
    {
        Assert.DoesNotThrow(() =>
        {
            TaskMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_TaskTypeMap()
    {
        Assert.DoesNotThrow(() =>
        {
            TaskTypeMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_TemplateMap()
    {
        Assert.DoesNotThrow(() =>
        {
            TemplateMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_TemplateResourceMap()
    {
        Assert.DoesNotThrow(() =>
        {
            TemplateResourceMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_VersionMap()
    {
        Assert.DoesNotThrow(() =>
        {
            VersionMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_WorkShopMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WorkShopMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_WsSqlPlu1cFkMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPlu1CFkMap map = new();
            TestContext.WriteLine(map);
        });
    }

    #endregion
}
