// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class ProductionFacilityValidatorTests
{
	private DataCoreHelper DataCore { get; } = DataCoreHelper.Instance;

	[Test]
	public void Model_Validate_IsFalse()
	{
		// Arrange & Act.
		ProductionFacilityModel item = DataCore.CreateNewSubstitute<ProductionFacilityModel>(false);
		// Assert.
		DataCore.AssertSqlValidate(item, false);
	}

	[Test]
	public void Model_Validate_IsTrue()
	{
		// Arrange & Act.
		ProductionFacilityModel item = DataCore.CreateNewSubstitute<ProductionFacilityModel>(true);
		// Assert.
		DataCore.AssertSqlValidate(item, true);
	}
}
