// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework.Constraints;
using WsStorageCore.Views.ViewScaleModels.PluWeightings;

namespace WsStorageCoreTests.Views.ViewScaleModels.PluWeightings;

[TestFixture]
public sealed class ViewPluWeightingRepositoryTests : ViewRepositoryTests
{
    private IViewPluWeightingRepository ViewPluWeightingRepository { get; } = new WsSqlViewPluWeightingRepository();

    protected override IResolveConstraint SortOrderValue => Is
        .Ordered.By(nameof(WsSqlViewPluWeightingModel.CreateDt)).Descending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewPluWeightingModel> items = ViewPluWeightingRepository.GetList(SqlCrudConfig);
            PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }
}