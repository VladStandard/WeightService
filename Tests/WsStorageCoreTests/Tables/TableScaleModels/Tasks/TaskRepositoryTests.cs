// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework.Constraints;
using WsStorageCoreTests.Tables.Common;

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