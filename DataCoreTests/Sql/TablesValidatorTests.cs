//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//namespace DataCoreTests.Sql;

//[TestFixture]
//internal class TablesValidatorTests
//{
//	#region Public and private fields, properties, constructor

//	private DataCoreHelper DataCore { get; } = DataCoreHelper.Instance;

//	#endregion

//	#region Public and private methods

//	[Test]
//	public void Model_Validate_IsFalse()
//	{
//		DataCore.AssertAction(() =>
//		{
//			List<SqlTableBase> sqlTables = DataCore.DataContext.GetSqlTableModels(false);
//			foreach (Type type in types)
//			{
//				//TestContext.WriteLine(item.GetType());
//				// Arrange & Act.
//				typeof(type) item = DataCore.CreateNewSubstitute< typeof(type) > (false);
//				// Assert.
//				DataCore.AssertSqlValidate(item, false);
//			}
//		});
//	}

//	[Test]
//	public void Model_Validate_IsTrue()
//	{
//		DataCore.AssertAction(() =>
//		{
//			List<SqlTableBase> sqlTables = DataCore.DataContext.GetSqlTableModels();
//			foreach (SqlTableBase item in items)
//			{
//				TestContext.WriteLine(item.GetType());
//				// Assert.
//				DataCore.AssertSqlValidate(item, true);
//			}
//		});
//	}

//	#endregion
//}
