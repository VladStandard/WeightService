namespace WsStorageCoreTests.Core;

[TestFixture]
public sealed class DataAccessHelperMapTests
{
    // [Test]
    // public void Set_fluent_configuration_for_test()
    // {
    //     TestsUtils.DataTests.AssertAction(() =>
    //     {
    //         if (TestsUtils.ContextManager.SqlConfiguration is null)
    //             throw new ArgumentNullException(nameof(TestsUtils.ContextManager.SqlConfiguration));
    //
    //         FluentConfiguration fluentConfiguration =
    //             Fluently.Configure().Database(TestsUtils.ContextManager.SqlConfiguration);
    //         SqlContextManagerHelper.Instance.SqlCore.AddConfigurationMappings(fluentConfiguration);
    //         fluentConfiguration.ExposeConfiguration(cfg => cfg.SetProperty("hbm2ddl.keywords", "auto-quote"));
    //         ISessionFactory sessionFactory = fluentConfiguration.BuildSessionFactory();
    //         sessionFactory.OpenSession();
    //         sessionFactory.Close();
    //         sessionFactory.Dispose();
    //     }, false, new() { EnumConfiguration.DevelopVS, EnumConfiguration.ReleaseVS });
    // }
}