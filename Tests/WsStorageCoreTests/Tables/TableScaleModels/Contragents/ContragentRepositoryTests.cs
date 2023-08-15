namespace WsStorageCoreTests.Tables.TableScaleModels.Contragents;

[TestFixture]
public sealed class ContragentRepositoryTests : TableRepositoryTests
{
    private WsSqlContragentRepository ContragentRepository { get; } = new();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlContragentModel> items = ContragentRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
}