using CoreTests;
using DataProjectsCore.DAL.TableScaleModels;
using FluentNHibernate.Mapping;
using NUnit.Framework;
using System;

namespace BlazorCoreTests.DAL.TableModels
{
    [TestFixture]
    internal class TemplatesTests
    {
        [Test]
        public void Entity_Equals_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                TemplateEntity entityNew = new();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                object entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (int i in TestsEnums.GetInt())
                foreach (DateTime dt in TestsEnums.GetDateTime())
                foreach (Guid guid in TestsEnums.GetGuid())
                foreach (string s in TestsEnums.GetString())
                {
                    TemplateEntity entity = new()
                    {
                        Id = i,
                        CreateDate = dt,
                        ModifiedDate = dt,
                        IdRRef = guid,
                        CategoryId = s,
                        Title = s,
                        ImageData = TestsEnums.GetBytes().ToArray(),
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
                    string title = "TemplatesEntity test";
                    string titleChange = "TemplatesEntity test change";
                    // SaveEntity
                    TemplateEntity entity = new()
                    {
                        CreateDate = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        IdRRef = Guid.Empty,
                        CategoryId = string.Empty,
                        Title = title,
                        ImageData = null,
                    };
                    DataAccessUtils.DataAccess.TemplatesCrud.SaveEntity(entity);
                    // UpdateEntity
                    entity.Title = titleChange;
                    DataAccessUtils.DataAccess.TemplatesCrud.UpdateEntity(entity);
                    // GetEntities
                    foreach (TemplateEntity entityGet in DataAccessUtils.DataAccess.TemplatesCrud.GetEntities<TemplateEntity>(null, null))
                    {
                        if (entityGet.Title.Equals(titleChange))
                        {
                            DataAccessUtils.DataAccess.TemplatesCrud.DeleteEntity(entityGet);
                        }
                    }
                }
            );

            TestsUtils.MethodComplete();
        }
    }

    public class GetTemplateResourcesEntity
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string ImageData { get; set; }

        //public override string ToString()
        //{
        //    return $"{nameof(Id)}: {Id}. {nameof(Name)}: {Name}. {nameof(ImageData)}: {ImageData}.";
        //}
    }

    public class GetTemplateResourcesMap : ClassMap<GetTemplateResourcesEntity>
    {
        public GetTemplateResourcesMap()
        {
            //LazyLoad();
            Map(x => x.Name).CustomSqlType("NVARCHAR(MAX)").Column("Name").Not.Nullable();
            Map(x => x.ImageData).CustomSqlType("NVARCHAR(MAX)").Column("ImageData").Not.Nullable();
        }
    }
}
