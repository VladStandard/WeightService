using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Models.Entities.SchemaScale;
using Ws.StorageCore.Entities.Scales.PlusTemplatesFks;

namespace Ws.StorageCoreTests.Tables.TableScaleModels.PlusTemplatesFks;

[TestFixture]
public sealed class PluTemplateFkRepositoryTests : TableRepositoryTests
{
    private SqlPluTemplateFkRepository PluTemplateFkRepository { get; set; } = new();

    private PluTemplateFkEntity GetFirstPluTemplateFkModel()
    {
        SqlCrudConfig.SelectTopRowsCount = 1;
        return PluTemplateFkRepository.GetList(SqlCrudConfig).First();
    }

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<PluTemplateFkEntity> items = PluTemplateFkRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        });
    }

    [Test]
    public void GetItemByPlu()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            PluTemplateFkEntity oldPluTemplateFk = GetFirstPluTemplateFkModel();
            PluEntity plu = oldPluTemplateFk.Plu;
            PluTemplateFkEntity pluTemplateByPlu = PluTemplateFkRepository.GetItemByPlu(plu);

            Assert.That(pluTemplateByPlu.IsExists, Is.True);
            Assert.That(pluTemplateByPlu, Is.EqualTo(oldPluTemplateFk));

            TestContext.WriteLine(pluTemplateByPlu);
        });
    }
}