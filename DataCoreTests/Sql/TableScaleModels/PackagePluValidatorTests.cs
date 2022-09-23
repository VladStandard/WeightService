// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class PackagePluValidatorTests
{
	private DataCoreHelper Helper { get; } = DataCoreHelper.Instance;

	[Test]
	public void Model_Validate_IsFalse()
	{
		// Arrange & Act.
		PluPackageModel item = Helper.CreateNewSubstitute<PluPackageModel>(false);
		// Assert.
		Helper.AssertSqlValidate(item, false);
	}

	[Test]
	public void Model_Validate_IsTrue()
	{
		// Arrange & Act.
		PluPackageModel item = Helper.CreateNewSubstitute<PluPackageModel>(true);
		// Assert.
		Helper.AssertSqlValidate(item, true);
	}
}
