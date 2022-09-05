// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

//using DataCore;
//using NUnit.Framework;
//using System;

//namespace DataCoreTests.Sql.TableModels
//{
//    [TestFixture]
//    internal class TemplateResourcesTests
//    {
//        [Test]
//        public void Model_Equals_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//            {
//                TemplateResourceEntity entityNew = new();
//                Assert.AreEqual(true, entityNew.EqualsNew());
//                Assert.AreEqual(true, entityNew.EqualsDefault());
//                object entityCopy = entityNew.CloneCast();
//                Assert.AreEqual(true, entityNew.Equals(entityCopy));

//                foreach (int i in TestsEnums.GetInt())
//                    foreach (DateTime dt in TestsEnums.GetDateTime())
//                        foreach (Guid guid in TestsEnums.GetGuid())
//                            foreach (string s in TestsEnums.GetString())
//                                foreach (bool b in TestsEnums.GetBool())
//                                {
//                                    TemplateResourceEntity item = new()
//                                    {
//                                        Id = i,
//                                        CreateDate = dt,
//                                        ChangeDt = dt,
//                                        Name = s,
//                                        Description = s,
//                                        Type = s,
//                                        ImageData = TestsEnums.GetBytes().ToArray(),
//                                        IdRRef = guid,
//                                        IsMarked = b,
//                                    };
//                                    _ = item.ToString();
//                                    Assert.AreEqual(false, entityNew.Equals(item));
//                                }
//            });

//            TestsUtils.MethodComplete();
//        }

//        [Test]
//        public void Entity_Crud_DoesNotThrow()
//        {
//            TestsUtils.MethodStart();

//            Assert.DoesNotThrow(() =>
//                {
//                    int id = DataAccessUtilsTests.DataAccess.GetItem<TemplateResourceEntity>(null,
//                        new FieldOrderModel { Use = true, Name = ShareEnums.DbField.Id, Direction = ShareEnums.DbOrderDirection.Desc }).Id;
//                    TemplateResourceEntity item = new()
//                    {
//                        Id = id + 1,
//                        Name = "TemplateResourcesEntity name",
//                        Description = "TemplateResourcesEntity description",
//                        CreateDate = DateTime.Now,
//                        ChangeDt = DateTime.Now,
//                    };
//                    DataAccessUtilsTests.DataAccess.SaveEntity(item);

//                    item.Description = "Updated";
//                    DataAccessUtilsTests.DataAccess.UpdateEntity(item);

//                    //var result = DataAccessUtils.DataAccess.TemplateResourceCrud.LoadResource(
//                    //    item.Id, item.Name, "New description", "GRF", 
//                    //    Encoding.ASCII.GetBytes("new data"));
//                    //Assert.AreEqual(1, result);

//                    // GetEntities.
//                    foreach (TemplateResourceEntity entityGet in DataAccessUtilsTests.DataAccess.GetEntities<TemplateResourceEntity>(null, null))
//                    {
//                        if (Equals(entityGet.Name, item.Name))
//                        {
//                            // DeleteEntity.
//                            //DataAccessUtils.DataAccess.TemplateResourcesCrud.DeleteEntity(entityGet);
//                        }
//                    }
//                }
//            );

//            TestsUtils.MethodComplete();
//        }
//    }
//}
