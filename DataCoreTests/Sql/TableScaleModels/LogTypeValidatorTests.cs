// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.LogsTypes;

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class LogTypeValidatorTests
{
	private static DataCoreHelper DataCore => DataCoreHelper.Instance;

	[Test]
	public void Model_Validate_IsFalse()
	{
		// Arrange & Act.
		LogTypeModel item = DataCore.CreateNewSubstitute<LogTypeModel>(false);
		// Assert.
		DataCore.AssertSqlValidate(item, false);
	}

	[Test]
	public void Model_Validate_IsTrue()
	{
		// Arrange & Act.
		LogTypeModel item = DataCore.CreateNewSubstitute<LogTypeModel>(true);
		// Assert.
		DataCore.AssertSqlValidate(item, true);
	}
}
