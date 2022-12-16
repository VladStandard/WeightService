// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.DeviceTypes;

namespace DataCoreTests.Sql.TableScaleModels.DeviceTypes;

[TestFixture]
internal class DeviceTypeValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        // Arrange & Act.
        DeviceTypeModel item = DataCore.CreateNewSubstitute<DeviceTypeModel>(false);
        // Assert.
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        // Arrange & Act.
        DeviceTypeModel item = DataCore.CreateNewSubstitute<DeviceTypeModel>(true);
        // Assert.
        DataCore.AssertSqlValidate(item, true);
    }
}
