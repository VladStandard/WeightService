using WsStorageCore.Views.ViewRefModels.PluLines;
using WsStorageCore.Views.ViewScaleModels.PluLabels;

namespace WsStorageCoreTests.Views.ViewScaleModels.PluLabels;

[TestFixture]
public sealed class ViewPluLabelRepositoryTests
{
    private WsSqlViewPluLabelRepository PluLabelRepository = WsSqlViewPluLabelRepository.Instance;
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlCrudConfigModel sqlConfig = new WsSqlCrudConfigModel() { SelectTopRowsCount = 10 };
            List<WsSqlViewPluLabelModel> items = PluLabelRepository.GetList(sqlConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}