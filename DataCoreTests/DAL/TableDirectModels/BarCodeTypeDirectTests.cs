// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL;
using DataCore.DAL.TableDirectModels;
using DataProjectsCoreTests.DAL;
using Microsoft.Data.SqlClient;
using NUnit.Framework;
using System.Collections.Generic;

namespace DataCoreTests.DAL.TableDirectModels
{
    [TestFixture]
    internal class BarCodeTypeDirectTests
    {
        [Test]
        public void BarCodeTypeDirect_ExecuteReader_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                TestContext.WriteLine($"[db_scales].[BarCodeTypes]");
                List<int> listId = new();
                SqlConnectFactory.ExecuteReader(SqlQueries.DbScales.Tables.BarCodeTypes.GetAllItems, null, delegate (SqlDataReader reader)
                {
                    while (reader.Read())
                    {
                        listId.Add(SqlConnectFactory.GetValueAsNotNullable<int>(reader, "ID"));
                    }
                });

                foreach (int id in listId)
                {
                    BarCodeTypeDirect barCodeType = new(id);
                    TestContext.WriteLine($"{barCodeType}");
                }
            });
            TestContext.WriteLine();

            TestsUtils.MethodComplete();
        }
    }
}
