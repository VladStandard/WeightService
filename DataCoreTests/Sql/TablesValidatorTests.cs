//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//namespace DataCoreTests.Sql;

//[TestFixture]
//internal class TablesValidatorTests
//{
//	#region Public and private fields, properties, constructor

//	private DataCoreHelper Helper { get; } = DataCoreHelper.Instance;

//	#endregion

//	#region Public and private methods

//	[Test]
//	public void Model_Validate_IsFalse()
//	{
//		Assert.DoesNotThrow(() =>
//		{
//			List<SqlTableBase> items = DataCoreEnums.GetSqlTableModels(false);
//			foreach (Type type in types)
//			{
//				//TestContext.WriteLine(item.GetType());
//				// Arrange & Act.
//				typeof(type) item = Helper.CreateNewSubstitute< typeof(type) > (false);
//				// Assert.
//				Helper.AssertSqlValidate(item, false);
//			}
//		});
//	}

//	[Test]
//	public void Model_Validate_IsTrue()
//	{
//		Assert.DoesNotThrow(() =>
//		{
//			List<SqlTableBase> items = DataCoreEnums.GetSqlTableModels();
//			foreach (SqlTableBase item in items)
//			{
//				TestContext.WriteLine(item.GetType());
//				// Assert.
//				Helper.AssertSqlValidate(item, true);
//			}
//		});
//	}

//	#endregion
//}
