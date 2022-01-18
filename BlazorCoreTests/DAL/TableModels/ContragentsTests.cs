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
    internal class ContragentsTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                ContragentEntity entityNew = new();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                object entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (int i in TestsEnums.GetInt())
                foreach (DateTime dt in TestsEnums.GetDateTime())
                foreach (bool b in TestsEnums.GetBool())
                foreach (string s in TestsEnums.GetString())
                {
                                ContragentEntity entity = new()
                                {
                        Id = i,
                        CreateDate = dt,
                        ModifiedDate = dt,
                        Name = s,
                        Marked = b,
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
                ContragentEntity entity = new()
                {
                    Id = -1,
                    CreateDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Name = "ContragentsEntity test",
                    Marked = default,
                    SerializedRepresentationObject = null,
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
                string name = "ContragentsEntity test";
                ContragentEntity entityExists = DataAccessUtils.DataAccess.Crud.GetEntity<ContragentEntity>(new FieldListEntity(
                    new Dictionary<string, object> { { ShareEnums.DbField.Name.ToString(), name } }), null);
                if (entityExists.EqualsDefault())
                    return;
                // UpdateEntity
                entityExists.Marked = true;
                DataAccessUtils.DataAccess.Crud.UpdateEntity(entityExists);
            });

            Assert.DoesNotThrow(() =>
            {
                string name = "ContragentsEntity test";
                // GetEntities
                ContragentEntity[] entities = DataAccessUtils.DataAccess.Crud.GetEntities<ContragentEntity>(null, null);
                Assert.AreEqual(true, entities.Length > 0);
                foreach (ContragentEntity entity in entities)
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
