// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class NomenclatureValidatorTests
{
	[Test]
	public void Entity_Validate_IsFalse()
	{
		// Arrange.
		NomenclatureEntity item = Substitute.For<NomenclatureEntity>();
		// Act.
		// Assert.
		DataCoreUtils.AssertSqlValidate(item, false);
		// Act.
		item.Name = string.Empty;
		// Assert.
		DataCoreUtils.AssertSqlValidate(item, false);
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
			DataCoreUtils.FailureWriteLine(result);
			// Assert.
			Assert.IsTrue(result.IsValid);
		});
	}

	[Test]
	public void DbTable_Validate_IsTrue()
	{
		DataCoreUtils.AssertSqlDataValidate<NomenclatureEntity>(1000);
	}
}
