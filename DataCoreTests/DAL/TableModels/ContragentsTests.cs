// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using DataCore;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;

//namespace DataCoreTests.DAL.TableModels
//{
//    [TestFixture]
//    internal class ContragentsTests
//    {
//        [Test]
//        public void Entity_Equals_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                ContragentEntityV2 entityNew = new();
//                Assert.AreEqual(true, entityNew.EqualsNew());
//                Assert.AreEqual(true, entityNew.EqualsDefault());
//                object entityCopy = entityNew.Clone();
//                Assert.AreEqual(true, entityNew.Equals(entityCopy));

//                foreach (int i in TestsEnums.GetInt())
//                    foreach (DateTime dt in TestsEnums.GetDateTime())
//                        foreach (bool b in TestsEnums.GetBool())
//                            foreach (string s in TestsEnums.GetString())
//                            {
//                                ContragentEntityV2 entity = new()
//                                {
//                                    Id = i,
//                                    CreateDate = dt,
//                                    ModifiedDate = dt,
//                                    Name = s,
//                                    Marked = b,
//                                    SerializedRepresentationObject = s,
//                                };
//                                _ = entity.ToString();
//                                Assert.AreEqual(false, entityNew.Equals(entity));
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
//                ContragentEntityV2 entity = new()
//                {
//                    Id = -1,
//                    CreateDate = DateTime.Now,
//                    ModifiedDate = DateTime.Now,
//                    Name = "ContragentsEntity test",
//                    Marked = default,
//                    SerializedRepresentationObject = null,
//                };
//                DataAccessUtilsTests.DataAccess.Crud.SaveEntity(entity);
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
//                ContragentEntityV2 entityExists = DataAccessUtilsTests.DataAccess.Crud.GetEntity<ContragentEntityV2>(new FieldListEntity(
//                    new Dictionary<string, object> { { ShareEnums.DbField.Name.ToString(), name } }), null);
//                if (entityExists.EqualsDefault())
//                    return;
//                // UpdateEntity
//                entityExists.Marked = true;
//                DataAccessUtilsTests.DataAccess.Crud.UpdateEntity(entityExists);
//            });

//            Assert.DoesNotThrow(() =>
//            {
//                string name = "ContragentsEntity test";
//                // GetEntities
//                ContragentEntityV2[] entities = DataAccessUtilsTests.DataAccess.Crud.GetEntities<ContragentEntityV2>(null, null);
//                Assert.AreEqual(true, entities.Length > 0);
//                foreach (ContragentEntityV2 entity in entities)
//                {
//                    if (entity.Name.Equals(name))
//                    {
//                        _ = entity.ToString();
//                        // DeleteEntity
//                        //DataAccessUtils.DataAccess.ContragentsCrud.DeleteEntity(entity);
//                    }
//                }
//            });

//            TestsUtils.MethodComplete();
//        }
//    }
//}
