// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.DeviceScalesFks;

namespace WsStorageCoreTests.TableScaleFkModels.DeviceScaleFks;

[TestFixture]
public sealed class DeviceScaleFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        DeviceScaleFkModel item = WsTestsUtils.DataCore.CreateNewSubstitute<DeviceScaleFkModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        DeviceScaleFkModel item = WsTestsUtils.DataCore.CreateNewSubstitute<DeviceScaleFkModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}