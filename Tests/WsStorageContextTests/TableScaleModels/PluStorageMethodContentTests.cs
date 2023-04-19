// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageContextTests.TableScaleModels;

[TestFixture]
public sealed class PluStorageMethodContentTests
{
	[Test]
    public void Model_Content_Validate()
    {
		WsTestsUtils.DataTests.AssertSqlDbContentValidate<PluStorageMethodModel>();
	}

	[Test]
    public void Model_GetPluStorageMethod_Validate()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            SqlCrudConfigModel sqlCrudConfig = new(true, false, false, false, false);
            List<PluStorageMethodFkModel> pluStorageMethodFks = WsTestsUtils.DataTests.DataContext.UpdatePluStorageMethodFks(sqlCrudConfig);
            TestContext.WriteLine($"{nameof(pluStorageMethodFks)}.{nameof(pluStorageMethodFks.Count)}: {pluStorageMethodFks.Count}");
            
            List<PluModel> plus = WsTestsUtils.DataTests.DataContext.GetListNotNullablePlus(sqlCrudConfig);
            TestContext.WriteLine($"{nameof(plus)}.{nameof(plus.Count)}: {plus.Count}");

            foreach (PluStorageMethodModel method in plus.Select(plu => WsTestsUtils.DataTests.DataContext.GetPluStorageMethod(plu)))
            {
                if (method.IsExists)
                    WsTestsUtils.DataTests.AssertSqlValidate(method, true);
            }

            foreach (TemplateResourceModel resource in plus.Select(plu => WsTestsUtils.ContextManager.ContextPluStorage
                         .GetPluStorageResource(plu, WsTestsUtils.ContextManager.PluStorageMethodsFks)))
            {
                if (resource.IsExists)
                    WsTestsUtils.DataTests.AssertSqlValidate(resource, true);
            }

        }, false, new() { WsConfiguration.ReleaseVS, WsConfiguration.DevelopVS });
	}
}