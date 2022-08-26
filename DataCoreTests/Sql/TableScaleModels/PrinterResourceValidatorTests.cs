// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class PrinterResourceValidatorTests
{
	private DataCoreHelper DataCore { get; } = DataCoreHelper.Instance;

	[Test]
	public void Entity_Validate_IsFalse()
	{
		// Arrange & Act.
		PrinterResourceEntity item = DataCore.CreateNewSubstitute<PrinterResourceEntity>(false);
		// Assert.
		DataCore.AssertSqlValidate(item, false);
	}

	[Test]
	public void Entity_Validate_IsTrue()
	{
		// Arrange & Act.
		PrinterResourceEntity item = DataCore.CreateNewSubstitute<PrinterResourceEntity>(true);
		// Assert.
		DataCore.AssertSqlValidate(item, true);
	}

	[Test]
	public void DbTable_Validate_IsTrue()
	{
		DataCore.AssertSqlDataValidate<PrinterResourceEntity>(1_000);
	}
}
