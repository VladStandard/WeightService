// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class PluValidatorTests
{
	[Test]
	public void Entity_Validate_IsFalse()
	{
		// Arrange.
		PluEntity item = Substitute.For<PluEntity>();
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
			PluEntity item = Substitute.For<PluEntity>();
			PluValidator validator = new();
			// Act.
			item.Number = 100;
			item.Name = "Test";
			item.FullName = "Test";
			item.Description = "Test";
			item.Gtin = "Test";
			item.Ean13 = "Test";
			item.Itf14 = "Test";
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
			PluValidator validator = new();
			PluEntity[]? items = DataCoreUtils.DataAccess.Crud.GetEntities<PluEntity>();
			// Act.
			if (items == null || !items.Any())
			{
				TestContext.WriteLine($"{nameof(items)} is null or empty!");
			}
			else
			{
				TestContext.WriteLine($"Found {nameof(items)}.Count: {items.Count()}");
				int i = 0;
				foreach (PluEntity item in items)
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
