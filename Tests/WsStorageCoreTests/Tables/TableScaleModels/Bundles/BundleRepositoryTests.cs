namespace WsStorageCoreTests.Tables.TableScaleModels.Bundles;

[TestFixture]
public sealed class BundleRepositoryTests : TableRepositoryTests
{
    private WsSqlBundleRepository BundleRepository { get; } = new();

    private WsSqlBundleModel GetFirstBundleModel()
    {
        SqlCrudConfig.SelectTopRowsCount = 1;
        return BundleRepository.GetEnumerable(SqlCrudConfig).First();
    }
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlBundleModel> items = BundleRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }

    [Test]
    public void GetItemByUid1C()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlBundleModel oldBundle = GetFirstBundleModel();
            WsSqlBundleModel bundleBy1C = BundleRepository.GetItemByUid1C(oldBundle.Uid1C);

            Assert.That(bundleBy1C.IsNotNew, Is.True);
            Assert.That(bundleBy1C, Is.EqualTo(oldBundle));

            TestContext.WriteLine(bundleBy1C);
        }, false, DefaultConfigurations);
    }
}