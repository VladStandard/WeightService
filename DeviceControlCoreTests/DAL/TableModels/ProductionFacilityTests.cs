using System;
using System.Collections.Generic;
using DeviceControlCore.DAL;
using DeviceControlCore.DAL.DataModels;
using DeviceControlCore.DAL.TableModels;
using NUnit.Framework;

namespace DeviceControlCoreTests.DAL.TableModels
{
    [TestFixture]
    internal class ProductionFacilityTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var entityNew = new ProductionFacilityEntity();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                var entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (var i in EnumValues.GetInt())
                foreach (var s in EnumValues.GetString())
                foreach (var dt in EnumValues.GetDateTime())
                foreach (var guid in EnumValues.GetGuid())
                {
                    var entity = new ProductionFacilityEntity
                    {
                        Id = i,
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

        public ProductionFacilityEntity EntityCreate(string name)
        {
            var entity = new ProductionFacilityEntity
            {
                Id = -1,
                Name = name,
                CreateDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IdRRef = Guid.Empty
            };
            DataAccessUtils.DataAccess.ProductionFacilityCrud.SaveEntity(entity);
            return DataAccessUtils.DataAccess.ProductionFacilityCrud.GetEntity(
                new FieldListEntity(new Dictionary<string, object> { { EnumField.Name.ToString(), name } }),
                new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc));
        }

        [Test]
        public void Entity_Crud_DoesNotThrow()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var name = "ProductionFacility Test";
                var entityNew = EntityCreate(name);
                // UpdateEntity
                entityNew.Name = "Modify name";
                DataAccessUtils.DataAccess.ProductionFacilityCrud.UpdateEntity(entityNew);
                // GetEntities
                var entities = DataAccessUtils.DataAccess.ProductionFacilityCrud.GetEntities(null, null);
                Assert.AreEqual(true, entities.Length > 0);
                foreach (var entity in entities)
                {
                    if (entity.Name.Equals(entityNew.Name) || entity.Name.Equals(name))
                    {
                        // DeleteEntity
                        DataAccessUtils.DataAccess.ProductionFacilityCrud.DeleteEntity(entity);
                    }
                }
            }
            );

            Utils.MethodComplete();
        }
    }
}
