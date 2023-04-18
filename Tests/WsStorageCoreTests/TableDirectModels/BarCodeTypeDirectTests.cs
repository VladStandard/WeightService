//// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using WsDataCore.Sql;
//using WsDataCore.Sql.TableDirectModels;
//using Microsoft.Data.SqlClient;
//using NUnit.Framework;
//using System.Collections.Generic;

//namespace DataCoreTests.Sql.TableDirectModels
//{
//    [TestFixture]
//    public sealed class BarCodeTypeDirectTests
//    {
//        public SqlConnectFactory SqlConnect { get; private set; } = SqlConnectFactory.Instance;

//        [Test]
//        public void BarCodeTypeDirect_ExecuteReader_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                TestContext.WriteLine($"[db_scales].[BarCodeTypes]");
//                List<long> listId = new();
//                SqlConnect.ExecuteReader(WsSqlQueriesScales.Tables.BarCodeTypes.GetAllItems, (SqlDataReader reader) =>
//                {
//                    while (reader.Read())
//                    {
//                        listId.Add(SqlConnect.GetValueAsNotNullable<long>(reader, "ID"));
//                    }
//                });

//                foreach (long id in listId)
//                {
//                    BarCodeTypeDirect barCodeType = new(id);
//                    TestContext.WriteLine($"{barCodeType}");
//                }
//            });
//            TestContext.WriteLine();

//            TestsUtils.MethodComplete();
//        }
//    }
//}
