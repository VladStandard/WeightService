// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework.Constraints;
using WsStorageCore.Views.ViewRefModels.PluLines;

namespace WsStorageCoreTests.Views.ViewRefModels.PluLines;

public class ViewPluLineRepositoryTests : ViewRepositoryTests
{
    private IViewPluLineRepository ViewPluLineRepository { get; } = new WsSqlViewPluLineRepository();

    protected override IResolveConstraint SortOrderValue => Is
        .Ordered.By(nameof(WsSqlViewPluLineModel.ScaleId)).Ascending
        .Then.By(nameof(WsSqlViewPluLineModel.PluNumber)).Ascending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewPluLineModel> items = ViewPluLineRepository.GetList(SqlCrudConfig);
            PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }
}