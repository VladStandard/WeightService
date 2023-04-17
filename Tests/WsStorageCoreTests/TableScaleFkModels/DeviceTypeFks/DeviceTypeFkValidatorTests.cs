// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorage.TableScaleFkModels.DeviceTypesFks;

namespace WsStorageCoreTests.TableScaleFkModels.DeviceTypeFks;

[TestFixture]
internal class DeviceTypeFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        DeviceTypeFkModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<DeviceTypeFkModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        DeviceTypeFkModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<DeviceTypeFkModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}