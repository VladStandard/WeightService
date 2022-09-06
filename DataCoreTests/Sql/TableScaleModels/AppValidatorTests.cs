// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class AppValidatorTests
{
	private DataCoreHelper DataCore { get; } = DataCoreHelper.Instance;

	[Test]
	public void Model_Validate_IsFalse()
	{
		// Arrange & Act.
		AppModel item = DataCore.CreateNewSubstitute<AppModel>(false);
		// Assert.
		DataCore.AssertSqlValidate(item, false);
	}

	[Test]
	public void Model_Validate_IsTrue()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			AppValidator validator = new();
			// Act.
			AppModel item = DataCore.CreateNewSubstitute<AppModel>(true);
			ValidationResult result = validator.Validate(item);
			DataCore.FailureWriteLine(result);
			// Assert.
			Assert.IsTrue(result.IsValid);
		});
	}
}
