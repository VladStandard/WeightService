// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Packages;

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class PackageValidatorTests
{
	private static DataCoreHelper DataCore => DataCoreHelper.Instance;

	[Test]
	public void Model_Validate_IsFalse()
	{
		// Arrange & Act.
		PackageModel item = DataCore.CreateNewSubstitute<PackageModel>(false);
		// Assert.
		DataCore.AssertSqlValidate(item, false);
	}

	[Test]
	public void Model_Validate_IsTrue()
	{
		// Arrange & Act.
		PackageModel item = DataCore.CreateNewSubstitute<PackageModel>(true);
		// Assert.
		DataCore.AssertSqlValidate(item, true);
	}
}
