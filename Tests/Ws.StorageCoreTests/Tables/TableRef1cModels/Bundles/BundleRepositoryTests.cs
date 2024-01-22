using Ws.StorageCore.Entities.SchemaRef1c.Bundles;

namespace Ws.StorageCoreTests.Tables.TableRef1cModels.Bundles;

[TestFixture]
public sealed class BundleRepositoryTests : TableRepositoryTests
{
    private SqlBundleRepository BundleRepository { get; } = new();

    private SqlBundleEntity GetFirstBundleModel()
    {
        SqlCrudConfig.SelectTopRowsCount = 1;
        return BundleRepository.GetEnumerable().First();
    }
    
    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlBundleEntity> items = BundleRepository.GetEnumerable();
            ParseRecords(items);
        });
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
        });
    }
}