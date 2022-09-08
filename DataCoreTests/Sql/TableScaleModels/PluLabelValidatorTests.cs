// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class PluLabelValidatorTests
{
	private DataCoreHelper Helper { get; } = DataCoreHelper.Instance;

	[Test]
	public void Model_Validate_IsFalse()
	{
		// Arrange & Act.
		PluLabelModel item = Helper.CreateNewSubstitute<PluLabelModel>(false);
		// Assert.
		Helper.AssertSqlValidate(item, false);
	}

	[Test]
	public void Model_Validate_IsTrue()
	{
		// Arrange & Act.
		PluLabelModel item = Helper.CreateNewSubstitute<PluLabelModel>(true);
		// Assert.
		Helper.AssertSqlValidate(item, true);
	}
}
