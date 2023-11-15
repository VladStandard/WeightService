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
    private WsSqlCoreHelper SqlCore => WsSqlCoreHelper.Instance;
    #endregion

    #region Public and private methods

    public void AssertAction(Action action, bool isShowSql, List<WsEnumConfiguration> publishTypes)
    {
        Assert.DoesNotThrow(() =>
        {
            if (publishTypes.Contains(WsEnumConfiguration.DevelopVs))
            {
                SqlCore.SetSessionFactory(isShowSql);
                action();
                TestContext.WriteLine();
            }
            if (publishTypes.Contains(WsEnumConfiguration.ReleaseVs))
            {
                SqlCore.SetSessionFactory(isShowSql);
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
    
    public void TableBaseModelAssertEqualsNew<T>() where T : WsSqlEntityBase, new()
    {
        Assert.DoesNotThrow(() =>
        {
            T item = new();
            Assert.That(item.EqualsNew(), Is.True);
        });
    }
    
    public void TableBaseModelAssertEqualsDefault<T>() where T : WsSqlEntityBase, new()
    {
        Assert.DoesNotThrow(() =>
        {
            T item = new();
            Assert.That(item.EqualsDefault(), Is.True);
        });
    }
    
    public void TableBaseModelAssertToString<T>() where T : WsSqlEntityBase, new()
    {
        Assert.DoesNotThrow(() =>
        {
            // Arrange.
            T item = new();
            WsSqlEntityBase baseItem = new();
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