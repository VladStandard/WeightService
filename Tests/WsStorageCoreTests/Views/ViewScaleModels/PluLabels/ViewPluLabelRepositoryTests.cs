// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework.Constraints;
using WsStorageCore.Views.ViewScaleModels.PluLabels;

namespace WsStorageCoreTests.Views.ViewScaleModels.PluLabels;

[TestFixture]
public sealed class ViewPluLabelRepositoryTests : ViewRepositoryTests
{
    private IViewPluLabelRepository PluLabelRepository { get; } = new WsSqlViewPluLabelRepository();

    protected override IResolveConstraint SortOrderValue => Is
        .Ordered.By(nameof(WsSqlViewPluLabelModel.CreateDt)).Descending;


    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewPluLabelModel> items = PluLabelRepository.GetList(SqlCrudConfig);
            PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }
}