// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using DataCore.Sql;

namespace DataCoreTests.Sql;

[TestFixture]
internal class SqlConnectFactoryTests
{
	private DataCoreHelper DataCore { get; } = DataCoreHelper.Instance;

    [Test]
    public void SqlConnectFactory_ExecuteReader_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
	        DataCore.SqlConnect.ExecuteReader(SqlQueries.DbScales.Tables.Tasks.GetTasks, (reader) =>
            {
                while (reader.Read())
                {
                    TestContext.WriteLine($"TASK_UID: {DataCore.SqlConnect.GetValueAsNotNullable<Guid>(reader, "TASK_UID")}");
                    TestContext.WriteLine($"SCALE_ID: {DataCore.SqlConnect.GetValueAsNotNullable<long>(reader, "SCALE_ID")}");
                    TestContext.WriteLine($"SCALE: {DataCore.SqlConnect.GetValueAsNullable<string>(reader, "SCALE")}");
                    TestContext.WriteLine($"TASK_TYPE_UID: {DataCore.SqlConnect.GetValueAsNotNullable<Guid>(reader, "TASK_TYPE_UID")}");
                    TestContext.WriteLine($"TASK: {DataCore.SqlConnect.GetValueAsNullable<string>(reader, "TASK")}");
                    TestContext.WriteLine($"ENABLED: {DataCore.SqlConnect.GetValueAsNotNullable<bool>(reader, "ENABLED")}");
                    TestContext.WriteLine();
                }
            });
        });
    }
}
