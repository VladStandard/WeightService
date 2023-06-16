// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
            WsSqlContextManagerHelper.Instance.SqlCoreManager.AddConfigurationMappings(fluentConfiguration);
            fluentConfiguration.ExposeConfiguration(cfg => cfg.SetProperty("hbm2ddl.keywords", "auto-quote"));
            ISessionFactory sessionFactory = fluentConfiguration.BuildSessionFactory();
            sessionFactory.OpenSession();
            sessionFactory.Close();
            sessionFactory.Dispose();

        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
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
            WsSqlBarCodeMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_BoxMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlBoxMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_BrandMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlBrandMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_BundleMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlBundleMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_ClipMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlClipMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_ContragentMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlContragentMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_DeviceMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlDeviceMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_DeviceScaleFkMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlDeviceScaleFkMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_DeviceTypeFkMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlDeviceTypeFkMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_DeviceTypeMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlDeviceTypeMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_LogMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlLogMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_LogMemoryMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlLogMemoryMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_LogTypeMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlLogTypeMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_LogWebMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlLogWebMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_LogWebFkMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlLogWebFkMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_PluGroupMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPluGroupMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_NomenclaturesCharacteristicsFkMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPluCharacteristicsFkMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_NomenclaturesCharacteristicsMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPluCharacteristicMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_NomenclaturesGroupFkMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPluGroupFkMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_OrderMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlOrderMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_OrderWeighingMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlOrderWeighingMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_OrganizationMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlOrganizationMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_PluBrandFkMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPluBrandFkMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_PluBundleFkMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPluBundleFkMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_PluLabelMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPluLabelMap map = new();
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
            WsSqlPluScaleMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_PluTemplateFkMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPluTemplateFkMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_PluWeighingMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPluWeighingMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_PrinterMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPrinterMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_PrinterResourceMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPrinterResourceFkMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_PrinterTypeMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPrinterTypeMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_ProductionFacilityMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlProductionFacilityMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void DataAccess_ProductSeriesMap_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlProductSeriesMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_ScaleMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlScaleMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_ScaleScreenShotMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlScaleScreenShotMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_TaskMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlTaskMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_TaskTypeMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlTaskTypeMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_TemplateMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlTemplateMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_TemplateResourceMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlTemplateResourceMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_VersionMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlVersionMap map = new();
            TestContext.WriteLine(map);
        });
    }

    [Test]
    public void Get_string_from_WorkShopMap()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlWorkshopMap map = new();
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
