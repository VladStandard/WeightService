// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.DeviceTypesFks;

namespace WsStorageCoreTests.TableScaleFkModels.DeviceTypeFks;

[TestFixture]
public sealed class DeviceTypeFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        DeviceTypeFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<DeviceTypeFkModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        DeviceTypeFkModel item = WsTestsUtils.DataTests.CreateNewSubstitute<DeviceTypeFkModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}