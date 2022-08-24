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
		// Arrange & Act.
		ContragentV2Entity item = DataCoreUtils.CreateNewSubstitute<ContragentV2Entity>(false);
		// Assert.
		DataCoreUtils.AssertSqlValidate(item, false);
	}

	[Test]
	public void Entity_Validate_IsTrue()
	{
		Assert.DoesNotThrow(() =>
		{
			// Arrange & Act.
			ContragentV2Entity item = DataCoreUtils.CreateNewSubstitute<ContragentV2Entity>(true);
			// Assert.
			DataCoreUtils.AssertSqlValidate(item, true);
		});
	}

	[Test]
	public void DbTable_Validate_IsTrue()
	{
		DataCoreUtils.AssertSqlDataValidate<ContragentV2Entity>(100);
	}
}
