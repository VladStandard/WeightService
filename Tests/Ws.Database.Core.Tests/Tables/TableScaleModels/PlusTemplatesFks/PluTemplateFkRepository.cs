// using Ws.Database.Core.Entities.Scales.PlusTemplatesFks;
// using Ws.Domain.Models.Entities.Ref1c;
// using Ws.Domain.Models.Entities.Scale;
//
// namespace Ws.StorageCoreTests.Tables.TableScaleModels.PlusTemplatesFks;
//
// [TestFixture]
// public sealed class PluTemplateFkRepositoryTests : TableRepositoryTests
// {
//     private SqlPluTemplateFkRepository PluTemplateFkRepository { get; set; } = new();
//
//     private PluTemplateFkEntity GetFirstPluTemplateFkModel()
//     {
//         return PluTemplateFkRepository.GetList().First();
//     }
//
//     [Test]
//     public void GetList()
//     {
//         AssertAction(() =>
//         {
//             IEnumerable<PluTemplateFkEntity> items = PluTemplateFkRepository.GetList(SqlCrudConfig);
//             ParseRecords(items);
//         });
//     }
//
//     [Test]
//     public void GetItemByPlu()
//     {
//         AssertAction(() =>
//         {
//             PluTemplateFkEntity oldPluTemplateFk = GetFirstPluTemplateFkModel();
//             PluEntity plu = oldPluTemplateFk.Plu;
//             PluTemplateFkEntity pluTemplateByPlu = PluTemplateFkRepository.GetItemByPlu(plu);
//
//             Assert.That(pluTemplateByPlu.IsExists, Is.True);
//             Assert.That(pluTemplateByPlu, Is.EqualTo(oldPluTemplateFk));
//
//             TestContext.WriteLine(pluTemplateByPlu);
//         });
//     }
// }