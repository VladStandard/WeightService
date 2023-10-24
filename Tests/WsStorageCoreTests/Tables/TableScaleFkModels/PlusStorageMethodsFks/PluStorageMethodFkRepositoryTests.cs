using WsStorageCore.Entities.SchemaRef1c.Plus;
using WsStorageCore.Entities.SchemaScale.PlusStorageMethodsFks;
namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusStorageMethodsFks;

[TestFixture]
public sealed class PluStorageMethodsFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluStorageMethodFkRepository PluStorageMethodFkRepository { get; } = new();

    private WsSqlPluStorageMethodFkEntity GetFirstPluStorageMethodFk()
    {
        SqlCrudConfig.SelectTopRowsCount = 1;
        return PluStorageMethodFkRepository.GetList(SqlCrudConfig).First();
    }

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluStorageMethodFkEntity> items = PluStorageMethodFkRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }

    [Test]
    public void GetItemByPlu()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlPluStorageMethodFkEntity oldPluStorageMethodFk = GetFirstPluStorageMethodFk();
            WsSqlPluEntity plu = oldPluStorageMethodFk.Plu;
            WsSqlPluStorageMethodFkEntity pluStorageMethodFksByPlu = PluStorageMethodFkRepository.GetItemByPlu(plu);

            Assert.That(pluStorageMethodFksByPlu.IsExists, Is.True);
            Assert.That(pluStorageMethodFksByPlu, Is.EqualTo(oldPluStorageMethodFk));

            TestContext.WriteLine(pluStorageMethodFksByPlu);
        }, false, DefaultConfigurations);
    }
}