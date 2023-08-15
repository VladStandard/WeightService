namespace WsStorageCoreTests.Tables.TableDiagModels.LogsTypes;

[TestFixture]
public sealed class LogTypesRepositoryTests : TableRepositoryTests
{
    private WsSqlLogTypeRepository LogTypeRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(WsSqlLogTypeModel.Number)).Ascending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlLogTypeModel> items = LogTypeRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
}