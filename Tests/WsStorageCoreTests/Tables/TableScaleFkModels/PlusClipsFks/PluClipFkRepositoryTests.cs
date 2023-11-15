using WsStorageCore.Entities.SchemaScale.PlusClipsFks;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusClipsFks;

[TestFixture]
public sealed class PluClipsFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluClipFkRepository PluClipFkRepository { get; } = new();

    private WsSqlPluClipFkEntity GetFirstPluClipFkModel()
    {
        SqlCrudConfig.SelectTopRowsCount = 1;
        return PluClipFkRepository.GetEnumerable(SqlCrudConfig).First();
    }

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlPluClipFkEntity> items = PluClipFkRepository.GetEnumerable(SqlCrudConfig);
            //ParseRecords(items);
        }, false);
    }

    //[Test]
    //public void GetItemByPlu()
    //{
    //    throw new NotImplementedException();
    //}
}