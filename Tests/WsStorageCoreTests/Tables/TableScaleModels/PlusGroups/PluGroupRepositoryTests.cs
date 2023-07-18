namespace WsStorageCoreTests.Tables.TableScaleModels.PlusGroups;

[TestFixture]
public sealed class PluGroupRepositoryTests : TableRepositoryTests
{
    private WsSqlPluGroupRepository PluGroupRepository  { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluGroupModel> items = PluGroupRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}