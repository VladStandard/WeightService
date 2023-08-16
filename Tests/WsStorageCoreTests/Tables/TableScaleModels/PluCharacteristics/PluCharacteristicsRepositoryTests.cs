namespace WsStorageCoreTests.Tables.TableScaleModels.PluCharacteristics;

[TestFixture]
public sealed class PluCharacteristicsRepositoryTests : TableRepositoryTests
{
    private WsSqlPluCharacteristicRepository PluCharacteristicRepository { get; set; } = new();

    [Test]
    public void GetEnumerable()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlPluCharacteristicModel> items = PluCharacteristicRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
}