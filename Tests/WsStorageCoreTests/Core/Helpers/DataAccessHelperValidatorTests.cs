// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableRefModels.Plus1CFk;

namespace WsStorageCoreTests.Core.Helpers;

[TestFixture]
public sealed class DataAccessHelperValidatorTests
{
    #region Public and private methods

    [Test]
    public void Get_string_from_validators()
    {
        Assert.DoesNotThrow(() =>
        {
            List<Type> sqlTableValidators = WsTestsUtils.DataTests.ContextManager.GetTableValidators();
            foreach (Type sqlTableValidator in sqlTableValidators)
            {
                TestContext.WriteLine(sqlTableValidator);
            }
        });
    }

    [Test]
    public void Get_string_from_AccessValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlAccessValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_AppValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlAppValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_BarCodeValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlBarCodeValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_BoxValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlBoxValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_BrandValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlBrandValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_BundleValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlBundleValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_ClipValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlClipValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_ContragentValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlContragentValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_DeviceValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlDeviceValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_DeviceScaleFkValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlDeviceScaleFkValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_DeviceTypeFkValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlDeviceTypeFkValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_DeviceTypeValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlDeviceTypeValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_LogValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlLogValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_LogTypeValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlLogTypeValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_PluGroupValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPluGroupValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_PluCharacteristicsFkValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPluCharacteristicsFkValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_PluCharacteristicValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPluCharacteristicValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_PluGroupFkValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPluGroupFkValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_OrderValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlOrderValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_OrderWeighingValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlOrderWeighingValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_OrganizationValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlOrganizationValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_PluBundleFkValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPluBundleFkValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_PluLabelValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPluLabelValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_PluValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPluValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_PluScaleValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPluScaleValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_PluTemplateFkValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPluTemplateFkValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_PluWeighingValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPluWeighingValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_PrinterValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPrinterValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_PrinterResourceValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPrinterResourceFkValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_PrinterTypeValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPrinterTypeValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_ProductionFacilityValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlProductionFacilityValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_ProductSeriesValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlProductSeriesValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_ScaleValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlScaleValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_ScaleScreenShotValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlScaleScreenShotValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_TaskValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlTaskValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_TaskTypeValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlTaskTypeValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_TemplateValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlTemplateValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_TemplateResourceValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlTemplateResourceValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_VersionValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlVersionValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_WorkShopValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlWorkShopValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_WsSqlPlu1cFkValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WsSqlPlu1CFkValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    #endregion
}