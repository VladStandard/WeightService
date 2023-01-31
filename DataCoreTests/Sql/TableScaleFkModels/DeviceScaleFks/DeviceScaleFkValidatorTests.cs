// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;

namespace DataCoreTests.Sql.TableScaleFkModels.DeviceScaleFks;

[TestFixture]
internal class DeviceScaleFkValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        DeviceScaleFkModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<DeviceScaleFkModel>(false);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        DeviceScaleFkModel item = DataCoreTestsUtils.DataCore.CreateNewSubstitute<DeviceScaleFkModel>(true);
        DataCoreTestsUtils.DataCore.AssertSqlValidate(item, true);
    }
}