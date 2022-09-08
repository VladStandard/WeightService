// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class BarCodeTypeValidatorTests
{
	#region Public and private fields, properties, constructor

	private DataCoreHelper Helper { get; } = DataCoreHelper.Instance;

	#endregion

	#region Public and private methods

	[Test]
	public void Model_Validate_IsFalse()
	{
		// Arrange & Act.
		BarCodeTypeModel item = Helper.CreateNewSubstitute<BarCodeTypeModel>(false);
		// Assert.
		Helper.AssertSqlValidate(item, false);
	}

	[Test]
	public void Model_Validate_IsTrue()
	{
		// Arrange & Act.
		BarCodeTypeModel item = Helper.CreateNewSubstitute<BarCodeTypeModel>(true);
		// Assert.
		Helper.AssertSqlValidate(item, true);
	}

	#endregion
}
