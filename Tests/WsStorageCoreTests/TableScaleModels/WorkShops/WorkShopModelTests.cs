// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.Tables;
using WsStorage.TableScaleModels.WorkShops;

namespace WsStorageCoreTests.TableScaleModels.WorkShops;

[TestFixture]
internal class WorkShopModelTests
{
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        DataCoreTestsUtils.DataCore.AssertSqlPropertyCheckDt<WorkShopModel>(nameof(WsSqlTableBase.CreateDt));
        DataCoreTestsUtils.DataCore.AssertSqlPropertyCheckDt<WorkShopModel>(nameof(WsSqlTableBase.CreateDt));
        DataCoreTestsUtils.DataCore.AssertSqlPropertyCheckBool<WorkShopModel>(nameof(WsSqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        DataCoreTestsUtils.DataCore.TableBaseModelAssertToString<WorkShopModel>();
    }
    
    [Test]
    public void Model_EqualsNew()
    {
        DataCoreTestsUtils.DataCore.TableBaseModelAssertEqualsNew<WorkShopModel>();
    }
    
    [Test]
    public void Model_Serialize()
    {
        DataCoreTestsUtils.DataCore.TableBaseModelAssertSerialize<WorkShopModel>();
    }
}