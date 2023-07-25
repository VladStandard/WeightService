using WsStorageCore.Tables.TableScaleFkModels.PlusClipsFks;
using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusClipsFks;

// TODO: add cases to PlusClips

public sealed class PluClipsFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluClipFkRepository PluClipFkRepository { get; set; } = new();

    private WsSqlPluClipFkModel GetFirstPluClipFkModel()
    {
        SqlCrudConfig.SelectTopRowsCount = 1;
        return PluClipFkRepository.GetList(SqlCrudConfig).First();
    }
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluClipFkModel> items = PluClipFkRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultPublishTypes);
    }

    [Test]
    public void GetItemByPlu()
    {
        throw new NotImplementedException();
    }
}