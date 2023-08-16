namespace WsStorageCoreTests.Tables.TableScaleModels.Boxes;

[TestFixture]
public sealed class BoxRepositoryTests : TableRepositoryTests
{
    private WsSqlBoxRepository BoxRepository { get; } = new();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlBoxModel> items = BoxRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
}