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
//                            NomenclatureEntity item = new()
//                            {
//                                Id = i,
//                                CreateDate = dt,
//                                ChangeDt = dt,
//                                Code = s,
//                                Name = s,
//                                SerializedRepresentationObject = s,
//                            };
//                            _ = item.ToString();
//                            Assert.AreEqual(false, entityNew.Equals(item));
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
//                NomenclatureEntity item = new()
//                {
//                    Id = -1,
//                    CreateDate = DateTime.Now,
//                    ChangeDt = DateTime.Now,
//                    Code = default,
//                    Name = "NomenclatureEntity test",
//                    SerializedRepresentationObject = default,
//                };
//                DataAccessUtilsTests.DataAccess.Crud.SaveEntity(item);
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
//                foreach (NomenclatureEntity item in entities)
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
