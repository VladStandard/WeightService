namespace WsStorageCoreTests.Core;

[TestFixture]
public sealed class DataAccessHelperMapTests
{
    // [Test]
    // public void Set_fluent_configuration_for_test()
    // {
    //     WsTestsUtils.DataTests.AssertAction(() =>
    //     {
    //         if (WsTestsUtils.ContextManager.SqlConfiguration is null)
    //             throw new ArgumentNullException(nameof(WsTestsUtils.ContextManager.SqlConfiguration));
    //
    //         FluentConfiguration fluentConfiguration =
    //             Fluently.Configure().Database(WsTestsUtils.ContextManager.SqlConfiguration);
    //         WsSqlContextManagerHelper.Instance.SqlCore.AddConfigurationMappings(fluentConfiguration);
    //         fluentConfiguration.ExposeConfiguration(cfg => cfg.SetProperty("hbm2ddl.keywords", "auto-quote"));
    //         ISessionFactory sessionFactory = fluentConfiguration.BuildSessionFactory();
    //         sessionFactory.OpenSession();
    //         sessionFactory.Close();
    //         sessionFactory.Dispose();
    //     }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    // }
}