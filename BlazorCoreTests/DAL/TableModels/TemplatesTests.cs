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
                var entityNew = new TemplateEntity();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                var entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (var i in TestsEnums.GetInt())
                foreach (var dt in TestsEnums.GetDateTime())
                foreach (var guid in TestsEnums.GetGuid())
                foreach (var s in TestsEnums.GetString())
                foreach (var bytes in TestsEnums.GetBytes())
                {
                    var entity = new TemplateEntity
                    {
                        Id = i,
                        CreateDate = dt,
                        ModifiedDate = dt,
                        IdRRef = guid,
                        CategoryId = s,
                        Title = s,
                        ImageData = bytes,
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
                    var title = "TemplatesEntity test";
                    var titleChange = "TemplatesEntity test change";
                    // SaveEntity
                    var entity = new TemplateEntity
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
                    foreach (var entityGet in DataAccessUtils.DataAccess.TemplatesCrud.GetEntities(null, null))
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
