// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework.Constraints;
using WsStorageCore.Views.ViewRefModels.PluStorageMethods;

namespace WsStorageCoreTests.Views.ViewRefModels.PluStorageMethods;

public class ViewPluStorageRepository : ViewRepositoryTests
{
    private IViewStorageMethodsRepository ViewPluStorageMethodRepository { get; } =
        new WsSqlViewPluStorageMethodRepository();

    protected override IResolveConstraint SortOrderValue => Is
        .Ordered.By(nameof(WsSqlViewPluStorageMethodModel.PluNumber)).Ascending
        .Then.By(nameof(WsSqlViewPluStorageMethodModel.PluName)).Ascending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlViewPluStorageMethodModel> items = ViewPluStorageMethodRepository.GetList(SqlCrudConfig);
            PrintViewRecords(items);
        }, false, DefaultPublishTypes);
    }
}