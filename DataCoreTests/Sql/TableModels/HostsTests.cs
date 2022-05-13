// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using DataCore;
//using NUnit.Framework;

//namespace DataCoreTests.Sql.TableModels
//{
//    [TestFixture]
//    internal class HostsTests
//    {
//        [Test]
//        public void Entity_Equals_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                HostEntity entityNew = new();
//                Assert.AreEqual(true, entityNew.EqualsNew());
//                Assert.AreEqual(true, entityNew.EqualsDefault());
//                object entityCopy = entityNew.CloneCast();
//                Assert.AreEqual(true, entityNew.Equals(entityCopy));

//                foreach (int i in TestsEnums.GetInt())
//                    foreach (string s in TestsEnums.GetString())
//                        foreach (System.DateTime dt in TestsEnums.GetDateTime())
//                            foreach (System.Guid guid in TestsEnums.GetGuid())
//                                foreach (bool b in TestsEnums.GetBool())
//                                    foreach (int bytes in TestsEnums.GetBytes())
//                                    {
//                                        HostEntity item = new()
//                                        {
//                                            Id = i,
//                                            CreateDate = dt,
//                                            ChangeDt = dt,
//                                            Name = s,
//                                            Ip = s,
//                                            MacAddress = new DataShareCore.DAL.Models.MacAddressEntity(s),
//                                            IdRRef = guid,
//                                            IsMarked = b,
//                                            SettingsFile = s,
//                                        };
//                                        _ = item.ToString();
//                                        Assert.AreEqual(false, entityNew.Equals(item));
//                                    }
//            });

//            TestsUtils.MethodComplete();
//        }

//        [Test]
//        public void Entity_Crud_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                // GetEntities.
//                foreach (HostEntity item in DataAccessUtilsTests.DataAccess.Crud.GetEntities<HostEntity>(null,
//                    new FieldOrderEntity { Use = true, Name = ShareEnums.DbField.Name, Direction = ShareEnums.DbOrderDirection.Desc }))
//                {
//                    TestContext.WriteLine(item.ToString());
//                    TestContext.WriteLine($"SettingsFile: {item.SettingsFile}");
//                    TestContext.WriteLine();
//                }
//            });

//            TestsUtils.MethodComplete();
//        }

//        [Test]
//        public void Entity_GetFreeHosts_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                // GetEntities.
//                foreach (HostEntity item in DataAccessUtilsTests.DataAccess.Crud.GetFreeHosts(null))
//                {
//                    TestContext.WriteLine(item.ToString());
//                    TestContext.WriteLine($"SettingsFile: {item.SettingsFile}");
//                    TestContext.WriteLine();
//                }
//            });

//            TestsUtils.MethodComplete();
//        }
//    }
//}
