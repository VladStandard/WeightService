// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using WsDataCore;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;

//namespace DataCoreTests.Sql.TableModels
//{
//    [TestFixture]
//    public sealed class ProductionFacilityTests
//    {
//        [Test]
//        public void Model_Equals_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                ProductionFacilityEntity entityNew = new();
//                Assert.AreEqual(true, entityNew.EqualsNew());
//                Assert.AreEqual(true, entityNew.EqualsDefault());
//                object entityCopy = new(entityNew);
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
//            DataAccessUtilsTests.DataAccess.SaveEntity(item);
//            return DataAccessUtilsTests.DataAccess.GetItem<ProductionFacilityEntity>(
//                new (new Dictionary<DbField, object?> { { DbField.Name, name } }),
//                new FieldOrderModel(DbField.Id, DbOrderDirection.Desc));
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
//                DataAccessUtilsTests.DataAccess.UpdateEntity(entityNew);
//                // GetEntities
//                ProductionFacilityEntity[] entities = DataAccessUtilsTests.DataAccess.GetEntities<ProductionFacilityEntity>(null, null);
//                Assert.AreEqual(true, entities.Length > 0);
//                foreach (ProductionFacilityEntity item in entities)
//                {
//                    if (item.Name.Equals(entityNew.Name) || item.Name.Equals(name))
//                    {
//                        // DeleteEntity
//                        DataAccessUtilsTests.DataAccess.DeleteEntity(item);
//                    }
//                }
//            }
//            );

//            TestsUtils.MethodComplete();
//        }
//    }
//}
