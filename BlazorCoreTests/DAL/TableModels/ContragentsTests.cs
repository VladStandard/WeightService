using System;
using System.Collections.Generic;
using BlazorCore.DAL;
using BlazorCore.DAL.DataModels;
using BlazorCore.DAL.TableModels;
using NUnit.Framework;

namespace BlazorCoreTests.DAL.TableModels
{
    [TestFixture]
    internal class ContragentsTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var entityNew = new ContragentEntity();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                var entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (var i in EnumValues.GetInt())
                foreach (var dt in EnumValues.GetDateTime())
                foreach (var b in EnumValues.GetBool())
                foreach (var s in EnumValues.GetString())
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

            Utils.MethodComplete();
        }

        [Test]
        public void Entity_Crud_Throw()
        {
            Utils.MethodStart();

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

            Utils.MethodComplete();
        }

        [Test]
        public void Entity_Crud_DoesNotThrow()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var name = "ContragentsEntity test";
                var entityExists = DataAccessUtils.DataAccess.ContragentsCrud.GetEntity(new FieldListEntity(
                    new Dictionary<string, object> { { EnumField.Name.ToString(), name } }), null);
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

            Utils.MethodComplete();
        }
    }
}
