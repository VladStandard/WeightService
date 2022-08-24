// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class BarCodeTypeV2ValidatorTests
{
	[Test]
	public void Entity_Validate_IsFalse()
	{
		// Arrange & Act.
		BarCodeTypeV2Entity item = DataCoreUtils.CreateNewSubstitute<BarCodeTypeV2Entity>(false);
		// Assert.
		DataCoreUtils.AssertSqlValidate(item, false);
	}

	[Test]
	public void Entity_Validate_IsTrue()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange & Act.
			BarCodeTypeV2Entity item = DataCoreUtils.CreateNewSubstitute<BarCodeTypeV2Entity>(true);
			// Assert.
			DataCoreUtils.AssertSqlValidate(item, true);
		});
	}

	[Test]
	public void DbTable_Validate_IsTrue()
	{
		DataCoreUtils.AssertSqlDataValidate<BarCodeTypeV2Entity>();
	}
}
