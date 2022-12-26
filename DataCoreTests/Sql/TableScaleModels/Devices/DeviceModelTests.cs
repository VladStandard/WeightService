// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Devices;

namespace DataCoreTests.Sql.TableScaleModels.Devices;

[TestFixture]
internal class DeviceModelTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_AssertSqlFields_Check()
    {
        DataCore.AssertSqlPropertyCheckDt<DeviceModel>(nameof(DeviceModel.CreateDt));
        DataCore.AssertSqlPropertyCheckDt<DeviceModel>(nameof(DeviceModel.ChangeDt));
        DataCore.AssertSqlPropertyCheckBool<DeviceModel>(nameof(SqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        DataCore.TableBaseModelAssertToString<DeviceModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        DataCore.TableBaseModelAssertEqualsNew<DeviceModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        DataCore.TableBaseModelAssertSerialize<DeviceModel>();
    }
}