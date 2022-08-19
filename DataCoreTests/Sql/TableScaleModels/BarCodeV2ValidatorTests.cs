// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class BarCodeV2ValidatorTests
{
	[Test]
	public void Entity_Validate_IsFalse()
	{
		// Arrange.
		BarCodeV2Entity item = Substitute.For<BarCodeV2Entity>();
		// Act.
		// Assert.
		DataCoreUtils.AssertSqlValidate(item, false);
		// Act.
		item.Value = "";
		// Assert.
		DataCoreUtils.AssertSqlValidate(item, false);
	}

	[Test]
	public void Entity_Validate_IsTrue()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			BarCodeV2Entity item = Substitute.For<BarCodeV2Entity>();
			BarCodeV2Validator validator = new();
			// Act.
			item.CreateDt = DateTime.Now;
			item.ChangeDt = DateTime.Now;
			item.IdentityUid = Guid.NewGuid();
			item.Value = "Test";
			ValidationResult result = validator.Validate(item);
			DataCoreUtils.FailureWriteLine(result);
			// Assert.
			Assert.IsTrue(result.IsValid);
		});
	}

	[Test]
	public void DbTable_Validate_IsTrue()
	{
		DataCoreUtils.AssertSqlDataValidate<BarCodeV2Entity>(0);
	}
}
