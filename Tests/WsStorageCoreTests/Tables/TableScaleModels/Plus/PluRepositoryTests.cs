using WsStorageCore.Entities.SchemaRef1c.Plus;
namespace WsStorageCoreTests.Tables.TableScaleModels.Plus;

[TestFixture]
public sealed class PluRepositoryTests : TableRepositoryTests
{
    private WsSqlPluRepository PluRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(WsSqlPluEntity.Number)).Ascending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlPluEntity> items = PluRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }

    [Test]
    public void GetListByNumber()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlPluEntity> plus = PluRepository.GetEnumerableByNumber(301).ToList();

            Assert.That(plus.Any(), Is.True);
            foreach (WsSqlPluEntity plu in plus)
                Assert.That(plu.Number, Is.EqualTo(301));

            ParseRecords(plus);
        }, false, new() { WsEnumConfiguration.ReleaseVS, WsEnumConfiguration.DevelopVS });
    }
    
}