using WsStorageCore.Entities.SchemaRef1c.Bundles;

namespace WsStorageCoreTests.Tables.TableScaleModels.Bundles;

[TestFixture]
public sealed class BundleRepositoryTests : TableRepositoryTests
{
    private SqlBundleRepository BundleRepository { get; } = new();

    private SqlBundleEntity GetFirstBundleModel()
    {
        SqlCrudConfig.SelectTopRowsCount = 1;
        return BundleRepository.GetEnumerable(SqlCrudConfig).First();
    }
    
    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlBundleEntity> items = BundleRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }

    [Test]
    public void GetItemByUid1C()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            SqlBundleEntity oldBundle = GetFirstBundleModel();
            SqlBundleEntity bundleBy1C = BundleRepository.GetItemByUid1C(oldBundle.Uid1C);

            Assert.That(bundleBy1C.IsExists, Is.True);
            Assert.That(bundleBy1C, Is.EqualTo(oldBundle));

            TestContext.WriteLine(bundleBy1C);
        }, false);
    }
}