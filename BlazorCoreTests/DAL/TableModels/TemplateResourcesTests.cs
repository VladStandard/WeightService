using System;
using CoreTests;
using DataProjectsCore.DAL.Models;
using DataProjectsCore.DAL.TableScaleModels;
using DataShareCore;
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
                TemplateResourceEntity entityNew = new();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                object entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (int i in TestsEnums.GetInt())
                foreach (DateTime dt in TestsEnums.GetDateTime())
                foreach (Guid guid in TestsEnums.GetGuid())
                foreach (string s in TestsEnums.GetString())
                foreach (bool b in TestsEnums.GetBool())
                {
                    TemplateResourceEntity entity = new()
                    {
                        Id = i,
                        CreateDate = dt,
                        ModifiedDate = dt,
                        Name = s,
                        Description = s,
                        Type = s,
                        ImageData = TestsEnums.GetBytes().ToArray(),
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
                    int id = DataAccessUtils.DataAccess.Crud.GetEntity<TemplateResourceEntity>(null,
                        new FieldOrderEntity{Use = true, Name = ShareEnums.DbField.Id, Direction = ShareEnums.DbOrderDirection.Desc}).Id;
                    TemplateResourceEntity entity = new()
                    {
                        Id = id + 1,
                        Name = "TemplateResourcesEntity name",
                        Description = "TemplateResourcesEntity description",
                        CreateDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                    };
                    DataAccessUtils.DataAccess.Crud.SaveEntity(entity);

                    entity.Description = "Updated";
                    DataAccessUtils.DataAccess.Crud.UpdateEntity(entity);

                    //var result = DataAccessUtils.DataAccess.TemplateResourceCrud.LoadResource(
                    //    entity.Id, entity.Name, "New description", "GRF", 
                    //    Encoding.ASCII.GetBytes("new data"));
                    //Assert.AreEqual(1, result);

                    // GetEntities.
                    foreach (TemplateResourceEntity entityGet in DataAccessUtils.DataAccess.Crud.GetEntities<TemplateResourceEntity>(null, null))
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
