// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class PrinterResourceValidatorTests
{
	[Test]
	public void Entity_Validate_IsFalse()
	{
		// Arrange & Act.
		PrinterResourceEntity item = DataCoreUtils.CreateNewSubstitute<PrinterResourceEntity>(false);
		// Assert.
		DataCoreUtils.AssertSqlValidate(item, false);
	}

	[Test]
	public void Entity_Validate_IsTrue()
	{
		// Arrange & Act.
		PrinterResourceEntity item = DataCoreUtils.CreateNewSubstitute<PrinterResourceEntity>(true);
		// Assert.
		DataCoreUtils.AssertSqlValidate(item, true);
	}

	[Test]
	public void DbTable_Validate_IsTrue()
	{
		DataCoreUtils.AssertSqlDataValidate<PrinterResourceEntity>(1_000);
	}
}
