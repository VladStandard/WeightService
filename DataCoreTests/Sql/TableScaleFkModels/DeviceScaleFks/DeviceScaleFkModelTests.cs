// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;

namespace DataCoreTests.Sql.TableScaleFkModels.DeviceScaleFks;

[TestFixture]
internal class DeviceScaleFkModelTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;
   
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        DataCore.AssertSqlPropertyCheckDt<DeviceScaleFkModel>(nameof(DeviceScaleFkModel.CreateDt));
        DataCore.AssertSqlPropertyCheckDt<DeviceScaleFkModel>(nameof(DeviceScaleFkModel.ChangeDt));
        DataCore.AssertSqlPropertyCheckBool<DeviceScaleFkModel>(nameof(SqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        DataCore.TableBaseModelAssertToString<DeviceScaleFkModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        DataCore.TableBaseModelAssertEqualsNew<DeviceScaleFkModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        DataCore.TableBaseModelAssertSerialize<DeviceScaleFkModel>();
    }
}