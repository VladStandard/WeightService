// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.Tables.TableScaleFkModels.DeviceLinesFks;

[TestFixture]
public sealed class DeviceScaleFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        WsSqlDeviceScaleFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlDeviceScaleFkModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        WsSqlDeviceScaleFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<WsSqlDeviceScaleFkModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}