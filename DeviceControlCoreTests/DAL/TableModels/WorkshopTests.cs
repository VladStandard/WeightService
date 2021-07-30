using System;
using System.Linq;
using DeviceControlCore.DAL.TableModels;
using NUnit.Framework;

namespace DeviceControlCoreTests.DAL.TableModels
{
    [TestFixture]
    internal class WorkshopTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var entityNew = new WorkshopEntity();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                var entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (var i in EnumValues.GetInt())
                foreach (var s in EnumValues.GetString())
                foreach (var dt in EnumValues.GetDateTime())
                foreach (var guid in EnumValues.GetGuid())
                {
                    var entity = new WorkshopEntity
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

            Utils.MethodComplete();
        }

        [Test]
        public void Entity_Crud_DoesNotThrow()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var iStart = -10;
                var iEnd = 0;
                var nameFafility = "ProductionFacility Name 1";
                var nameWorkshop1 = "WorkShop Name 1";
                var nameWorkshop2 = "WorkShop Name 2";
                // SaveEntity
                var facility = new ProductionFacilityEntity()
                {
                    Name = nameFafility,
                    CreateDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IdRRef = Guid.Empty,
                };
                DataAccessUtils.DataAccess.ProductionFacilityCrud.SaveEntity(facility);
                facility = DataAccessUtils.DataAccess.ProductionFacilityCrud.GetEntities(null, null).ToList().FirstOrDefault();
                for (var i = iStart; i < iEnd; i++)
                {
                    var entity = new WorkshopEntity()
                    {
                        ProductionFacility = facility,
                        Name = nameWorkshop1,
                        CreateDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        IdRRef = Guid.Empty,
                    };
                    DataAccessUtils.DataAccess.WorkshopCrud.SaveEntity(entity);
                }
                // GetEntities
                foreach (var entity in DataAccessUtils.DataAccess.WorkshopCrud.GetEntities(null, null))
                {
                    if (entity.ProductionFacility.Name.Equals(nameFafility))
                    {
                        // UpdateEntity
                        entity.Name = nameWorkshop2;
                        DataAccessUtils.DataAccess.WorkshopCrud.UpdateEntity(entity);
                    }
                }
                // GetEntities
                foreach (var entity in DataAccessUtils.DataAccess.WorkshopCrud.GetEntities(null, null))
                {
                    if (entity.Name.Equals(nameWorkshop1) || entity.Name.Equals(nameWorkshop2))
                    {
                        // DeleteEntity
                        DataAccessUtils.DataAccess.WorkshopCrud.DeleteEntity(entity);
                    }
                }
                // DeleteEntity
                foreach (var facilityEntity in DataAccessUtils.DataAccess.ProductionFacilityCrud.GetEntities(null, null))
                {
                    if (facilityEntity.Name.Equals(nameFafility))
                        DataAccessUtils.DataAccess.ProductionFacilityCrud.DeleteEntity(facilityEntity);
                }
            }
            );

            Utils.MethodComplete();
        }
    }
}
