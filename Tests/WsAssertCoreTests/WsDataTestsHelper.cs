namespace WsAssertCoreTests;

public class WsDataTestsHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsDataTestsHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsDataTestsHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    public WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;
    public WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;

    #endregion

    #region Public and private methods

    private void SetupDevelopAleksandrov(bool isShowSql)
    {
        ContextManager.SetupJsonTestsDevelopAleksandrov(Directory.GetCurrentDirectory(),
            MdNetUtils.GetLocalDeviceName(true), nameof(WsAssertCoreTests), isShowSql);
        TestContext.WriteLine(ContextManager.SqlCore.GetConnectionServer());
    }

    private void SetupDevelopMorozov(bool isShowSql)
    {
        ContextManager.SetupJsonTestsDevelopMorozov(Directory.GetCurrentDirectory(),
            MdNetUtils.GetLocalDeviceName(true), nameof(WsAssertCoreTests), isShowSql);
        TestContext.WriteLine(ContextManager.SqlCore.GetConnectionServer());
    }

    private void SetupDevelopVs(bool isShowSql)
    {
        ContextManager.SetupJsonTestsDevelopVs(Directory.GetCurrentDirectory(),
            MdNetUtils.GetLocalDeviceName(true), nameof(WsAssertCoreTests), isShowSql);
        TestContext.WriteLine(ContextManager.SqlCore.GetConnectionServer());
    }
    
    private void SetupReleaseVs(bool isShowSql)
    {
        ContextManager.SetupJsonTestsReleaseVs(Directory.GetCurrentDirectory(),
            MdNetUtils.GetLocalDeviceName(true), nameof(WsAssertCoreTests), isShowSql);
        TestContext.WriteLine(ContextManager.SqlCore.GetConnectionServer());
    }

    public void AssertAction(Action action, bool isShowSql, List<WsEnumConfiguration> publishTypes)
    {
        Assert.DoesNotThrow(() =>
        {
            if (publishTypes.Contains(WsEnumConfiguration.DevelopAleksandrov))
            {
                SetupDevelopAleksandrov(isShowSql);
                action();
                TestContext.WriteLine();
            }
            if (publishTypes.Contains(WsEnumConfiguration.DevelopMorozov))
            {
                SetupDevelopMorozov(isShowSql);
                action();
                TestContext.WriteLine();
            }
            if (publishTypes.Contains(WsEnumConfiguration.DevelopVS))
            {
                SetupDevelopVs(isShowSql);
                action();
                TestContext.WriteLine();
            }
            if (publishTypes.Contains(WsEnumConfiguration.ReleaseVS))
            {
                SetupReleaseVs(isShowSql);
                action();
                TestContext.WriteLine();
            }
        });
    }

    private void FailureWriteLine(ValidationResult result)
    {
        if (result.IsValid) return;
        foreach (ValidationFailure failure in result.Errors)
            TestContext.WriteLine($"{WsLocaleCore.Validator.Property} {failure.PropertyName} {WsLocaleCore.Validator.FailedValidation}. {WsLocaleCore.Validator.Error}: {failure.ErrorMessage}");
    }

    public void AssertSqlValidate<T>(T item, bool assertResult) where T : WsSqlTableBase, new() =>
        AssertSqlTablesValidate(item, assertResult);

    private void AssertSqlTablesValidate<T>(T item, bool assertResult) where T : class, new()
    {
        Assert.DoesNotThrow(() =>
        {
            ValidationResult validationResult = WsSqlValidationUtils.GetValidationResult(item, true);
            FailureWriteLine(validationResult);
            // Assert.
            switch (assertResult)
            {
                case true:
                    Assert.IsTrue(validationResult.IsValid);
                    break;
                default:
                    Assert.IsFalse(validationResult.IsValid);
                    break;
            }
        });
    }
    

    public void TableBaseModelAssertEqualsNew<T>() where T : WsSqlTableBase, new()
    {
        Assert.DoesNotThrow(() =>
        {
            T item = new();
            Assert.That(item.EqualsNew(), Is.True);
        });
    }
    
    public void TableBaseModelAssertEqualsDefault<T>() where T : WsSqlTableBase, new()
    {
        Assert.DoesNotThrow(() =>
        {
            T item = new();
            Assert.That(item.EqualsDefault(), Is.True);
        });
    }

    public void FieldBaseModelAssertEqualsNew<T>() where T : WsSqlFieldBase, new()
    {
        Assert.DoesNotThrow(() =>
        {
            // Arrange.
            T item = new();
            WsSqlFieldBase baseItem = new();
            // Act.
            bool itemEqualsNew = item.EqualsNew();
            bool baseEqualsNew = baseItem.EqualsNew();
            // Assert.
            Assert.AreEqual(baseEqualsNew, itemEqualsNew);
        });
    }
    
    public void TableBaseModelAssertToString<T>() where T : WsSqlTableBase, new()
    {
        Assert.DoesNotThrow(() =>
        {
            // Arrange.
            T item = new();
            WsSqlTableBase baseItem = new();
            // Act.
            string itemString = item.ToString();
            string baseString = baseItem.ToString();
            TestContext.WriteLine($"{nameof(itemString)}: {itemString}");
            TestContext.WriteLine($"{nameof(baseString)}: {baseString}");
            // Assert.
            Assert.AreNotEqual(baseString, itemString);
        });
    }

    #endregion
}