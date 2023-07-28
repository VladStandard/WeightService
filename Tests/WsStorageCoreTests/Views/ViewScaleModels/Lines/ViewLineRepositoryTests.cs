// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework.Constraints;
using WsStorageCore.Views.ViewScaleModels.Lines;

namespace WsStorageCoreTests.Views.ViewScaleModels.Lines;

[TestFixture]
public sealed class ViewLinesRepositoryTests : ViewRepositoryTests
{
    private IViewLineRepository ViewLineRepository { get; } = new WsSqlViewLineRepository();

    protected override IResolveConstraint SortOrderValue => Is
        .Ordered.By(nameof(WsSqlViewLineModel.Name)).Ascending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewLineModel> items = ViewLineRepository.GetList(SqlCrudConfig);
            PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }
}