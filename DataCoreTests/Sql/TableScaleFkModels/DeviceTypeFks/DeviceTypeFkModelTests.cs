// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;

namespace DataCoreTests.Sql.TableScaleFkModels.DeviceTypeFks;

[TestFixture]
internal class DeviceTypeFkModelTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_AssertSqlFields_Check()
    {
        DataCore.AssertSqlPropertyCheckDt<DeviceTypeFkModel>(nameof(DeviceTypeFkModel.CreateDt));
        DataCore.AssertSqlPropertyCheckDt<DeviceTypeFkModel>(nameof(DeviceTypeFkModel.ChangeDt));
        DataCore.AssertSqlPropertyCheckBool<DeviceTypeFkModel>(nameof(SqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        DataCore.TableBaseModelAssertToString<DeviceTypeFkModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        DataCore.TableBaseModelAssertEqualsNew<DeviceTypeFkModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        DataCore.TableBaseModelAssertSerialize<DeviceTypeFkModel>();
    }
}