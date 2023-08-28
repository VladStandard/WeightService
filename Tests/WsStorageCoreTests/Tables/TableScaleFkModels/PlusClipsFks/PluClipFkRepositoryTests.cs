namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusClipsFks;

[TestFixture]
public sealed class PluClipsFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluClipFkRepository PluClipFkRepository { get; } = new();

    private WsSqlPluClipFkModel GetFirstPluClipFkModel()
    {
        SqlCrudConfig.SelectTopRowsCount = 1;
        return PluClipFkRepository.GetEnumerable(SqlCrudConfig).First();
    }

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlPluClipFkModel> items = PluClipFkRepository.GetEnumerable(SqlCrudConfig);
            //ParseRecords(items);
        }, false, DefaultConfigurations);
    }

    //[Test]
    //public void GetItemByPlu()
    //{
    //    throw new NotImplementedException();
    //}
}