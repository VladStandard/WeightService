// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.DeviceTypes;

namespace WsStorageCoreTests.TableScaleModels.DeviceTypes;

[TestFixture]
public sealed class DeviceTypeModelTests
{
    [Test]
    public void Model_AssertSqlFields_Check()
    {
        WsTestsUtils.DataCore.AssertSqlPropertyCheckDt<DeviceTypeModel>(nameof(WsSqlTableBase.CreateDt));
        WsTestsUtils.DataCore.AssertSqlPropertyCheckDt<DeviceTypeModel>(nameof(WsSqlTableBase.ChangeDt));
        WsTestsUtils.DataCore.AssertSqlPropertyCheckBool<DeviceTypeModel>(nameof(WsSqlTableBase.IsMarked));
    }

    [Test]
    public void Model_ToString()
    {
        WsTestsUtils.DataCore.TableBaseModelAssertToString<DeviceTypeModel>();
    }

    [Test]
    public void Model_EqualsNew()
    {
        WsTestsUtils.DataCore.TableBaseModelAssertEqualsNew<DeviceTypeModel>();
    }

    [Test]
    public void Model_Serialize()
    {
        WsTestsUtils.DataCore.TableBaseModelAssertSerialize<DeviceTypeModel>();
    }
}