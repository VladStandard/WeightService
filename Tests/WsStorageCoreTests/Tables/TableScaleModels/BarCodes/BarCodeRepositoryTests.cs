using WsStorageCoreTests.Tables.Common;

namespace WsStorageCoreTests.Tables.TableScaleModels.BarCodes;

[TestFixture]
public sealed class BarCodeRepositoryTests : TableRepositoryTests
{
    private WsSqlBarcodeRepository BarcodeRepository { get; set; } = new();
    
    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlBarCodeModel> items = BarcodeRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultPublishTypes);
    }
}