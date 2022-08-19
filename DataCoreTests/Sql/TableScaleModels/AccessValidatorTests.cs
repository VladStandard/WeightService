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
		// Arrange.
		AccessEntity item = Substitute.For<AccessEntity>();
		// Act.
		// Assert.
		DataCoreUtils.AssertSqlValidate(item, false);
		// Act.
		item.User = "";
		// Assert.
		DataCoreUtils.AssertSqlValidate(item, false);
		// Act.
		item.User = "Test";
		item.Rights = (byte)AccessRights.Admin + 1;
		// Assert.
		DataCoreUtils.AssertSqlValidate(item, false);
	}

	[Test]
	public void Entity_Validate_IsTrue()
	{
		// Arrange.
		AccessEntity item = Substitute.For<AccessEntity>();
		// Act.
		item.CreateDt = DateTime.Now;
		item.ChangeDt = DateTime.Now;
		item.IdentityUid = Guid.NewGuid();
		item.User = "Test";
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
		DataCoreUtils.AssertSqlDataValidate<AccessEntity>(0);
	}
}
