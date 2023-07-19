using WsStorageCore.Tables.TableScaleModels.PlusLabels;
using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleModels.PluLabels;

[TestFixture]
public sealed class PluLabelRepositoryTests : TableRepositoryTests
{
    private WsSqlPluLabelRepository PluLabelRepository  { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluLabelModel> items = PluLabelRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}