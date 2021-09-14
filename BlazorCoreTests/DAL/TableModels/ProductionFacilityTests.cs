using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace BlazorCoreTests.DAL.TableModels
{
    [TestFixture]
    internal class ProductionFacilityTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var entityNew = new ProductionFacilityEntity();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                var entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (var i in TestsEnums.GetInt())
                foreach (var s in TestsEnums.GetString())
                foreach (var dt in TestsEnums.GetDateTime())
                foreach (var guid in TestsEnums.GetGuid())
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

            TestsUtils.MethodComplete();
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
            DataAccessUtils.DataAccess.ProductionFacilitiesCrud.SaveEntity(entity);
            return DataAccessUtils.DataAccess.ProductionFacilitiesCrud.GetEntity(
                new FieldListEntity(new Dictionary<string, object> { { EnumField.Name.ToString(), name } }),
                new FieldOrderEntity(EnumField.Id, EnumOrderDirection.Desc));
        }

        [Test]
        public void Entity_Crud_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var name = "ProductionFacility Test";
                var entityNew = EntityCreate(name);
                // UpdateEntity
                entityNew.Name = "Modify name";
                DataAccessUtils.DataAccess.ProductionFacilitiesCrud.UpdateEntity(entityNew);
                // GetEntities
                var entities = DataAccessUtils.DataAccess.ProductionFacilitiesCrud.GetEntities(null, null);
                Assert.AreEqual(true, entities.Length > 0);
                foreach (var entity in entities)
                {
                    if (entity.Name.Equals(entityNew.Name) || entity.Name.Equals(name))
                    {
                        // DeleteEntity
                        DataAccessUtils.DataAccess.ProductionFacilitiesCrud.DeleteEntity(entity);
                    }
                }
            }
            );

            TestsUtils.MethodComplete();
        }
    }
}
