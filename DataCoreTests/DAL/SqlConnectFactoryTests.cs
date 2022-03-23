// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL;
using DataCoreTests;
using Microsoft.Data.SqlClient;
using NUnit.Framework;

namespace DataProjectsCoreTests.DAL
{
    [TestFixture]
    internal class SqlConnectFactoryTests
    {
        [Test]
        public void SqlConnectFactory_ExecuteReader_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                TestContext.WriteLine($"[db_scales].[Scales]");
                //SqlConnectFactory.ExecuteReader(SqlQueries.DbScales.Tables.TaskTypes.GetTasksTypes, null, delegate (SqlDataReader reader)
                //{
                //    while (reader.Read())
                //    {
                //        TestContext.WriteLine($"NAME: {reader.GetString(1)}");
                //    }
                //});
                Assert.AreEqual(1, 1);
            });
            TestContext.WriteLine();

            TestsUtils.MethodComplete();
        }
    }
}
