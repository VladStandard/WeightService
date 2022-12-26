// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.ScalesScreenshots;

namespace DataCoreTests.Sql.TableScaleModels.ScalesScreenshots;

[TestFixture]
internal class ScaleScreenShotModelTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;
    
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        DataCore.AssertSqlPropertyCheckDt<ScaleScreenShotModel>(nameof(ScaleScreenShotModel.CreateDt));
        DataCore.AssertSqlPropertyCheckDt<ScaleScreenShotModel>(nameof(ScaleScreenShotModel.ChangeDt));
        DataCore.AssertSqlPropertyCheckBool<ScaleScreenShotModel>(nameof(ScaleScreenShotModel.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        DataCore.TableBaseModelAssertToString<ScaleScreenShotModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        DataCore.TableBaseModelAssertEqualsNew<ScaleScreenShotModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        DataCore.TableBaseModelAssertSerialize<ScaleScreenShotModel>();
    }
}