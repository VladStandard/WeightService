// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//namespace DataCoreTests.DAL.TableModels
//{
//    [TestFixture]
//    internal class WorkshopTests
//    {
//        [Test]
//        public void Entity_Equals_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                WorkshopEntity entityNew = new();
//                Assert.AreEqual(true, entityNew.EqualsNew());
//                Assert.AreEqual(true, entityNew.EqualsDefault());
//                object entityCopy = entityNew.Clone();
//                Assert.AreEqual(true, entityNew.Equals(entityCopy));

//                foreach (int i in TestsEnums.GetInt())
//                foreach (string s in TestsEnums.GetString())
//                foreach (DateTime dt in TestsEnums.GetDateTime())
//                foreach (Guid guid in TestsEnums.GetGuid())
//                {
//                                WorkshopEntity entity = new()
//                                {
//                        Id = i,
//                        ProductionFacility = new ProductionFacilityEntity(),
//                        Name = s,
//                        CreateDate = dt,
//                        ModifiedDate = dt,
//                        IdRRef = guid
//                    };
//                    _ = entity.ToString();
//                    Assert.AreEqual(false, entityNew.Equals(entity));
//                }
//            });

//            TestsUtils.MethodComplete();
//        }

//        [Test]
//        public void Entity_Crud_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                int iStart = -10;
//                int iEnd = 0;
//                string nameFafility = "ProductionFacility Name 1";
//                string nameWorkshop1 = "WorkShop Name 1";
//                string nameWorkshop2 = "WorkShop Name 2";
//                // SaveEntity
//                ProductionFacilityEntity facility = new()
//                {
//                    Name = nameFafility,
//                    CreateDate = DateTime.Now,
//                    ModifiedDate = DateTime.Now,
//                    IdRRef = Guid.Empty,
//                };
//                DataAccessUtilsTests.DataAccess.Crud.SaveEntity(facility);
//                facility = DataAccessUtilsTests.DataAccess.Crud.GetEntities<ProductionFacilityEntity>(null, null).ToList().FirstOrDefault();
//                for (int i = iStart; i < iEnd; i++)
//                {
//                    WorkshopEntity entity = new()
//                    {
//                        ProductionFacility = facility,
//                        Name = nameWorkshop1,
//                        CreateDate = DateTime.Now,
//                        ModifiedDate = DateTime.Now,
//                        IdRRef = Guid.Empty,
//                    };
//                    DataAccessUtilsTests.DataAccess.WorkshopsCrud.SaveEntity(entity);
//                }
//                // GetEntities
//                foreach (WorkshopEntity entity in DataAccessUtilsTests.DataAccess.WorkshopsCrud.GetEntities<WorkshopEntity>(null, null))
//                {
//                    if (entity.ProductionFacility.Name.Equals(nameFafility))
//                    {
//                        // UpdateEntity
//                        entity.Name = nameWorkshop2;
//                        DataAccessUtilsTests.DataAccess.WorkshopsCrud.UpdateEntity(entity);
//                    }
//                }
//                // GetEntities
//                foreach (WorkshopEntity entity in DataAccessUtilsTests.DataAccess.WorkshopsCrud.GetEntities<WorkshopEntity>(null, null))
//                {
//                    if (entity.Name.Equals(nameWorkshop1) || entity.Name.Equals(nameWorkshop2))
//                    {
//                        // DeleteEntity
//                        DataAccessUtilsTests.DataAccess.WorkshopsCrud.DeleteEntity(entity);
//                    }
//                }
//                // DeleteEntity
//                foreach (ProductionFacilityEntity facilityEntity in DataAccessUtilsTests.DataAccess.Crud.GetEntities<ProductionFacilityEntity>(null, null))
//                {
//                    if (facilityEntity.Name.Equals(nameFafility))
//                        DataAccessUtilsTests.DataAccess.Crud.DeleteEntity(facilityEntity);
//                }
//            }
//            );

//            TestsUtils.MethodComplete();
//        }
//    }
//}
