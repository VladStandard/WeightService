using WsStorageCore.Entities.SchemaRef1c.Plus;
using WsStorageCore.Entities.SchemaScale.PlusTemplatesFks;

namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusTemplatesFks;

[TestFixture]
public sealed class PluTemplateFkRepositoryTests : TableRepositoryTests
{
    private SqlPluTemplateFkRepository PluTemplateFkRepository { get; set; } = new();

    private SqlPluTemplateFkEntity GetFirstPluTemplateFkModel()
    {
        SqlCrudConfig.SelectTopRowsCount = 1;
        return PluTemplateFkRepository.GetList(SqlCrudConfig).First();
    }

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<SqlPluTemplateFkEntity> items = PluTemplateFkRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false);
    }

    [Test]
    public void GetItemByPlu()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            SqlPluTemplateFkEntity oldPluTemplateFk = GetFirstPluTemplateFkModel();
            SqlPluEntity plu = oldPluTemplateFk.Plu;
            SqlPluTemplateFkEntity pluTemplateByPlu = PluTemplateFkRepository.GetItemByPlu(plu);

            Assert.That(pluTemplateByPlu.IsExists, Is.True);
            Assert.That(pluTemplateByPlu, Is.EqualTo(oldPluTemplateFk));

            TestContext.WriteLine(pluTemplateByPlu);
        }, false);
    }
}