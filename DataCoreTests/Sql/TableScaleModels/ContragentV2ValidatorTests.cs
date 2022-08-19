// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class ContragentV2ValidatorTests
{
	[Test]
	public void Entity_Validate_IsFalse()
	{
		// Arrange.
		ContragentV2Entity item = Substitute.For<ContragentV2Entity>();
		// Act.
		// Assert.
		DataCoreUtils.AssertSqlValidate(item, false);
		// Act.
		item.Name = "";
		// Assert.
		DataCoreUtils.AssertSqlValidate(item, false);
	}

	[Test]
	public void Entity_Validate_IsTrue()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			ContragentV2Entity item = Substitute.For<ContragentV2Entity>();
			ContragentV2Validator validator = new();
			// Act.
			item.CreateDt = DateTime.Now;
			item.ChangeDt = DateTime.Now;
			item.IdentityUid = Guid.NewGuid();
			item.Name = "Test";
			ValidationResult result = validator.Validate(item);
			DataCoreUtils.FailureWriteLine(result);
			// Assert.
			Assert.IsTrue(result.IsValid);
		});
	}

	[Test]
	public void DbTable_Validate_IsTrue()
	{
		DataCoreUtils.AssertSqlDataValidate<ContragentV2Entity>(100);
	}
}
