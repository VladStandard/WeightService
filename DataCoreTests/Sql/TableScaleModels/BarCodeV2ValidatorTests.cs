// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class BarCodeV2ValidatorTests
{
	[Test]
	public void Entity_Validate_IsFalse()
	{
		// Arrange & Act.
		BarCodeEntity item = DataCoreUtils.CreateNewSubstitute<BarCodeEntity>(false);
		// Assert.
		DataCoreUtils.AssertSqlValidate(item, false);
	}

	[Test]
	public void Entity_Validate_IsTrue()
	{
		// Arrange & Act.
		BarCodeEntity item = DataCoreUtils.CreateNewSubstitute<BarCodeEntity>(true);
		// Assert.
		DataCoreUtils.AssertSqlValidate(item, true);
	}

	[Test]
	public void DbTable_Validate_IsTrue()
	{
		DataCoreUtils.AssertSqlDataValidate<BarCodeEntity>(1_000);
	}
}
