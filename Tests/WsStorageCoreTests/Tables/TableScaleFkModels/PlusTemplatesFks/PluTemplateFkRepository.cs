namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusTemplatesFks;

[TestFixture]
public sealed class PluTemplateFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluTemplateFkRepository PluTemplateFkRepository { get; set; } = new();

    private WsSqlPluTemplateFkModel GetFirstPluTemplateFkModel()
    {
        SqlCrudConfig.SelectTopRowsCount = 1;
        return PluTemplateFkRepository.GetList(SqlCrudConfig).First();
    }

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluTemplateFkModel> items = PluTemplateFkRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }

    [Test]
    public void GetItemByPlu()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlPluTemplateFkModel oldPluTemplateFk = GetFirstPluTemplateFkModel();
            WsSqlPluModel plu = oldPluTemplateFk.Plu;
            WsSqlPluTemplateFkModel pluTemplateByPlu = PluTemplateFkRepository.GetItemByPlu(plu);

            Assert.That(pluTemplateByPlu.IsExists, Is.True);
            Assert.That(pluTemplateByPlu, Is.EqualTo(oldPluTemplateFk));

            TestContext.WriteLine(pluTemplateByPlu);
        }, false, DefaultConfigurations);
    }
}