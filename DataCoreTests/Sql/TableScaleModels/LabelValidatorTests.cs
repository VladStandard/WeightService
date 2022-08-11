// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class LabelValidatorTests
{
	[Test]
	public void Entity_Validate_IsFalse()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			LabelEntity item = Substitute.For<LabelEntity>();
			LabelValidator validator = new();
			// Act.
			item.WeithingFact = new(0, false);
			ValidationResult result = validator.Validate(item);
			TestsUtils.FailureWriteLine(result);
			// Assert.
			//Assert.IsFalse(result.IsValid);
			//// Act.
			//item.WeithingFact = null;
			//result = validator.Validate(item);
			//TestsUtils.FailureWriteLine(result);
			//// Assert.
			//Assert.IsFalse(result.IsValid);
		});
	}

	[Test]
	public void Entity_Validate_IsTrue()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			LabelEntity item = Substitute.For<LabelEntity>();
			LabelValidator validator = new();
			// Act.
			item.CreateDt = DateTime.Now;
			item.ChangeDt = DateTime.Now;
			item.IdentityId = -1;
			item.Label = new byte[0x00];
			ValidationResult result = validator.Validate(item);
			TestsUtils.FailureWriteLine(result);
			// Assert.
			Assert.IsTrue(result.IsValid);
		});
	}

	[Test]
	public void DbTable_Validate_IsTrue()
	{
		TestsUtils.DbTable_UniversalValidate_IsTrue<LabelEntity>(100);
	}
}
