// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class AppValidatorTests
{
	[Test]
	public void Entity_Validate_IsFalse()
	{
		// Arrange & Act.
		AppEntity item = DataCoreUtils.CreateNewSubstitute<AppEntity>(false);
		// Assert.
		DataCoreUtils.AssertSqlValidate(item, false);
	}

	[Test]
	public void Entity_Validate_IsTrue()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			AppValidator validator = new();
			// Act.
			AppEntity item = DataCoreUtils.CreateNewSubstitute<AppEntity>(true);
			ValidationResult result = validator.Validate(item);
			DataCoreUtils.FailureWriteLine(result);
			// Assert.
			Assert.IsTrue(result.IsValid);
		});
	}

	[Test]
	public void DbTable_Validate_IsTrue()
	{
		DataCoreUtils.AssertSqlDataValidate<AppEntity>(1_000);
	}
}
