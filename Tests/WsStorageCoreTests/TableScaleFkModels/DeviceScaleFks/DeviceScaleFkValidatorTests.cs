// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleFkModels.DeviceScaleFks;

[TestFixture]
public sealed class DeviceScaleFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        DeviceScaleFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<DeviceScaleFkModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        DeviceScaleFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<DeviceScaleFkModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}