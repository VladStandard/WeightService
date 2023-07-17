// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using CoreTests;
//using DataProjectsCore.DAL.TableScaleModels;
//using FluentNHibernate.Mapping;
//using NUnit.Framework;
//using System;

//namespace DataCoreTests.Sql.TableModels
//{
//    [TestFixture]
//    public sealed class TemplatesTests
//    {
//        [Test]
//        public void Model_Equals_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                TemplateEntity entityNew = new();
//                Assert.AreEqual(true, entityNew.EqualsNew());
//                Assert.AreEqual(true, entityNew.EqualsDefault());
//                object entityCopy = new(entityNew);
//                Assert.AreEqual(true, entityNew.Equals(entityCopy));

//                foreach (int i in TestsEnums.GetInt())
//                foreach (DateTime dt in TestsEnums.GetDateTime())
//                foreach (Guid guid in TestsEnums.GetGuid())
//                foreach (string s in TestsEnums.GetString())
//                {
//                    TemplateEntity item = new()
//                    {
//                        Id = i,
//                        CreateDate = dt,
//                        ChangeDt = dt,
//                        IdRRef = guid,
//                        CategoryId = s,
//                        Title = s,
//                        ImageData = TestsEnums.GetBytes().ToArray(),
//                    };
//                    _ = item.ToString();
//                    Assert.AreEqual(false, entityNew.Equals(item));
//                }
//            });

//            TestsUtils.MethodComplete();
//        }

//        [Test]
//        public void Entity_Crud_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//                {
//                    string title = "TemplatesEntity test";
//                    string titleChange = "TemplatesEntity test change";
//                    // SaveEntity
//                    TemplateEntity item = new()
//                    {
//                        CreateDate = DateTime.Now,
//                        ChangeDt = DateTime.Now,
//                        IdRRef = Guid.Empty,
//                        CategoryId = string.Empty,
//                        Title = title,
//                        ImageData = null,
//                    };
//                    DataAccessUtilsTests.DataAccess.TemplatesCrud.SaveEntity(item);
//                    // UpdateEntity
//                    item.Title = titleChange;
//                    DataAccessUtilsTests.DataAccess.TemplatesCrud.UpdateEntity(item);
//                    // GetEntities
//                    foreach (TemplateEntity entityGet in DataAccessUtilsTests.DataAccess.TemplatesCrud.GetEntities<TemplateEntity>(null, null))
//                    {
//                        if (entityGet.Title.Equals(titleChange))
//                        {
//                            DataAccessUtilsTests.DataAccess.TemplatesCrud.DeleteEntity(entityGet);
//                        }
//                    }
//                }
//            );

//            TestsUtils.MethodComplete();
//        }
//    }

//    public class GetTemplateResourcesMap : ClassMap<GetTemplateResourcesEntity>
//    {
//        public GetTemplateResourcesMap()
//        {
//            //LazyLoad();
//            Map(item => item.Name).CustomSqlType("NVARCHAR(MAX)").Column("Name").Not.Nullable();
//            Map(item => item.ImageData).CustomSqlType("NVARCHAR(MAX)").Column("ImageData").Not.Nullable();
//        }
//    }
//}

namespace WsStorageCoreTests.Tables.TableModels;