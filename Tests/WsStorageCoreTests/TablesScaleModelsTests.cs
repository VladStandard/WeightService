// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests;

[TestFixture]
public sealed class TablesScaleModelsTests
{
	#region Public and private methods

	[Test]
	public void SqlTableModel_New_EqualsDefault()
	{
        Assert.DoesNotThrow(() =>
        {
            List<WsSqlTableBase> sqlTables = WsTestsUtils.DataTests.DataContext.GetTableModels();
            List<(string, bool, bool)> asserts = new();

            foreach (WsSqlTableBase sqlTable in sqlTables)
	            asserts.Add((sqlTable.GetType().Name, sqlTable.EqualsNew(), sqlTable.EqualsDefault()));
            
			foreach ((string, bool, bool) assert in asserts)
            {
                TestContext.WriteLine(
                    ((!assert.Item2 || !assert.Item3) ? "[x] " : "[✓] ") +
                    $"{assert.Item1}\t|\t" +
                    $"{nameof(WsSqlTableBase.EqualsNew)} = {assert.Item2}\t|\t" +
                    $"{nameof(WsSqlTableBase.EqualsDefault)} = {assert.Item3}");
            }

            foreach ((string, bool, bool) assert in asserts)
            {
                Assert.That(assert.Item2, Is.EqualTo(true));
                Assert.That(assert.Item3, Is.EqualTo(true));
            }
		});
	}
	
	#endregion
}