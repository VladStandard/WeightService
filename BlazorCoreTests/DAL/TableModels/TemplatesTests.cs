using BlazorCore.DAL.TableModels;
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
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
            {
                var entityNew = new TemplatesEntity();
                Assert.AreEqual(true, entityNew.EqualsNew());
                Assert.AreEqual(true, entityNew.EqualsDefault());
                var entityCopy = entityNew.Clone();
                Assert.AreEqual(true, entityNew.Equals(entityCopy));

                foreach (var i in EnumValues.GetInt())
                foreach (var dt in EnumValues.GetDateTime())
                foreach (var guid in EnumValues.GetGuid())
                foreach (var s in EnumValues.GetString())
                foreach (var bytes in EnumValues.GetBytes())
                {
                    var entity = new TemplatesEntity
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

            Utils.MethodComplete();
        }

        [Test]
        public void Entity_Crud_DoesNotThrow()
        {
            Utils.MethodStart();

            Assert.DoesNotThrow(() =>
                {
                    var title = "TemplatesEntity test";
                    var titleChange = "TemplatesEntity test change";
                    // SaveEntity
                    var entity = new TemplatesEntity
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

            Utils.MethodComplete();
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
