// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class AppValidatorTests
{
	#region Public and private fields, properties, constructor

	private DataCoreHelper Helper { get; } = DataCoreHelper.Instance;

	#endregion

	#region Public and private methods

	[Test]
	public void Model_Validate_IsFalse()
	{
		// Arrange & Act.
		AppModel item = Helper.CreateNewSubstitute<AppModel>(false);
		// Assert.
		Helper.AssertSqlValidate(item, false);
	}

	[Test]
	public void Model_Validate_IsTrue()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			AppValidator validator = new();
			// Act.
			AppModel item = Helper.CreateNewSubstitute<AppModel>(true);
			ValidationResult result = validator.Validate(item);
			Helper.FailureWriteLine(result);
			// Assert.
			Assert.IsTrue(result.IsValid);
		});
	}

	#endregion
}
