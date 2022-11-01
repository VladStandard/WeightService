// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace DataCoreTests.Sql;

[TestFixture]
internal class TablesScaleModelsTests
{
	#region Public and private fields, properties, constructor

	private DataCoreHelper DataCore { get; } = DataCoreHelper.Instance;

	#endregion

	#region Public and private methods

	[Test]
	public void SqlTableModel_New_EqualsDefault()
	{
		DataCore.AssertAction(() =>
		{
			List<SqlTableBase> items = DataCoreEnums.GetSqlTableModels();
			foreach (SqlTableBase item in items)
			{
				TestContext.WriteLine(item.GetType());
				Assert.AreEqual(true, item.EqualsNew());
				Assert.AreEqual(true, item.EqualsDefault());
			}
		});
	}

	[Test]
	public void SqlTableType_Validate_IsFalse()
	{
		DataCore.AssertAction(() =>
		{
			List<Type> sqlTableTypes = DataCoreEnums.GetSqlTableTypes();
			foreach (Type sqlTableType in sqlTableTypes)
			{
				switch (sqlTableType)
				{
					case var cls when cls == typeof(AccessModel):
						// Arrange & Act.
						AccessModel access = DataCore.CreateNewSubstitute<AccessModel>(false);
						// Assert.
						DataCore.AssertSqlValidate(access, false);
						break;
					case var cls when cls == typeof(AppModel):
						// Arrange & Act.
						AppModel app = DataCore.CreateNewSubstitute<AppModel>(false);
						// Assert.
						DataCore.AssertSqlValidate(app, false);
						break;
				}
			}
		});
	}

	[Test]
	public void SqlTableType_Validate_IsTrue()
	{
		DataCore.AssertAction(() =>
		{
			List<Type> sqlTableTypes = DataCoreEnums.GetSqlTableTypes();
			foreach (Type sqlTableType in sqlTableTypes)
			{
				switch (sqlTableType)
				{
					case var cls when cls == typeof(AccessModel):
						// Arrange.
						AccessModel access = DataCore.CreateNewSubstitute<AccessModel>(true);
						// Act.
						foreach (AccessRightsEnum rights in Enum.GetValues(typeof(AccessRightsEnum)))
						{
							access.Rights = (byte)rights;
							// Assert.
							DataCore.AssertSqlValidate(access, true);
						}
						break;
					case var cls when cls == typeof(AppModel):
						// Arrange & Act.
						AppModel app = DataCore.CreateNewSubstitute<AppModel>(true);
						// Assert.
						DataCore.AssertSqlValidate(app, true);
						break;
				}
			}
		});
	}

	#endregion
}
