using System;
using System.Text;
using NUnit.Framework;

namespace BlazorCoreTests.DAL.TableModels
{
    [TestFixture]
    internal class TemplateResourcesTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var entityNew = new TemplateResourceEntity();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                var entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (var i in TestsEnums.GetInt())
                foreach (var dt in TestsEnums.GetDateTime())
                foreach (var guid in TestsEnums.GetGuid())
                foreach (var s in TestsEnums.GetString())
                foreach (var bytes in TestsEnums.GetBytes())
                foreach (var b in TestsEnums.GetBool())
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

            TestsUtils.MethodComplete();
        }

        [Test]
        public void Entity_Crud_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
                {
                    var id = DataAccessUtils.DataAccess.TemplateResourcesCrud.GetEntity(
                        null,
                        new FieldOrderEntity{Use = true, Name = ShareEnums.DbField.Id, Direction = ShareEnums.DbOrderDirection.Desc}).Id;
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

            TestsUtils.MethodComplete();
        }
    }
}
