// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Apps;

namespace DataCoreTests.Sql.TableScaleModels.Apps;

[TestFixture]
internal class AppModelTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test] 
    public void Model_AssertSqlFields_Check()
    {
        DataCore.AssertSqlPropertyCheckDt<AppModel>(nameof(AppModel.CreateDt));
        DataCore.AssertSqlPropertyCheckDt<AppModel>(nameof(AppModel.ChangeDt));
        DataCore.AssertSqlPropertyCheckBool<AppModel>(nameof(SqlTableBase.IsMarked));
    }
    
    [Test]
    public void Model_ToString()
    {
        DataCore.TableBaseModelAssertToString<AppModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        DataCore.TableBaseModelAssertEqualsNew<AppModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        DataCore.TableBaseModelAssertSerialize<AppModel>();
    }
}