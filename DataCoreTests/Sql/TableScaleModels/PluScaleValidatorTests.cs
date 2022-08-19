// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class PluScaleValidatorTests
{
	[Test]
	public void Entity_Validate_IsFalse()
	{
		// Arrange.
		PluScaleEntity item = Substitute.For<PluScaleEntity>();
		// Act.
		//item.Plu = null;
		// Assert.
		DataCoreUtils.AssertSqlValidate(item, false);
	}

	[Test]
	public void Entity_Validate_IsTrue()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			PluScaleEntity item = Substitute.For<PluScaleEntity>();
			PluScaleValidator validator = new();
			// Act.
			item.CreateDt = DateTime.Now;
			item.ChangeDt = DateTime.Now;
			item.IsMarked = false;
			item.IsActive = true;
			item.IdentityUid = Guid.NewGuid();
			ValidationResult result = validator.Validate(item);
			DataCoreUtils.FailureWriteLine(result);
			// Assert.
			Assert.IsTrue(result.IsValid);
		});
	}

	[Test]
	public void DbTable_Validate_IsTrue()
	{
		DataCoreUtils.AssertAction(() =>
		{
			// Arrange.
			PluScaleValidator validator = new();
			PluScaleEntity[]? items = DataCoreUtils.DataAccess.Crud.GetEntities<PluScaleEntity>();
			// Act.
			if (items == null || !items.Any())
			{
				TestContext.WriteLine($"{nameof(items)} is null or empty!");
			}
			else
			{
				TestContext.WriteLine($"Found {nameof(items)}.Count: {items.Count()}");
				int i = 0;
				foreach (PluScaleEntity item in items)
				{
					if (i < 10)
						TestContext.WriteLine(item);
					i++;
					ValidationResult result = validator.Validate(item);
					DataCoreUtils.FailureWriteLine(result);
					// Assert.
					Assert.IsTrue(result.IsValid);
				}
			}
		});
	}
}
