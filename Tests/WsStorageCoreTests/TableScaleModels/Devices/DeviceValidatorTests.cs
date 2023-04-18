// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Devices;

namespace WsStorageCoreTests.TableScaleModels.Devices;

[TestFixture]
public sealed class DeviceValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        DeviceModel item = WsTestsUtils.DataCore.CreateNewSubstitute<DeviceModel>(false);
        WsTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        DeviceModel item = WsTestsUtils.DataCore.CreateNewSubstitute<DeviceModel>(true);
        WsTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}