// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Models;

namespace DataCoreTests.Sql;

[TestFixture]
internal class TablesScaleModelsTests
{
	#region Public and private fields, properties, constructor

	private DataCoreHelper Helper { get; } = DataCoreHelper.Instance;

	#endregion

	#region Public and private methods

	[Test]
	public void SqlTableModel_New_EqualsDefault()
	{
		Assert.DoesNotThrow(() =>
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
		Assert.DoesNotThrow(() =>
		{
			List<Type> sqlTableTypes = DataCoreEnums.GetSqlTableTypes();
			foreach (Type sqlTableType in sqlTableTypes)
			{
				switch (sqlTableType)
				{
					case var cls when cls == typeof(AccessModel):
						// Arrange & Act.
						AccessModel access = Helper.CreateNewSubstitute<AccessModel>(false);
						// Assert.
						Helper.AssertSqlValidate(access, false);
						break;
					case var cls when cls == typeof(AppModel):
						// Arrange & Act.
						AppModel app = Helper.CreateNewSubstitute<AppModel>(false);
						// Assert.
						Helper.AssertSqlValidate(app, false);
						break;
				}
			}
		});
	}

	[Test]
	public void SqlTableType_Validate_IsTrue()
	{
		Assert.DoesNotThrow(() =>
		{
			List<Type> sqlTableTypes = DataCoreEnums.GetSqlTableTypes();
			foreach (Type sqlTableType in sqlTableTypes)
			{
				switch (sqlTableType)
				{
					case var cls when cls == typeof(AccessModel):
						// Arrange.
						AccessModel access = Helper.CreateNewSubstitute<AccessModel>(true);
						// Act.
						foreach (AccessRightsEnum rights in Enum.GetValues(typeof(AccessRightsEnum)))
						{
							access.Rights = (byte)rights;
							// Assert.
							Helper.AssertSqlValidate(access, true);
						}
						break;
					case var cls when cls == typeof(AppModel):
						// Arrange & Act.
						AppModel app = Helper.CreateNewSubstitute<AppModel>(true);
						// Assert.
						Helper.AssertSqlValidate(app, true);
						break;
				}
			}
		});
	}

	#endregion
}
