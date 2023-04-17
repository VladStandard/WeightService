// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.Tables;
using WsStorage.TableScaleFkModels.PlusFks;

namespace WsStorageCoreTests.TableScaleFkModels.PlusFks;

[TestFixture]
internal class PluGroupFkModelTests
{
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        DataCoreTestsUtils.DataCore.AssertSqlPropertyCheckDt<PluFkModel>(nameof(WsSqlTableBase.CreateDt));
        DataCoreTestsUtils.DataCore.AssertSqlPropertyCheckDt<PluFkModel>(nameof(WsSqlTableBase.ChangeDt));
        DataCoreTestsUtils.DataCore.AssertSqlPropertyCheckBool<PluFkModel>(nameof(WsSqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        DataCoreTestsUtils.DataCore.TableBaseModelAssertToString<PluFkModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        DataCoreTestsUtils.DataCore.TableBaseModelAssertEqualsNew<PluFkModel>();
    }
    
    [Test]
    public void Model_Serialize()
    {
        DataCoreTestsUtils.DataCore.TableBaseModelAssertSerialize<PluFkModel>();
    }
}