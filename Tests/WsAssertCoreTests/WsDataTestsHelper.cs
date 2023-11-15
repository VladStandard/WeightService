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

    public void AssertAction(Action action, bool isShowSql)
    {
        Assert.DoesNotThrow(() =>
        {
            SqlCore.SetSessionFactory(isShowSql);
            action();
            TestContext.WriteLine();
        });
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