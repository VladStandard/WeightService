// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Logs;

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class LogValidatorTests
{
	private static DataCoreHelper DataCore => DataCoreHelper.Instance;

	[Test]
	public void Model_Validate_IsFalse()
	{
		// Arrange & Act.
		LogModel item = DataCore.CreateNewSubstitute<LogModel>(false);
		// Assert.
		DataCore.AssertSqlValidate(item, false);
	}

	[Test]
	public void Model_Validate_IsTrue()
	{
		// Arrange & Act.
		LogModel item = DataCore.CreateNewSubstitute<LogModel>(true);
		// Assert.
		DataCore.AssertSqlValidate(item, true);
	}
}
