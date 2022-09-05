// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using DataCore;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;

//namespace DataCoreTests.Sql.TableModels
//{
//    [TestFixture]
//    internal class ContragentsTests
//    {
//        [Test]
//        public void Model_Equals_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                ContragentEntityV2 entityNew = new();
//                Assert.AreEqual(true, entityNew.EqualsNew());
//                Assert.AreEqual(true, entityNew.EqualsDefault());
//                object entityCopy = entityNew.CloneCast();
//                Assert.AreEqual(true, entityNew.Equals(entityCopy));

//                foreach (int i in TestsEnums.GetInt())
//                    foreach (DateTime dt in TestsEnums.GetDateTime())
//                        foreach (bool b in TestsEnums.GetBool())
//                            foreach (string s in TestsEnums.GetString())
//                            {
//                                ContragentEntityV2 item = new()
//                                {
//                                    Id = i,
//                                    CreateDate = dt,
//                                    ChangeDt = dt,
//                                    Name = s,
//                                    IsMarked = b,
//                                    SerializedRepresentationObject = s,
//                                };
//                                _ = item.ToString();
//                                Assert.AreEqual(false, entityNew.Equals(item));
//                            }
//            });

//            TestsUtils.MethodComplete();
//        }

//        [Test]
//        public void Entity_Crud_Throw()
//        {
//            TestsUtils.MethodStart();

//            Assert.Throws<Exception>(() =>
//            {
//                ContragentEntityV2 item = new()
//                {
//                    Id = -1,
//                    CreateDate = DateTime.Now,
//                    ChangeDt = DateTime.Now,
//                    Name = "ContragentsEntity test",
//                    IsMarked = default,
//                    SerializedRepresentationObject = null,
//                };
//                DataAccessUtilsTests.DataAccess.SaveEntity(item);
//            });

//            TestsUtils.MethodComplete();
//        }

//        [Test]
//        public void Entity_Crud_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                string name = "ContragentsEntity test";
//                ContragentEntityV2 entityExists = DataAccessUtilsTests.DataAccess.GetItem<ContragentEntityV2>(new (
//                    new Dictionary<string, object> { { ShareEnums.DbField.Name.ToString(), name } }), null);
//                if (entityExists.EqualsDefault())
//                    return;
//                // UpdateEntity
//                entityExists.IsMarked = true;
//                DataAccessUtilsTests.DataAccess.UpdateEntity(entityExists);
//            });

//            Assert.DoesNotThrow(() =>
//            {
//                string name = "ContragentsEntity test";
//                // GetEntities
//                ContragentEntityV2[] entities = DataAccessUtilsTests.DataAccess.GetEntities<ContragentEntityV2>(null, null);
//                Assert.AreEqual(true, entities.Length > 0);
//                foreach (ContragentEntityV2 item in entities)
//                {
//                    if (item.Name.Equals(name))
//                    {
//                        _ = item.ToString();
//                        // DeleteEntity
//                        //DataAccessUtils.DataAccess.ContragentsCrud.DeleteEntity(item);
//                    }
//                }
//            });

//            TestsUtils.MethodComplete();
//        }
//    }
//}
