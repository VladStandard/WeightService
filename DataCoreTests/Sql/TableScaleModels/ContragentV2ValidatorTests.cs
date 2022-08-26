// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class ContragentV2ValidatorTests
{
	private DataCoreHelper DataCore { get; } = DataCoreHelper.Instance;

	[Test]
	public void Entity_Validate_IsFalse()
	{
		// Arrange & Act.
		ContragentEntity item = DataCore.CreateNewSubstitute<ContragentEntity>(false);
		// Assert.
		DataCore.AssertSqlValidate(item, false);
	}

	[Test]
	public void Entity_Validate_IsTrue()
	{
		// Arrange & Act.
		ContragentEntity item = DataCore.CreateNewSubstitute<ContragentEntity>(true);
		// Assert.
		DataCore.AssertSqlValidate(item, true);
	}

	[Test]
	public void DbTable_Validate_IsTrue()
	{
		DataCore.AssertSqlDataValidate<ContragentEntity>(1_000);
	}
}
