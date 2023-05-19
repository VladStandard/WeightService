// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageContextTests.TableScaleModels;

[TestFixture]
public sealed class PluStorageMethodContentTests
{
	[Test]
    public void Model_GetPluStorageMethod_Validate()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlCrudConfigModel sqlCrudConfig = new(WsSqlIsMarked.ShowAll, false, false, false, false);
            List<WsSqlPluStorageMethodFkModel> pluStorageMethodFks = WsTestsUtils.DataTests.ContextManager.ContextPluStorage.GetListFks(sqlCrudConfig);
            TestContext.WriteLine($"{nameof(pluStorageMethodFks)}.{nameof(pluStorageMethodFks.Count)}: {pluStorageMethodFks.Count}");
            
            List<WsSqlPluModel> plus = WsTestsUtils.DataTests.ContextManager.ContextList.GetListNotNullablePlus(sqlCrudConfig);
            TestContext.WriteLine($"{nameof(plus)}.{nameof(plus.Count)}: {plus.Count}");

            foreach (WsSqlPluStorageMethodModel method in plus.Select(plu => WsTestsUtils.DataTests.ContextManager.
                ContextPluStorage.GetItem(plu, pluStorageMethodFks)))
            {
                if (method.IsExists)
                    WsTestsUtils.DataTests.AssertSqlValidate(method, true);
            }

            foreach (WsSqlTemplateResourceModel resource in plus.Select(
                plu => WsTestsUtils.ContextManager.ContextPluStorage.GetItemResource(plu)))
            {
                if (resource.IsExists)
                    WsTestsUtils.DataTests.AssertSqlValidate(resource, true);
            }

        }, false, new() { WsEnumConfiguration.ReleaseVS, WsEnumConfiguration.DevelopVS });
	}
}