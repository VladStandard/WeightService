// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework.Constraints;
using WsStorageCore.Views.ViewScaleModels.Aggregations;

namespace WsStorageCoreTests.Views.ViewScaleModels.Aggregations;

[TestFixture]
public sealed class ViewAggregationsRepositoryTests : ViewRepositoryTests
{
    private IViewWeightingAggrRepository ViewWeightingAggrRepository { get; } = new WsSqlViewWeightingAggrRepository();

    protected override IResolveConstraint SortOrderValue => Is
        .Ordered.By(nameof(WsSqlViewWeightingAggrModel.ChangeDt)).Descending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewWeightingAggrModel> items = ViewWeightingAggrRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }
}