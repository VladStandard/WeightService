namespace WsStorageCoreTests.Tables.TableScaleModels.Clips;

[TestFixture]
public sealed class ClipRepositoryTests : TableRepositoryTests
{
    private WsSqlClipRepository ClipRepository { get; } = new();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlClipModel> items = ClipRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
}