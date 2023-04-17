// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.Tables;
using WsStorage.TableScaleModels.PlusGroups;

namespace WsStorageCoreTests.TableScaleModels.PlusGroups;

[TestFixture]
internal class PluGroupModelTests
{
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        DataCoreTestsUtils.DataCore.AssertSqlPropertyCheckDt<PluGroupModel>(nameof(WsSqlTableBase.CreateDt));
        DataCoreTestsUtils.DataCore.AssertSqlPropertyCheckDt<PluGroupModel>(nameof(WsSqlTableBase.ChangeDt));
        DataCoreTestsUtils.DataCore.AssertSqlPropertyCheckBool<PluGroupModel>(nameof(WsSqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        DataCoreTestsUtils.DataCore.TableBaseModelAssertToString<PluGroupModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        DataCoreTestsUtils.DataCore.TableBaseModelAssertEqualsNew<PluGroupModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        DataCoreTestsUtils.DataCore.TableBaseModelAssertSerialize<PluGroupModel>();
    }
}