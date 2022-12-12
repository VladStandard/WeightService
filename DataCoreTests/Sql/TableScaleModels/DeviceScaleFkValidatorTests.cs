// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class DeviceScaleFkValidatorTests
{
	private static DataCoreHelper DataCore => DataCoreHelper.Instance;

	[Test]
	public void Model_Validate_IsFalse()
	{
		// Arrange & Act.
		DeviceScaleFkModel item = DataCore.CreateNewSubstitute<DeviceScaleFkModel>(false);
		// Assert.
		DataCore.AssertSqlValidate(item, false);
	}

	[Test]
	public void Model_Validate_IsTrue()
	{
		// Arrange & Act.
		DeviceScaleFkModel item = DataCore.CreateNewSubstitute<DeviceScaleFkModel>(true);
		// Assert.
		DataCore.AssertSqlValidate(item, true);
	}
}
