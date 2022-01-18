using System;
using System.Collections.Generic;
using CoreTests;
using DataProjectsCore.DAL.Models;
using DataProjectsCore.DAL.TableScaleModels;
using DataShareCore;
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
                ProductionFacilityEntity entityNew = new();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                object entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (int i in TestsEnums.GetInt())
                foreach (string s in TestsEnums.GetString())
                foreach (DateTime dt in TestsEnums.GetDateTime())
                foreach (Guid guid in TestsEnums.GetGuid())
                {
                    ProductionFacilityEntity entity = new()
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
            ProductionFacilityEntity entity = new()
            {
                Id = -1,
                Name = name,
                CreateDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                IdRRef = Guid.Empty
            };
            DataAccessUtils.DataAccess.Crud.SaveEntity(entity);
            return DataAccessUtils.DataAccess.Crud.GetEntity<ProductionFacilityEntity>(
                new FieldListEntity(new Dictionary<string, object> { { ShareEnums.DbField.Name.ToString(), name } }),
                new FieldOrderEntity(ShareEnums.DbField.Id, ShareEnums.DbOrderDirection.Desc));
        }

        [Test]
        public void Entity_Crud_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                string name = "ProductionFacility Test";
                ProductionFacilityEntity entityNew = EntityCreate(name);
                // UpdateEntity
                entityNew.Name = "Modify name";
                DataAccessUtils.DataAccess.Crud.UpdateEntity(entityNew);
                // GetEntities
                ProductionFacilityEntity[] entities = DataAccessUtils.DataAccess.Crud.GetEntities<ProductionFacilityEntity>(null, null);
                Assert.AreEqual(true, entities.Length > 0);
                foreach (ProductionFacilityEntity entity in entities)
                {
                    if (entity.Name.Equals(entityNew.Name) || entity.Name.Equals(name))
                    {
                        // DeleteEntity
                        DataAccessUtils.DataAccess.Crud.DeleteEntity(entity);
                    }
                }
            }
            );

            TestsUtils.MethodComplete();
        }
    }
}
