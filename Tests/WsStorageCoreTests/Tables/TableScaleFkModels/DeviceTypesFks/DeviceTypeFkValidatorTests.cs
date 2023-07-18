// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableScaleFkModels.DeviceTypesFks;

[TestFixture]
public sealed class DeviceTypeFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlDeviceTypeFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlDeviceTypeFkModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlDeviceTypeFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlDeviceTypeFkModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}