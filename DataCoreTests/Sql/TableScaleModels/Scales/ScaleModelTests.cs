// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Scales;

namespace DataCoreTests.Sql.TableScaleModels.Scales;

[TestFixture]
internal class ScaleModelTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;
   
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        DataCore.AssertSqlPropertyCheckDt<ScaleModel>(nameof(ScaleModel.CreateDt));
        DataCore.AssertSqlPropertyCheckDt<ScaleModel>(nameof(ScaleModel.ChangeDt));
        DataCore.AssertSqlPropertyCheckBool<ScaleModel>(nameof(SqlTableBase.IsMarked));
        //DataCore.AssertSqlFieldStringCheck<ScaleModel>(nameof(ScaleModel.Host.Name));
        DataCore.AssertSqlPropertyCheckString<ScaleModel>(nameof(ScaleModel.Description));
    }

    [Test]
    public void Model_ToString()
    {
        DataCore.TableBaseModelAssertToString<ScaleModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        DataCore.TableBaseModelAssertEqualsNew<ScaleModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        DataCore.TableBaseModelAssertSerialize<ScaleModel>();
    }
}