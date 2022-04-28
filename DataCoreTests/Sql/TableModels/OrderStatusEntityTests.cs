// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using NUnit.Framework;

//namespace DataCoreTests.Sql.TableModels
//{
//    [TestFixture]
//    internal class OrderStatusEntityTests
//    {
//        [Test]
//        public void Entity_Equals_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                OrderStatusEntity entityNew = new();
//                Assert.AreEqual(true, entityNew.EqualsNew());
//                Assert.AreEqual(true, entityNew.EqualsDefault());
//                object entityCopy = entityNew.CloneCast;
//                Assert.AreEqual(true, entityNew.Equals(entityCopy));

//                foreach (int i in TestsEnums.GetInt())
//                    foreach (System.DateTime dt in TestsEnums.GetDateTime())
//                        foreach (string s in TestsEnums.GetString())
//                            foreach (byte bytes in TestsEnums.GetBytes())
//                            {
//                                OrderStatusEntity item = new()
//                                {
//                                    Id = i,
//                                    OrderId = s,
//                                    CurrentDate = dt,
//                                    CurrentStatus = bytes
//                                };
//                                _ = item.ToString();
//                                Assert.AreEqual(false, entityNew.Equals(item));
//                            }
//            });

//            TestsUtils.MethodComplete();
//        }
//    }
//}
