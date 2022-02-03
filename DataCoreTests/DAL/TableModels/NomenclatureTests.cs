// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using DataCore;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;

//namespace DataCoreTests.DAL.TableModels
//{
//    [TestFixture]
//    internal class NomenclatureTests
//    {
//        [Test]
//        public void Entity_Equals_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                NomenclatureEntity entityNew = new();
//                Assert.AreEqual(true, entityNew.EqualsNew());
//                Assert.AreEqual(true, entityNew.EqualsDefault());
//                object entityCopy = entityNew.Clone();
//                Assert.AreEqual(true, entityNew.Equals(entityCopy));

//                foreach (int i in TestsEnums.GetInt())
//                    foreach (DateTime dt in TestsEnums.GetDateTime())
//                        foreach (string s in TestsEnums.GetString())
//                        {
//                            NomenclatureEntity entity = new()
//                            {
//                                Id = i,
//                                CreateDate = dt,
//                                ModifiedDate = dt,
//                                Code = s,
//                                Name = s,
//                                SerializedRepresentationObject = s,
//                            };
//                            _ = entity.ToString();
//                            Assert.AreEqual(false, entityNew.Equals(entity));
//                        }
//            });

//            TestsUtils.MethodComplete();
//        }

//        [Test]
//        public void Entity_Crud_Throw()
//        {
//            TestsUtils.MethodStart();

//            Assert.Throws<Exception>(() =>
//            {
//                NomenclatureEntity entity = new()
//                {
//                    Id = -1,
//                    CreateDate = DateTime.Now,
//                    ModifiedDate = DateTime.Now,
//                    Code = default,
//                    Name = "NomenclatureEntity test",
//                    SerializedRepresentationObject = default,
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
//                string name = "NomenclatureEntity test";
//                NomenclatureEntity entityExists = DataAccessUtilsTests.DataAccess.Crud.GetEntity<NomenclatureEntity>(new FieldListEntity(
//                    new Dictionary<string, object> { { ShareEnums.DbField.Name.ToString(), name } }), null);
//                if (entityExists.EqualsDefault())
//                    return;
//                // UpdateEntity
//                entityExists.Code = "code test";
//                DataAccessUtilsTests.DataAccess.Crud.UpdateEntity(entityExists);
//            });

//            Assert.DoesNotThrow(() =>
//            {
//                string name = "NomenclatureEntity test";
//                // GetEntities
//                NomenclatureEntity[] entities = DataAccessUtilsTests.DataAccess.Crud.GetEntities<NomenclatureEntity>(null, null);
//                Assert.AreEqual(true, entities.Length > 0);
//                foreach (NomenclatureEntity entity in entities)
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
