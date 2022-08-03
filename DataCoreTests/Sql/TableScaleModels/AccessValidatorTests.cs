// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels;
using FluentValidation.Results;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Linq;
using static DataCore.ShareEnums;

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class AccessValidatorTests
{
	[Test]
	public void Entity_Validate_IsFalse()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			AccessEntity item = Substitute.For<AccessEntity>();
			AccessValidator validator = new();
			// Act.
			ValidationResult result = validator.Validate(item);
			TestsUtils.FailureWriteLine(result);
			// Assert.
			Assert.IsFalse(result.IsValid);
			// Act.
			item.User = "";
			result = validator.Validate(item);
			TestsUtils.FailureWriteLine(result);
			// Assert.
			Assert.IsFalse(result.IsValid);
			// Act.
			item.User = "Test";
			item.Rights = (byte)AccessRights.Admin + 1;
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
			AccessEntity item = Substitute.For<AccessEntity>();
			AccessValidator validator = new();
			// Act.
			item.User = "Test";
			foreach (AccessRights rights in Enum.GetValues(typeof(AccessRights)))
			{
				item.Rights = (byte)rights;
				ValidationResult result = validator.Validate(item);
				TestsUtils.FailureWriteLine(result);
				// Assert.
				Assert.IsTrue(result.IsValid);
			}
		});
	}

	[Test]
	public void DbTable_Validate_IsTrue()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			AccessValidator validator = new();
			AccessEntity[]? items = TestsUtils.DataAccess.Crud.GetEntities<AccessEntity>();
			// Act.
			if (items == null || !items.Any())
			{
				TestContext.WriteLine($"{nameof(items)} is null or empty!");
			}
			else
			{
				TestContext.WriteLine($"Found {nameof(items)}.Count: {items.Count()}");
				foreach (AccessEntity item in items)
				{
					TestContext.WriteLine(item);
					ValidationResult result = validator.Validate(item);
					TestsUtils.FailureWriteLine(result);
					// Assert.
					Assert.IsTrue(result.IsValid);
				}
			}
		});
	}
}
