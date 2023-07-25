using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleModels.Plus;

[TestFixture]
public sealed class PluRepositoryTests : TableRepositoryTests
{
    private WsSqlPluRepository PluRepository  { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluModel> items = PluRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
    
    [Test]
    public void GetListByNumber()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluModel> plus = PluRepository.GetListByNumber(301);
            
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
            List<short> numbers = new () { 301, 1301 };
            List<WsSqlPluModel> plus = PluRepository.GetListByNumbers(numbers, WsSqlEnumIsMarked.ShowAll);
           
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
            const short minNumber = 300;
            const short maxNumber = 399;
            List<WsSqlPluModel> plus = PluRepository.GetListByRange(minNumber, maxNumber);
           
            Assert.That(plus.Any(), Is.True);
            foreach (WsSqlPluModel plu in plus)
                Assert.That(plu.Number is >= minNumber and <= maxNumber, Is.True);

            ParseRecords(plus);
        }, false, new() { WsEnumConfiguration.ReleaseVS, WsEnumConfiguration.DevelopVS });
    }
	
    [Test]
    public void GetListByUid1C()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            Guid uid = Guid.Parse("B912B17D-E328-11EC-BD1B-00155D8A460F");
            List<WsSqlPluModel> plus = PluRepository.GetListByUid1C(uid);
            
            foreach (WsSqlPluModel plu in plus)
                Assert.That(plu.Uid1C, Is.EqualTo(uid));

            ParseRecords(plus);
        }, false, new() { WsEnumConfiguration.ReleaseVS, WsEnumConfiguration.DevelopVS });
	}
}