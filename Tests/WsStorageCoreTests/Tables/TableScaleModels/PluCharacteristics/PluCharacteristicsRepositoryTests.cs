namespace WsStorageCoreTests.Tables.TableScaleModels.PluCharacteristics;

[TestFixture]
public sealed class PluCharacteristicsRepositoryTests : TableRepositoryTests
{
    private WsSqlPluCharacteristicRepository PluCharacteristicRepository  { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluCharacteristicModel> items = PluCharacteristicRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}