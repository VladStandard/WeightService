using Ws.Database.Nhibernate.Entities.Ref1c.Bundles;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.StorageCoreTests.Tables.TableRef1cModels.Bundles;

[TestFixture]
public sealed class BundleRepositoryTests : TableRepositoryTests
{
    private SqlBundleRepository BundleRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(BundleEntity.Weight)).Ascending;

    private BundleEntity GetFirstBundleModel()
    {
        return BundleRepository.GetAll().First();
    }

    [Test]
    public void GetList()
    {
        AssertAction(() =>
        {
            IEnumerable<BundleEntity> items = BundleRepository.GetAll();
            ParseRecords(items);
        });
    }

    [Test]
    public void GetItemByUid1C()
    {
        AssertAction(() =>
        {
            BundleEntity oldBundle = GetFirstBundleModel();
            BundleEntity bundleBy1C = BundleRepository.GetByUid1C(oldBundle.Uid1C);

            Assert.Multiple(() =>
            {
                Assert.That(bundleBy1C.IsExists, Is.True);
                Assert.That(bundleBy1C, Is.EqualTo(oldBundle));
            });

            TestContext.WriteLine(bundleBy1C);
        });
    }
}