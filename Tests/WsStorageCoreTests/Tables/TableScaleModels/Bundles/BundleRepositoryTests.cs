using WsStorageCore.Entities.SchemaRef1c.Bundles;
namespace WsStorageCoreTests.Tables.TableScaleModels.Bundles;

[TestFixture]
public sealed class BundleRepositoryTests : TableRepositoryTests
{
    private WsSqlBundleRepository BundleRepository { get; } = new();

    private WsSqlBundleEntity GetFirstBundleModel()
    {
        SqlCrudConfig.SelectTopRowsCount = 1;
        return BundleRepository.GetEnumerable(SqlCrudConfig).First();
    }
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlBundleEntity> items = BundleRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }

    [Test]
    public void GetItemByUid1C()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlBundleEntity oldBundle = GetFirstBundleModel();
            WsSqlBundleEntity bundleBy1C = BundleRepository.GetItemByUid1C(oldBundle.Uid1C);

            Assert.That(bundleBy1C.IsExists, Is.True);
            Assert.That(bundleBy1C, Is.EqualTo(oldBundle));

            TestContext.WriteLine(bundleBy1C);
        }, false, DefaultConfigurations);
    }
}