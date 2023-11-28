namespace Ws.AssertCoreTests;

public class DataTestsHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static DataTestsHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static DataTestsHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields, properties, constructor

    public SqlContextCacheHelper ContextCache => SqlContextCacheHelper.Instance;
    private SqlCoreHelper SqlCore => SqlCoreHelper.Instance;
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

    #endregion
}