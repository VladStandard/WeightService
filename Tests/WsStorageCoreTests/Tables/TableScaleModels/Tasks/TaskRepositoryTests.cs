using NUnit.Framework.Constraints;

namespace WsStorageCoreTests.Tables.TableScaleModels.Tasks;

[TestFixture]
public sealed class TaskRepositoryTests : TableRepositoryTests
{
    private WsSqlTaskRepository TaskRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue =>
        Is.Ordered.Using((IComparer<WsSqlTaskModel>)Comparer<WsSqlTaskModel>.
            // ReSharper disable once StringCompareToIsCultureSpecific
            Create((x, y) => x.Scale.Description.CompareTo(y.Scale.Description))).Ascending;


    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlTaskModel> items = TaskRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
}