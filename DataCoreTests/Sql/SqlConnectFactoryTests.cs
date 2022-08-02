// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using DataCore.Sql;
using NUnit.Framework;

namespace DataCoreTests.Sql;

[TestFixture]
internal class SqlConnectFactoryTests
{
    [Test]
    public void SqlConnectFactory_ExecuteReader_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            TestsUtils.SqlConnect.ExecuteReader(SqlQueries.DbScales.Tables.Tasks.GetTasks, (reader) =>
            {
                while (reader.Read())
                {
                    TestContext.WriteLine($"TASK_UID: {TestsUtils.SqlConnect.GetValueAsNotNullable<Guid>(reader, "TASK_UID")}");
                    TestContext.WriteLine($"SCALE_ID: {TestsUtils.SqlConnect.GetValueAsNotNullable<long>(reader, "SCALE_ID")}");
                    TestContext.WriteLine($"SCALE: {TestsUtils.SqlConnect.GetValueAsNullable<string>(reader, "SCALE")}");
                    TestContext.WriteLine($"TASK_TYPE_UID: {TestsUtils.SqlConnect.GetValueAsNotNullable<Guid>(reader, "TASK_TYPE_UID")}");
                    TestContext.WriteLine($"TASK: {TestsUtils.SqlConnect.GetValueAsNullable<string>(reader, "TASK")}");
                    TestContext.WriteLine($"ENABLED: {TestsUtils.SqlConnect.GetValueAsNotNullable<bool>(reader, "ENABLED")}");
                    TestContext.WriteLine();
                }
            });
        });
    }
}
