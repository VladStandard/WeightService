using WsStorageCore.Entities.SchemaScale.PlusClipsFks;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusClipsFks;

[TestFixture]
public sealed class PluClipsFkRepositoryTests : TableRepositoryTests
{
    private SqlPluClipFkRepository PluClipFkRepository { get; } = new();

    private SqlPluClipFkEntity GetFirstPluClipFkModel()
    {
        SqlCrudConfig.SelectTopRowsCount = 1;
        return PluClipFkRepository.GetEnumerable(SqlCrudConfig).First();
    }

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<SqlPluClipFkEntity> items = PluClipFkRepository.GetEnumerable(SqlCrudConfig);
            //ParseRecords(items);
        }, false);
    }

    //[Test]
    //public void GetItemByPlu()
    //{
    //    throw new NotImplementedException();
    //}
}