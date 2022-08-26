// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class AccessValidatorTests
{
	private DataCoreHelper DataCore { get; } = DataCoreHelper.Instance;

	[Test]
	public void Entity_Validate_IsFalse()
	{
		// Arrange & Act.
		AccessEntity item = DataCore.CreateNewSubstitute<AccessEntity>(false);
		// Assert.
		DataCore.AssertSqlValidate(item, false);
	}

	[Test]
	public void Entity_Validate_IsTrue()
	{
		// Arrange.
		AccessEntity item = DataCore.CreateNewSubstitute<AccessEntity>(true);
		// Act.
		foreach (AccessRights rights in Enum.GetValues(typeof(AccessRights)))
		{
			item.Rights = (byte)rights;
			// Assert.
			DataCore.AssertSqlValidate(item, true);
		}
	}

	[Test]
	public void DbTable_Validate_IsTrue()
	{
		DataCore.AssertSqlDataValidate<AccessEntity>(1_000);
	}
}
