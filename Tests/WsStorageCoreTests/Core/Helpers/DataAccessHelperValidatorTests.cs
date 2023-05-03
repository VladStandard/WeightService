// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableRefFkModels.Plus1CFk;

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
            BarCodeValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_BoxValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            BoxValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_BrandValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            BrandValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_BundleValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            BundleValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_ClipValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            ClipValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_ContragentValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            ContragentValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_DeviceValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            DeviceValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_DeviceScaleFkValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            DeviceScaleFkValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_DeviceTypeFkValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            DeviceTypeFkValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_DeviceTypeValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            DeviceTypeValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_LogValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            LogValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_LogTypeValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            LogTypeValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_PluGroupValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            PluGroupValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_PluCharacteristicsFkValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            PluCharacteristicsFkValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_PluCharacteristicValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            PluCharacteristicValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_PluGroupFkValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            PluGroupFkValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_OrderValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            OrderValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_OrderWeighingValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            OrderWeighingValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_OrganizationValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            OrganizationValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_PluBundleFkValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            PluBundleFkValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_PluLabelValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            PluLabelValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_PluValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            PluValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_PluScaleValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            PluScaleValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_PluTemplateFkValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            PluTemplateFkValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_PluWeighingValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            PluWeighingValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_PrinterValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            PrinterValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_PrinterResourceValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            PrinterResourceFkValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_PrinterTypeValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            PrinterTypeValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_ProductionFacilityValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            ProductionFacilityValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_ProductSeriesValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            ProductSeriesValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_ScaleValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            ScaleValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_ScaleScreenShotValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            ScaleScreenShotValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_TaskValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            TaskValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_TaskTypeValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            TaskTypeValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_TemplateValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            TemplateValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_TemplateResourceValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            TemplateResourceValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_VersionValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            VersionValidator validator = new();
            TestContext.WriteLine(validator);
        });
    }

    [Test]
    public void Get_string_from_WorkShopValidator()
    {
        Assert.DoesNotThrow(() =>
        {
            WorkShopValidator validator = new();
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