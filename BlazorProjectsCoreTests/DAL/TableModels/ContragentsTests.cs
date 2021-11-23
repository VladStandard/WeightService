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
                var entityNew = new ContragentEntity();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                var entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (var i in TestsEnums.GetInt())
                foreach (var dt in TestsEnums.GetDateTime())
                foreach (var b in TestsEnums.GetBool())
                foreach (var s in TestsEnums.GetString())
                {
                    var entity = new ContragentEntity
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
                var entity = new ContragentEntity
                {
                    Id = -1,
                    CreateDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Name = "ContragentsEntity test",
                    Marked = default,
                    SerializedRepresentationObject = null,
                };
                DataAccessUtils.DataAccess.ContragentsCrud.SaveEntity(entity);
            });

            TestsUtils.MethodComplete();
        }

        [Test]
        public void Entity_Crud_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var name = "ContragentsEntity test";
                var entityExists = DataAccessUtils.DataAccess.ContragentsCrud.GetEntity(new FieldListEntity(
                    new Dictionary<string, object> { { ShareEnums.DbField.Name.ToString(), name } }), null);
                if (entityExists.EqualsDefault())
                    return;
                // UpdateEntity
                entityExists.Marked = true;
                DataAccessUtils.DataAccess.ContragentsCrud.UpdateEntity(entityExists);
            });

            Assert.DoesNotThrow(() =>
            {
                var name = "ContragentsEntity test";
                // GetEntities
                var entities = DataAccessUtils.DataAccess.ContragentsCrud.GetEntities(null, null);
                Assert.AreEqual(true, entities.Length > 0);
                foreach (var entity in entities)
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
