// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PrintersResourcesFks;

namespace WsStorageCoreTests.Core.Helpers;

[TestFixture]
internal class DataAccessHelperValidatorTests
{
    #region Public and private methods

    [Test]
    public void DataAccess_Validator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            List<Type> sqlTableValidators = DataCoreTestsUtils.DataCore.DataContext.GetTableValidators();
            foreach (Type sqlTableValidator in sqlTableValidators)
            {
                TestContext.WriteLine(sqlTableValidator);
            }
        });
    }

    [Test]
    public void DataAccess_AccessValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            AccessValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_AppValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            AppValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_BarCodeValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            BarCodeValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_BoxValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            BoxValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_BrandValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            BrandValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_BundleValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            BundleValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_ClipValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            ClipValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_ContragentValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            ContragentValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_DeviceValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            DeviceValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_DeviceScaleFkValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            DeviceScaleFkValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_DeviceTypeFkValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            DeviceTypeFkValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_DeviceTypeValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            DeviceTypeValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_LogValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            LogValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_LogTypeValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            LogTypeValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_PluGroupValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PluGroupValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_NomenclaturesCharacteristicsFkValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PluCharacteristicsFkValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_NomenclaturesCharacteristicsValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PluCharacteristicValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_NomenclaturesGroupFkValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PluGroupFkValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_OrderValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            OrderValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_OrderWeighingValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            OrderWeighingValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_OrganizationValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            OrganizationValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_PluBundleFkValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PluBundleFkValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_PluLabelValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PluLabelValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_PluValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PluValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_PluScaleValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PluScaleValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_PluTemplateFkValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PluTemplateFkValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_PluWeighingValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PluWeighingValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_PrinterValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PrinterValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_PrinterResourceValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PrinterResourceFkValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_PrinterTypeValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PrinterTypeValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_ProductionFacilityValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            ProductionFacilityValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_ProductSeriesValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            ProductSeriesValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_ScaleValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            ScaleValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_ScaleScreenShotValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            ScaleScreenShotValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_TaskValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            TaskValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_TaskTypeValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            TaskTypeValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_TemplateValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            TemplateValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_TemplateResourceValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            TemplateResourceValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_VersionValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            VersionValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    [Test]
    public void DataAccess_WorkShopValidator_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            WorkShopValidator item = new();
            TestContext.WriteLine(item);
        });
    }

    #endregion
}