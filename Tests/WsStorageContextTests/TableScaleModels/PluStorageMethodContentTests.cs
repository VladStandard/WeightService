// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageContextTests.TableScaleModels;

[TestFixture]
public sealed class PluStorageMethodContentTests
{
	[Test]
    public void Model_Content_Validate()
    {
		WsTestsUtils.DataCore.AssertSqlDbContentValidate<PluStorageMethodModel>();
	}

	[Test]
    public void Model_GetPluStorageMethod_Validate()
    {
        WsTestsUtils.DataCore.AssertAction(() =>
        {
            SqlCrudConfigModel sqlCrudConfig = new(true, false, false, false, false);
            List<PluStorageMethodFkModel> pluStorageMethodFks = WsTestsUtils.DataCore.DataContext.UpdatePluStorageMethodFks(sqlCrudConfig);
            TestContext.WriteLine($"{nameof(pluStorageMethodFks)}.{nameof(pluStorageMethodFks.Count)}: {pluStorageMethodFks.Count}");
            
            List<PluModel> plus = WsTestsUtils.DataCore.DataContext.GetListNotNullablePlus(sqlCrudConfig);
            TestContext.WriteLine($"{nameof(plus)}.{nameof(plus.Count)}: {plus.Count}");

            foreach (PluStorageMethodModel method in plus.Select(plu => WsTestsUtils.DataCore.DataContext.GetPluStorageMethod(plu)))
            {
                if (method.IsExists)
                    WsTestsUtils.DataCore.AssertSqlValidate(method, true);
            }

            foreach (TemplateResourceModel resource in plus.Select(plu => WsTestsUtils.DataCore.DataContext.GetPluStorageResource(plu)))
            {
                if (resource.IsExists)
                    WsTestsUtils.DataCore.AssertSqlValidate(resource, true);
            }

        }, false, new() { WsConfiguration.ReleaseVS, WsConfiguration.DevelopVS });
	}
}