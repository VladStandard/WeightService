using WsStorageCore.Entities.SchemaScale.PlusStorageMethods;
namespace WsStorageCoreTests.Tables.TableScaleModels.PluStorageMethods;

[TestFixture]
public sealed class PluStorageRepositoryTests : TableRepositoryTests
{
    private WsSqlPluStorageMethodRepository PluStorageMethodRepository { get; } = new();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluStorageMethodEntity> items = PluStorageMethodRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
}