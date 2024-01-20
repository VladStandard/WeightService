using Ws.Domain.Models.Entities.Ref1c;
using Ws.StorageCore.Entities.Ref1c.Plus;

namespace Ws.StorageCoreTests.Tables.TableScaleModels.Plus;

[TestFixture]
public sealed class PluRepositoryTests : TableRepositoryTests
{
    private SqlPluRepository PluRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(PluEntity.Number)).Ascending;

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<PluEntity> items = PluRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        });
    }

    [Test]
    public void GetListByNumber()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<PluEntity> plus = PluRepository.GetEnumerableByNumber(301).ToList();

            Assert.That(plus.Any(), Is.True);
            foreach (PluEntity plu in plus)
                Assert.That(plu.Number, Is.EqualTo(301));

            ParseRecords(plus);
        });
    }
    
}