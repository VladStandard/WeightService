// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class AppValidatorTests
{
	private DataCoreHelper DataCore { get; } = DataCoreHelper.Instance;

	[Test]
	public void Entity_Validate_IsFalse()
	{
		// Arrange & Act.
		AppEntity item = DataCore.CreateNewSubstitute<AppEntity>(false);
		// Assert.
		DataCore.AssertSqlValidate(item, false);
	}

	[Test]
	public void Entity_Validate_IsTrue()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			AppValidator validator = new();
			// Act.
			AppEntity item = DataCore.CreateNewSubstitute<AppEntity>(true);
			ValidationResult result = validator.Validate(item);
			DataCore.FailureWriteLine(result);
			// Assert.
			Assert.IsTrue(result.IsValid);
		});
	}

	[Test]
	public void DbTable_Validate_IsTrue()
	{
		DataCore.AssertSqlDataValidate<AppEntity>(1_000);
	}
}
