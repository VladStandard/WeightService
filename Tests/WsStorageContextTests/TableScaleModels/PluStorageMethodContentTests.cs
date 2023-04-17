// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Enums;
using WsStorage.Models;
using WsStorage.TableScaleFkModels.PlusStorageMethodsFks;
using WsStorage.TableScaleModels.Plus;
using WsStorage.TableScaleModels.PlusStorageMethods;
using WsStorage.TableScaleModels.TemplatesResources;

namespace WsStorageContextTests.TableScaleModels;

[TestFixture]
internal class PluStorageMethodContentTests
{
	[Test]
    public void Model_Content_Validate()
    {
		DataCoreTestsUtils.DataCore.AssertSqlDbContentValidate<PluStorageMethodModel>();
	}

	[Test]
    public void Model_GetPluStorageMethod_Validate()
    {
        DataCoreTestsUtils.DataCore.AssertAction(() =>
        {
            SqlCrudConfigModel sqlCrudConfig = new(true, false, false, false, false);
            List<PluStorageMethodFkModel> pluStorageMethodFks = DataCoreTestsUtils.DataCore.DataContext.UpdatePluStorageMethodFks(sqlCrudConfig);
            TestContext.WriteLine($"{nameof(pluStorageMethodFks)}.{nameof(pluStorageMethodFks.Count)}: {pluStorageMethodFks.Count}");
            
            List<PluModel> plus = DataCoreTestsUtils.DataCore.DataContext.GetListNotNullablePlus(sqlCrudConfig);
            TestContext.WriteLine($"{nameof(plus)}.{nameof(plus.Count)}: {plus.Count}");

            foreach (PluStorageMethodModel method in plus.Select(plu => DataCoreTestsUtils.DataCore.DataContext.GetPluStorageMethod(plu)))
            {
                if (method.IsExists)
                    DataCoreTestsUtils.DataCore.AssertSqlValidate(method, true);
            }

            foreach (TemplateResourceModel resource in plus.Select(plu => DataCoreTestsUtils.DataCore.DataContext.GetPluStorageResource(plu)))
            {
                if (resource.IsExists)
                    DataCoreTestsUtils.DataCore.AssertSqlValidate(resource, true);
            }

        }, false, new() { WsConfiguration.ReleaseVS, WsConfiguration.DevelopVS });
	}
}