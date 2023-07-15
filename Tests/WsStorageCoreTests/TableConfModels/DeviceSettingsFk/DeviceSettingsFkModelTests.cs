// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableConfModels.DeviceSettingsFk;

[TestFixture]
public sealed class DeviceSettingsFkModelTests
{
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        WsTestsUtils.DataTests.AssertSqlPropertyCheckDt<WsSqlDeviceSettingsFkModel>(nameof(WsSqlTableBase.CreateDt));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckDt<WsSqlDeviceSettingsFkModel>(nameof(WsSqlTableBase.ChangeDt));
        WsTestsUtils.DataTests.AssertSqlPropertyCheckBool<WsSqlDeviceSettingsFkModel>(nameof(WsSqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertToString<WsSqlDeviceSettingsFkModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertEqualsNew<WsSqlDeviceSettingsFkModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        WsTestsUtils.DataTests.TableBaseModelAssertSerialize<WsSqlDeviceSettingsFkModel>();
    }
}