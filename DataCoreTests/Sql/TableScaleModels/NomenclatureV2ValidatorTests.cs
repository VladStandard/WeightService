// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class NomenclatureV2ValidatorTests
{
	private DataCoreHelper DataCore { get; } = DataCoreHelper.Instance;

	[Test]
	public void Model_Validate_IsFalse()
	{
		// Arrange & Act.
		NomenclatureV2Model item = DataCore.CreateNewSubstitute<NomenclatureV2Model>(false);
		// Assert.
		DataCore.AssertSqlValidate(item, false);
	}

	[Test]
	public void Model_Validate_IsTrue()
	{
		// Arrange & Act.
		NomenclatureV2Model item = DataCore.CreateNewSubstitute<NomenclatureV2Model>(true);
		// Assert.
		DataCore.AssertSqlValidate(item, true);
	}
}
