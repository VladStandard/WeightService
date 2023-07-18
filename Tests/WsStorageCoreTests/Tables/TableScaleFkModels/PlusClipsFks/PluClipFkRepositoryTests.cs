using WsStorageCore.Tables.TableScaleFkModels.PlusClipsFks;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusClipsFks;

// TODO: add cases to PlusClips

public sealed class PluClipsFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluClipFkRepository PluClipFkRepository { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluClipFkModel> items = PluClipFkRepository.GetList(SqlCrudConfig);
            // Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}