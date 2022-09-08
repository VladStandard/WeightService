// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class LogValidatorTests
{
	private DataCoreHelper Helper { get; } = DataCoreHelper.Instance;

	[Test]
	public void Model_Validate_IsFalse()
	{
		// Arrange & Act.
		LogModel item = Helper.CreateNewSubstitute<LogModel>(false);
		// Assert.
		Helper.AssertSqlValidate(item, false);
	}

	[Test]
	public void Model_Validate_IsTrue()
	{
		// Arrange & Act.
		LogModel item = Helper.CreateNewSubstitute<LogModel>(true);
		// Assert.
		Helper.AssertSqlValidate(item, true);
	}
}
