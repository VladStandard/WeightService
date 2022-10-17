//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//namespace DataCoreTests.Sql.TableScaleModels;

//[TestFixture]
//internal class BarCodeTypeValidatorTests
//{
//	#region Public and private fields, properties, constructor

//	private DataCoreHelper DataCore { get; } = DataCoreHelper.Instance;

//	#endregion

//	#region Public and private methods

//	[Test]
//	public void Model_Validate_IsFalse()
//	{
//		// Arrange & Act.
//		BarCodeTypeModel item = DataCore.CreateNewSubstitute<BarCodeTypeModel>(false);
//		// Assert.
//		DataCore.AssertSqlValidate(item, false);
//	}

//	[Test]
//	public void Model_Validate_IsTrue()
//	{
//		// Arrange & Act.
//		BarCodeTypeModel item = DataCore.CreateNewSubstitute<BarCodeTypeModel>(true);
//		// Assert.
//		DataCore.AssertSqlValidate(item, true);
//	}

//	#endregion
//}
