using WsStorageCore.Tables.TableScaleModels.PlusStorageMethods;

namespace WsStorageCoreTests.Tables.TableScaleModels.PluStorageMethods;

[TestFixture]
public sealed class PluStorageRepositoryTests : TableRepositoryTests
{
    private WsSqlPluStorageMethodRepository PluStorageMethodRepository { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluStorageMethodModel> items = PluStorageMethodRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}