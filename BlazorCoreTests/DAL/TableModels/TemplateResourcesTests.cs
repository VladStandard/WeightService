using System;
using System.Text;
using BlazorCore.DAL;
using BlazorCore.DAL.DataModels;
using BlazorCore.DAL.TableModels;
using NUnit.Framework;

namespace BlazorCoreTests.DAL.TableModels
{
    [TestFixture]
    internal class TemplateResourcesTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var entityNew = new TemplateResourceEntity();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                var entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (var i in EnumValues.GetInt())
                foreach (var dt in EnumValues.GetDateTime())
                foreach (var guid in EnumValues.GetGuid())
                foreach (var s in EnumValues.GetString())
                foreach (var bytes in EnumValues.GetBytes())
                foreach (var b in EnumValues.GetBool())
                {
                    var entity = new TemplateResourceEntity
                    {
                        Id = i,
                        CreateDate = dt,
                        ModifiedDate = dt,
                        Name = s,
                        Description = s,
                        Type = s,
                        ImageData = bytes,
                        IdRRef = guid,
                        Marked = b,
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
                    var id = DataAccessUtils.DataAccess.TemplateResourcesCrud.GetEntity(
                        null,
                        new FieldOrderEntity{Use = true, Name = EnumField.Id, Direction = EnumOrderDirection.Desc}).Id;
                    var entity = new TemplateResourceEntity
                    {
                        Id = id + 1,
                        Name = "TemplateResourcesEntity name",
                        Description = "TemplateResourcesEntity description",
                        CreateDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                    };
                    DataAccessUtils.DataAccess.TemplateResourcesCrud.SaveEntity(entity);

                    entity.Description = "Updated";
                    DataAccessUtils.DataAccess.TemplateResourcesCrud.UpdateEntity(entity);

                    var result = DataAccessUtils.DataAccess.TemplateResourcesCrud.LoadResource(
                        entity.Id, entity.Name, "New description", "GRF", 
                        Encoding.ASCII.GetBytes("new data"));
                    Assert.AreEqual(1, result);

                    // GetEntities.
                    foreach (var entityGet in DataAccessUtils.DataAccess.TemplateResourcesCrud.GetEntities(null, null))
                    {
                        if (Equals(entityGet.Name, entity.Name))
                        {
                            // DeleteEntity.
                            //DataAccessUtils.DataAccess.TemplateResourcesCrud.DeleteEntity(entityGet);
                        }
                    }
                }
            );

            Utils.MethodComplete();
        }
    }
}
