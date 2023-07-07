// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//namespace DataCoreTests.Sql.TableModels
//{
//    [TestFixture]
//    public sealed class WorkshopTests
//    {
//        [Test]
//        public void Model_Equals_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                WorkshopEntity entityNew = new();
//                Assert.AreEqual(true, entityNew.EqualsNew());
//                Assert.AreEqual(true, entityNew.EqualsDefault());
//                object entityCopy = new(entityNew);
//                Assert.AreEqual(true, entityNew.Equals(entityCopy));

//                foreach (int i in TestsEnums.GetInt())
//                foreach (string s in TestsEnums.GetString())
//                foreach (DateTime dt in TestsEnums.GetDateTime())
//                foreach (Guid guid in TestsEnums.GetGuid())
//                {
//                                WorkshopEntity item = new()
//                                {
//                        Id = i,
//                        ProductionFacility = new ProductionFacilityEntity(),
//                        Name = s,
//                        CreateDate = dt,
//                        ChangeDt = dt,
//                        IdRRef = guid
//                    };
//                    _ = item.ToString();
//                    Assert.AreEqual(false, entityNew.Equals(item));
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
//                    ChangeDt = DateTime.Now,
//                    IdRRef = Guid.Empty,
//                };
//                DataAccessUtilsTests.DataAccess.SaveEntity(facility);
//                facility = DataAccessUtilsTests.DataAccess.GetEntities<ProductionFacilityEntity>(null, null).ToList().FirstOrDefault();
//                for (int i = iStart; i < iEnd; i++)
//                {
//                    WorkshopEntity item = new()
//                    {
//                        ProductionFacility = facility,
//                        Name = nameWorkshop1,
//                        CreateDate = DateTime.Now,
//                        ChangeDt = DateTime.Now,
//                        IdRRef = Guid.Empty,
//                    };
//                    DataAccessUtilsTests.DataAccess.WorkshopsCrud.SaveEntity(item);
//                }
//                // GetEntities
//                foreach (WorkshopEntity item in DataAccessUtilsTests.DataAccess.WorkshopsCrud.GetEntities<WorkshopEntity>(null, null))
//                {
//                    if (item.ProductionFacility.Name.Equals(nameFafility))
//                    {
//                        // UpdateEntity
//                        item.Name = nameWorkshop2;
//                        DataAccessUtilsTests.DataAccess.WorkshopsCrud.UpdateEntity(item);
//                    }
//                }
//                // GetEntities
//                foreach (WorkshopEntity item in DataAccessUtilsTests.DataAccess.WorkshopsCrud.GetEntities<WorkshopEntity>(null, null))
//                {
//                    if (item.Name.Equals(nameWorkshop1) || item.Name.Equals(nameWorkshop2))
//                    {
//                        // DeleteEntity
//                        DataAccessUtilsTests.DataAccess.WorkshopsCrud.DeleteEntity(item);
//                    }
//                }
//                // DeleteEntity
//                foreach (ProductionFacilityEntity facilityEntity in DataAccessUtilsTests.DataAccess.GetEntities<ProductionFacilityEntity>(null, null))
//                {
//                    if (facilityEntity.Name.Equals(nameFafility))
//                        DataAccessUtilsTests.DataAccess.DeleteEntity(facilityEntity);
//                }
//            }
//            );

//            TestsUtils.MethodComplete();
//        }
//    }
//}
