// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql;

[TestFixture]
internal class TablesSerializeTests
{
	#region Public and private fields, properties, constructor

	private DataCoreHelper Helper { get; } = DataCoreHelper.Instance;

	#endregion

	#region Public and private methods

	[Test]
	public void Table_AssertSerialize_Model()
	{
		Assert.DoesNotThrow(() =>
		{
			List<SqlTableBase> items = DataCoreEnums.GetSqlTableModels();
			foreach (SqlTableBase item in items)
			{
				TestContext.WriteLine(item.GetType());
				Helper.TableBaseModelAssertSerialize<AccessModel>();
			}
		});

	}

	#endregion
}
