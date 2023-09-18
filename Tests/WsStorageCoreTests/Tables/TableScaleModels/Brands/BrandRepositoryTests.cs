using WsStorageCore.Tables.TableRef1cModels.Brands;

namespace WsStorageCoreTests.Tables.TableScaleModels.Brands;

[TestFixture]
public sealed class BrandRepositoryTests : TableRepositoryTests
{
    private WsSqlBrandRepository BrandRepository { get; } = new();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<WsSqlBrandModel> items = BrandRepository.GetEnumerable(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
}