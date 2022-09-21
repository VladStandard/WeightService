// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Sql;

[TestFixture]
internal class SqlConnectFactoryTests
{
    #region Public and private fields, properties, constructor

    private DataCoreHelper Helper { get; } = DataCoreHelper.Instance;

    #endregion

    #region Public and private methods

    [Test]
    public void SqlConnectFactory_ExecuteReader_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            Helper.SqlConnect.ExecuteReader(SqlQueries.DbScales.Tables.Tasks.GetTasks, (reader) =>
            {
                while (reader.Read())
                {
                    TestContext.WriteLine($"TASK_UID: {Helper.SqlConnect.GetValueAsNotNullable<Guid>(reader, "TASK_UID")}");
                    TestContext.WriteLine($"SCALE_ID: {Helper.SqlConnect.GetValueAsNotNullable<long>(reader, "SCALE_ID")}");
                    TestContext.WriteLine($"SCALE: {Helper.SqlConnect.GetValueAsNullable<string>(reader, "SCALE")}");
                    TestContext.WriteLine($"TASK_TYPE_UID: {Helper.SqlConnect.GetValueAsNotNullable<Guid>(reader, "TASK_TYPE_UID")}");
                    TestContext.WriteLine($"TASK: {Helper.SqlConnect.GetValueAsNullable<string>(reader, "TASK")}");
                    TestContext.WriteLine($"ENABLED: {Helper.SqlConnect.GetValueAsNotNullable<bool>(reader, "ENABLED")}");
                    TestContext.WriteLine();
                }
            });
        });
    }

    #endregion
}
