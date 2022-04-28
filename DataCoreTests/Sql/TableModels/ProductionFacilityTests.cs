// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using DataCore;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;

//namespace DataCoreTests.Sql.TableModels
//{
//    [TestFixture]
//    internal class ProductionFacilityTests
//    {
//        [Test]
//        public void Entity_Equals_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                ProductionFacilityEntity entityNew = new();
//                Assert.AreEqual(true, entityNew.EqualsNew());
//                Assert.AreEqual(true, entityNew.EqualsDefault());
//                object entityCopy = entityNew.CloneCast;
//                Assert.AreEqual(true, entityNew.Equals(entityCopy));

//                foreach (int i in TestsEnums.GetInt())
//                    foreach (string s in TestsEnums.GetString())
//                        foreach (DateTime dt in TestsEnums.GetDateTime())
//                            foreach (Guid guid in TestsEnums.GetGuid())
//                            {
//                                ProductionFacilityEntity item = new()
//                                {
//                                    Id = i,
//                                    Name = s,
//                                    CreateDate = dt,
//                                    ChangeDt = dt,
//                                    IdRRef = guid
//                                };
//                                _ = item.ToString();
//                                Assert.AreEqual(false, entityNew.Equals(item));
//                            }
//            });

//            TestsUtils.MethodComplete();
//        }

//        public ProductionFacilityEntity EntityCreate(string name)
//        {
//            ProductionFacilityEntity item = new()
//            {
//                Id = -1,
//                Name = name,
//                CreateDate = DateTime.Now,
//                ChangeDt = DateTime.Now,
//                IdRRef = Guid.Empty
//            };
//            DataAccessUtilsTests.DataAccess.Crud.SaveEntity(item);
//            return DataAccessUtilsTests.DataAccess.Crud.GetEntity<ProductionFacilityEntity>(
//                new FieldListEntity(new Dictionary<DbField, object?> { { ShareEnums.DbField.Name, name } }),
//                new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc));
//        }

//        [Test]
//        public void Entity_Crud_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                string name = "ProductionFacility Test";
//                ProductionFacilityEntity entityNew = EntityCreate(name);
//                // UpdateEntity
//                entityNew.Name = "Modify name";
//                DataAccessUtilsTests.DataAccess.Crud.UpdateEntity(entityNew);
//                // GetEntities
//                ProductionFacilityEntity[] entities = DataAccessUtilsTests.DataAccess.Crud.GetEntities<ProductionFacilityEntity>(null, null);
//                Assert.AreEqual(true, entities.Length > 0);
//                foreach (ProductionFacilityEntity item in entities)
//                {
//                    if (item.Name.Equals(entityNew.Name) || item.Name.Equals(name))
//                    {
//                        // DeleteEntity
//                        DataAccessUtilsTests.DataAccess.Crud.DeleteEntity(item);
//                    }
//                }
//            }
//            );

//            TestsUtils.MethodComplete();
//        }
//    }
//}
