// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using FluentNHibernate.Data;
using FluentValidation;
using System;

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class NomenclatureValidatorTests
{
	[Test]
	public void Entity_Validate_IsFalse()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange.
			NomenclatureEntity item = Substitute.For<NomenclatureEntity>();
			NomenclatureValidator validator = new();
			// Act.
			ValidationResult result = validator.Validate(item);
			TestsUtils.FailureWriteLine(result);
			// Assert.
			Assert.IsFalse(result.IsValid);
			// Act.
			item.Name = string.Empty;
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
			NomenclatureEntity item = Substitute.For<NomenclatureEntity>();
			NomenclatureValidator validator = new();
			// Act.
			item.CreateDt = DateTime.Now;
			item.ChangeDt = DateTime.Now;
			item.IsMarked = false;
			item.IdentityId = -1;
			item.Name = "0.1.2";
			item.Code = "ЦБД00012345";
			item.Xml = "<Product Category=\"Сосиски\" > </Product>";
			item.Weighted = false;
			ValidationResult result = validator.Validate(item);
			TestsUtils.FailureWriteLine(result);
			// Assert.
			Assert.IsTrue(result.IsValid);
		});
	}

	[Test]
	public void DbTable_Validate_IsTrue()
	{
		TestsUtils.DbTable_UniversalValidate_IsTrue<NomenclatureEntity>(1000);
	}
}
