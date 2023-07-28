// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework.Constraints;
using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableDiagModels.ScalesScreenshots;

[TestFixture]
public sealed class ScaleScreenshotRepositoryTests : TableRepositoryTests
{
    private WsSqlScaleScreenshotRepository ScaleScreenshotRepository { get; set; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(WsSqlTableBase.ChangeDt)).Descending;

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlScaleScreenShotModel> items = ScaleScreenshotRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
}