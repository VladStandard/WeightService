// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.ComponentModel.DataAnnotations;

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class AccessValidatorTests
{
	[Test]
	public void Entity_Validate_IsFalse()
	{
		// Arrange & Act.
		AccessEntity item = DataCoreUtils.CreateNewSubstitute<AccessEntity>(false);
		// Assert.
		DataCoreUtils.AssertSqlValidate(item, false);
	}

	[Test]
	public void Entity_Validate_IsTrue()
	{
		// Arrange.
		AccessEntity item = DataCoreUtils.CreateNewSubstitute<AccessEntity>(false);
		// Act.
		foreach (AccessRights rights in Enum.GetValues(typeof(AccessRights)))
		{
			item.Rights = (byte)rights;
			// Assert.
			DataCoreUtils.AssertSqlValidate(item, true);
		}
	}

	[Test]
	public void DbTable_Validate_IsTrue()
	{
		DataCoreUtils.AssertSqlDataValidate<AccessEntity>();
	}
}
