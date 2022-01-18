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
    internal class NomenclatureTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                NomenclatureEntity entityNew = new();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                object entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (int i in TestsEnums.GetInt())
                foreach (DateTime dt in TestsEnums.GetDateTime())
                foreach (string s in TestsEnums.GetString())
                {
                            NomenclatureEntity entity = new()
                            {
                        Id = i,
                        CreateDate = dt,
                        ModifiedDate = dt,
                        Code = s,
                        Name = s,
                        SerializedRepresentationObject = s,
                    };
                    _ = entity.ToString();
                    Assert.AreEqual(false, entityNew.Equals(entity));
                }
            });

            TestsUtils.MethodComplete();
        }

        [Test]
        public void Entity_Crud_Throw()
        {
            TestsUtils.MethodStart();

            Assert.Throws<Exception>(() =>
            {
                NomenclatureEntity entity = new()
                {
                    Id = -1,
                    CreateDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Code = default,
                    Name = "NomenclatureEntity test",
                    SerializedRepresentationObject = default,
                };
                DataAccessUtils.DataAccess.Crud.SaveEntity(entity);
            });

            TestsUtils.MethodComplete();
        }
        
        [Test]
        public void Entity_Crud_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                string name = "NomenclatureEntity test";
                NomenclatureEntity entityExists = DataAccessUtils.DataAccess.Crud.GetEntity<NomenclatureEntity>(new FieldListEntity(
                    new Dictionary<string, object> { { ShareEnums.DbField.Name.ToString(), name } }), null);
                if (entityExists.EqualsDefault())
                    return;
                // UpdateEntity
                entityExists.Code = "code test";
                DataAccessUtils.DataAccess.Crud.UpdateEntity(entityExists);
            });

            Assert.DoesNotThrow(() =>
            {
                string name = "NomenclatureEntity test";
                // GetEntities
                NomenclatureEntity[] entities = DataAccessUtils.DataAccess.Crud.GetEntities<NomenclatureEntity>(null, null);
                Assert.AreEqual(true, entities.Length > 0);
                foreach (NomenclatureEntity entity in entities)
                {
                    if (entity.Name.Equals(name))
                    {
                        _ = entity.ToString();
                        // DeleteEntity
                        //DataAccessUtils.DataAccess.ContragentsCrud.DeleteEntity(entity);
                    }
                }
            });

            TestsUtils.MethodComplete();
        }
    }
}
