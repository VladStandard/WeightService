// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class OrderWeighingValidatorTests
{
	[Test]
	public void Entity_Validate_IsFalse()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			OrderWeighingEntity item = Substitute.For<OrderWeighingEntity>();
			OrderWeighingValidator validator = new();
			// Act.
			ValidationResult result = validator.Validate(item);
			TestsUtils.FailureWriteLine(result);
			// Assert.
			Assert.IsFalse(result.IsValid);
			// Act.
			result = validator.Validate(item);
			TestsUtils.FailureWriteLine(result);
			// Assert.
			Assert.IsFalse(result.IsValid);
		});
	}

	[Test]
	public void Entity_Validate_IsTrue()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			OrderEntity order = Substitute.For<OrderEntity>();
			order.CreateDt = DateTime.Now;
			order.ChangeDt = DateTime.Now;
			order.IsMarked = false;
			order.IdentityUid = Guid.NewGuid();
			order.Name = "Test";
			order.BoxCount = 1;
			order.PalletCount = 1;
			OrderWeighingEntity item = Substitute.For<OrderWeighingEntity>();
			OrderWeighingValidator validator = new();
			// Act.
			item.CreateDt = DateTime.Now;
			item.ChangeDt = DateTime.Now;
			item.IsMarked = false;
			item.IdentityUid = Guid.NewGuid();
			item.Order = order;
			ValidationResult result = validator.Validate(item);
			TestsUtils.FailureWriteLine(result);
			// Assert.
			Assert.IsTrue(result.IsValid);
		});
	}

	[Test]
	public void DbTable_Validate_IsTrue()
	{
		TestsUtils.DbTable_UniversalValidate_IsTrue<OrderWeighingEntity>(1000);
	}
}
