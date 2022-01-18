using System;
using System.Linq;
using CoreTests;
using DataProjectsCore.DAL.TableScaleModels;
using NUnit.Framework;

namespace BlazorCoreTests.DAL.TableModels
{
    [TestFixture]
    internal class WorkshopTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                WorkshopEntity entityNew = new();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                object entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (int i in TestsEnums.GetInt())
                foreach (string s in TestsEnums.GetString())
                foreach (DateTime dt in TestsEnums.GetDateTime())
                foreach (Guid guid in TestsEnums.GetGuid())
                {
                                WorkshopEntity entity = new()
                                {
                        Id = i,
                        ProductionFacility = new ProductionFacilityEntity(),
                        Name = s,
                        CreateDate = dt,
                        ModifiedDate = dt,
                        IdRRef = guid
                    };
                    _ = entity.ToString();
                    Assert.AreEqual(false, entityNew.Equals(entity));
                }
            });

            TestsUtils.MethodComplete();
        }

        [Test]
        public void Entity_Crud_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                int iStart = -10;
                int iEnd = 0;
                string nameFafility = "ProductionFacility Name 1";
                string nameWorkshop1 = "WorkShop Name 1";
                string nameWorkshop2 = "WorkShop Name 2";
                // SaveEntity
                ProductionFacilityEntity facility = new()
                {
                    Name = nameFafility,
                    CreateDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IdRRef = Guid.Empty,
                };
                DataAccessUtils.DataAccess.Crud.SaveEntity(facility);
                facility = DataAccessUtils.DataAccess.Crud.GetEntities<ProductionFacilityEntity>(null, null).ToList().FirstOrDefault();
                for (int i = iStart; i < iEnd; i++)
                {
                    WorkshopEntity entity = new()
                    {
                        ProductionFacility = facility,
                        Name = nameWorkshop1,
                        CreateDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        IdRRef = Guid.Empty,
                    };
                    DataAccessUtils.DataAccess.WorkshopsCrud.SaveEntity(entity);
                }
                // GetEntities
                foreach (WorkshopEntity entity in DataAccessUtils.DataAccess.WorkshopsCrud.GetEntities<WorkshopEntity>(null, null))
                {
                    if (entity.ProductionFacility.Name.Equals(nameFafility))
                    {
                        // UpdateEntity
                        entity.Name = nameWorkshop2;
                        DataAccessUtils.DataAccess.WorkshopsCrud.UpdateEntity(entity);
                    }
                }
                // GetEntities
                foreach (WorkshopEntity entity in DataAccessUtils.DataAccess.WorkshopsCrud.GetEntities<WorkshopEntity>(null, null))
                {
                    if (entity.Name.Equals(nameWorkshop1) || entity.Name.Equals(nameWorkshop2))
                    {
                        // DeleteEntity
                        DataAccessUtils.DataAccess.WorkshopsCrud.DeleteEntity(entity);
                    }
                }
                // DeleteEntity
                foreach (ProductionFacilityEntity facilityEntity in DataAccessUtils.DataAccess.Crud.GetEntities<ProductionFacilityEntity>(null, null))
                {
                    if (facilityEntity.Name.Equals(nameFafility))
                        DataAccessUtils.DataAccess.Crud.DeleteEntity(facilityEntity);
                }
            }
            );

            TestsUtils.MethodComplete();
        }
    }
}
