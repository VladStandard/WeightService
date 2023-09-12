namespace WsStorageCoreTests.Tables.TableScaleModels.Plus;

[TestFixture]
public sealed class PluRepositoryTests : TableRepositoryTests
{
    private WsSqlPluRepository PluRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(WsSqlPluModel.Number)).Ascending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlPluModel> items = PluRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }

    [Test]
    public void GetListByNumber()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlPluModel> plus = PluRepository.GetEnumerableByNumber(301).ToList();

            Assert.That(plus.Any(), Is.True);
            foreach (WsSqlPluModel plu in plus)
                Assert.That(plu.Number, Is.EqualTo(301));

            ParseRecords(plus);
        }, false, new() { WsEnumConfiguration.ReleaseVS, WsEnumConfiguration.DevelopVS });
    }

    [Test]
    public void GetListByNumbers()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<short> numbers = new() { 301, 1301 };
            IEnumerable<WsSqlPluModel> plus = PluRepository.GetEnumerableByNumbers(numbers, WsSqlEnumIsMarked.ShowAll).ToList();

            Assert.That(plus.Any(), Is.True);
            foreach (WsSqlPluModel plu in plus)
                Assert.That(numbers, Does.Contain(plu.Number));

            ParseRecords(plus);
        }, false, new() { WsEnumConfiguration.ReleaseVS, WsEnumConfiguration.DevelopVS });
    }

    [Test]
    public void GetListByRangeNumber()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            const short minNumber = 200;
            const short maxNumber = 300;
            IEnumerable<WsSqlPluModel> plus = PluRepository.GetEnumerableByRange(minNumber, maxNumber).ToList();

            Assert.That(plus.Any(), Is.True);
            foreach (WsSqlPluModel plu in plus)
                Assert.That(plu.Number is >= minNumber and <= maxNumber, Is.True);

            ParseRecords(plus);
        }, false, new() { WsEnumConfiguration.ReleaseVS, WsEnumConfiguration.DevelopVS });
    }
    
}