using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.PlusStorageMethodsFks;

namespace Ws.StorageCoreTests.Tables.TableScaleModels.PlusStorageMethodsFks;

[TestFixture]
public sealed class PluStorageMethodsFkRepositoryTests : TableRepositoryTests
{
    private SqlPluStorageMethodFkRepository PluStorageMethodFkRepository { get; } = new();

    private SqlPluStorageMethodFkEntity GetFirstPluStorageMethodFk()
    {
        SqlCrudConfig.SelectTopRowsCount = 1;
        return PluStorageMethodFkRepository.GetList(SqlCrudConfig).First();
    }

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            List<SqlPluStorageMethodFkEntity> items = PluStorageMethodFkRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }

    [Test]
    public void GetItemByPlu()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            SqlPluStorageMethodFkEntity oldPluStorageMethodFk = GetFirstPluStorageMethodFk();
            SqlPluEntity plu = oldPluStorageMethodFk.Plu;
            SqlPluStorageMethodFkEntity pluStorageMethodFksByPlu = PluStorageMethodFkRepository.GetItemByPlu(plu);

            Assert.That(pluStorageMethodFksByPlu.IsExists, Is.True);
            Assert.That(pluStorageMethodFksByPlu, Is.EqualTo(oldPluStorageMethodFk));

            TestContext.WriteLine(pluStorageMethodFksByPlu);
        }, false);
    }
}